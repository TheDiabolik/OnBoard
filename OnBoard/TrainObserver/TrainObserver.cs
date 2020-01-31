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
        private ThreadSafeList<ITrainMovementCreatedSendMessageWatcher> m_trainMovementCreatedWatcher = new ThreadSafeList<ITrainMovementCreatedSendMessageWatcher>();
        private ThreadSafeList<ITrainMovementRouteCreatedWatcher> m_trainMovementRouteCreatedWatcher = new ThreadSafeList<ITrainMovementRouteCreatedWatcher>();
        private ThreadSafeList<ITrainMovementUIWatcher> m_trainMovementUIWatcher = new ThreadSafeList<ITrainMovementUIWatcher>();
        private ThreadSafeList<ITrainNewMovementAuthorityCreatedWatcher> m_trainNewMovementAuthorityCreatedWatcher = new ThreadSafeList<ITrainNewMovementAuthorityCreatedWatcher>();

        private OBATP m_OBATP;
        private UIOBATP m_UIOBATP;
        private Route m_route;
        private ThreadSafeList<Track> m_newMovementAuthorityList;

        public void InformTrainCreatedWatcher()
        {
            foreach (ITrainCreatedWatcher watcher in m_trainCreatedWatcher)
            {
                watcher.TrainCreated(m_OBATP); 
            } 
        }

        public void InformTrainMovementCreatedSendMessageWatcher()
        {
            foreach (ITrainMovementCreatedSendMessageWatcher watcher in m_trainMovementCreatedWatcher)
            {
                watcher.TrainMovementCreatedSendMessage(m_OBATP);
            }
        }

        public void InformTrainMovementRouteCreatedWatcher()
        {
            foreach (ITrainMovementRouteCreatedWatcher watcher in m_trainMovementRouteCreatedWatcher)
            {
                watcher.TrainMovementRouteCreated(m_route);
            }
        }


        public void InformTrainNewMovementAuthorityCreatedWatcher()
        {
            foreach (ITrainNewMovementAuthorityCreatedWatcher watcher in m_trainNewMovementAuthorityCreatedWatcher)
            {
                watcher.TrainNewMovementAuthorityCreated(m_newMovementAuthorityList);
            }
        }


        public void InformTrainMovementUIAllTrainListWatcher()
        {
            foreach (ITrainMovementUIWatcher watcher in m_trainMovementUIWatcher)
            {
                watcher.TrainMovementUIRefreshAllTrainList(m_OBATP);
            }
        }


        public void InformTrainMovementUITracksListWatcher()
        {
            foreach (ITrainMovementUIWatcher watcher in m_trainMovementUIWatcher)
            {
                watcher.TrainMovementUIRefreshTracksList(m_OBATP, m_UIOBATP);
            }
        }

        public void DisposeTrainCreated(OBATP OBATP)
        {
            //this.m_OBATP = OBATP;
            //InformTrainCreatedWatcher();

            MovementTracks.Clear();
        }

        public void TrainCreated(OBATP OBATP)
        {
            this.m_OBATP = OBATP;
            InformTrainCreatedWatcher();
        }
        public void TrainMovementSendMessageCreated(OBATP OBATP)
        {
            //lock(OBATP)
            {
                this.m_OBATP = OBATP;
                InformTrainMovementCreatedSendMessageWatcher();
            }
          
        }

        public void TrainMovementRouteCreated(Route route)
        {
            this.m_route = route;
            InformTrainMovementRouteCreatedWatcher();
        }

        
        public void TrainNewMovementAuthorityCreated(ThreadSafeList<Track> newMovementAuthorityList)
        {
            this.m_newMovementAuthorityList = newMovementAuthorityList;

            InformTrainNewMovementAuthorityCreatedWatcher();
        }

        public void TrainMovementUIRefreshAllTrainList(OBATP OBATP)
        {
            this.m_OBATP = OBATP;

            InformTrainMovementUIAllTrainListWatcher();
        } 





            TrainOnTracks sdf = new TrainOnTracks();

        bool zozo;
       
        public ThreadSafeList<Track> MovementTracks = new ThreadSafeList<Track>();
        public void TrainMovementUIRefreshTracksList(OBATP OBATP, UIOBATP UIOBATP)
        {
            //lock (OBATP)
            {
                if(DisplayManager.ComboBoxGetSelectedItemInvoke(MainForm.m_mf.m_comboBoxTrain) != null && OBATP.Train_Name == DisplayManager.ComboBoxGetSelectedItemInvoke(MainForm.m_mf.m_comboBoxTrain).ToString())
                {
                    using (UIOBATP adapter = new OBATPUIAdapter(OBATP))
                    {
                        bool isSameActualLocationTracks = sdf.ActualLocationTracks.SequenceEqual(adapter.TrainOnTracks.ActualLocationTracks);
                        bool isSameVirtualOccupationTracks = sdf.VirtualOccupationTracks.SequenceEqual(adapter.TrainOnTracks.VirtualOccupationTracks);
                        bool isSameFootPrintTracks = sdf.FootPrintTracks.SequenceEqual(adapter.TrainOnTracks.FootPrintTracks);
                        //bool isSameRoute_Tracks = RouteTracks.SequenceEqual(OBATP.m_route.Route_Tracks);
                        bool isSameRoute_Tracks = MovementTracks.SequenceEqual(OBATP.movementTrack);


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

                            //RouteTracks = OBATP.m_route.Route_Tracks;
                            MovementTracks = OBATP.movementTrack;
                            adapter.RefreshRouteTracks = true;
                        }


                        this.m_UIOBATP = adapter;
                    }

                     
                    this.m_OBATP = OBATP;

                    InformTrainMovementUITracksListWatcher();
                }

             
            }
          
        }



        public void AddTrainCreatedWatcher(ITrainCreatedWatcher watcher)
        {
            m_trainCreatedWatcher.Add(watcher);
        }

        public void RemoveTrainCreatedWatcher(ITrainCreatedWatcher watcher)
        {
            if (m_trainCreatedWatcher.Contains(watcher))
                m_trainCreatedWatcher.Remove(watcher);
        }




        public void AddTrainMovementCreatedSendMessageWatcher(ITrainMovementCreatedSendMessageWatcher watcher)
        {
            m_trainMovementCreatedWatcher.Add(watcher);
        }


        public void RemoveTrainMovementCreatedSendMessageWatcher(ITrainMovementCreatedSendMessageWatcher watcher)
        {
            if (m_trainMovementCreatedWatcher.Contains(watcher))
                m_trainMovementCreatedWatcher.Remove(watcher);
        }

         
        public void AddTrainNewMovementAuthorityCreatedWatcher(ITrainNewMovementAuthorityCreatedWatcher watcher)
        {
            m_trainNewMovementAuthorityCreatedWatcher.Add(watcher);
        }


        public void RemoveTrainNewMovementAuthorityCreatedWatcher(ITrainNewMovementAuthorityCreatedWatcher watcher)
        {
            if (m_trainNewMovementAuthorityCreatedWatcher.Contains(watcher))
                m_trainNewMovementAuthorityCreatedWatcher.Remove(watcher);
        }


        public void AddTrainMovementRouteCreatedWatcher(ITrainMovementRouteCreatedWatcher watcher)
        {
            m_trainMovementRouteCreatedWatcher.Add(watcher);
        }

        public void RemoveTrainMovementRouteCreatedWatcher(ITrainMovementRouteCreatedWatcher watcher)
        {
            if (m_trainMovementRouteCreatedWatcher.Contains(watcher))
                m_trainMovementRouteCreatedWatcher.Remove(watcher);
        }


        public void AddTrainMovementUIWatcher(ITrainMovementUIWatcher watcher)
        {
            m_trainMovementUIWatcher.Add(watcher);
        }


        public void RemoveTrainMovementUIWatcher(ITrainMovementUIWatcher watcher)
        {
            if (m_trainMovementUIWatcher.Contains(watcher))
                m_trainMovementUIWatcher.Remove(watcher);
        }


        //public void AddTrainMovementUITracksListWatcher(ITrainMovementUIWatcher watcher)
        //{
        //    m_trainMovementUIWatcher.Add(watcher);
        //}


        //public void RemoveTrainMovementUITracksListWatcher(ITrainMovementUIWatcher watcher)
        //{
        //    if (m_trainMovementUIWatcher.Contains(watcher))
        //        m_trainMovementUIWatcher.Remove(watcher);
        //}
    }
}
