using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
   public class OBATO_TO_ATSAdapter : OBATO_TO_ATS
    {
        private OBATO_TO_ATSAdaptee _adaptee;


       

        public OBATO_TO_ATSAdapter(OBATP OBATPToAdapt)
        {
            _adaptee = new OBATO_TO_ATSAdaptee(OBATPToAdapt);


            AdaptData();
        }

        public void AdaptData()
        {
            base.OBATCActive = _adaptee.OBATCActive.BoolToHex();
            base.TrainEmergencyBrakeApplied = _adaptee.TrainEmergencyBrakeApplied.BoolToHex();
            base.WaitingApprovalReleaseEmergencyBrake = _adaptee.WaitingApprovalReleaseEmergencyBrake.BoolToHex();
            base.TrainEmergencyBrakeReleased = _adaptee.TrainEmergencyBrakeReleased.BoolToHex();
            base.EmergencyHandleActive = _adaptee.EmergencyHandleActive.BoolToHex();

            base.TrainTemporaryCoastingAccepted = _adaptee.TrainTemporaryCoastingAccepted.BoolToHex();
            base.TrainTemporaryCoastingRejected = _adaptee.TrainTemporaryCoastingRejected.BoolToHex();
            base.HoldTrainAccepted = _adaptee.HoldTrainAccepted.BoolToHex();
            base.HoldTrainRejected = _adaptee.HoldTrainRejected.BoolToHex();
            base.CancelHoldTrainAccepted = _adaptee.CancelHoldTrainAccepted.BoolToHex();


            base.SkipStationAccepted = _adaptee.SkipStationAccepted.BoolToHex();
            base.SkipStationRejected = _adaptee.SkipStationRejected.BoolToHex();
            base.CancelSkipStationAccepted = _adaptee.CancelSkipStationAccepted.BoolToHex();
            base.CancelSkipStationRejected = _adaptee.CancelSkipStationRejected.BoolToHex();
            base.StandbyActive = _adaptee.StandbyActive.BoolToHex();

            base.StandbyCmdRejected = _adaptee.StandbyCmdRejected.BoolToHex();
            base.TrainLeftDoorOpened = _adaptee.TrainLeftDoorOpened.BoolToHex();
            base.TrainRightDoorOpened = _adaptee.TrainRightDoorOpened.BoolToHex();
            base.TrainDoorClosedAndLocked = _adaptee.TrainDoorClosedAndLocked.BoolToHex();
            base.TrainDoorClosedAndLockedFault = _adaptee.TrainDoorClosedAndLockedFault.BoolToHex();

            base.TrainDoorWrongSideOpenedFault = _adaptee.TrainDoorWrongSideOpenedFault.BoolToHex();
            base.DoorFaultAtStandby = _adaptee.DoorFaultAtStandby.BoolToHex();
            base.DerailmentDetection = _adaptee.DerailmentDetection.BoolToHex();
            base.FireDetection = _adaptee.FireDetection.BoolToHex();
            base.ObstacleDetection =  _adaptee.ObstacleDetection.BoolToHex();

            base.BerthingOK = _adaptee.BerthingOK.BoolToHex();
            base.UnsuccessfulBerthingAlarm = _adaptee.UnsuccessfulBerthingAlarm.BoolToHex();
            base.TrainIntegrityAlarm = _adaptee.TrainIntegrityAlarm.BoolToHex();
            base.TrainActiveCab = _adaptee.TrainActiveCab.BoolToHex();
            base.TrainDirection = _adaptee.TrainDirection.BoolToHex();
            //
            base.OBATCOff = _adaptee.OBATCOff.BoolToHex();
            base.OBATCClockFault = _adaptee.OBATCClockFault.BoolToHex();
            base.TrainDepartureFailure = _adaptee.TrainDepartureFailure.BoolToHex();
            base.TrainRollBack = _adaptee.TrainRollBack.BoolToHex(); ;
            base.BatteryOK = _adaptee.BatteryOK.BoolToHex();
            base.TractionStatus = _adaptee.TractionStatus.BoolToHex();
            base.BrakingStatus = _adaptee.BrakingStatus.BoolToHex();


            //   
            base.TrainCBTCMode= Convert.ToByte(_adaptee.TrainCBTCMode);
            base.PerformanceLevel = Convert.ToByte(_adaptee.PerformanceLevel);
            base.OBATCSoftwareVersion = Convert.ToByte(_adaptee.OBATCSoftwareVersion);
            base.OBATCHardwareVersion = Convert.ToByte(_adaptee.OBATCHardwareVersion);
            base.TrainNumber = Convert.ToByte(_adaptee.TrainNumber);
            base.TrainSetCarNumber = Convert.ToByte(_adaptee.TrainSetCarNumber);
            base.TrainSpeed = Convert.ToByte(_adaptee.TrainSpeed);

            //

            Array.Copy(_adaptee.FootPrintTrackSectionID, base.FootPrintTrackSectionID, _adaptee.FootPrintTrackSectionID.Length);

            base.FootPrintFirstTrackSectionOffset = Convert.ToUInt16(_adaptee.FootPrintFirstTrackSectionOffset);
            base.FootPrintLastTrackSectionOffset = Convert.ToUInt16(_adaptee.FootPrintLastTrackSectionOffset);

            Array.Copy(_adaptee.VirtualOccupancyTrackSectionID, base.VirtualOccupancyTrackSectionID, _adaptee.VirtualOccupancyTrackSectionID.Length);

            base.VirtualOccupancyFirstTrackSectionOffset = Convert.ToUInt16(_adaptee.VirtualOccupancyFirstTrackSectionOffset);
            base.VirtualOccupancyLastTrackSectionOffset = Convert.ToUInt16(_adaptee.VirtualOccupancyFirstTrackSectionOffset);

            //belli olmayanlar gelecek

            base.TrainCoupled = _adaptee.TrainCoupled.TrainCoupledToHex();
            base.DwellTime = Convert.ToUInt16(_adaptee.DwellTime);

            //belli olmayanlar gelecek


            base.OBATCtoATS_OverspeedAlarm = _adaptee.OBATCtoATS_OverspeedAlarm.BoolToHex();
            base.OBATCtoATS_SafeDistanceAlarm = _adaptee.OBATCtoATS_SafeDistanceAlarm.BoolToHex();
            base.OBATCtoATS_UnsuccessfulTrainStop = _adaptee.OBATCtoATS_UnsuccessfulTrainStop.BoolToHex();
            base.OBATCtoATS_UnexpectedSkipStation = _adaptee.OBATCtoATS_UnexpectedSkipStation.BoolToHex();
            base.OBATCtoATS_PSDEnableFault = _adaptee.OBATCtoATS_PSDEnableFault.BoolToHex();
            base.OBATCtoATS_TrainDoorEnableFault = _adaptee.OBATCtoATS_TrainDoorEnableFault.BoolToHex();
            base.OBATCtoATS_PSDOpenFault = _adaptee.OBATCtoATS_PSDOpenFault.BoolToHex();
            base.OBATCtoATS_TrainDoorOpenFault = _adaptee.OBATCtoATS_TrainDoorOpenFault.BoolToHex();  




            //FaultyTrainDoors1
            base.FaultyTrainDoors1_1 = BoolsToBit(_adaptee.FaultyTrainDoors1_1, _adaptee.FaultyTrainDoors1_2, _adaptee.FaultyTrainDoors1_3, _adaptee.FaultyTrainDoors1_4, _adaptee.FaultyTrainDoors1_5,
                _adaptee.FaultyTrainDoors1_6, _adaptee.FaultyTrainDoors1_7, _adaptee.FaultyTrainDoors1_8);

            base.FaultyTrainDoors1_2 = BoolsToBit(_adaptee.FaultyTrainDoors1_9, _adaptee.FaultyTrainDoors1_10, _adaptee.FaultyTrainDoors1_11, _adaptee.FaultyTrainDoors1_12, _adaptee.FaultyTrainDoors1_13,
                _adaptee.FaultyTrainDoors1_14, _adaptee.FaultyTrainDoors1_15, _adaptee.FaultyTrainDoors1_16);

            base.FaultyTrainDoors1_3 = BoolsToBit(_adaptee.FaultyTrainDoors1_17, _adaptee.FaultyTrainDoors1_18, _adaptee.FaultyTrainDoors1_19, _adaptee.FaultyTrainDoors1_20, _adaptee.FaultyTrainDoors1_21,
                _adaptee.FaultyTrainDoors1_22, _adaptee.FaultyTrainDoors1_23, _adaptee.FaultyTrainDoors1_24);

            //FaultyTrainDoors2
            base.FaultyTrainDoors2_1 = BoolsToBit(_adaptee.FaultyTrainDoors2_1, _adaptee.FaultyTrainDoors2_2, _adaptee.FaultyTrainDoors2_3, _adaptee.FaultyTrainDoors2_4, _adaptee.FaultyTrainDoors2_5,
              _adaptee.FaultyTrainDoors2_6, _adaptee.FaultyTrainDoors2_7, _adaptee.FaultyTrainDoors2_8);

            base.FaultyTrainDoors2_2 = BoolsToBit(_adaptee.FaultyTrainDoors2_9, _adaptee.FaultyTrainDoors2_10, _adaptee.FaultyTrainDoors2_11, _adaptee.FaultyTrainDoors2_12, _adaptee.FaultyTrainDoors2_13,
                _adaptee.FaultyTrainDoors2_14, _adaptee.FaultyTrainDoors2_15, _adaptee.FaultyTrainDoors2_16);

            base.FaultyTrainDoors2_3 = BoolsToBit(_adaptee.FaultyTrainDoors2_17, _adaptee.FaultyTrainDoors2_18, _adaptee.FaultyTrainDoors2_19, _adaptee.FaultyTrainDoors2_20, _adaptee.FaultyTrainDoors2_21,
                _adaptee.FaultyTrainDoors2_22, _adaptee.FaultyTrainDoors2_23, _adaptee.FaultyTrainDoors2_24);

          //  BypassedTrainDoors1
            base.BypassedTrainDoors1_1 = BoolsToBit(_adaptee.BypassedTrainDoors1_1, _adaptee.BypassedTrainDoors1_2, _adaptee.BypassedTrainDoors1_3, _adaptee.BypassedTrainDoors1_4, _adaptee.BypassedTrainDoors1_5,
                _adaptee.BypassedTrainDoors1_6, _adaptee.BypassedTrainDoors1_7, _adaptee.BypassedTrainDoors1_8);

            base.BypassedTrainDoors1_2 = BoolsToBit(_adaptee.BypassedTrainDoors1_9, _adaptee.BypassedTrainDoors1_10, _adaptee.BypassedTrainDoors1_11, _adaptee.BypassedTrainDoors1_12, _adaptee.BypassedTrainDoors1_13,
                _adaptee.BypassedTrainDoors1_14, _adaptee.BypassedTrainDoors1_15, _adaptee.BypassedTrainDoors1_16);

            base.BypassedTrainDoors1_3 = BoolsToBit(_adaptee.BypassedTrainDoors1_17, _adaptee.BypassedTrainDoors1_18, _adaptee.BypassedTrainDoors1_19, _adaptee.BypassedTrainDoors1_20, _adaptee.BypassedTrainDoors1_21,
                _adaptee.BypassedTrainDoors1_22, _adaptee.BypassedTrainDoors1_23, _adaptee.BypassedTrainDoors1_24);

            //  BypassedTrainDoors2
            base.BypassedTrainDoors2_1 = BoolsToBit(_adaptee.BypassedTrainDoors2_1, _adaptee.BypassedTrainDoors2_2, _adaptee.BypassedTrainDoors2_3, _adaptee.BypassedTrainDoors2_4, _adaptee.BypassedTrainDoors2_5,
                _adaptee.BypassedTrainDoors2_6, _adaptee.BypassedTrainDoors2_7, _adaptee.BypassedTrainDoors2_8);

            base.BypassedTrainDoors2_2 = BoolsToBit(_adaptee.BypassedTrainDoors2_9, _adaptee.BypassedTrainDoors2_10, _adaptee.BypassedTrainDoors2_11, _adaptee.BypassedTrainDoors2_12, _adaptee.BypassedTrainDoors2_13,
                _adaptee.BypassedTrainDoors2_14, _adaptee.BypassedTrainDoors2_15, _adaptee.BypassedTrainDoors2_16);

            base.BypassedTrainDoors2_3 = BoolsToBit(_adaptee.BypassedTrainDoors2_17, _adaptee.BypassedTrainDoors2_18, _adaptee.BypassedTrainDoors2_19, _adaptee.BypassedTrainDoors2_20, _adaptee.BypassedTrainDoors2_21,
                _adaptee.BypassedTrainDoors2_22, _adaptee.BypassedTrainDoors2_23, _adaptee.BypassedTrainDoors2_24);

        }


        public byte BoolsToBit(params bool[] bools)
        {
            //bool[] bools = new bool[8];
            //bools[0] = _adaptee.FaultyTrainDoors1_1;
            //bools[1] = _adaptee.FaultyTrainDoors1_2;
            //bools[2] = _adaptee.FaultyTrainDoors1_3;
            //bools[3] = _adaptee.FaultyTrainDoors1_4;
            //bools[4] = _adaptee.FaultyTrainDoors1_5;
            //bools[5] = _adaptee.FaultyTrainDoors1_6;
            //bools[6] = _adaptee.FaultyTrainDoors1_7;
            //bools[7] = _adaptee.FaultyTrainDoors1_8;

            BitArray a = new BitArray(bools);
            byte[] bytes = new byte[a.Length / 8];
            a.CopyTo(bytes, 0);

           
            return bytes[0];
        }



    }
}
