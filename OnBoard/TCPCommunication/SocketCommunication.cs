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
        internal enum CommunicationType { Server, Client}
        #endregion

        #region constructor
        SocketCommunication()
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


            public void Start(CommunicationType communicationType, string ipAddress, int port)
            { 

                if (communicationType == CommunicationType.Server)
                {
                    
                    SocketCommunication.Server.Singleton().StartServer(ipAddress, port);
                }

                else
                {
                   
                    SocketCommunication.Client.Singleton().StartClient(ipAddress, port);
                }
            }

        public void Stop(CommunicationType communicationType)
        {

            if (communicationType == CommunicationType.Server)
            {
                SocketCommunication.Server.Singleton().StopServer();
            }
            else
            {
                SocketCommunication.Client.Singleton().StopClient(false);
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
 
