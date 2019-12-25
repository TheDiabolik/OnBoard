using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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
           
            string incomingMsg, imageName, exitImageName;
           
            private byte[] m_clientBufRecv, m_clientBufSend;
            private int m_clientIndexRecv, m_clientLeftRecv, m_clientIndexSend, m_clientLeftSend;
            private bool m_clientLengthFlagRecv;
           
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
                m_timer = new System.Timers.Timer(15000);
                m_timer.Elapsed += m_stopWatch_Elapsed;

                MainForm.m_trainMovement.AddWatcher(this);
            }

            void m_stopWatch_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                //if (m_clientSock != null)
                //{
                //    if (m_stopWatch.Elapsed.TotalSeconds > 5)
                //    {
                //        SendMsgToServer("AREYOUALIVE$");
                //        //DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Bağlantı Kontrol Ediliyor...!", Color.DarkRed);
                //    }
                //}
                //else
                //{
                //    //m_clientSock.Dispose(); 
                //    StartClient(SocketCommunication.Singleton().m_settings.m_entryTagIP, "stopwatch");
                //}


                //m_stopWatch.Restart();
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
                if (m_timer.Enabled)
                    m_timer.Stop();


                m_timer.Start();
                 

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
                    m_clientLeftRecv = 4;
                    m_clientLengthFlagRecv = true;

                    //SendMsgToServer("CONNECT$" );

                   

                    m_clientSock.BeginReceive(m_clientBufRecv, m_clientIndexRecv, m_clientLeftRecv,
                       SocketFlags.None, new AsyncCallback(ClientReceiveProc), null);
                }
                catch (Exception e)
                {
                    
                }
            }


            private void ClientReceiveProc(IAsyncResult iar)
            {
                try
                {
                    int n = 0;

                    n = m_clientSock.EndReceive(iar);

                    m_clientIndexRecv += n;
                    m_clientLeftRecv -= n;

                    if (m_clientLeftRecv == 0)
                    {
                        if (m_clientLengthFlagRecv)
                        {
                            m_clientLeftRecv = BitConverter.ToInt32(m_clientBufRecv, 0);
                            m_clientIndexRecv = 0;
                            m_clientLengthFlagRecv = false;
                        }
                        else
                        {
                            string msg = Encoding.UTF8.GetString(m_clientBufRecv, 0, m_clientIndexRecv);
                            incomingMsg = msg.Substring(0, msg.IndexOf("$"));
                            imageName = msg.Substring(msg.IndexOf("$") + 1);

                            if (!m_stopWatch.IsRunning)
                            {
                                m_stopWatch.Start();
                            }

                            m_stopWatch.Restart();
 
                            m_clientLengthFlagRecv = true;
                            m_clientIndexRecv = 0;
                            m_clientLeftRecv = 4;

                        }
                    }

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
                    using (Message message = new Message())
                    {
                        message.DS = (UInt32)Enums.Message.DS.DS;
                        message.Size = (UInt32)Enums.Message.Size.Size; //(40 + sourceMessageData.Length); //
                        message.ID = (UInt32)Enums.Message.ID.OBATP_TO_WSATP;
                        message.DST = 61001;
                        message.SRC = (UInt32)OBATP.OBATP_ID;
                        message.RTC = DateTimeExtensions.GetAllMiliSeconds();
                        message.NO = 1;

                        message.CRC = 47851476196393100;
                        ////OBATP_TO_WSATP içerik oluşturma
                        using (OBATP_TO_WSATP OBATP_TO_WSATP = new OBATP_TO_WSATP()) // buradan çıkınca patlayabilir bakılacak
                        {
                            OBATP_TO_WSATP.EmergencyBrakeApplied = Convert.ToByte(false);
                            OBATP_TO_WSATP.TrainAbsoluteZeroSpeed = Convert.ToByte(false);
                            OBATP_TO_WSATP.AllTrainDoorsClosedAndLocked = Convert.ToByte(false);
                            OBATP_TO_WSATP.EnablePSD1 = Convert.ToByte(255);
                            OBATP_TO_WSATP.EnablePSD2 = Convert.ToByte(255);
                            //OBATP_TO_WSATP.ActiveATP = Convert.ToByte("\x02".Substring(2), NumberStyles.HexNumber); // Encoding.ASCII.GetBytes("\x02");
                            OBATP_TO_WSATP.ActiveATP = Byte.Parse("0x02".Substring(2), NumberStyles.HexNumber); // Encoding.ASCII.GetBytes("\x02");
                            OBATP_TO_WSATP.ActiveCab = Byte.Parse("0x03".Substring(2), NumberStyles.HexNumber);//Convert.ToByte("\x03");
                            OBATP_TO_WSATP.TrainDirection = Convert.ToByte(OBATP.Direction);
                            OBATP_TO_WSATP.TrainCoupled = Byte.Parse("0x04".Substring(2), NumberStyles.HexNumber); //Convert.ToByte("\x04");
                            OBATP_TO_WSATP.TrainIntegrity = Convert.ToByte(false);
                            OBATP_TO_WSATP.TrainLocationDeterminationFault = Convert.ToByte(false);
                            OBATP_TO_WSATP.TrackDatabaseVersionMajor = Convert.ToByte(1);
                            OBATP_TO_WSATP.TrackDatabaseVersionMinor = Convert.ToByte(1);
                            OBATP_TO_WSATP.TrainDerailment = Convert.ToByte(false);

                            Array.Copy(OBATP.footPrintTracks, OBATP_TO_WSATP.FootPrintTrackSectionID, OBATP.footPrintTracks.Length);
                            Array.Copy(OBATP.virtualOccupationTracks, OBATP_TO_WSATP.VirtualOccupancyTrackSectionID, OBATP.virtualOccupationTracks.Length);

                            OBATP_TO_WSATP.BerthingOk = Convert.ToByte(false);
                            OBATP_TO_WSATP.TrainNumber = Convert.ToByte((UInt32)OBATP.Vehicle.TrainID);

                            byte[] OBATP_TO_WSATP_ByteArray = OBATP_TO_WSATP.ToByte();
                            message.DATA = OBATP_TO_WSATP_ByteArray; 

                        }

                        //send message to wsatp
                        SendMsgToServer(message.ToByte());
                    }
                }
            } 



            public void TrainMovementRouteCreated(Route route)
            {
               
            }
            #endregion

        }
    }
}
