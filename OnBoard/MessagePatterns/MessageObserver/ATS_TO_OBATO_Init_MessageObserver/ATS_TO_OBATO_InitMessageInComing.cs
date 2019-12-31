using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
     class ATS_TO_OBATO_InitMessageInComing : IATS_TO_OBATO_InitMessageCreate
    {
        private ThreadSafeList<IATS_TO_OBATO_InitMessageWatcher> m_syncFileWatcher = new ThreadSafeList<IATS_TO_OBATO_InitMessageWatcher>();
        private ATS_TO_OBATO_InitAdapter m_ATS_TO_OBATO_InitAdapter;
        private Enums.OBATP_ID m_OBATP_ID;
        public void InformWatcher()
        {
            foreach (IATS_TO_OBATO_InitMessageWatcher watcher in m_syncFileWatcher)
            {
                watcher.ATS_TO_OBATO_InitMessageInComing(m_OBATP_ID, m_ATS_TO_OBATO_InitAdapter);
            }
        }

        public void AddWatcher(IATS_TO_OBATO_InitMessageWatcher watcher)
        {
            m_syncFileWatcher.Add(watcher);
        }


        public void ATS_TO_OBATO_InitNewMessageInComing(Enums.OBATP_ID OBATP_ID, ATS_TO_OBATO_InitAdapter ATS_TO_OBATO_InitAdapter)
        {
            this.m_ATS_TO_OBATO_InitAdapter = ATS_TO_OBATO_InitAdapter;
            this.m_OBATP_ID = OBATP_ID;
            InformWatcher();
        }
    }
}
