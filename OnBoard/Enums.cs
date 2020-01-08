using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public class Enums
    {
        public enum TrainDirection { ToYenikapı , FromYenikapı };
        public enum Direction { Left = 1, Right = 2 };
        public enum DoorStatus { Open, Close };

        public enum Route { In, Out };
        public enum TrackInput { Manuel, FromFile };

        public enum CommunicationType { Server, Client };

        public enum TrainCoupled { NotCoupled, MD1Coupled, MD2Coupled, MD1AndMD2Coupled };

        public enum ActiveATP { First, Second };

        public enum ActiveCab { MD1Active, MD2Active, NotActive };


        public enum TrainCBTCMode { EM=10, RM1 = 20, RM2 = 30, MCS = 40, ATO = 50, UTO = 60, SB = 61, JOG = 62 };
        public enum TrainSetCarNumber { Four , Eight }

        public enum WakeUpTrain { WithDepartureTest, WithOutDepartureTest, False }


 


        public enum Train_ID : UInt32
        {
            Train1 = 1,
            Train2= 2,
            Train3= 3,
            Train4= 4,
            Train5= 5,
            Train6= 6,
            Train7= 7,
            Train8= 8,
            Train9 = 9,
            Train10= 10,
            Train11= 11,
            Train12= 12,
            Train13= 13,
            Train14= 14,
            Train15= 15,
            Train16= 16,
            Train17= 17,
            Train18= 18,
            Train19= 19,
            Train20 = 20,
        };




        public enum OBATP_ID : UInt32
        {
            Train1 = 25001,
            Train2 = 25002,
            Train3 = 25003,
            Train4 = 25004,
            Train5 = 25005,
            Train6 = 25006,
            Train7 = 25007,
            Train8 = 25008,
            Train9 = 25009,
            Train10 = 25010,
            Train11 = 25011,
            Train12 = 25012,
            Train13 = 25013,
            Train14 = 25014,
            Train15 = 25015,
            Train16 = 25016,
            Train17 = 25017,
            Train18 = 25018,
            Train19 = 25019,
            Train20 = 25020,
        };
        public enum OBATO_ID : UInt32
        {
            Train1 = 20001,
            Train2 = 20002,
            Train3 = 20003,
            Train4 = 20004,
            Train5 = 20005,
            Train6 = 20006,
            Train7 = 20007,
            Train8 = 20008,
            Train9 = 20009,
            Train10 = 20010,
            Train11 = 20011,
            Train12 = 20012,
            Train13 = 20013,
            Train14 = 20014,
            Train15 = 20015,
            Train16 = 20016,
            Train17 = 20017,
            Train18 = 20018,
            Train19 = 20019,
            Train20 = 20020,
        };


        public class Message
        {
            const UInt32 MSG_ID_BASE = 2857740885;
            //UInt64 msdf = DateTimeExtensions.GetAllMiliSeconds();

            public enum DS : UInt32
            {
                //DS = 2//2710927548
                OBATO_TO_WSATO = 2,
                OBATP_TO_WSATP = 2,
                WSATO_TO_OBATO = 2,
                WSATP_TO_OBATP = 2,
                ATS_SERVER_TO_OBATO = 2,
                ATS_SERVER_TO_OBATO_Init = 2,
                OBATO_TO_ATS_SERVER = 2,

            }

            public enum Size : UInt32
            {
                Size = 126 ,//512,
                OBATO_TO_WSATO = 52, 
                OBATP_TO_WSATP = 126,  
                WSATO_TO_OBATO = 50,  
                WSATP_TO_OBATP = 105,  
                ATS_SERVER_TO_OBATO = 80,  
                ATS_SERVER_TO_OBATO_Init = 165, 
                OBATO_TO_ATS_SERVER = 228,
            }
            public enum ID : UInt32
            {
                OBATO_TO_WSATO = 128,// MSG_ID_BASE + 100,
                OBATP_TO_WSATP = 144,//MSG_ID_BASE + 90, // MSG_ID_BASE + 1000,
                WSATO_TO_OBATO = 528, //MSG_ID_BASE + 200,
                WSATP_TO_OBATP = 544, //MSG_ID_BASE + 1100,
                ATS_SERVER_TO_OBATO = 16, //MSG_ID_BASE + 7000,
                ATS_SERVER_TO_OBATO_Init = 832, //MSG_ID_BASE + 7000,
                OBATO_TO_ATS_SERVER = 112//MSG_ID_BASE + 7100,


                //ATS_WORKSTATION_TO_ATS_SERVER = MSG_ID_BASE + 2000,
                //ATS_SERVER_TO_ATS_WORKSTATION = MSG_ID_BASE + 2100,
                //ATS_SERVER_TO_DATABASE = MSG_ID_BASE + 3000,
                //DATABASE_TO_ATS_SERVER = MSG_ID_BASE + 3100,
                //WSATO_TO_WSATP = MSG_ID_BASE + 4000,
                //WSATP_TO_WSATO = MSG_ID_BASE + 4100,
                //OBATP_TO_OBATO = MSG_ID_BASE + 5000,
                //OBATO_TO_OBATP = MSG_ID_BASE + 5100,
                //ATS_SERVER_TO_WSATO = MSG_ID_BASE + 6000,
                //WSATO_TO_ATS_SERVER = MSG_ID_BASE + 6100,
                //ATS_SERVER_TO_WSATP = MSG_ID_BASE + 8000,
                //WSATP_TO_ATS_SERVER = MSG_ID_BASE + 8100,
            }





            //public enum RTC : UInt64
            //{
            //    RTC = msdf
            //}

            public enum CRC : UInt64
            {
                CRC = 47851476196393100
            }

        }
    }
}
