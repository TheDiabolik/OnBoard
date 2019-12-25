using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public class Message
    {
        public UInt32 DS;
        public UInt32 Size;
        public UInt32 ID;
        public UInt32 DST;
        public UInt32 SRC;
        public ulong RTC;
        public UInt32 NO;
        public byte[] DATA;
        public UInt64 CRC;

        public Message()
        {

        }

        public Message(byte[] data)
        {
            this.DS = BitConverter.ToUInt32(data, 0);

            this.Size = BitConverter.ToUInt32(data, 4);

            this.ID = BitConverter.ToUInt32(data, 8);

            this.DST = BitConverter.ToUInt32(data, 12);

            this.SRC = BitConverter.ToUInt32(data, 16);

            this.RTC = BitConverter.ToUInt64(data, 20);

            //total miliseconds
            //UInt64 miliSeconds = BitConverter.ToUInt64(data, 20); 
            //DateTime dt = new DateTime();
            //dt.SetAllMiliSeconds(miliSeconds);  

            this.NO = BitConverter.ToUInt32(data, 28);

            #region message array
            int crcLen = 8;
            int dataMsgStart = 32;
            int dataLen = Convert.ToInt32(this.Size - crcLen - dataMsgStart);

            string dataString = Encoding.UTF8.GetString(data, 32, dataLen);
            this.DATA = Encoding.GetEncoding("iso-8859-9").GetBytes(dataString);

            #endregion

            this.CRC = BitConverter.ToUInt64(data, Convert.ToInt32(this.Size - crcLen));
        }

        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();

            result.AddRange(BitConverter.GetBytes((UInt32)DS));

            result.AddRange(BitConverter.GetBytes((UInt32)this.Size));

            result.AddRange(BitConverter.GetBytes((UInt32)this.ID));

            result.AddRange(BitConverter.GetBytes((UInt32)this.DST));

            result.AddRange(BitConverter.GetBytes((UInt32)this.SRC));

            result.AddRange(BitConverter.GetBytes((UInt64)this.RTC));

            result.AddRange(BitConverter.GetBytes((UInt32)this.NO));

            if (DATA != null)
                result.AddRange(this.DATA);
            else
                result.AddRange(BitConverter.GetBytes(0));

            result.AddRange(BitConverter.GetBytes((UInt64)this.CRC));

            return result.ToArray();
        }

        static volatile UInt32 m_msgCounter = 0;



        //public static byte[] PrepMsg(string msgToSend)
        //{
        //    byte[] sourceMessageData = UDPConnection.StringToByteMsg(msgToSend);
        //    byte[] destinationMessageData = new byte[472];

        //    Array.Copy(sourceMessageData, destinationMessageData, sourceMessageData.Length);

        //    Message message = new Message();
        //    message.DS = (UInt32)Enums.Message.DS.DS;
        //    message.Size = (UInt32)Enums.Message.Size.Size; //(40 + sourceMessageData.Length); //
        //    message.ID = (UInt32)Enums.Message.ID.OBATO_TO_WSATO;
        //    message.DST = 60001;
        //    message.SRC = 20001;
        //    message.RTC = DateTimeExtensions.GetAllMiliSeconds();
        //    message.NO = m_msgCounter++;
        //    message.DATA = destinationMessageData;

        //    //convert message struct to byte array
        //    byte[] byteData = message.ToByte();
        //    UInt64 calcCRC = Crc.Crc64_Standard_Calculate(destinationMessageData);
        //    //add crc to message struct
        //    message.CRC = calcCRC;
        //    //convert message struct with crc to byte array
        //    byteData = message.ToByte();

        //    return byteData;
        //}


    }
}
