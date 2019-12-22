using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public class TrainMovementCreate : ITrainMovementCreate
    {

        private ThreadSafeList<ITrainMovementWatcher> m_syncFileWatcher = new ThreadSafeList<ITrainMovementWatcher>();
        private OBATP m_OBATP;
        private Route m_route;
        public void InformWatcher()
        {
            foreach (ITrainMovementWatcher watcher in m_syncFileWatcher)
            {
                watcher.TrainMovementCreated(m_OBATP);
            }
        }

        public void InformWatcherForNewRoute()
        {
            foreach (ITrainMovementWatcher watcher in m_syncFileWatcher)
            {
                watcher.TrainMovementRouteCreated(m_route);
            }
        } 

        public void TrainMovementCreated(OBATP OBATP)
        {
            this.m_OBATP = OBATP;
            InformWatcher();
        }

        //public void TrainMovementRouteCreated(List<Track> route)
        //{
        //    this.m_route = route;
        //    InformWatcherForNewRoute();
        //}

        public void TrainMovementRouteCreated(Route route)
        {
            this.m_route = route;
            InformWatcherForNewRoute();
        }

        public void AddWatcher(ITrainMovementWatcher watcher)
        {
            m_syncFileWatcher.Add(watcher);
        }
    }
}
