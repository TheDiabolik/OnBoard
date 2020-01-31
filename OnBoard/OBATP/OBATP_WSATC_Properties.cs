using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public partial class OBATP : IWSATP_TO_OBATPMessageWatcher, IATS_TO_OBATO_InitMessageWatcher, IATS_TO_OBATO_MessageWatcher, IATS, IWSATC, IDisposable
    {
        #region IWSATC interface implemantation
        [Browsable(false)]
        public bool TrainAbsoluteZeroSpeed { get; set; }
        [Browsable(false)]
        public bool OBATCtoWSATC_BerthingOk { get; set; } 






        public void CheckBerthingStatus()
        {
            TrackWithPosition berhthingTrack = ActualFrontOfTrainCurrent;

            Track track = MainForm.m_mf.m_YNK1_KIR2_YNK1.Find(x => x.Track_ID == berhthingTrack.Track.Track_ID);

            if (!string.IsNullOrEmpty(track.Station_Name))
            {
                Enums.Direction direction = Direction;
                double location;

                if (direction == Enums.Direction.Right)
                {

                    location = track.Stopping_Point_Positon_2;

                    if (((location - 40) <= berhthingTrack.Location) && (berhthingTrack.Location <= (location + 40)))
                    {
                        OBATCtoWSATC_BerthingOk = true;
                    }
                    else
                    {
                        OBATCtoWSATC_BerthingOk = false;
                    }
                }
                else if (direction == Enums.Direction.Left)
                {
                    location = track.Stopping_Point_Position_1;

                    if (((location - 40) <= berhthingTrack.Location) && (berhthingTrack.Location <= (location + 40)))
                    {
                        OBATCtoWSATC_BerthingOk = true;
                    }
                    else
                    {
                        OBATCtoWSATC_BerthingOk = false;
                    }
                }
            }
        }

        public void CheckTrainAbsoluteZeroSpeed()
        {
            if (Vehicle.CurrentTrainSpeedCMS == 0 && OBATCtoWSATC_BerthingOk)
            {
                this.TrainAbsoluteZeroSpeed = true;
            }
            else
                this.TrainAbsoluteZeroSpeed = false;
        }



        #endregion
    }
}
