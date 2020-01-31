using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    partial class SocketCommunication
    {
        class ClientWSATC : Client, ITrainMovementCreatedSendMessageWatcher, ITrainCreatedWatcher
        {
            public ClientWSATC()
            {
                #region ayarları okuma
                m_settings = XMLSerialization.Singleton();
                m_settings = m_settings.DeSerialize(m_settings);
                #endregion


                //create
                MainForm.m_trainObserver.AddTrainCreatedWatcher(this);
                //movement
                MainForm.m_trainObserver.AddTrainMovementCreatedSendMessageWatcher(this);
            }



            #region TrainCreated
            public void TrainCreated(OBATP OBATP)
            {
                //lock (OBATP)
                //{ 
                //    using (MessageCreator messageCreator = new MessageCreator())
                //    {
                //        OBATP_TO_WSATP_MessageBuilder OBATP_TO_WSATP_Message = new OBATP_TO_WSATP_MessageBuilder();
                //        messageCreator.SetMessageBuilder(OBATP_TO_WSATP_Message);

                //        using (OBATP_TO_WSATPAdapter adappppppppp = new OBATP_TO_WSATPAdapter(OBATP))
                //        {
                //            byte[] OBATP_TO_WSATP_ByteArray = adappppppppp.ToByte();

                //            int OBATP_ID = 25000 + Convert.ToInt32(OBATP.Vehicle.TrainID);
                //            messageCreator.CreateMessage(61001, (UInt32)OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATP_TO_WSATP_ByteArray, 47851476196393100);

                //            Message message = messageCreator.GetMessage();

                //            SendMsgToServer(message.ToByte(), Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adappppppppp.ToString()));
                //        }
                //    }    




                    //using (OBATP_TO_WSATPAdapter adappppppppp = new OBATP_TO_WSATPAdapter(OBATP))
                    //{
                    //    byte[] OBATP_TO_WSATP_ByteArray = adappppppppp.ToByte();



                    //    MessageCreator messageCreator = new MessageCreator();
                    //    OBATP_TO_WSATP_MessageBuilder OBATP_TO_WSATP_Message = new OBATP_TO_WSATP_MessageBuilder();


                    //    messageCreator.SetMessageBuilder(OBATP_TO_WSATP_Message);

                    //    int OBATP_ID = 25000 + Convert.ToInt32(OBATP.Vehicle.TrainID);

                    //    messageCreator.CreateMessage(61001, (UInt32)OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATP_TO_WSATP_ByteArray, 47851476196393100);


                    //    Message message = messageCreator.GetMessage();

                    //    SendMsgToServer(message.ToByte(), Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adappppppppp.ToString()));
                    //}
                //}
            }
            #endregion

            #region TrainMovementCreated
            public void TrainMovementCreatedSendMessage(OBATP OBATP)
            { 
                lock (OBATP)
                {

                    using (MessageCreator messageCreator = new MessageCreator())
                    {
                        OBATP_TO_WSATP_MessageBuilder OBATP_TO_WSATP_Message = new OBATP_TO_WSATP_MessageBuilder();
                        messageCreator.SetMessageBuilder(OBATP_TO_WSATP_Message);

                        using (OBATP_TO_WSATPAdapter adappppppppp = new OBATP_TO_WSATPAdapter(OBATP))
                        {
                            byte[] OBATP_TO_WSATP_ByteArray = adappppppppp.ToByte(); 


                            int OBATP_ID = 25000 + Convert.ToInt32(OBATP.Vehicle.TrainID); 
                            messageCreator.CreateMessage(61001, (UInt32)OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATP_TO_WSATP_ByteArray, 47851476196393100); 

                            Message message = messageCreator.GetMessage();

                            SendMsgToServer(message.ToByte(), Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adappppppppp.ToString()));
                        }
                    }



                    //using (OBATP_TO_WSATPAdapter adappppppppp = new OBATP_TO_WSATPAdapter(OBATP))
                    //{
                    //    byte[] OBATP_TO_WSATP_ByteArray = adappppppppp.ToByte();



                    //    MessageCreator messageCreator = new MessageCreator();
                    //    OBATP_TO_WSATP_MessageBuilder OBATP_TO_WSATP_Message = new OBATP_TO_WSATP_MessageBuilder();


                    //    messageCreator.SetMessageBuilder(OBATP_TO_WSATP_Message);

                    //    int OBATP_ID = 25000 + Convert.ToInt32(OBATP.Vehicle.TrainID);

                    //    messageCreator.CreateMessage(61001, (UInt32)OBATP_ID, DateTimeExtensions.GetAllMiliSeconds(), 1, OBATP_TO_WSATP_ByteArray, 47851476196393100);


                    //    Message message = messageCreator.GetMessage();

                    //    SendMsgToServer(message.ToByte(), Tuple.Create<DateTime, string, string>(DateTime.Now, message.ToString(), adappppppppp.ToString()));
                    //} 
                }
            }

            #endregion
        }
    }
}
