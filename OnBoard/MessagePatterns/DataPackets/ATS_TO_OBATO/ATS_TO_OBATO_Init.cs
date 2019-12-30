using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    class ATS_TO_OBATO_Init : IDisposable
    {
        private bool m_disposed = false;

        public byte ATStoOBATO_TrainNumber { get; set; }
        public ushort TrackSectionID { get; set; }
        public byte ATStoOBATO_TrainDirection { get; set; }
        public byte ATStoOBATO_TrainSpeed { get; set; }


        //yukarıdaki yapıdan bi sürü olacak bu kısım ayarlanacak
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
