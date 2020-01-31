using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoard 
{
    public partial class OBATP : IWSATP_TO_OBATPMessageWatcher, IATS_TO_OBATO_InitMessageWatcher, IATS_TO_OBATO_MessageWatcher, IATS, IWSATC, IDisposable
    {


        public void ManageOBATCTimer()
        {
            if (!m_controlOBATCTimer)
            {
                m_controlOBATCTimer = true;

                if (m_OBATCTimer != null)
                    m_OBATCTimer.Dispose();

                //this.STTimer = new System.Threading.Timer(DeleteInValidImages, null, 1000, System.Threading.Timeout.Infinite);
                this.m_OBATCTimer = new System.Threading.Timer(StartProcess, null, 0, m_settings.OBATCWorkingCycle);
                //this.m_UIRefreshTimer = new System.Threading.Timer(UIRefresh, this, 0, m_settings.UIRefreshWorkingCycle);
                //this.m_UIRefreshTimer = new System.Threading.Timer(UIRefresh, null, 0, 1000);
            }
        }



        public void ManageMessageSendTimer()
        {
            if (!m_controlMessageSendTimer)
            {
                m_controlMessageSendTimer = true;

                if (m_messageSendTimer != null)
                    m_messageSendTimer.Dispose();

                //this.STTimer = new System.Threading.Timer(DeleteInValidImages, null, 1000, System.Threading.Timeout.Infinite);
                this.m_messageSendTimer = new System.Threading.Timer(SendOBATCMessage, null, 0, m_settings.MessageSendWorkingCycle);
                //this.m_messageSendTimer = new System.Threading.Timer(SendOBATCMessage, this, 0, m_settings.MessageSendWorkingCycle);
            }

        }



        public void ManageUIRefreshTimer()
        {
            if (!m_controlUIRefreshTimer)
            {
                m_controlUIRefreshTimer = true;

                if (m_UIRefreshTimer != null)
                    m_UIRefreshTimer.Dispose();

                //this.STTimer = new System.Threading.Timer(DeleteInValidImages, null, 1000, System.Threading.Timeout.Infinite);
                this.m_UIRefreshTimer = new System.Threading.Timer(UIRefresh, null, 0, m_settings.UIRefreshWorkingCycle);
                //this.m_UIRefreshTimer = new System.Threading.Timer(UIRefresh, this, 0, m_settings.UIRefreshWorkingCycle);
                //this.m_UIRefreshTimer = new System.Threading.Timer(UIRefresh, null, 0, 1000);
            } 
        }

        internal readonly object m_messageSend = new object();
        public void SendOBATCMessage(object o)
        {
            try
            {
                //lock (zozo)
                {
                    //OBATP oBATP = (OBATP)o;
                    //MainForm.m_trainObserver.TrainMovementCreated(oBATP);

                    m_messageSendTimer.Change(Timeout.Infinite, Timeout.Infinite);


                    //MainForm.m_trainObserver.TrainMovementSendMessageCreated(this);
                    OBATP oBATP = (OBATP)o;
                    MainForm.m_trainObserver.TrainMovementSendMessageCreated(oBATP);
                }
            }
            catch (ThreadInterruptedException ex)
            {
                m_controlMessageSendTimer = false;
                ManageMessageSendTimer();
                Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "SendOBATCMessage1");

            }
            catch (Exception ex)
            {
                m_controlMessageSendTimer = false;
                ManageMessageSendTimer();
                Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "SendOBATCMessage2");
            }
            finally
            {
                m_messageSendStopwatch.Restart();
            }
        }

        internal readonly object m_UIRefresh = new object();

        Stopwatch swe = new Stopwatch();

        public void UIRefresh(object o)
        {
            try
            {
                //swe.Start();

                //lock (zozo)
                {
                    //OBATP oBATP = (OBATP)o;
                    //MainForm.m_trainObserver.TrainMovementUI(oBATP, new OBATPUIAdapter(oBATP));

                    m_UIRefreshTimer.Change(Timeout.Infinite, Timeout.Infinite);

                    //MainForm.m_trainObserver.TrainMovementUIRefreshAllTrainList(this);
                    //MainForm.m_trainObserver.TrainMovementUIRefreshTracksList(this, new OBATPUIAdapter(this));

                    OBATP oBATP = (OBATP)o;
                    MainForm.m_trainObserver.TrainMovementUIRefreshAllTrainList(oBATP);
                    MainForm.m_trainObserver.TrainMovementUIRefreshTracksList(oBATP, new OBATPUIAdapter(oBATP));
                }
                

                //swe.Stop();



            }
            catch (ThreadInterruptedException ex)
            {
                m_controlUIRefreshTimer = false;
                ManageUIRefreshTimer();
                Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "UIRefresh1");

            }
            catch (Exception ex)
            {
                m_controlUIRefreshTimer = false;
                ManageUIRefreshTimer();
                Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "UIRefresh2");
            }
            finally
            {
                m_UIRefreshStopwatch.Restart();
            }
        }







        //şimdilik rotaları parametrik olarak alıyor
        //sonrasında yapının şekillenmesine göre ayarlanacak
        public Track FindNextTrack(Track track, Enums.Direction direction)// rota zaten bütün trackleri içermiyor mu? burayı inceleyip anlamak lazım
        {


            Track nextTrack = null;

            if (direction == Enums.Direction.Right)
            {

                //int index =  m_route.Route_Tracks.ToList().FindIndex(x => x == track);

                int index = movementTrack.IndexOf(track);


                //if(track == MovementAuthorityTrack)
                //{
                //    nextTrack = track;
                //}
                //else
                if (index + 1 < movementTrack.Count)
                    nextTrack = movementTrack[index + 1];
                else
                {

                    nextTrack = movementTrack[index];
                    //m_route = Route.CreateNewRoute(this.ActualRearOfTrainCurrent.Track.Track_ID, MainForm.m_mf.m_allRoute);

                    //m_route = Route.CreateNewRouteStationToStation(this.ActualRearOfTrainCurrent.Track.Track_ID, MainForm.m_mf.m_allRoute);
                    //Route newRoute = Route.CreateNewRoute(track.Track_ID, MainForm.m_mf.m_allRoute);

                }



                //nextTrack = m_route.Route_Tracks.Find(x => x.Track_ID == track.Track_Connection_Exit_1);

                //if (nextTrack == null)
                //    nextTrack = m_route.Route_Tracks.Find(x => x.Track_ID == track.Track_Connection_Exit_2);
            }
            else if (direction == Enums.Direction.Left)
            {
                //nextTrack = MainForm.m_mf.m_allTracks.Find(x => x.Track_ID == track.Track_Connection_Entry_1);


                int index = movementTrack.IndexOf(track);

                //if (track == MovementAuthorityTrack)
                //{
                //    nextTrack = track;
                //}
                //else
                if (index + 1 < movementTrack.Count)
                    nextTrack = movementTrack[index + 1];
                else
                {
                    nextTrack = movementTrack[index];
                } 

            } 


            return nextTrack;

        }


        public Enums.Direction FindDirection(ThreadSafeList<Track> routeTracks, Enums.Direction gloDirection)
        {
            Enums.Direction direction = Enums.Direction.Right;

            List<Track> tracksWithOutSwitches = routeTracks.FindAll(x => x.Track_ID.ToString().Substring(0, 1) != "6");

            if (tracksWithOutSwitches.Count >= 2)
            {
                int track1 = tracksWithOutSwitches[0].Track_ID;
                int track2 = tracksWithOutSwitches[1].Track_ID;

                if (track1 < track2)
                    direction = Enums.Direction.Right;
                else
                    direction = Enums.Direction.Left;
            }

            return direction;
        }

        public int TrackStoppingPointForDirection(Track track, Enums.Direction direction)
        {
            int stoppingPoint;

            if (direction == Enums.Direction.Right)
                stoppingPoint = track.Track_Length - track.Stopping_Point_Positon_2;
            else
                stoppingPoint = track.Stopping_Point_Position_1;

            return stoppingPoint;
        }

        /// <summary>
        /// Trenin rota sınırını durma mesafesiyle birlikte aşıp aşmadığının kontrolü yapılır.
        /// </summary>
        public Enums.Route IsRouteLimitExceeded()
        {
            //System.Threading.Timer STTimer = new System.Threading.Timer(ManageTrainDoors, null, 1000, 5000);

            #region OBATC Normalde Olması gereken
            //double den = MainForm.m_mf.m_YNK1_KIR2_YNK1.Last().StopPositionInRoute;
            #endregion

            #region OBATC Normalde Olması gereken
            double den = movementTrack.Last().StopPositionInRoute;
            #endregion


            if ((FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance < den))
            //if ((ActualFrontOfTrainCurrentLocation + Vehicle.BrakingDistance < m_route.Length))
            {
                return Enums.Route.In; //false;
            }
            else
            {
                return Enums.Route.Out;//true;
            }
        }



        public Enums.Route CanIGO()
        {
            //System.Threading.Timer STTimer = new System.Threading.Timer(ManageTrainDoors, null, 1000, 5000);

            #region OBATC Normalde Olması gereken
            //double den = MainForm.m_mf.m_YNK1_KIR2_YNK1.Last().StopPositionInRoute;
            #endregion

            #region WSATC
         
            int lastTrackID = movementTrack.Last().Track_ID;
            #endregion


            if ((ActualFrontOfTrainCurrent.Track.Track_ID == lastTrackID) && (DwellTimeFinished = true)  && 
                (ActualFrontOfTrainCurrent.Location - 40 <= ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2) &&
                 (ActualFrontOfTrainCurrent.Location + 40 >= ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2))
            //if ((ActualFrontOfTrainCurrentLocation + Vehicle.BrakingDistance < m_route.Length))
            {
                return Enums.Route.Out;//true;
            }
            else
            {
                return Enums.Route.In; //false;
                
            }
        }



        public double FindMinSpeedInTracks(Track frontTrack, Track rearTrack, List<Track> routeTracks)
        {
            //int frontTrackIndex = routeTracks.FindIndex(x => x == frontTrack);
            //int rearTrackIndex = routeTracks.FindIndex(x => x == rearTrack);

            int frontTrackIndex = routeTracks.FindIndex(x => x.Track_ID == frontTrack.Track_ID);
            int rearTrackIndex = routeTracks.FindIndex(x => x.Track_ID == rearTrack.Track_ID);

            List<Track> findMinSpeed = new List<Track>();

            //if (Direction == Enums.Direction.Left)
            //findMinSpeed = routeTracks.Where((element, index) => (index >= frontTrackIndex) && (index <= rearTrackIndex)).ToList();
            //else if (Direction == Enums.Direction.Right)
            findMinSpeed = routeTracks.Where((element, index) => (index <= frontTrackIndex) && (index >= rearTrackIndex)).ToList();


            //if (frontTrackIndex != -1 && rearTrackIndex != -1)
            //{
            //    if (frontTrackIndex >= rearTrackIndex)
            //        findMinSpeed = routeTracks.Where((element, index) => (index <= frontTrackIndex) && (index >= rearTrackIndex)).ToList();
            //    else if (frontTrackIndex <= rearTrackIndex)
            //        findMinSpeed = routeTracks.Where((element, index) => (index >= frontTrackIndex) && (index <= rearTrackIndex)).ToList();
            //}



            double minSpeed = 0;

            minSpeed = findMinSpeed.Min(x => x.MaxTrackSpeedKMH);


            return minSpeed;
        }



        //public static ushort[] FindTrackRangeInAllTracks(Track frontTrack, Track rearTrack, ThreadSafeList<Track> allTracks)
        //{
        //    ushort[] trackRangeList = new ushort[15];

        //    int frontTrackIndex = allTracks.ToList().FindIndex(x => x == frontTrack);
        //    int rearTrackIndex = allTracks.ToList().FindIndex(x => x == rearTrack);

        //    if (frontTrackIndex != -1 && rearTrackIndex != -1)
        //        trackRangeList = allTracks.Where((element, index) => (index <= frontTrackIndex) && (index >= rearTrackIndex)).Select(x => (ushort)x.Track_ID).ToList().ToArray();
        //    else if (frontTrackIndex != -1 && rearTrackIndex == -1)
        //        trackRangeList = allTracks.Where((element, index) => (index <= frontTrackIndex) && (index >= frontTrackIndex-1)).Select(x => (ushort)x.Track_ID).ToList().ToArray();

        //    return trackRangeList;
        //} 



        public void SetStart(int startTrackID, bool rou)
        {

            movementTrack = Route.CreateMovementTracksStationToStation(startTrackID, MainForm.m_mf.m_YNK1_KIR2_YNK1, MainForm.m_mf.m_YNK2_HAV2_YNK2, true);


            ActualFrontOfTrainCurrent.Track = movementTrack.First();
            ActualRearOfTrainCurrent.Track = movementTrack.First();


            int stoppingPoint;

            Direction = FindDirection(movementTrack, Direction);

            if (Direction == Enums.Direction.Right)
            {
                ActualFrontOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2;
                ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location - this.Vehicle.TrainLength;
            }
            else
            {
                ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Position_1);
                ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location + this.Vehicle.TrainLength;
            }



        }

    }
}
