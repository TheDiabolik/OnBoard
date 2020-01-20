using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoard
{ 
        partial class SocketCommunication 
    {
            #region variables   
        private System.Threading.Timer STTimer;
        bool kontrol = false;
        //Server m_server;
        //Client m_client;

        #endregion

        #region constructor
       public SocketCommunication()
        {
           
        }

        

        #endregion

        #region singletonpattern
            private static SocketCommunication m_do;

            public static SocketCommunication Singleton()
            {
                if (m_do == null)
                    m_do = new SocketCommunication();

                return m_do;
            }
        #endregion


        public void Start(Enums.CommunicationType communicationType, string ipAddress, int port)
        {

            if (communicationType == Enums.CommunicationType.Server)
            {

                SocketCommunication.Server.Singleton().StartServer(ipAddress, port);
                //Server server = new Server();
                //server.StartServer(ipAddress, port);
            }

            else
            {
                // Client  client = new Client();

                //client.StartClient(ipAddress, port);  m_client = client;
                SocketCommunication.Client.Singleton().StartClient(ipAddress, port);
            }
        }

        public void Stop(Enums.CommunicationType communicationType)
        {

            if (communicationType == Enums.CommunicationType.Server)
            {
                //SocketCommunication.Server.Singleton().StopServer();

                //m_server.StopServer();
            }
            else
            {
              
                //m_client.StopClient(false);
                //SocketCommunication.Client.Singleton().StopClient(false);
            }
        } 
         
            #region methods  

          
            private static int StringToByteMsg(string str, byte[] buf)
            {
               
                int textLen;

                textLen = Encoding.UTF8.GetBytes(str, 0, str.Length, buf, 4);
                byte[] bufLen = BitConverter.GetBytes(textLen);
                Array.Copy(bufLen, buf, 4);

                return textLen + 4;
               
            }

        public void sda()
        {
            if (!kontrol)
            {
                kontrol = true;

                if (STTimer != null)
                    STTimer.Dispose();

                //this.STTimer = new System.Threading.Timer(DeleteInValidImages, null, 1000, System.Threading.Timeout.Infinite);
                this.STTimer = new System.Threading.Timer(DeleteInValidImages, null, 1000, 5000);
            }

        }
        public void DeleteInValidImages(object o)
        {
            try
            {

                SocketCommunication.Client.Singleton().SendTrainCreated();

            }
            catch (ThreadInterruptedException ex)
            {
                kontrol = false;
                sda();
                //Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "socket1");

            }
            catch (Exception ex)
            {
                kontrol = false;
                sda();
                //Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "socket2");
            }
        }


        #endregion

        #region classinfo
        class ClientInfo
            {
                private Socket m_sock;
                private byte[] m_bufRecv, m_bufSend;
                private int m_indexRecv, m_indexSend;
                private int m_leftRecv, m_leftSend;
                private bool m_lengthFlagRecv;


                public ClientInfo()
                {
                    m_bufRecv = new byte[1024];
                }

                public Socket Sock
                {
                    get { return m_sock; }
                    set { m_sock = value; }
                }

                public byte[] BufRecv
                {
                    get { return m_bufRecv; }
                    set { m_bufRecv = value; }
                }

                public byte[] BufSend
                {
                    get { return m_bufSend; }
                    set { m_bufSend = value; }
                }
                public int IndexRecv
                {
                    get { return m_indexRecv; }
                    set { m_indexRecv = value; }
                }
                public int LeftRecv
                {
                    get { return m_leftRecv; }
                    set { m_leftRecv = value; }
                }
                public int LeftSend
                {
                    get { return m_leftSend; }
                    set { m_leftSend = value; }
                }

                public bool LengthFlagRecv
                {
                    get { return m_lengthFlagRecv; }
                    set { m_lengthFlagRecv = value; }
                }

                public int IndexSend
                {
                    get { return m_indexSend; }
                    set { m_indexSend = value; }
                }
            }
            #endregion
        }
    }
 
