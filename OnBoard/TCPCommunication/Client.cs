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
using System.Threading.Tasks;

namespace OnBoard
{
    partial class SocketCommunication
    {
        public class Client : ITrainMovementWatcher
        {
            #region variables
            private static Client m_do = null;
            internal Socket m_clientSock; 
           
           
           
            private byte[] m_clientBufRecv, m_clientBufSend;
            private int m_clientIndexRecv, m_clientLeftRecv, m_clientIndexSend, m_clientLeftSend; 
           
            //System.Threading.Timer m_threadTimer;
          
            Stopwatch m_stopWatch;
            System.Timers.Timer m_timer;
            #endregion

            #region constructor
            Client()
            {
               
                m_clientBufRecv = new byte[1024];
                m_clientBufSend = new byte[1024];

                m_stopWatch = new Stopwatch();
                m_timer = new System.Timers.Timer(1000);
                m_timer.Elapsed += m_stopWatch_Elapsed;

                MainForm.m_trainMovement.AddWatcher(this);



                //MainForm.m_WSATP_TO_OBATPMessageInComing.AddWatcher((MainForm.m_mf);

              
            }

            void m_stopWatch_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
               

                if (m_clientSock != null)
                {
                    if (m_stopWatch.Elapsed.TotalSeconds > 3)
                    {
                        SendMsgToServer("AREYOUALIVE$");
                        DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Connect Check to WaySide...", Color.DarkRed);

                        //DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Bağlantı Kontrol Ediliyor...!", Color.DarkRed);
                    }
                }
                else
                {
                    //m_clientSock.Dispose(); 
                    StartClient( "127.0.0.1", 5050);
                    DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Try to ReConnect to WaySide...", Color.DarkRed);
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

 
            public void StartClient(string ipAddress, int port)
            {

                DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Try to Connect WaySide...", Color.DarkRed);


                if (m_timer.Enabled)
                    m_timer.Stop();


                m_timer.Start();
                m_stopWatch.Start();
                 

                m_clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_clientSock.NoDelay = true;
                m_clientSock.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

                m_clientSock.BeginConnect(new IPEndPoint(IPAddress.Parse(ipAddress), port), new AsyncCallback(ClientConnectProc), null);

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


                    //SendMsgToServer("CONNECT$" );

                    DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Connect to WaySide...", Color.DarkRed);

                    m_clientSock.BeginReceive(m_clientBufRecv, m_clientIndexRecv, m_clientLeftRecv,  SocketFlags.None, new AsyncCallback(ClientReceiveProc), null);
                }
                catch (Exception e)
                {
                    DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Connection Failure to WaySide...", Color.DarkRed);
                }
            }


            private void ClientReceiveProc(IAsyncResult iar)
            {
                try
                {
                    int receivedBytes = 0;

                    receivedBytes = m_clientSock.EndReceive(iar);


                    uint messageBase = BitConverter.ToUInt32(m_clientBufRecv, 0);


                    if(messageBase == 2)
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
                            Enums.OBATP_ID OBATPID = (Enums.OBATP_ID)message.DST;
                            MainForm.m_WSATP_TO_OBATPMessageInComing.WSATP_TO_OBATPNewMessageInComing(OBATPID, adap);

                        }
                        else if (messageID == (UInt32)Enums.Message.ID.ATS_SERVER_TO_OBATO_Init)
                        {
                            Message message = new Message(incomingBytes);

                            MessageSelector messageSelector = new MessageSelector();
                            IMessageType messageType = messageSelector.GetMessageType((Enums.Message.ID)messageID);
                            IMessageType dataorj = messageType.CreateMessage(message.DATA);



                            ATS_TO_OBATO_InitAdapter adap = new ATS_TO_OBATO_InitAdapter(dataorj);


                            string asdasdasdasd = adap.ATStoOBATO_TrainNumber.ToString();

                            //atsden gelen init mesajı
                            Enums.OBATP_ID OBATPID = (Enums.OBATP_ID)message.DST;
                            MainForm.m_ATS_TO_OBATO_InitMessageInComing.ATS_TO_OBATO_InitNewMessageInComing(OBATPID, adap);

                        }
                        else if (messageID == (UInt32)Enums.Message.ID.ATS_SERVER_TO_OBATO)
                        {
                            Message message = new Message(incomingBytes);

                            MessageSelector messageSelector = new MessageSelector();
                            IMessageType messageType = messageSelector.GetMessageType((Enums.Message.ID)messageID);
                            IMessageType dataorj = messageType.CreateMessage(message.DATA);



                            ATS_TO_OBATOAdapter adap = new ATS_TO_OBATOAdapter(dataorj);


                        

                            //atsden gelen init mesajı
                            Enums.OBATP_ID OBATPID = (Enums.OBATP_ID)message.DST;
                            MainForm.m_ATS_TO_OBATO_MessageInComing.ATS_TO_OBATO_NewMessageInComing(OBATPID, adap);

                        }





                    }








