using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{

    class OBATP_TO_WSATP
    {

        public byte EmergencyBrakeApplied { get; set; }
        public byte TrainAbsoluteZeroSpeed { get; set; }
        public byte AllTrainDoorsClosedAndLocked { get; set; }
        public byte EnablePSD1 { get; set; }
        public byte EnablePSD2 { get; set; }
        public byte ActiveATP { get; set; }
        public byte ActiveCab { get; set; }
        public byte TrainDirection { get; set; }
        public byte TrainCoupled { get; set; }
        public byte TrainIntegrity { get; set; }
        public byte TrainLocationDeterminationFault { get; set; }
        public byte TrackDatabaseVersionMajor { get; set; }
        public byte TrackDatabaseVersionMinor { get; set; }
        public byte TrainDerailment { get; set; }

        //public ushort FootPrintTrackSectionID { get; set; }
        public ushort[] FootPrintTrackSectionID { get; set; } = new ushort[15];

        //public ushort VirtualOccupancyTrackSectionID { get; set; }
        public ushort[] VirtualOccupancyTrackSectionID { get; set; } = new ushort[20];
        public byte BerthingOk { get; set; }
        public byte TrainNumber { get; set; }

        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();


            result.Add(this.EmergencyBrakeApplied);

            result.Add(this.TrainAbsoluteZeroSpeed);
            result.Add(this.AllTrainDoorsClosedAndLocked);

            result.Add(this.EnablePSD1);

            result.Add(this.EnablePSD2);

            result.Add(this.ActiveATP);

            result.Add(this.ActiveCab);

            result.Add(this.TrainDirection);
            result.Add(this.TrainCoupled);
            result.Add(this.TrainIntegrity);
            result.Add(this.TrainLocationDeterminationFault);
            result.Add(this.TrackDatabaseVersionMajor);
            result.Add(this.TrackDatabaseVersionMinor);
            result.Add(this.TrainDerailment);


            //result.AddRange(BitConverter.GetBytes((ushort)this.FootPrintTrackSectionID));
            //result.AddRange(BitConverter.GetBytes((ushort)this.VirtualOccupancyTrackSectionID));


            for (int i = 0; i < FootPrintTrackSectionID.Length; i++)
            {
                //ushort osman = this.FootPrintTrackSectionID[i]; 
                //int sdf = Marshal.SizeOf(osman); 

                result.AddRange(BitConverter.GetBytes((ushort)this.FootPrintTrackSectionID[i]));
            }

            for (int i = 0; i < VirtualOccupancyTrackSectionID.Length; i++)
            {
                result.AddRange(BitConverter.GetBytes((ushort)this.VirtualOccupancyTrackSectionID[i]));
            }


            result.Add(this.BerthingOk);
            result.Add(this.TrainNumber);

            return result.ToArray();
        }







        //public OBATP_TO_WSATP()
        //{
        //   this.FootPrintTrackSectionID  = new ushort[14];
        //}
















        //             FootPrintTrackSectionID[2]
        //FootPrintTrackSectionID[3]
        //FootPrintTrackSectionID[4]
        //FootPrintTrackSectionID[5]
        //FootPrintTrackSectionID[6]
        //FootPrintTrackSectionID[7]
        //FootPrintTrackSectionID[8]
        //FootPrintTrackSectionID[9]
        //FootPrintTrackSectionID[10]
        //            FootPrintTrackSectionID[11]
        //FootPrintTrackSectionID[12]
        //FootPrintTrackSectionID[13]
        //FootPrintTrackSectionID[14]
        //FootPrintTrackSectionID[15]





        //        VirtualOccupancyTrackSectionID[1]
        //VirtualOccupancyTrackSectionID[2]
        //VirtualOccupancyTrackSectionID[3]
        //VirtualOccupancyTrackSectionID[4]
        //VirtualOccupancyTrackSectionID[5]
        //VirtualOccupancyTrackSectionID[6]
        //VirtualOccupancyTrackSectionID[7]
        //VirtualOccupancyTrackSectionID[8]
        //VirtualOccupancyTrackSectionID[9]
        //VirtualOccupancyTrackSectionID[10]
        //VirtualOccupancyTrackSectionID[11]
        //VirtualOccupancyTrackSectionID[12]
        //VirtualOccupancyTrackSectionID[13]
        //VirtualOccupancyTrackSectionID[14]
        //VirtualOccupancyTrackSectionID[15]
        //VirtualOccupancyTrackSectionID[16]
        //VirtualOccupancyTrackSectionID[17]
        //VirtualOccupancyTrackSectionID[18]
        //VirtualOccupancyTrackSectionID[19]
        //VirtualOccupancyTrackSectionID[20]
    }
}
 
