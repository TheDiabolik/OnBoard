using MetroTcpLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace OnBoard
{
    public class TcpClient
    {
        Client objectClient = new Client();
        private Thread TcpClientThread = null;
        public static bool ConnectionStatus = false;

        public TcpClient(int port,string DstIp,int srcPort)
        {
            objectClient.onDataReceived += ObjectClient_onDataReceived1;
            objectClient.onByteDataReceived += ObjectClient_onByteDataReceived;
            objectClient.onAutoByteDataSend += ObjectClient_onAutoByteDataSend;
            objectClient.onDisconnected += ObjectClient_onDisconnected;
            objectClient.onErrorMessage += ObjectClient_onErrorMessage;
            objectClient.onConnected += ObjectClient_onConnected;

            
            if (TcpClientThread == null || !TcpClientThread.IsAlive)
            {
              
                    System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(DstIp);

                    TcpClientThread = new Thread(() => objectClient.Connect(ipaddress, port,srcPort))
                    {
                        IsBackground = true

                    };

                    TcpClientThread.Start();



            }
            else if (TcpClientThread.IsAlive && ConnectionStatus)
            {

                objectClient.Disconnect();
            }

        }

        public void Send(byte[] data)
        {

            objectClient.Send(data);


        }


        private void ObjectClient_onDataReceived1(string message)
        {
            
        }

        private void ObjectClient_onAutoByteDataSend(byte[] message)
        {
        }

        private void ObjectClient_onByteDataReceived(byte[] message)
        {

            //KeepLog.log.Info(System.Text.ASCIIEncoding.GetEncoding("iso-8859-9").GetString(message));
        }

        private void ObjectClient_onConnected(System.Net.Sockets.TcpClient client)
        {
            //KeepLog.log.Info(client.Client.LocalEndPoint + " adresli WSATC Client ATS Server'a bağlandı");
        }

        private void ObjectClient_onErrorMessage(string message)
        {
            //KeepLog.log.Error(message);     
        }

        private void ObjectClient_onDisconnected(System.Net.Sockets.TcpClient client)
        {
        }

        private void ObjectClient_onAutoDataSend(string message)
        {
          
        }

        private void ObjectClient_onDataReceived(string message)
        {
          
        }

        public void Close()
        {
            objectClient.Disconnect();


        }
    }
}
