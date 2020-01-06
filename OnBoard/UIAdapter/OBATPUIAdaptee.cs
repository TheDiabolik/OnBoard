using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    class OBATPUIAdaptee
    {
        //general
        public string ID { get; set; }
        public string Train_Name { get; set; }
        public string Speed { get; set; }
        //front
        public string Front_Track_ID { get; set; }
        public string Front_Track_Location { get; set; }
        public string Front_Track_Length { get; set; }
        public string Front_Track_Max_Speed { get; set; }
        //rear
        public string Rear_Track_ID { get; set; }
        public string Rear_Track_Location { get; set; }
        public string Rear_Track_Length { get; set; }
        public string Rear_Track_Max_Speed { get; set; }

        //general
        public string Total_Route_Distance { get; set; }

        public OBATPUIAdaptee(OBATP OBATP)
        {

            this.ID = OBATP.Vehicle.TrainIndex.ToString();
            this.Train_Name = OBATP.Vehicle.TrainName;
            this.Speed = OBATP.Vehicle.CurrentTrainSpeedKMH.ToString();
            this.Front_Track_ID = OBATP.ActualFrontOfTrainCurrent.Track.Track_ID.ToString();
            this.Front_Track_Location = OBATP.ActualFrontOfTrainCurrent.Location.ToString("0.##");
            this.Front_Track_Length = OBATP.ActualFrontOfTrainCurrent.Track.Track_Length.ToString();
            this.Front_Track_Max_Speed = OBATP.ActualFrontOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString();
            this.Rear_Track_ID = OBATP.ActualRearOfTrainCurrent.Track.Track_ID.ToString();
            this.Rear_Track_Location = OBATP.ActualRearOfTrainCurrent.Location.ToString("0.##");
            this.Rear_Track_Length = OBATP.ActualRearOfTrainCurrent.Track.Track_Length.ToString();
            this.Rear_Track_Max_Speed = OBATP.ActualRearOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString();
            this.Total_Route_Distance = OBATP.TotalTrainDistance.ToString("0.##");
        }
    }
}
