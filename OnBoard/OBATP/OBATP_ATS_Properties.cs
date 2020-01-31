using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public partial class OBATP : IWSATP_TO_OBATPMessageWatcher, IATS_TO_OBATO_InitMessageWatcher, IATS_TO_OBATO_MessageWatcher, IATS, IWSATC, IDisposable
    {
        #region IATS interface implemantation
        //[Browsable(false)]
        //public int DwellTime { get; set; } 
        private volatile int dwellTime;

        [Browsable(false)]
        public int DwellTime
        {
            get { return dwellTime; }

            set
            {
                if (value != dwellTime)
                { 
                    dwellTime = value;

                    zongurt = dwellTime;
                }
              
            }
        }
         

        [Browsable(false)]
        public int SkipStation { get; set; }
        #endregion
    }
}
