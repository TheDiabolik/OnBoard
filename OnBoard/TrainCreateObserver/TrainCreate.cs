using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public class TrainCreate : ITrainCreate
    {
        private ThreadSafeList<ITrainCreateWatcher> m_syncFileWatcher = new ThreadSafeList<ITrainCreateWatcher>();
        private OBATP m_OBATP;


        public void InformWatcher()
        {
            foreach (ITrainCreateWatcher watcher in m_syncFileWatcher)
            {
                watcher.TrainCreated(m_OBATP); 
            }
        }


        public void AddWatcher(ITrainCreateWatcher watcher)
        {
            m_syncFileWatcher.Add(watcher);
        }

        public void TrainCreated(OBATP OBATP)
        {
            this.m_OBATP = OBATP;
            InformWatcher();
        }
    }
}
