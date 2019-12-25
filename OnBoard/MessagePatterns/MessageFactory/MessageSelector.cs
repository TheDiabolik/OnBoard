using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard 
{
    class MessageSelector
    {
        public IMessageType GetMessageType(Enums.Message.ID messageID)
        {
            if (messageID == Enums.Message.ID.WSATP_TO_OBATP)
            {
                return new WSATP_TO_OBATP();
            }
           else //if (messageID == Enums.Message.ID.OBATP_TO_WSATP)
            {
                return new OBATP_TO_WSATP();
            }
        }
    }
}
