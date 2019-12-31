using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    class Oldie
    {
        //public Enums.Direction FindDirection(List<Track> routeTracks, Enums.Direction gloDirection)
        //{
        //    Enums.Direction direction;
        //    int track1 = routeTracks[0].Track_ID;
        //    int track2 = routeTracks[1].Track_ID;

        //    if (track1 < track2)
        //    {
        //        direction = Enums.Direction.One;
        //    }
        //    else
        //    {
        //        direction = Enums.Direction.Second;
        //    }

        //    if (direction != gloDirection)
        //    {
        //        //train.TemporaryLastRouteTracks = newListRouteTracks;
        //        double tempLocation = RealFrontCurrentLocation;
        //        RealFrontCurrentLocation = RealRearCurrentLocation;
        //        RealRearCurrentLocation = tempLocation;

        //        if (FrontCurrentTrack != RearCurrentTrack)
        //        {
        //            Track tempTrack = FrontCurrentTrack;
        //            FrontCurrentTrack = RearCurrentTrack;
        //            RearCurrentTrack = tempTrack;
        //        }
        //        //train.RouteTracks.Clear();
        //        //train.RouteCurrentLocation = 0;
        //    }

        //    RearDirection = direction; // sen şimdilik kal seni düşüneceğim

        //    return direction;
        //}

        /// <summary>
        /// Trenin gitmesi gereken hızı belirler.
        /// </summary>
        public void FindTargetSpeed(List<Track> routeTracks)
        {
            //// Gitme izni yoksa tren durmalı
            //if (ManageTrainDoors() || IsRouteLimitExceeded() || !IsConnectionOkayToGo)
            //{
            //    Vehicle.TargetSpeedKM = 0;
            //}

            //// Gelecek Track'leri kontrol edip Tracklere uygun hıza düşme işlemini gerçekleştirir.
            //else if (FrontTrainLocationInRoute + Vehicle.BrakingDistance >= FrontCurrentTrack.StopPositionInRoute)
            //{
            //    // Yavaşlamaya başlaması gereken konuma geldiyse tren durmalı
            //    if (IsRouteLimitExceeded())
            //    {
            //        Vehicle.TargetSpeedKM = 0;
            //    }
            //    else
            //    {
            //        // Durma mesafesini içeren önündeki tüm Track'lere bakar.
            //        for (int j = routeTracks.IndexOf(MovementAuthorityTrack); routeTracks.IndexOf(FrontCurrentTrack) < j; j--)
            //        {
            //            if (FrontTrainLocationInRoute + Vehicle.BrakingDistance >= routeTracks[j].StartPositionInRoute)
            //            {

            //                // Bir sonraki Track'e maksimum hız ile girdiğinde daha sonraki Track hızına düşebilmeyi sağlıyorsa, önceliği bir sonraki Track'e ver
            //                // Bir sonraki Track hız limiti daha sonraki Track'ten daha düşükse öncelikli olarak bir sonraki Track'e bak
            //                for (int i = routeTracks.IndexOf(FrontNextTrack); i <= routeTracks.IndexOf(routeTracks[j]); i++)
            //                {
            //                    if (routeTracks[i].MaxTrackSpeed < FrontCurrentTrack.MaxTrackSpeed)
            //                    {
            //                        if (routeTracks.IndexOf(routeTracks[i]) >= routeTracks.IndexOf(MovementAuthorityTrack))
            //                        {
            //                            break;
            //                        }
            //                        else
            //                        {
            //                            for (int m = routeTracks.IndexOf(routeTracks[j]); m > routeTracks.IndexOf(routeTracks[i]); m--)
            //                            {
            //                                //mesafe varsa
            //                                if (((routeTracks[i].MaxTrackSpeed > routeTracks[m].MaxTrackSpeed) && 
            //                                    (routeTracks[i].StartPositionInRoute + (routeTracks[i].MaxTrackSpeed * ((routeTracks[i].MaxTrackSpeed - routeTracks[m].MaxTrackSpeed) / Vehicle.MaxTrainDeceleration)) - (0.5 * (routeTracks[i].MaxTrackSpeed - routeTracks[m].MaxTrackSpeed) * ((routeTracks[i].MaxTrackSpeed - routeTracks[m].MaxTrackSpeed) / Vehicle.MaxTrainDeceleration))) < routeTracks[m].StartPositionInRoute))
            //                                {
            //                                    // Bakması gereken Track numarasını değiştirir
            //                                    j = i;
            //                                    goto End;
            //                                }
            //                                //mesafe yoksa
            //                                else if (routeTracks[i].MaxTrackSpeed > routeTracks[m].MaxTrackSpeed)
            //                                {
            //                                    j = m;
            //                                    goto End;
            //                                }
            //                            }
            //                            j = i;
            //                            break;
            //                        }


            //                        //if ((Tracks[i].MaxSpeed < CurrentTrack.MaxSpeed && Tracks[i].MaxSpeed < Tracks[i+1].MaxSpeed) || (Tracks[i].MaxSpeed < CurrentTrack.MaxSpeed) && ((Tracks[i].MaxSpeed > Tracks[i + 1].MaxSpeed) && (Tracks[i].StartMeter + (Tracks[i + 1].MaxSpeed * ((Tracks[i].MaxSpeed - Tracks[i + 1].MaxSpeed) / MaxTrainDeceleration)) + ((0.5) * MaxTrainDeceleration * Math.Pow((Tracks[i].MaxSpeed - Tracks[i + 1].MaxSpeed) / MaxTrainDeceleration, 2))) < Tracks[i + 1].StartMeter) && ((Tracks[i].MaxSpeed > Tracks[j].MaxSpeed) && (Tracks[i].StartMeter + (Tracks[j].MaxSpeed * ((Tracks[i].MaxSpeed - Tracks[j].MaxSpeed) / MaxTrainDeceleration)) + ((0.5) * MaxTrainDeceleration * Math.Pow((Tracks[i].MaxSpeed - Tracks[j].MaxSpeed) / MaxTrainDeceleration, 2))) < Tracks[j].StartMeter))
            //                        //{
            //                        //    // Bakması gereken Track numarasını değiştirir
            //                        //    j = i;
            //                        //    break;
            //                        //}
            //                    }
            //                }

            //            End:
            //                // Anlık hız gelecek dikkate alınması gereken sonraki bir Track hızından düşükse
            //                if (CurrentTrainSpeed < routeTracks[j].MaxTrackSpeed)
            //                {
            //                    // Hız farkının ivmeye bölümü, trenin hız değişimini tamamlayacağı süreyi verir.
            //                    double Time = Math.Abs(CurrentTrainSpeed - routeTracks[j].MaxTrackSpeed) / Vehicle.MaxTrainDeceleration;

            //                    // O hıza gidebileceği mesafeye geldiyse
            //                    if (routeTracks[j].StartPositionInRoute - FrontTrainLocationInRoute <= (CurrentTrainSpeed * Time) + (Math.Abs(CurrentTrainSpeed - routeTracks[j].MaxTrackSpeed) * Time / 2))
            //                    {
            //                        double minSpeed = RearCurrentTrack.MaxTrackSpeedKM;
            //                        // Tren dikkate alınması gereken sonraki bir Track'e kadar arkasından itibaren uzunluğunun da denk geldiği tüm Track hız limitlerinden en düşük olanını seçer
            //                        for (int m = routeTracks.IndexOf(RearCurrentTrack); m <= routeTracks.IndexOf(routeTracks[j]); m++)
            //                        {
            //                            if (routeTracks[m].MaxTrackSpeedKM < minSpeed)
            //                            {
            //                                minSpeed = routeTracks[m].MaxTrackSpeedKM;
            //                            }
            //                        }
            //                        Vehicle.TargetSpeedKM = minSpeed;
            //                        return;
            //                    }
            //                    else if (FrontTrainLocationInRoute - Vehicle.TrainLength >= FrontCurrentTrack.StartPositionInRoute)
            //                    {
            //                        Vehicle.TargetSpeedKM = FrontCurrentTrack.MaxTrackSpeedKM;
            //                        return;
            //                    }
            //                }
            //                // Anlık hız gelecek dikkate alınması gereken sonraki bir Track hızından yüksekse
            //                else if (CurrentTrainSpeed > routeTracks[j].MaxTrackSpeed)
            //                {
            //                    // Hız farkının ivmeye bölümü, trenin hız değişimini tamamlayacağı süreyi verir.
            //                    double Time = Math.Abs(CurrentTrainSpeed - routeTracks[j].MaxTrackSpeed) / Vehicle.MaxTrainDeceleration;

            //                    if (routeTracks[j].StartPositionInRoute - FrontTrainLocationInRoute <= (CurrentTrainSpeed * Time) - (Math.Abs(CurrentTrainSpeed - routeTracks[j].MaxTrackSpeed) * Time / 2))
            //                    {

            //                        double minSpeed = RearCurrentTrack.MaxTrackSpeedKM;
            //                        // Tren dikkate alınması gereken sonraki bir Track'e kadar arkasından itibaren uzunluğunun da denk geldiği tüm Track hız limitlerinden en düşük olanını seçer
            //                        for (int m = routeTracks.IndexOf(RearCurrentTrack); m <= routeTracks.IndexOf(routeTracks[j]); m++)
            //                        {
            //                            if (routeTracks[m].MaxTrackSpeedKM < minSpeed)
            //                            {
            //                                minSpeed = routeTracks[m].MaxTrackSpeedKM;
            //                            }
            //                        }
            //                        Vehicle.TargetSpeedKM = minSpeed;
            //                        return;
            //                    }
            //                    else if (FrontTrainLocationInRoute - Vehicle.TrainLength >= FrontCurrentTrack.StartPositionInRoute)
            //                    {
            //                        Vehicle.TargetSpeedKM = FrontCurrentTrack.MaxTrackSpeedKM;
            //                        return;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}



            // Gitme izni varsa ve başka bir koşul bulunmuyorsa bulunduğu Track hız limitine uygun devam eder.
            //else if (!ManageTrainDoors() && !IsRouteLimitExceeded())
            //{
            //    double minSpeed = RearCurrentTrack.MaxTrackSpeedKM;
            //    // Tren uzunluğunun denk geldiği tüm Track hız limitlerinden en düşük olanını seçer
            //    for (int m = routeTracks.IndexOf(RearCurrentTrack); m <= routeTracks.IndexOf(FrontCurrentTrack); m++)
            //    {
            //        if (routeTracks[m].MaxTrackSpeedKM < minSpeed)
            //        {
            //            minSpeed = routeTracks[m].MaxTrackSpeedKM;
            //        }
            //    }
            //    Vehicle.TargetSpeedKM = minSpeed;
            //    return;
            //}
        }


        public Track FindNextTrack(Track track, Enums.Direction direction, List<Track> route, List<Track> allTracks)// rota zaten bütün trackleri içermiyor mu? burayı inceleyip anlamak lazım
        {
            // Tekrar bir gözden geçir
            Track nextTrack = null;

            if (route.Count > 1)
            {
                if (track != route[route.Count - 1])
                {
                    nextTrack = route[route.IndexOf(track) + 1];
                }
                else if (track == route[route.Count - 1])
                {
                    if (direction == Enums.Direction.Right)
                    {
                        //neden sonraki tracki bütün trackler tablosundan arıyoruz?? anlamadım
                        nextTrack = allTracks.Find(x => x.Track_ID == track.Track_Connection_Exit_1);
                    }
                    else if (direction == Enums.Direction.Left)
                    {
                        //neden sonraki tracki bütün trackler tablosundan arıyoruz?? anlamadım
                        // tren geri geldiği için ama emin değilim ters gelişi sormak lazım
                        nextTrack = allTracks.Find(x => x.Track_ID == track.Track_Connection_Entry_1);


                    }
                }
            }
            else
            {
                ////şimdilik kal
                //FrontOfTrainNextTrack = null;
                //RearOfTrainNextTrack = null;
            }

            return nextTrack;

        }
    }
}
