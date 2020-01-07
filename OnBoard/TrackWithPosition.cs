using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    [Serializable]
    public struct TrackWithPosition
    {
        public volatile Track Track;
        public double Location;

    }
}