                    //if (messageID == (uint)Enums.Message.ID.OBATP_TO_WSATP)
                    //{
                    //    Message message = new Message(incomingBytes);
                    //}
                    //else if(messageID == (uint)Enums.Message.ID.WSATP_TO_OBATP)
                    //{
                    //    Message message = new Message(incomingBytes);

                    //    MessageSelector messageSelector = new MessageSelector();
                    //    IMessageType messageType = messageSelector.GetMessageType((Enums.Message.ID)messageID);



                    //}





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
                    //int len;


                    //byte[] bufLen = BitConverter.GetBytes(buf.Length);
                    ////byte[] clientBufSend = new byte[buf.Length + bufLen.Length];

                    //m_clientBufSend = new byte[buf.Length + bufLen.Length];

                    //Array.Copy(bufLen, m_clientBufSend, 4); 
                    //Array.Copy(buf,0, m_clientBufSend, 4, buf.Length); 

                    //m_clientIndexSend = 0;
                    //m_clientLeftSend = m_clientBufSend.Length;

                    //m_clientSock.BeginSend(m_clientBufSend, m_clientIndexSend, m_clientLeftSend, SocketFlags.None, new AsyncCallback(ClientSendProc), null);

                    int len;


                    //byte[] bufLen = BitConverter.GetBytes(buf.Length);
                    ////byte[] clientBufSend = new byte[buf.Length + bufLen.Length];

                    //m_clientBufSend = new byte[buf.Length + bufLen.Length];

                    //Array.Copy(bufLen, m_clientBufSend, 4);
                    //Array.Copy(buf, 0, m_clientBufSend, 4, buf.Length);
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




