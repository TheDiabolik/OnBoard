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
        public class ClientAAA :  ITrainCreatedWatcher, ITrainMovementCreatedSendMessageWatcher   
        {
            #region variables
            private static ClientAAA m_do = null;
            internal Socket m_clientSock;
            string m_ipAddress;
            int m_port;

            internal ConcurrentBag<OBATP> m_createdTrainsMessage; 

            private byte[] m_clientBufRecv, m_clientBufSend;
            private int m_clientIndexRecv, m_clientLeftRecv, m_clientIndexSend, m_clientLeftSend; 

           
            //System.Threading.Timer m_threadTimer;
          
            Stopwatch m_stopWatch;
            System.Timers.Timer m_timer;
            #endregion

            #region constructor
            public ClientAAA()
            {
               
                m_clientBufRecv = new byte[1024];
                m_clientBufSend = new byte[1024];

                m_stopWatch = new Stopwatch();
                m_timer = new System.Timers.Timer(1000);
                m_timer.Elapsed += m_stopWatch_Elapsed;

                m_createdTrainsMessage = new ConcurrentBag<OBATP>(); 


                //create
                MainForm.m_trainObserver.AddTrainCreatedWatcher(this);
                //movement
                MainForm.m_trainObserver.AddTrainMovementCreatedSendMessageWatcher(this);
                
 


            }

            void m_stopWatch_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            { 
                //if (m_clientSock != null)
                //{
                //    if (m_stopWatch.Elapsed.TotalSeconds > 3)
                //    {
                //        SendMsgToServer("AREYOUALIVE$");
                //        DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Connect Check to WaySide...", Color.DarkRed); 
                        
                //    }
                //}
                //else
                //{
                //    //m_clientSock.Dispose(); 
                //    //StartClient( "127.0.0.1", 5050);
                //    StartClient(m_ipAddress, m_port);
                //    DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Try to ReConnect to WaySide...", Color.DarkRed);
                //}


                //m_stopWatch.Restart();
            }

            #endregion

            #region singleton
            public static ClientAAA Singleton()
            {
                if (m_do == null)
                    m_do = new ClientAAA();

                return m_do;
            }
            #endregion

 
            public void StartClient(string ipAddress, int port)
            {
                try
                {
                    m_ipAddress = ipAddress;
                    m_port = port;
                    DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBoxCommunicationLogs, "Try to Connect WaySide...", Color.DarkRed);


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
                catch(Exception ex)
                {
                    Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "StartClient");

                }
              

            }

            public void StopClient(bool keepAlive)
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

            private void ClientConnectProc(IAsyncResult iar)
            {
                try
                {
                    m_clientSock.EndConnect(iar);


                    //alma
                    m_clientIndexRecv = 0;
                    m_clientLeftRecv = m_clientBufRecv.Length; 

                    DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBoxCommunicationLogs, "Connect to WaySide...", Color.DarkRed);

                    m_clientSock.BeginReceive(m_clientBufRecv, m_clientIndexRecv, m_clientLeftRecv,  SocketFlags.None, new AsyncCallback(ClientReceiveProc), null);
                }
                catch (Exception e)
                {
                    DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBoxCommunicationLogs, "Connection Failure to WaySide...", Color.DarkRed);

                    if (m_clientSock != null)
                    {
                        m_clientSock.Close();
                        m_clientSock = null;


                    }
                    return;

                }
            }


            public Enums.Train_ID FindTrainID(uint msgDST)
            { 
               
                uint lastdigit = (msgDST % 100);
                Enums.Train_ID train_ID = (Enums.Train_ID)lastdigit;

                return train_ID;

            }

            private void ClientReceiveProc(IAsyncResult iar)
            {
                try
                {
                    int receivedBytes = 0;

                    receivedBytes = m_clientSock.EndReceive(iar);


                    uint messageBase = BitConverter.ToUInt32(m_clientBufRecv, 0);


                    if(messageBase == 5)
                    {
                        uint messageLength = BitConverter.ToUInt32(m_clientBufRecv, 4);
                        byte[] incomingBytes = new byte[messageLength];
                        Array.Copy(m_clientBufRecv, incomingBytes, messageLength);
                         

                        uint messageID = BitConverter.ToUInt32(incomingBytes, 8); 

                        if(messageID == (UInt32) Enums.Message.ID.WSATP_TO_OBATP)
                        {
                            Message message = new Message(incomingBytes);

                            MessageSelector messageSelector = new MessageSelector();
                            IMessageType messageType = messageSelector.GetMessageType((Enums.Message.ID)messageID);
                            IMessageType dataorj = messageType.CreateMessage(message.DATA);

                       

                            WSATP_TO_OBATPAdapter adap = new WSATP_TO_OBATPAdapter(dataorj);

                            //obatpye wsatp gelen wsatp mesajını gönderiyor
                            //Enums.OBATP_ID OBATPID = (Enums.OBATP_ID)message.DST;
                            Enums.Train_ID train_ID = FindTrainID(message.DST);

                            MainForm.m_WSATP_TO_OBATPMessageInComing.WSATP_TO_OBATPNewMessageInComing(train_ID, adap); 
                            
                            MainForm.m_mf.m_communicationLogs.Add(Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adap.ToString()));

                        }
                        else if (messageID == (UInt32)Enums.Message.ID.ATS_SERVER_TO_OBATO_Init)
                        {
                            Message message = new Message(incomingBytes);

                            MessageSelector messageSelector = new MessageSelector();
                            IMessageType messageType = messageSelector.GetMessageType((Enums.Message.ID)messageID);
                            IMessageType dataorj = messageType.CreateMessage(message.DATA); 


                            ATS_TO_OBATO_InitAdapter adap = new ATS_TO_OBATO_InitAdapter(dataorj);


                   

                            //atsden gelen init mesajı
                            //Enums.OBATP_ID OBATPID = (Enums.OBATP_ID)message.DST;
                            Enums.Train_ID train_ID = FindTrainID(message.DST);


                            //Debug.WriteLine(train_ID.ToString());
                            //Debug.WriteLine(message.DST.ToString());

                            MainForm.m_ATS_TO_OBATO_InitMessageInComing.ATS_TO_OBATO_InitNewMessageInComing(train_ID, adap);

                         
                            MainForm.m_mf.m_communicationLogs.Add(Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adap.ToString()));

                        }
                        else if (messageID == (UInt32)Enums.Message.ID.ATS_SERVER_TO_OBATO)
                        {
                            Message message = new Message(incomingBytes);

                            MessageSelector messageSelector = new MessageSelector();
                            IMessageType messageType = messageSelector.GetMessageType((Enums.Message.ID)messageID);
                            IMessageType dataorj = messageType.CreateMessage(message.DATA);



                            ATS_TO_OBATOAdapter adap = new ATS_TO_OBATOAdapter(dataorj); 

                            //atsden gelen init mesajı
                            //Enums.OBATP_ID OBATPID = (Enums.OBATP_ID)message.DST;
                            Enums.Train_ID train_ID = FindTrainID(message.DST);
                            MainForm.m_ATS_TO_OBATO_MessageInComing.ATS_TO_OBATO_NewMessageInComing(train_ID, adap);

                            
                            MainForm.m_mf.m_communicationLogs.Add(Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adap.ToString()));

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

            private void ClientSendProc(IAsyncResult iar)
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
            public void SendMsgToServer(byte[] buf)
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
            }


            #endregion


            #region TrainCreated
            public void TrainCreated(OBATP OBATP)
            {

                //m_createdTrains.Add(OBATP);

                m_createdTrainsMessage.Add(OBATP);

                //lock (OBATP)
                //{
                //    using (OBATO_TO_ATSAdapter adappppppppp = new OBATO_TO_ATSAdapter(OBATP))
                //    {
                //        byte[] OBATO_TO_ATS_ByteArray = adappppppppp.ToByte();

                //        MessageCreator messageCreator = new MessageCreator();
                //        OBATO_TO_ATS_MessageBuilder OBATO_TO_ATS_Message = new OBATO_TO_ATS_MessageBuilder();

                //        messageCreator.SetMessageBuilder(OBATO_TO_ATS_Message);


                //        int OBATP_ID = 20000 + Convert.ToInt32(OBATP.Vehicle.TrainID);

                //        messageCreator.CreateMessage(1, (UInt32)OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATO_TO_ATS_ByteArray, 47851476196393100);

                //        Message message = messageCreator.GetMessage();

                //        var dsfsd = message.ToByte();

                //        m_createdTrainsMessage.Add(message.ToByte());



                //        //SendMsgToServer(message.ToByte());
                //    }
                //}
            }
            #endregion


            #region TrainMovementCreated
            public void TrainMovementCreatedSendMessage(OBATP OBATP)
            {

                lock (OBATP)
                {
                    #region OBATP_TO_WSATPAdapter

                    //using (OBATP_TO_WSATPAdapter adappppppppp = new OBATP_TO_WSATPAdapter(OBATP))
                    //{
                    //    byte[] OBATP_TO_WSATP_ByteArray = adappppppppp.ToByte();



                    //    MessageCreator messageCreator = new MessageCreator();
                    //    OBATP_TO_WSATP_MessageBuilder OBATP_TO_WSATP_Message = new OBATP_TO_WSATP_MessageBuilder();


                    //    messageCreator.SetMessageBuilder(OBATP_TO_WSATP_Message);

                    //    int OBATP_ID = 25000 + Convert.ToInt32(OBATP.Vehicle.TrainID);

                    //    messageCreator.CreateMessage(61001, (UInt32)OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATP_TO_WSATP_ByteArray, 47851476196393100);


                    //    Message message = messageCreator.GetMessage();

                    //    SendMsgToServer(message.ToByte());
                    //}
                    #endregion


                    #region OBATP_TO_WSATPAdapter

                    using (OBATO_TO_ATSAdapter adappppppppp = new OBATO_TO_ATSAdapter(OBATP))
                    {

                        //if(adappppppppp.VirtualOccupancyTrackSectionID.Length > 1)
                        //{

                        //}


                        byte[] OBATO_TO_ATS_ByteArray = adappppppppp.ToByte();  

                        MessageCreator messageCreator = new MessageCreator();
                        OBATO_TO_ATS_MessageBuilder OBATO_TO_ATS_Message = new OBATO_TO_ATS_MessageBuilder();


                        messageCreator.SetMessageBuilder(OBATO_TO_ATS_Message);

                        int OBATP_ID = 20000 + Convert.ToInt32(OBATP.Vehicle.TrainID);

                        //Debug.WriteLine(OBATP.Vehicle.TrainID.ToString() + " movement");
                        //Debug.WriteLine(OBATP_ID.ToString() + " movement");
                       

                        messageCreator.CreateMessage(1, (UInt32)OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATO_TO_ATS_ByteArray, 47851476196393100);


                        Message message = messageCreator.GetMessage();


                        MainForm.m_mf.m_communicationLogs.Add(Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adappppppppp.ToString()));

                        //Debug.WriteLine(adappppppppp);

                        //Debug.WriteLine(message);


                       

                      

                        SendMsgToServer(message.ToByte());
                    }
                    #endregion


                }
            }


            #region SendCreatedTrain
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




            //    //if (m_clientSock.Connected)
            //    //{
            //    //    foreach (OBATP OBATP in m_createdTrains.GetConsumingEnumerable())
            //    //    {
            //    //        using (OBATO_TO_ATSAdapter adappppppppp = new OBATO_TO_ATSAdapter(OBATP))
            //    //        {
            //    //            byte[] OBATO_TO_ATS_ByteArray = adappppppppp.ToByte();

            //    //            MessageCreator messageCreator = new MessageCreator();
            //    //            OBATO_TO_ATS_MessageBuilder OBATO_TO_ATS_Message = new OBATO_TO_ATS_MessageBuilder();

            //    //            messageCreator.SetMessageBuilder(OBATO_TO_ATS_Message);


            //    //            int OBATP_ID = 20000 + Convert.ToInt32(OBATP.Vehicle.TrainID);

            //    //            messageCreator.CreateMessage(1, (UInt32)OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATO_TO_ATS_ByteArray, 47851476196393100);

            //    //            Message message = messageCreator.GetMessage();

            //    //            var dsfsd = message.ToByte();

            //    //            //m_createdTrainsMessage.Add(message.ToByte()); 

            //    //            SendMsgToServer(message.ToByte());


            //    //        }
            //    //    }
            //    //}


            //}
            #endregion

            #endregion

        }
    }
}
