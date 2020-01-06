using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public class UIOBATP
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


 



    }
}
