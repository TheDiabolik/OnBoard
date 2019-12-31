using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    class WSATP_TO_OBATPMessageInComing : IWSATP_TO_OBATPMessageCreate
    {
        private ThreadSafeList<IWSATP_TO_OBATPMessageWatcher> m_syncFileWatcher = new ThreadSafeList<IWSATP_TO_OBATPMessageWatcher>();
        private WSATP_TO_OBATPAdapter m_WSATP_TO_OBATPAdapter;
        private Enums.OBATP_ID m_OBATP_ID;
        public void InformWatcher()
        {
            foreach (IWSATP_TO_OBATPMessageWatcher watcher in m_syncFileWatcher)
            {
                watcher.WSATP_TO_OBATPMessageInComing(m_OBATP_ID, m_WSATP_TO_OBATPAdapter);
            }
        }

        public void AddWatcher(IWSATP_TO_OBATPMessageWatcher watcher)
        {
            m_syncFileWatcher.Add(watcher);
        }


        public void WSATP_TO_OBATPNewMessageInComing(Enums.OBATP_ID OBATP_ID, WSATP_TO_OBATPAdapter WSATP_TO_OBATPAdapter)
        {
            this.m_WSATP_TO_OBATPAdapter = WSATP_TO_OBATPAdapter;
            this.m_OBATP_ID = OBATP_ID;
            InformWatcher();
        }
    }
}
