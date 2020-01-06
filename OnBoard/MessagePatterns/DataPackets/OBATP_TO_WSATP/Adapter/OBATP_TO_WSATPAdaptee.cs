using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    class OBATP_TO_WSATPAdaptee
    {
        OBATP m_messageType;

        public bool EmergencyBrakeApplied { get; set; }
        public bool TrainAbsoluteZeroSpeed { get; set; }
        public bool AllTrainDoorsClosedAndLocked { get; set; }
        public int EnablePSD1 { get; set; }
        public int EnablePSD2 { get; set; }
        public int ActiveATP { get; set; }
        public int ActiveCab { get; set; }
        public Enums.Direction TrainDirection { get; set; }
        public int TrainCoupled { get; set; }
        public bool TrainIntegrity { get; set; }
        public bool TrainLocationDeterminationFault { get; set; }
        public int TrackDatabaseVersionMajor { get; set; }
        public int TrackDatabaseVersionMinor { get; set; }
        public bool TrainDerailment { get; set; }

        //public ushort FootPrintTrackSectionID { get; set; }
        public ushort[] FootPrintTrackSectionID { get; set; } = new ushort[15];

        //public ushort VirtualOccupancyTrackSectionID { get; set; }
        public ushort[] VirtualOccupancyTrackSectionID { get; set; } = new ushort[20];
        public bool BerthingOk { get; set; }
        public Enums.Train_ID TrainNumber { get; set; }  


        public OBATP_TO_WSATPAdaptee(OBATP OBATP)
        {
            m_messageType = (OBATP)OBATP;


 

            this.EmergencyBrakeApplied = false;
            this.TrainAbsoluteZeroSpeed = false;
            this.AllTrainDoorsClosedAndLocked = false;
            this.EnablePSD1 = 1;
            this.EnablePSD2 = 1;
            //OBATP_TO_WSATP.ActiveATP = Convert.ToByte("\x02".Substring(2), NumberStyles.HexNumber); // Encoding.ASCII.GetBytes("\x02");
            this.ActiveATP = 1; //Byte.Parse("0x02".Substring(2), NumberStyles.HexNumber); // Encoding.ASCII.GetBytes("\x02");
            this.ActiveCab = 1;// Byte.Parse("0x03".Substring(2), NumberStyles.HexNumber);//Convert.ToByte("\x03");
            this.TrainDirection = OBATP.Direction;// Convert.ToByte(OBATP.Direction);
            this.TrainCoupled = 4; // Byte.Parse("0x04".Substring(2), NumberStyles.HexNumber); //Convert.ToByte("\x04");
            this.TrainIntegrity = false;
            this.TrainLocationDeterminationFault = false;
            this.TrackDatabaseVersionMajor = 1;
            this.TrackDatabaseVersionMinor = 1;
            this.TrainDerailment = false;




            ushort[] footPrintTracks = FindTrackRangeInAllTracks(OBATP.FrontOfTrainTrackWithFootPrint.Track, OBATP.RearOfTrainTrackWithFootPrint.Track, MainForm.m_allTracks);
            ushort[] virtualOccupationTracks = FindTrackRangeInAllTracks(OBATP.FrontOfTrainVirtualOccupation.Track, OBATP.RearOfTrainVirtualOccupation.Track, MainForm.m_allTracks);




            Array.Copy(footPrintTracks, this.FootPrintTrackSectionID, OBATP.footPrintTracks.Length);
            Array.Copy(virtualOccupationTracks, this.VirtualOccupancyTrackSectionID, OBATP.virtualOccupationTracks.Length);

            this.BerthingOk = false;
            this.TrainNumber = OBATP.Vehicle.TrainID;


        }
        


        public  ushort[] FindTrackRangeInAllTracks(Track frontTrack, Track rearTrack, List<Track> allTracks)
        {
            ushort[] trackRangeList = new ushort[15];

            int frontTrackIndex = allTracks.FindIndex(x => x == frontTrack);
            int rearTrackIndex = allTracks.FindIndex(x => x == rearTrack);

            if (frontTrackIndex != -1 && rearTrackIndex != -1)
                trackRangeList = allTracks.Where((element, index) => (index <= frontTrackIndex) && (index >= rearTrackIndex)).Select(x => (ushort)x.Track_ID).ToList().ToArray();
            else if (frontTrackIndex != -1 && rearTrackIndex == -1)
                trackRangeList = allTracks.Where((element, index) => (index <= frontTrackIndex) && (index >= frontTrackIndex - 1)).Select(x => (ushort)x.Track_ID).ToList().ToArray();

            return trackRangeList;
        }



    }
}
