using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard 
{
    public partial class OBATP : IWSATP_TO_OBATPMessageWatcher, IATS_TO_OBATO_InitMessageWatcher, IATS_TO_OBATO_MessageWatcher, IATS, IWSATC, IDisposable
    {

        //waysidedan gelen haraket yetkisi tracklerini işleyen metot
        public void WSATP_TO_OBATPMessageInComing(Enums.Train_ID train_ID, WSATP_TO_OBATPAdapter WSATP_TO_OBATPAdapter)
        {
            if (this.Vehicle.TrainID == train_ID)
            {

                if (movementTrack.Count < 20)
                {
                    List<Track> newMovementTracks = WSATP_TO_OBATPAdapter.MovementAuthorityTracks.Except(movementTrack).ToList();

                    //List<Track> newMovementTracks = movementTrack.Except(WSATP_TO_OBATPAdapter.MovementAuthorityTracks).ToList();


                    //newMovementTracks.Reverse();

                    for (int i = 0; i < newMovementTracks.Count; i++)
                    {
                        if (movementTrack.Count < 20)
                            movementTrack.Add(newMovementTracks[i]);
                        else
                            break;
                    }



                    if (newMovementTracks.Count > 0)
                    {
                        MainForm.m_trainObserver.TrainNewMovementAuthorityCreated(movementTrack);
                    }

                        //total = movementTrack.Sum(x => x.Track_Length);


                        //if (newMovementTracks.Count > 0)
                        //{
                        //    ActualFrontOfTrainCurrent.Track = movementTrack.First();
                        //    ActualRearOfTrainCurrent.Track = movementTrack.First();


                        //    int stoppingPoint;

                        //    Direction = FindDirection(movementTrack, Direction);

                        //    if (Direction == Enums.Direction.Right)
                        //    {
                        //        ActualFrontOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2;
                        //        ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location - this.Vehicle.TrainLength;
                        //    }
                        //    else
                        //    {
                        //        ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Position_1);
                        //        ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location + this.Vehicle.TrainLength;
                        //    }


                        //    //this.RequestStartProcess();
                        //}
                        //else if (WSATP_TO_OBATPAdapter.MovementAuthorityTracks.Count == 1)
                        //{
                        //    movementTrack.Clear();
                        //}


                    }






                //SetStart(movementTrack.First().Track_ID, true);



                //m_route = Route.CreateNewRoute(m_route.Route_Tracks[0].Track_ID, m_route.Route_Tracks[m_route.Route_Tracks.Count - 1].Track_ID, MainForm.m_mf.m_allTracks);

                //ikisi de kullanılabilir henüz karar vermedin hangisini kullanmalı

                //if(m_route.Route_Tracks.Count < 20)
                //{
                //    List<Track> sdfsdfsdf = WSATP_TO_OBATPAdapter.MovementAuthorityTracks.Except(m_route.Route_Tracks).ToList();

                //} 

                //bool isSame = m_route.Route_Tracks.SequenceEqual(WSATP_TO_OBATPAdapter.MovementAuthorityTracks);

                //if(!isSame)
                //{
                //    List<Track> ahmetabi = m_route.Route_Tracks.Union(WSATP_TO_OBATPAdapter.MovementAuthorityTracks).ToList();

                //    m_route.Route_Tracks.Clear();


                //    m_route.Route_Tracks.AddRange(ahmetabi);

                //}





            }
        }
    }
}
