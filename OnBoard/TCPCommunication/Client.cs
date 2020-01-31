using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoard
{
    partial class SocketCommunication
    {
        public class Client
        {
            #region variables
            private static Client m_do = null;
            internal Socket m_clientSock;
            string m_ipAddress;
            int m_port;
            string m_name;

            internal XMLSerialization m_settings;

            //ConcurrentBag<OBATP> m_createdTrainsMessage;
            //BlockingCollection<OBATP> m_createdTrains;

            internal byte[] m_clientBufRecv, m_clientBufSend;
            internal int m_clientIndexRecv, m_clientLeftRecv, m_clientIndexSend, m_clientLeftSend;


            //System.Threading.Timer m_threadTimer;

            internal Stopwatch m_stopWatch;
            internal System.Timers.Timer m_timer;
            #endregion

            #region constructor
            public Client()
            {

                m_clientBufRecv = new byte[1024];
                m_clientBufSend = new byte[1024];

                m_stopWatch = new Stopwatch();
                m_timer = new System.Timers.Timer(1000);
                m_timer.Elapsed += m_stopWatch_Elapsed;

                //m_createdTrainsMessage = new ConcurrentBag<OBATP>();
                //m_createdTrains = new BlockingCollection<OBATP>(); 

                #region ayarları okuma
                m_settings = XMLSerialization.Singleton();
                m_settings = m_settings.DeSerialize(m_settings);
                #endregion


            }

            void m_stopWatch_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                if (m_clientSock != null)
                {
                    if (m_stopWatch.Elapsed.TotalSeconds > 3)
                    {
                        //SendMsgToServer("AREYOUALIVE$");
                        //DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Connect Check to WaySide...", Color.DarkRed);

                    }
                }
                else
                {
                    //m_clientSock.Dispose(); 
                    //StartClient( "127.0.0.1", 5050);
                    StartClient(m_ipAddress, m_port);

                    MainForm.m_mf.m_UILogs.Add(Tuple.Create<string, Color>("Try to ReConnect... - " + m_name, Color.DarkRed));
                    //DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Try to ReConnect...", Color.DarkRed);
                }


                m_stopWatch.Restart();
            }

            #endregion

            #region singleton
            public static Client Singleton()
            {
                if (m_do == null)
                    m_do = new Client();

                return m_do;
            }
            #endregion


            public virtual void StartClient(string ipAddress, int port)
            {
                m_ipAddress = ipAddress;
                m_port = port; 
              

                if (m_port == 11001)
                    m_name = "WSATC";
                else if (m_port == 12101)
                    m_name = "ATS"; 

                try
                { 


                    //DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Try to Connect...", Color.DarkRed);
                    MainForm.m_mf.m_UILogs.Add(Tuple.Create<string, Color>("Try to Connect... - " + m_name, Color.DarkRed));

                    if (m_timer.Enabled)
                        m_timer.Stop();


                    m_timer.Start();
                    m_stopWatch.Start();


                    //SocketCommunication.Singleton().sda();

                    m_clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    m_clientSock.NoDelay = true;
                    m_clientSock.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

                    m_clientSock.BeginConnect(new IPEndPoint(IPAddress.Parse(ipAddress), port), new AsyncCallback(ClientConnectProc), null);
                }
                catch (Exception ex)
                {
                    Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "StartClient");

                }


            }

            public virtual void StopClient(bool keepAlive)
            {
                if ((m_clientSock != null) || (m_clientSock != null && m_clientSock.Connected))
                {
                    if (!m_clientSock.Connected)
                    {
                        //DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Sorgulama Durduruluyor...", Color.DarkRed);
                        //stopAsking = true;
                    }

                    if (m_clientSock.Connected)
                    {
                        //SendMsgToServer("DISCONNECT$");
                    }

                }

                if (!keepAlive)
                    m_timer.Stop();
            }

            internal virtual void ClientConnectProc(IAsyncResult iar)
            {
               

                try
                {
                    m_clientSock.EndConnect(iar);


                    //alma
                    m_clientIndexRecv = 0;
                    m_clientLeftRecv = m_clientBufRecv.Length;



                 

               

                    //DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Connect to " + isim, Color.DarkRed);

                    MainForm.m_mf.m_UILogs.Add(Tuple.Create<string, Color>("Connect to " + m_name, Color.DarkRed));

                    //SendMsgToServer("");

                    m_clientSock.BeginReceive(m_clientBufRecv, m_clientIndexRecv, m_clientLeftRecv, SocketFlags.None, new AsyncCallback(ClientReceiveProc), null);
                }
                catch (Exception e)
                {
                    //DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Connection Failure to " + isim, Color.DarkRed);

                    MainForm.m_mf.m_UILogs.Add(Tuple.Create<string, Color>("Connection Failure to " + m_name, Color.DarkRed));


                    if (m_clientSock != null)
                    {
                        m_clientSock.Close();
                        m_clientSock = null;


                    }
                    return;

                }
            }


            readonly object m_msgDST = new object();

            public Enums.Train_ID FindTrainID(uint msgDST)
            {
                lock(m_msgDST)
                {
                    uint lastdigit = (msgDST % 100);
                    Enums.Train_ID train_ID = (Enums.Train_ID)lastdigit;

                    return train_ID;
                }

              

            }

            internal virtual void ClientReceiveProc(IAsyncResult iar)
            {
                try
                {
                    int receivedBytes = 0;

                    receivedBytes = m_clientSock.EndReceive(iar);


                    uint messageBase = BitConverter.ToUInt32(m_clientBufRecv, 0);


                    if (messageBase == 5)
                    {
                        uint messageLength = BitConverter.ToUInt32(m_clientBufRecv, 4);
                        byte[] incomingBytes = new byte[messageLength];
                        Array.Copy(m_clientBufRecv, incomingBytes, messageLength);


                        uint messageID = BitConverter.ToUInt32(incomingBytes, 8);

                        if (messageID == (UInt32)Enums.Message.ID.WSATP_TO_OBATP)
                        {
                            using (Message message = new Message(incomingBytes))
                            {
                                MessageSelector messageSelector = new MessageSelector();
                                IMessageType messageType = messageSelector.GetMessageType((Enums.Message.ID)messageID);
                                IMessageType dataorj = messageType.CreateMessage(message.DATA);

                                using (WSATP_TO_OBATPAdapter adap = new WSATP_TO_OBATPAdapter(dataorj))
                                {
                                    //obatpye wsatp gelen wsatp mesajını gönderiyor
                                    //Enums.OBATP_ID OBATPID = (Enums.OBATP_ID)message.DST;
                                    Enums.Train_ID train_ID = FindTrainID(message.DST);

                                    MainForm.m_WSATP_TO_OBATPMessageInComing.WSATP_TO_OBATPNewMessageInComing(train_ID, adap);

                                    CheckWriteLog(Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adap.ToString()));
                                }
                            }

                            //MainForm.m_mf.m_communicationLogs.Add(Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adap.ToString()));

                        }
                        else if (messageID == (UInt32)Enums.Message.ID.ATS_SERVER_TO_OBATO_Init)
                        {
                            using (Message message = new Message(incomingBytes))
                            { 
                                MessageSelector messageSelector = new MessageSelector();
                                IMessageType messageType = messageSelector.GetMessageType((Enums.Message.ID)messageID);
                                IMessageType dataorj = messageType.CreateMessage(message.DATA); 

                                using (ATS_TO_OBATO_InitAdapter adap = new ATS_TO_OBATO_InitAdapter(dataorj))
                                {  
                                    //atsden gelen init mesajı
                                    //Enums.OBATP_ID OBATPID = (Enums.OBATP_ID)message.DST;
                                    Enums.Train_ID train_ID = FindTrainID(message.DST); 

                                    MainForm.m_ATS_TO_OBATO_InitMessageInComing.ATS_TO_OBATO_InitNewMessageInComing(train_ID, adap);

                                    CheckWriteLog(Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adap.ToString()));
                                    //MainForm.m_mf.m_communicationLogs.Add(Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adap.ToString()));
                                }
                            }

                        }
                        else if (messageID == (UInt32)Enums.Message.ID.ATS_SERVER_TO_OBATO)
                        {
                            using (Message message = new Message(incomingBytes))
                            {
                                MessageSelector messageSelector = new MessageSelector();
                                IMessageType messageType = messageSelector.GetMessageType((Enums.Message.ID)messageID);
                                IMessageType dataorj = messageType.CreateMessage(message.DATA);

                                using (ATS_TO_OBATOAdapter adap = new ATS_TO_OBATOAdapter(dataorj))
                                {
                                    adap.DwellTime = 50;

                                    //atsden gelen init mesajı
                                    //Enums.OBATP_ID OBATPID = (Enums.OBATP_ID)message.DST;
                                    Enums.Train_ID train_ID = FindTrainID(message.DST);
                                    MainForm.m_ATS_TO_OBATO_MessageInComing.ATS_TO_OBATO_NewMessageInComing(train_ID, adap);

                                    CheckWriteLog(Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adap.ToString()));
                                    //MainForm.m_mf.m_communicationLogs.Add(Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adap.ToString()));
                                }
                            }
                        }
                    }






                    if (!m_stopWatch.IsRunning)
                    {
                        m_stopWatch.Start();
                    }

                    m_stopWatch.Restart();


                    m_clientSock.BeginReceive(m_clientBufRecv, m_clientIndexRecv, m_clientLeftRecv, SocketFlags.None, new AsyncCallback(ClientReceiveProc), null);
                }
                catch (Exception e)
                {

                    if (m_clientSock != null)
                    {
                        m_clientSock.Close();
                        m_clientSock = null;


                    }
                    return;
                }
            }

            internal virtual void ClientSendProc(IAsyncResult iar)
            {
                try
                {
                    int n = m_clientSock.EndSend(iar);

                    m_clientIndexSend += n;
                    m_clientLeftSend -= n;

                    //if (m_clientLeftSend != 0)
                    //    m_clientSock.BeginSend(m_clientBufSend, m_clientIndexSend, m_clientLeftSend, SocketFlags.None, new AsyncCallback(ClientSendProc), null);
                    //else


                }
                catch (Exception e)
                {


                }
            }

            #region methods
            public void SendMsgToServer(string msg)
            {
                try
                {
                    int len;

                    len = StringToByteMsg(msg, m_clientBufSend);

                    m_clientIndexSend = 0;
                    m_clientLeftSend = len;

                    m_clientSock.BeginSend(m_clientBufSend, m_clientIndexSend, m_clientLeftSend, SocketFlags.None, new AsyncCallback(ClientSendProc), null);
                }
                catch (Exception e)
                {
                    if (m_clientSock != null)
                    {
                        m_clientSock.Close();
                        m_clientSock = null;


                    }
                    return;
                }
            }
            public virtual void SendMsgToServer(byte[] buf, Tuple<DateTime, string, string> sendingMessageToLog)
            {
                try
                {
                    m_clientBufSend = buf;

                    m_clientIndexSend = 0;
                    m_clientLeftSend = buf.Length;

                    m_clientSock.BeginSend(m_clientBufSend, m_clientIndexSend, m_clientLeftSend, SocketFlags.None, new AsyncCallback(ClientSendProc), null);
                }
                catch (Exception e)
                {

                    if (m_clientSock != null)
                    {
                        m_clientSock.Close();
                        m_clientSock = null;


                    }
                    return;
                }
                finally
                {

                    if (m_clientSock != null && m_clientSock.Connected)
                    {
                        CheckWriteLog(sendingMessageToLog);
                    }

                }


            }


            #endregion



            public void CheckWriteLog(Tuple<DateTime, string, string> sendingMessageToLog)
            {
                lock(sendingMessageToLog)
                {
                    //string messageID = 
                    string[] logToWriteSplitArray = sendingMessageToLog.Item2.ToString().Split('\r');
                    string idName = logToWriteSplitArray[2];
                    string[] idNameSplitArray = idName.Split(' ');
                    string id = idNameSplitArray[2];

                    switch (id)
                    {
                        case "OBATO_TO_WSATO":
                            {
                                break;
                            }
                        case "OBATP_TO_WSATP":
                            {
                                if (m_settings.WriteLogOBATP_TO_WSATP)
                                    MainForm.m_mf.m_communicationLogs.Add(sendingMessageToLog);

                                break;
                            }
                        case "WSATO_TO_OBATO":
                            {
                                break;
                            }
                        case "WSATP_TO_OBATP":
                            {
                                if (m_settings.WriteLogWSATP_TO_OBATP)
                                    MainForm.m_mf.m_communicationLogs.Add(sendingMessageToLog);

                                break;
                            }
                        case "ATS_SERVER_TO_OBATO":
                            {
                                if (m_settings.WriteLogATS_TO_OBATO)
                                    MainForm.m_mf.m_communicationLogs.Add(sendingMessageToLog);
                                break;
                            }
                        case "ATS_SERVER_TO_OBATO_Init":
                            {
                                if (m_settings.WriteLogATS_TO_OBATO_Init)
                                    MainForm.m_mf.m_communicationLogs.Add(sendingMessageToLog);

                                break;
                            }
                        case "OBATO_TO_ATS_SERVER":
                            {
                                if (m_settings.WriteLogOBATO_TO_ATS)
                                    MainForm.m_mf.m_communicationLogs.Add(sendingMessageToLog);

                                break;
                            }
                    }

                }


            }



            #region SendCreatedTrain

            //public virtual void SendTrainCreated()
            //{

            //    while (!m_createdTrainsMessage.IsEmpty &&  m_clientSock.Connected)
            //    {
            //        if (m_createdTrainsMessage.TryTake(out OBATP OBATP))
            //        {
            //            if (OBATP.Status == Enums.Status.Create)
            //                m_createdTrainsMessage.Add(OBATP);

            //            using (OBATO_TO_ATSAdapter adappppppppp = new OBATO_TO_ATSAdapter(OBATP))
            //            {
            //                byte[] OBATO_TO_ATS_ByteArray = adappppppppp.ToByte();

            //                MessageCreator messageCreator = new MessageCreator();
            //                OBATO_TO_ATS_MessageBuilder OBATO_TO_ATS_Message = new OBATO_TO_ATS_MessageBuilder();

            //                messageCreator.SetMessageBuilder(OBATO_TO_ATS_Message);


            //                int OBATP_ID = 20000 + Convert.ToInt32(OBATP.Vehicle.TrainID);

            //                messageCreator.CreateMessage(1, (UInt32)OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATO_TO_ATS_ByteArray, 47851476196393100);

            //                Message message = messageCreator.GetMessage();

            //                var dsfsd = message.ToByte();


            //                //if (OBATP_ID == 20001)
            //                //    Debug.WriteLine(OBATP_ID + " movement");
            //                //else if (OBATP_ID == 20002)
            //                Debug.WriteLine(OBATP_ID + " create");
            //                //m_createdTrainsMessage.Add(message.ToByte());  


            //                SendMsgToServer(message.ToByte());

            //                MainForm.m_mf.m_communicationLogs.Add(Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adappppppppp.ToString()));


            //                //Logging.WriteCommunicationLog((Enums.Message.DS)message.DS, (Enums.Message.Size)message.Size, (Enums.Message.ID) message.ID, message.DST.ToString(), message.SRC.ToString(), message.RTC.ToString(), message.NO.ToString(), message.CRC.ToString());







            //            }

            //            Thread.Sleep(3000);
            //        }

            //    }

            //}

            //public void SendTrainCreated()
            //{

            //    while (!m_createdTrainsMessage.IsEmpty && m_clientSock.Connected)
            //    {
            //        if (m_createdTrainsMessage.TryTake(out OBATP OBATP))
            //        {
            //            if (OBATP.Status == Enums.Status.Create)
            //                m_createdTrainsMessage.Add(OBATP);

            //            using (OBATO_TO_ATSAdapter adappppppppp = new OBATO_TO_ATSAdapter(OBATP))
            //            {
            //                byte[] OBATO_TO_ATS_ByteArray = adappppppppp.ToByte();

            //                MessageCreator messageCreator = new MessageCreator();
            //                OBATO_TO_ATS_MessageBuilder OBATO_TO_ATS_Message = new OBATO_TO_ATS_MessageBuilder();

            //                messageCreator.SetMessageBuilder(OBATO_TO_ATS_Message);


            //                int OBATP_ID = 20000 + Convert.ToInt32(OBATP.Vehicle.TrainID);

            //                messageCreator.CreateMessage(1, (UInt32)OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATO_TO_ATS_ByteArray, 47851476196393100);

            //                Message message = messageCreator.GetMessage();

            //                var dsfsd = message.ToByte();


            //                //if (OBATP_ID == 20001)
            //                //    Debug.WriteLine(OBATP_ID + " movement");
            //                //else if (OBATP_ID == 20002)
            //                Debug.WriteLine(OBATP_ID + " create");
            //                //m_createdTrainsMessage.Add(message.ToByte());  


            //                SendMsgToServer(message.ToByte());

            //                MainForm.m_mf.m_communicationLogs.Add(Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adappppppppp.ToString()));


            //                //Logging.WriteCommunicationLog((Enums.Message.DS)message.DS, (Enums.Message.Size)message.Size, (Enums.Message.ID) message.ID, message.DST.ToString(), message.SRC.ToString(), message.RTC.ToString(), message.NO.ToString(), message.CRC.ToString());







            //            }

            //            Thread.Sleep(3000);
            //        }

            //    }




            //if (m_clientSock.Connected)
            //{
            //    foreach (OBATP OBATP in m_createdTrains.GetConsumingEnumerable())
            //    {
            //        using (OBATO_TO_ATSAdapter adappppppppp = new OBATO_TO_ATSAdapter(OBATP))
            //        {
            //            byte[] OBATO_TO_ATS_ByteArray = adappppppppp.ToByte();

            //            MessageCreator messageCreator = new MessageCreator();
            //            OBATO_TO_ATS_MessageBuilder OBATO_TO_ATS_Message = new OBATO_TO_ATS_MessageBuilder();

            //            messageCreator.SetMessageBuilder(OBATO_TO_ATS_Message);


            //            int OBATP_ID = 20000 + Convert.ToInt32(OBATP.Vehicle.TrainID);

            //            messageCreator.CreateMessage(1, (UInt32)OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATO_TO_ATS_ByteArray, 47851476196393100);

            //            Message message = messageCreator.GetMessage();

            //            var dsfsd = message.ToByte();

            //            //m_createdTrainsMessage.Add(message.ToByte()); 

            //            SendMsgToServer(message.ToByte());


            //        }
            //    }
            //}


            //}
            #endregion



        }
    }
}
