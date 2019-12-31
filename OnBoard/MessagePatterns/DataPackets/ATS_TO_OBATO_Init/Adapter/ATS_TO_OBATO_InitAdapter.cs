using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public class ATS_TO_OBATO_InitAdapter
    {
        private ATS_TO_OBATO_InitAdaptee _adaptee;


        public Enums.Train_ID ATStoOBATO_TrainNumber { get; set; }
        public int TrackSectionID { get; set; }
        public Enums.Direction ATStoOBATO_TrainDirection { get; set; }
        public int ATStoOBATO_TrainSpeed { get; set; }


        public ATS_TO_OBATO_InitAdapter(IMessageType message)
        {
            _adaptee = new ATS_TO_OBATO_InitAdaptee(message);
            ATS_TO_OBATO_Init ATS_TO_OBATO_Init = _adaptee.GetMessageType();

            AdaptData(ATS_TO_OBATO_Init);
        }
        public void AdaptData(ATS_TO_OBATO_Init ATS_TO_OBATO_Init)
        {

            this.ATStoOBATO_TrainNumber = (Enums.Train_ID)(ATS_TO_OBATO_Init.ATStoOBATO_TrainNumber-1);
            this.TrackSectionID = Convert.ToInt32(ATS_TO_OBATO_Init.TrackSectionID);

            int direction = Convert.ToInt32(ATS_TO_OBATO_Init.ATStoOBATO_TrainDirection);

            if (direction == 1)
                this.ATStoOBATO_TrainDirection = Enums.Direction.Left;
            else
                this.ATStoOBATO_TrainDirection = Enums.Direction.Right; 

           
            this.ATStoOBATO_TrainSpeed = Convert.ToInt32(ATS_TO_OBATO_Init.ATStoOBATO_TrainSpeed); 




        }

        }
    }
