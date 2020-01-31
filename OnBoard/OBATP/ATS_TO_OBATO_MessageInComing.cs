using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard 
{
    public partial class OBATP : IWSATP_TO_OBATPMessageWatcher, IATS_TO_OBATO_InitMessageWatcher, IATS_TO_OBATO_MessageWatcher, IATS, IWSATC, IDisposable
    {
        public void ATS_TO_OBATO_MessageInComing(Enums.Train_ID train_ID, ATS_TO_OBATOAdapter ATS_TO_OBATOAdapter)
        {
            lock (ATS_TO_OBATOAdapter)
            {
                if (this.Vehicle.TrainID == train_ID)
                {

                    this.DwellTime = ATS_TO_OBATOAdapter.DwellTime;


                }
            }
        }
    }
}
