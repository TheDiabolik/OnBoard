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
        //public volatile Track Track;

        public   Track Track;

        //public volatile WrappedVolatileDouble Location;
        public   double Location;

    }

    //public class VolatileDoubleDemo
    //{
    //    private volatile WrappedVolatileDouble volatileData;
    //}
    //public class WrappedVolatileDouble
    //{
    //    public double Data { get; set; }
    //}
}