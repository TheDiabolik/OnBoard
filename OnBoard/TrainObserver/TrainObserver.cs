using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public class TrainObserver : ITrainObserver
    {

        private ThreadSafeList<ITrainCreatedWatcher> m_trainCreatedWatcher = new ThreadSafeList<ITrainCreatedWatcher>();
        private ThreadSafeList<ITrainMovementCreatedWatcher> m_trainMovementCreatedWatcher = new ThreadSafeList<ITrainMovementCreatedWatcher>();
        private ThreadSafeList<ITrainMovementRouteCreatedWatcher> m_trainMovementRouteCreatedWatcher = new ThreadSafeList<ITrainMovementRouteCreatedWatcher>();
        private ThreadSafeList<ITrainMovementUIWatcher> m_trainMovementUIWatcher = new ThreadSafeList<ITrainMovementUIWatcher>();


        private OBATP m_OBATP;
        private UIOBATP m_UIOBATP;
        private Route m_route;


        public void InformTrainCreatedWatcher()
        {
            foreach (ITrainCreatedWatcher watcher in m_trainCreatedWatcher)
            {
                watcher.TrainCreated(m_OBATP); 
            } 
        }

        public void InformTrainMovementCreatedWatcher()
        {
            foreach (ITrainMovementCreatedWatcher watcher in m_trainMovementCreatedWatcher)
            {
                watcher.TrainMovementCreated(m_OBATP);
            }
        }

        public void InformTrainMovementRouteCreatedWatcher()
        {
            foreach (ITrainMovementRouteCreatedWatcher watcher in m_trainMovementRouteCreatedWatcher)
            {
                watcher.TrainMovementRouteCreated(m_route);
            }
        }

        public void InformTrainMovementUIWatcher()
        {
            foreach (ITrainMovementUIWatcher watcher in m_trainMovementUIWatcher)
            {
                watcher.TrainMovementUI(m_OBATP, m_UIOBATP);
            }
        }

 

        public void TrainCreated(OBATP OBATP)
        {
            this.m_OBATP = OBATP;
            InformTrainCreatedWatcher();
        }
        public void TrainMovementCreated(OBATP OBATP)
        {
            this.m_OBATP = OBATP;
            InformTrainMovementCreatedWatcher();
        }

        public void TrainMovementRouteCreated(Route route)
        {
            this.m_route = route;
            InformTrainMovementRouteCreatedWatcher();
        }


        TrainOnTracks sdf = new TrainOnTracks();

        bool zozo;
       
        public ThreadSafeList<Track> RouteTracks = new ThreadSafeList<Track>();
        public void TrainMovementUI(OBATP OBATP, UIOBATP UIOBATP)
        {
            UIOBATP adapter = new OBATPUIAdapter(OBATP);

            bool isSameActualLocationTracks = sdf.ActualLocationTracks.SequenceEqual(adapter.TrainOnTracks.ActualLocationTracks);
            bool isSameVirtualOccupationTracks = sdf.VirtualOccupationTracks.SequenceEqual(adapter.TrainOnTracks.VirtualOccupationTracks); 
            bool isSameFootPrintTracks = sdf.FootPrintTracks.SequenceEqual(adapter.TrainOnTracks.FootPrintTracks);  
            bool isSameRoute_Tracks = RouteTracks.SequenceEqual(OBATP.m_route.Route_Tracks); 


            if (isSameActualLocationTracks)
            {
                adapter.RefreshActualLocationTracks = false;
            }
            else
            {
                sdf.ActualLocationTracks = adapter.TrainOnTracks.ActualLocationTracks;
                adapter.RefreshActualLocationTracks = true;
            }


            if (isSameVirtualOccupationTracks)
            {
                adapter.RefreshVirtualOccupationTracks = false;
            }
            else
            {
                sdf.VirtualOccupationTracks = adapter.TrainOnTracks.VirtualOccupationTracks;
                adapter.RefreshVirtualOccupationTracks = true;
            }


            if (isSameFootPrintTracks)
            {
                adapter.RefreshFootPrintTracks = false;
            }
            else
            {
                sdf.FootPrintTracks = adapter.TrainOnTracks.FootPrintTracks;
                adapter.RefreshFootPrintTracks = true;
            }


            if (isSameRoute_Tracks)
            {
                adapter.RefreshRouteTracks = false;
            }
            else
            {
                RouteTracks = OBATP.m_route.Route_Tracks;
                adapter.RefreshRouteTracks = true;
            }


            this.m_UIOBATP = adapter;
            this.m_OBATP = OBATP;  

            InformTrainMovementUIWatcher();
        }



        public void AddTrainCreatedWatcher(ITrainCreatedWatcher watcher)
        {
            m_trainCreatedWatcher.Add(watcher);
        }

        public void AddTrainMovementCreatedWatcher(ITrainMovementCreatedWatcher watcher)
        {
            m_trainMovementCreatedWatcher.Add(watcher);
        }

        public void AddTrainMovementRouteCreatedWatcher(ITrainMovementRouteCreatedWatcher watcher)
        {
            m_trainMovementRouteCreatedWatcher.Add(watcher);
        }

        public void AddTrainMovementUIWatcher(ITrainMovementUIWatcher watcher)
        {
            m_trainMovementUIWatcher.Add(watcher);
        }
        
    }
}
