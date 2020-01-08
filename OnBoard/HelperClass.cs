using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    class HelperClass
    {
        static readonly object m_lock = new object();

        public static byte BoolToHex(bool value)
        {
            byte boolToHexValue;

           if (value)
                boolToHexValue = Byte.Parse("0xAA".Substring(2), NumberStyles.HexNumber);
           else
                boolToHexValue = Byte.Parse("0x55".Substring(2), NumberStyles.HexNumber);

            return boolToHexValue;

        }

        public static ushort[] FindTrackRangeInAllTracks(Track frontTrack, Track rearTrack, ThreadSafeList<Track> allTracks)
        {
            lock(m_lock)
            {
                ushort[] trackRangeList = new ushort[15];

                int frontTrackIndex = allTracks.ToList().FindIndex(x => x == frontTrack);
                int rearTrackIndex = allTracks.ToList().FindIndex(x => x == rearTrack);

                if (frontTrackIndex != -1 && rearTrackIndex != -1)
                    trackRangeList = allTracks.Where((element, index) => (index <= frontTrackIndex) && (index >= rearTrackIndex)).Select(x => (ushort)x.Track_ID).ToList().ToArray();
                else if (frontTrackIndex != -1 && rearTrackIndex == -1)
                    trackRangeList = allTracks.Where((element, index) => (index <= frontTrackIndex) && (index >= frontTrackIndex - 1)).Select(x => (ushort)x.Track_ID).ToList().ToArray();

                return trackRangeList;
            }
           
        }
    }
}
