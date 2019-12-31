using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public class Enums
    {
        public enum Direction { One, Second };
        public enum DoorStatus { Open, Close };

        public enum Route { In, Out };

        public class Message
        {
            const UInt32 MSG_ID_BASE = 2857740885;
            //UInt64 msdf = DateTimeExtensions.GetAllMiliSeconds();

            public enum DS : UInt32
            {
                DS = 2710927548
            }

            public enum Size : UInt32
            {
                Size = 126 //512
            }
            public enum ID : UInt32
            {
                OBATO_TO_WSATO = 144,// MSG_ID_BASE + 100,
                WSATO_TO_OBATO = MSG_ID_BASE + 200,
                OBATP_TO_WSATP = MSG_ID_BASE + 1000,
                WSATP_TO_OBATP = MSG_ID_BASE + 1100,
                ATS_WORKSTATION_TO_ATS_SERVER = MSG_ID_BASE + 2000,
                ATS_SERVER_TO_ATS_WORKSTATION = MSG_ID_BASE + 2100,
                ATS_SERVER_TO_DATABASE = MSG_ID_BASE + 3000,
                DATABASE_TO_ATS_SERVER = MSG_ID_BASE + 3100,
                WSATO_TO_WSATP = MSG_ID_BASE + 4000,
                WSATP_TO_WSATO = MSG_ID_BASE + 4100,
                OBATP_TO_OBATO = MSG_ID_BASE + 5000,
                OBATO_TO_OBATP = MSG_ID_BASE + 5100,
                ATS_SERVER_TO_WSATO = MSG_ID_BASE + 6000,
                WSATO_TO_ATS_SERVER = MSG_ID_BASE + 6100,
                ATS_SERVER_TO_OBATO = MSG_ID_BASE + 7000,
                OBATO_TO_ATS_SERVER = MSG_ID_BASE + 7100,
                ATS_SERVER_TO_WSATP = MSG_ID_BASE + 8000,
                WSATP_TO_ATS_SERVER = MSG_ID_BASE + 8100,
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
