using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard

{
    class ATS_TO_OBATO : IDisposable
    {
        private bool m_disposed = false;

        public byte ApplyEmergencyBrake { get; set; }
        public byte ReleaseEmergencyBrake { get; set; }
        public byte BlockTrainDoor { get; set; }
        public byte UnblockTrainDoor { get; set; }
        public byte HoldTrain { get; set; }
        public byte CancelHoldTrain { get; set; }
        public byte SkipStation { get; set; }
        public byte CancelSkipStation { get; set; }
        public byte StandbyCmd { get; set; }
        public byte WakeUpTrain { get; set; }
        public byte OpenTrainLeftDoor { get; set; }
        //
        public byte OpenTrainRightDoor { get; set; }
        public byte CloseTrainLeftDoor { get; set; }
        public byte CloseTrainRightDoor { get; set; }
        public byte StopTrainAtNextStation { get; set; }
        public byte ResetOBATC { get; set; }
        public byte OutOfServiceTrain { get; set; }
        public byte TriggerAnnouncement { get; set; }


        public ushort CoastingStartTrackID { get; set; }
        public ushort CoastingEndTrackID { get; set; }
        public byte CoastingMinimumSpeed { get; set; }
        public byte PerformanceLevel { get; set; }
        public byte DoorOpenSequence { get; set; }
        //
        public ushort DwellTime { get; set; }
        public byte StartingStationID { get; set; }
        public byte TargetStationID { get; set; }
        public byte TargetPlatformID { get; set; }
        public byte NextFourStationID1 { get; set; }
        public byte NextFourStationID2 { get; set; }
        public byte NextFourStationID3 { get; set; }
        public byte NextFourStationID4 { get; set; }

        public ushort AnnouncementNumber { get; set; }
        public byte TrackDatabaseVersionMajor { get; set; }
        public byte TrackDatabaseVersionMinor { get; set; }




        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose time code 
                //buraya sonlanma için method eklenecek
            }

            // Finalize time code 
            m_disposed = true;
        }

        public void Dispose()
        {
            if (m_disposed)
            {
                Dispose(true);

                GC.SuppressFinalize(this);
            }
        }
    }
}
