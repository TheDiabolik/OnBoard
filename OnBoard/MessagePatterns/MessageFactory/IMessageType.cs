using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    interface IMessageType
    {
        IMessageType CreateMessage(byte[] message);
    }
}
