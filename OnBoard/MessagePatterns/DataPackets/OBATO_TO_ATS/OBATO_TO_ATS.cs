using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    class OBATO_TO_ATS
    {
        public byte OBATCActive { get; set; }
        public byte TrainEmergencyBrakeApplied { get; set; }
        public byte WaitingApprovalReleaseEmergencyBrake { get; set; }
        public byte TrainEmergencyBrakeReleased { get; set; }
        public byte EmergencyHandleActive { get; set; }

        public byte TrainTemporaryCoastingAccepted { get; set; }
        public byte TrainTemporaryCoastingRejected { get; set; }
        public byte HoldTrainAccepted { get; set; }
        public byte HoldTrainRejected { get; set; }
        public byte CancelHoldTrainAccepted { get; set; }


        public byte SkipStationAccepted { get; set; }
        public byte SkipStationRejected { get; set; }
        public byte CancelSkipStationAccepted { get; set; }
        public byte CancelSkipStationRejected { get; set; }
        public byte StandbyActive { get; set; }

        public byte StandbyCmdRejected { get; set; }
        public byte TrainLeftDoorOpened { get; set; }
        public byte TrainRightDoorOpened { get; set; }
        public byte TrainDoorClosedAndLocked { get; set; }
        public byte TrainDoorClosedAndLockedFault { get; set; }

        public byte TrainDoorWrongSideOpenedFault { get; set; }
        public byte DoorFaultAtStandby { get; set; }
        public byte DerailmentDetection { get; set; }
        public byte FireDetection { get; set; }
        public byte ObstacleDetection { get; set; }

        public byte BerthingOK { get; set; }
        public byte UnsuccessfulBerthingAlarm { get; set; }
        public byte TrainIntegrityAlarm { get; set; }
        public byte TrainActiveCab { get; set; }
        public byte TrainDirection { get; set; }
        //
        public byte OBATCOff { get; set; }
        public byte OBATCClockFault { get; set; }
        public byte TrainDepartureFailure { get; set; }
        public byte TrainRollBack { get; set; }
        public byte BatteryOK { get; set; }
        public byte TractionStatus { get; set; }
        public byte BrakingStatus { get; set; }
        public byte TrainCBTCMode { get; set; }
        public byte PerformanceLevel { get; set; }
        public byte OBATCSoftwareVersion { get; set; }
        public byte OBATCHardwareVersion { get; set; }
        public byte TrainNumber { get; set; }
        public byte TrainSetCarNumber { get; set; }
        public byte TrainSpeed { get; set; }

        //
        public ushort[] FootPrintTrackSectionID { get; set; } = new ushort[15];

        public ushort FootPrintFirstTrackSectionOffset { get; set; }
        public ushort FootPrintLastTrackSectionOffset { get; set; }

        public ushort[] VirtualOccupancyTrackSectionID { get; set; } = new ushort[20];

        public ushort VirtualOccupancyFirstTrackSectionOffset { get; set; }
        public ushort VirtualOccupancyLastTrackSectionOffset { get; set; }


        public ushort OBATCEquipmentStatus { get; set; }
        public ushort OBATCWarning1 { get; set; }
        public ushort OBATCWarning2 { get; set; }
        public ushort OBATCWarning3 { get; set; }
        public ushort OBATCWarning4 { get; set; }
        public ushort OBATCWarning5 { get; set; }
        public ushort OBATCFault1 { get; set; }
        public ushort OBATCFault2 { get; set; }
        public ushort OBATCEquipmentFault1 { get; set; }
        public ushort OBATCEquipmentFault2 { get; set; }

        //

        public ushort OBATCEquipmentFault4 { get; set; }
        public ushort OBATCEquipmentFault5 { get; set; }
        public ushort TrainFault1 { get; set; }
        public ushort TrainFault2 { get; set; }
        public ushort TrainFault3 { get; set; }

        public ushort TrainFault4 { get; set; }
        public ushort TrainFault5 { get; set; }
        public byte TrainCoupled { get; set; }
        public ushort DwellTime { get; set; }

        //

        public byte OBATCtoATS_ReverseTrafficDirection { get; set; }
        public byte OBATCtoATS_RejectedStopTrainAtNextStation { get; set; }
        public byte OBATCtoATS_TrainLocationDeterminationFault { get; set; }
        public byte OBATCtoATS_TrainSpeedSensorFault { get; set; }
        public byte OBATCtoATS_MissedBalise { get; set; }
        public byte OBATCtoATS_DepartureTestStarted { get; set; }
        public byte OBATCtoATS_DepartureTestResults { get; set; }
        public byte OBATCtoATS_OverspeedAlarm { get; set; }
        public byte OBATCtoATS_SafeDistanceAlarm { get; set; }
        public byte OBATCtoATS_UnsuccessfulTrainStop { get; set; }
        public byte OBATCtoATS_UnexpectedSkipStation { get; set; }
        public byte OBATCtoATS_PSDEnableFault { get; set; }
        public byte OBATCtoATS_TrainDoorEnableFault { get; set; }
        public byte OBATCtoATS_PSDOpenFault { get; set; }
        public byte OBATCtoATS_TrainDoorOpenFault { get; set; } 


    }
}
