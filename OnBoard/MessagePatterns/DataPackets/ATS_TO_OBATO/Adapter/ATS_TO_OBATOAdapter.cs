using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public class ATS_TO_OBATOAdapter
    {
        private ATS_TO_OBATOAdaptee _adaptee; 


        public bool ApplyEmergencyBrake { get; set; }
        public bool ReleaseEmergencyBrake { get; set; }
        public bool BlockTrainDoor { get; set; }
        public bool UnblockTrainDoor { get; set; }
        public bool HoldTrain { get; set; }
        public bool CancelHoldTrain { get; set; }
        public bool SkipStation { get; set; }
        public bool CancelSkipStation { get; set; }
        public bool StandbyCmd { get; set; }
        public bool WakeUpTrain { get; set; }
        public bool OpenTrainLeftDoor { get; set; }
        //
        public bool OpenTrainRightDoor { get; set; }
        public bool CloseTrainLeftDoor { get; set; }
        public bool CloseTrainRightDoor { get; set; }
        public bool StopTrainAtNextStation { get; set; }
        public bool ResetOBATC { get; set; }
        public bool OutOfServiceTrain { get; set; }
        public bool TriggerAnnouncement { get; set; }


        public int CoastingStartTrackID { get; set; }
        public int CoastingEndTrackID { get; set; }
        public int CoastingMinimumSpeed { get; set; }
        public int PerformanceLevel { get; set; }
        public byte DoorOpenSequence { get; set; }
        //
        public int DwellTime { get; set; }
        public int StartingStationID { get; set; }
        public int TargetStationID { get; set; }
        public int TargetPlatformID { get; set; }


        public List<int> NextFourStationID { get; set; } = new List<int>();
        public int AnnouncementNumber { get; set; }
        public int TrackDatabaseVersionMajor { get; set; }
        public int TrackDatabaseVersionMinor { get; set; } 


        public ATS_TO_OBATOAdapter(IMessageType message)
        {
            _adaptee = new ATS_TO_OBATOAdaptee(message);
            ATS_TO_OBATO ATS_TO_OBATO = _adaptee.GetMessageType();

            AdaptData(ATS_TO_OBATO);
        }

        public void AdaptData(ATS_TO_OBATO ATS_TO_OBATO)
        {

            this.ApplyEmergencyBrake = Convert.ToBoolean(ATS_TO_OBATO.ApplyEmergencyBrake);
            this.ReleaseEmergencyBrake = Convert.ToBoolean(ATS_TO_OBATO.ReleaseEmergencyBrake);
            this.BlockTrainDoor = Convert.ToBoolean(ATS_TO_OBATO.BlockTrainDoor);
            this.UnblockTrainDoor = Convert.ToBoolean(ATS_TO_OBATO.UnblockTrainDoor);
            this.HoldTrain = Convert.ToBoolean(ATS_TO_OBATO.HoldTrain);
            this.CancelHoldTrain = Convert.ToBoolean(ATS_TO_OBATO.CancelHoldTrain);
            this.SkipStation = Convert.ToBoolean(ATS_TO_OBATO.SkipStation);
            this.CancelSkipStation = Convert.ToBoolean(ATS_TO_OBATO.CancelSkipStation);
            this.StandbyCmd = Convert.ToBoolean(ATS_TO_OBATO.StandbyCmd);
            this.WakeUpTrain = Convert.ToBoolean(ATS_TO_OBATO.WakeUpTrain);

            //

            this.OpenTrainLeftDoor = Convert.ToBoolean(ATS_TO_OBATO.OpenTrainLeftDoor);
            this.OpenTrainRightDoor = Convert.ToBoolean(ATS_TO_OBATO.OpenTrainRightDoor);
            this.CloseTrainLeftDoor = Convert.ToBoolean(ATS_TO_OBATO.CloseTrainLeftDoor);

            this.CloseTrainRightDoor = Convert.ToBoolean(ATS_TO_OBATO.CloseTrainRightDoor);
            this.StopTrainAtNextStation = Convert.ToBoolean(ATS_TO_OBATO.StopTrainAtNextStation);
            this.ResetOBATC = Convert.ToBoolean(ATS_TO_OBATO.ResetOBATC);
            this.OutOfServiceTrain = Convert.ToBoolean(ATS_TO_OBATO.OutOfServiceTrain);
            this.TriggerAnnouncement = Convert.ToBoolean(ATS_TO_OBATO.TriggerAnnouncement);

            //
            this.CoastingStartTrackID = Convert.ToInt32(ATS_TO_OBATO.CoastingStartTrackID);
            this.CoastingEndTrackID = Convert.ToInt32(ATS_TO_OBATO.CoastingEndTrackID);
            this.CoastingMinimumSpeed = Convert.ToInt32(ATS_TO_OBATO.CoastingMinimumSpeed);
            this.PerformanceLevel = Convert.ToInt32(ATS_TO_OBATO.PerformanceLevel);
            this.DoorOpenSequence = ATS_TO_OBATO.DoorOpenSequence;


            this.DwellTime = Convert.ToInt32(ATS_TO_OBATO.DwellTime);
            this.StartingStationID = Convert.ToInt32(ATS_TO_OBATO.StartingStationID);
            this.TargetStationID = Convert.ToInt32(ATS_TO_OBATO.TargetStationID);
            this.TargetPlatformID = Convert.ToInt32(ATS_TO_OBATO.TargetPlatformID);




            int NextFourStationID1 = Convert.ToInt32(ATS_TO_OBATO.NextFourStationID1);
           int NextFourStationID2 = Convert.ToInt32(ATS_TO_OBATO.NextFourStationID2);
           int NextFourStationID3 = Convert.ToInt32(ATS_TO_OBATO.NextFourStationID3);
            int NextFourStationID4 = Convert.ToInt32(ATS_TO_OBATO.NextFourStationID4);

            this.NextFourStationID.Add(NextFourStationID1);
            this.NextFourStationID.Add(NextFourStationID2);
            this.NextFourStationID.Add(NextFourStationID3);
            this.NextFourStationID.Add(NextFourStationID4);



            this.AnnouncementNumber = Convert.ToInt32(ATS_TO_OBATO.AnnouncementNumber);

            this.TrackDatabaseVersionMajor = Convert.ToInt32(ATS_TO_OBATO.TrackDatabaseVersionMajor);
            this.TrackDatabaseVersionMinor = Convert.ToInt32(ATS_TO_OBATO.TrackDatabaseVersionMinor); 

        }
    }
}