            #region createdevent
            public void TrainMovementCreated(OBATP OBATP)
            {

                lock (OBATP)
                {


                    using (OBATP_TO_WSATPAdapter adappppppppp = new OBATP_TO_WSATPAdapter(OBATP))
                    {
                        byte[] OBATP_TO_WSATP_ByteArray = adappppppppp.ToByte();



                        MessageCreator messageCreator = new MessageCreator();
                        OBATP_TO_WSATP_MessageBuilder OBATP_TO_WSATP_Message = new OBATP_TO_WSATP_MessageBuilder();


                        messageCreator.SetMessageBuilder(OBATP_TO_WSATP_Message);
                        messageCreator.CreateMessage(61001, (UInt32)OBATP.OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATP_TO_WSATP_ByteArray, 47851476196393100);


                        Message message = messageCreator.GetMessage();

                        SendMsgToServer(message.ToByte());
                    }




                    //using (OBATP_TO_WSATP OBATP_TO_WSATP = new OBATP_TO_WSATP()) // buradan çıkınca patlayabilir bakılacak
                    //{
                    //    OBATP_TO_WSATP.EmergencyBrakeApplied = Convert.ToByte(false);
                    //    OBATP_TO_WSATP.TrainAbsoluteZeroSpeed = Convert.ToByte(false);
                    //    OBATP_TO_WSATP.AllTrainDoorsClosedAndLocked = Convert.ToByte(false);
                    //    OBATP_TO_WSATP.EnablePSD1 = Convert.ToByte(255);
                    //    OBATP_TO_WSATP.EnablePSD2 = Convert.ToByte(255);
                    //    //OBATP_TO_WSATP.ActiveATP = Convert.ToByte("\x02".Substring(2), NumberStyles.HexNumber); // Encoding.ASCII.GetBytes("\x02");
                    //    OBATP_TO_WSATP.ActiveATP = Byte.Parse("0x02".Substring(2), NumberStyles.HexNumber); // Encoding.ASCII.GetBytes("\x02");
                    //    OBATP_TO_WSATP.ActiveCab = Byte.Parse("0x03".Substring(2), NumberStyles.HexNumber);//Convert.ToByte("\x03");
                    //    OBATP_TO_WSATP.TrainDirection = Convert.ToByte(OBATP.Direction);
                    //    OBATP_TO_WSATP.TrainCoupled = Byte.Parse("0x04".Substring(2), NumberStyles.HexNumber); //Convert.ToByte("\x04");
                    //    OBATP_TO_WSATP.TrainIntegrity = Convert.ToByte(false);
                    //    OBATP_TO_WSATP.TrainLocationDeterminationFault = Convert.ToByte(false);
                    //    OBATP_TO_WSATP.TrackDatabaseVersionMajor = Convert.ToByte(1);
                    //    OBATP_TO_WSATP.TrackDatabaseVersionMinor = Convert.ToByte(1);
                    //    OBATP_TO_WSATP.TrainDerailment = Convert.ToByte(false);

                    //    Array.Copy(OBATP.footPrintTracks, OBATP_TO_WSATP.FootPrintTrackSectionID, OBATP.footPrintTracks.Length);
                    //    Array.Copy(OBATP.virtualOccupationTracks, OBATP_TO_WSATP.VirtualOccupancyTrackSectionID, OBATP.virtualOccupationTracks.Length);

                    //    OBATP_TO_WSATP.BerthingOk = Convert.ToByte(false);
                    //    OBATP_TO_WSATP.TrainNumber = Convert.ToByte((UInt32)OBATP.Vehicle.TrainID);

                    //    byte[] OBATP_TO_WSATP_ByteArray = OBATP_TO_WSATP.ToByte();



                    //    MessageCreator messageCreator = new MessageCreator();
                    //    OBATP_TO_WSATP_MessageBuilder OBATP_TO_WSATP_Message = new OBATP_TO_WSATP_MessageBuilder();


                    //    messageCreator.SetMessageBuilder(OBATP_TO_WSATP_Message);
                    //    messageCreator.CreateMessage(61001, (UInt32)OBATP.OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATP_TO_WSATP_ByteArray, 47851476196393100);


                    //    Message message = messageCreator.GetMessage();


                    //    SendMsgToServer(message.ToByte());
                    //}


                   



                    //using (Message message = new Message())
                    //{
                    //    message.DS = (UInt32)Enums.Message.DS.OBATP_TO_WSATP;
                    //    message.Size = (UInt32)Enums.Message.Size.Size; //(40 + sourceMessageData.Length); //
                    //    message.ID = (UInt32)Enums.Message.ID.OBATP_TO_WSATP;
                    //    message.DST = 61001;
                    //    message.SRC = (UInt32)OBATP.OBATP_ID;
                    //    message.RTC = DateTimeExtensions.GetAllMiliSeconds();
                    //    message.NO = 1;

                    //    message.CRC = 47851476196393100;
                    //    ////OBATP_TO_WSATP içerik oluşturma
                    //    using (OBATP_TO_WSATP OBATP_TO_WSATP = new OBATP_TO_WSATP()) // buradan çıkınca patlayabilir bakılacak
                    //    {
                    //        OBATP_TO_WSATP.EmergencyBrakeApplied = Convert.ToByte(false);
                    //        OBATP_TO_WSATP.TrainAbsoluteZeroSpeed = Convert.ToByte(false);
                    //        OBATP_TO_WSATP.AllTrainDoorsClosedAndLocked = Convert.ToByte(false);
                    //        OBATP_TO_WSATP.EnablePSD1 = Convert.ToByte(255);
                    //        OBATP_TO_WSATP.EnablePSD2 = Convert.ToByte(255);
                    //        //OBATP_TO_WSATP.ActiveATP = Convert.ToByte("\x02".Substring(2), NumberStyles.HexNumber); // Encoding.ASCII.GetBytes("\x02");
                    //        OBATP_TO_WSATP.ActiveATP = Byte.Parse("0x02".Substring(2), NumberStyles.HexNumber); // Encoding.ASCII.GetBytes("\x02");
                    //        OBATP_TO_WSATP.ActiveCab = Byte.Parse("0x03".Substring(2), NumberStyles.HexNumber);//Convert.ToByte("\x03");
                    //        OBATP_TO_WSATP.TrainDirection = Convert.ToByte(OBATP.Direction);
                    //        OBATP_TO_WSATP.TrainCoupled = Byte.Parse("0x04".Substring(2), NumberStyles.HexNumber); //Convert.ToByte("\x04");
                    //        OBATP_TO_WSATP.TrainIntegrity = Convert.ToByte(false);
                    //        OBATP_TO_WSATP.TrainLocationDeterminationFault = Convert.ToByte(false);
                    //        OBATP_TO_WSATP.TrackDatabaseVersionMajor = Convert.ToByte(1);
                    //        OBATP_TO_WSATP.TrackDatabaseVersionMinor = Convert.ToByte(1);
                    //        OBATP_TO_WSATP.TrainDerailment = Convert.ToByte(false);

                    //        Array.Copy(OBATP.footPrintTracks, OBATP_TO_WSATP.FootPrintTrackSectionID, OBATP.footPrintTracks.Length);
                    //        Array.Copy(OBATP.virtualOccupationTracks, OBATP_TO_WSATP.VirtualOccupancyTrackSectionID, OBATP.virtualOccupationTracks.Length);

                    //        OBATP_TO_WSATP.BerthingOk = Convert.ToByte(false);
                    //        OBATP_TO_WSATP.TrainNumber = Convert.ToByte((UInt32)OBATP.Vehicle.TrainID);

                    //        byte[] OBATP_TO_WSATP_ByteArray = OBATP_TO_WSATP.ToByte();
                    //        message.DATA = OBATP_TO_WSATP_ByteArray; 

                    //    }

                    //    //send message to wsatp
                    //    SendMsgToServer(message.ToByte());
                    //}
                }
            } 



            public void TrainMovementRouteCreated(Route route)
            {
               
            }
            #endregion

        }
    }
}
