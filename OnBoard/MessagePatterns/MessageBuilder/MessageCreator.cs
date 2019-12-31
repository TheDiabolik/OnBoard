using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public class MessageCreator
    {
        private MessageBuilder m_messageBuilder;


        public void SetMessageBuilder(MessageBuilder messageBuilder)
        {
            this.m_messageBuilder = messageBuilder;
        }

        public Message GetMessage()
        {

            return m_messageBuilder.GetMessage();
        }

        //public Message GetMessageToByte()
        //{

        //    //return m_messageBuilder.ge.GetMessage();
        //}

        public void CreateMessage(UInt32 DST, UInt32 SRC, ulong RTC, UInt32 NO, byte[] DATA, UInt64 CRC)
        {
            m_messageBuilder.MessageCreate();

            m_messageBuilder.CreateMessageDS();
            m_messageBuilder.CreateMessageSize();
            m_messageBuilder.CreateMessageID();
            m_messageBuilder.CreateMessageDST(DST);
            m_messageBuilder.CreateMessageSRC(SRC);
            m_messageBuilder.CreateMessageRTC(RTC);
            m_messageBuilder.CreateMessageNO(NO);
            m_messageBuilder.CreateMessageDATA(DATA);
            m_messageBuilder.CreateMessageCRC(CRC);
        }
    }
}
