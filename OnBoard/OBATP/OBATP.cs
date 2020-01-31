using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnBoard
{
    [Serializable]
    public partial class OBATP : IWSATP_TO_OBATPMessageWatcher, IATS_TO_OBATO_InitMessageWatcher, IATS_TO_OBATO_MessageWatcher, IATS, IWSATC, IDisposable
    {


        public void RequestStartProcess()
        {
            this.Status = Enums.Status.Start;

            //m_shouldStop = false;


            m_settings = XMLSerialization.Singleton();
            m_settings = m_settings.DeSerialize(m_settings);



            //ManageMessageSendTimer();
            //ManageUIRefreshTimer();


            m_OBATCTimer.Change(0, m_settings.OBATCWorkingCycle);


            m_messageSendStopwatch.Start();
            m_UIRefreshStopwatch.Start();

           
            //m_messageSendTimer.Change(0, m_settings.MessageSendWorkingCycle);
            //m_UIRefreshTimer.Change(0, m_settings.UIRefreshWorkingCycle);
            //StartProcess(null);
        }


        //public void RequestStartProcess()
        //{
        //    this.Status = Enums.Status.Start;

        //    m_shouldStop = false;


        //    m_settings = XMLSerialization.Singleton();
        //    m_settings = m_settings.DeSerialize(m_settings);

        //    Thread thread = new Thread(new ParameterizedThreadStart(StartProcess));
        //    thread.IsBackground = true;
        //    thread.Start(); 

        //    //StartProcess(null);
        //}




        public void RequestStopProcess()
        {
            //m_shouldStop = true;

            this.Status = Enums.Status.Stop;


            m_OBATCTimer.Change(Timeout.Infinite, Timeout.Infinite);

            m_messageSendTimer.Change(Timeout.Infinite, Timeout.Infinite);
            m_UIRefreshTimer.Change(Timeout.Infinite, Timeout.Infinite);


            if (m_doorTimer != null)
                m_doorTimer.Stop();
        }



        //public void RequestStopProcess()
        //{
        //    m_shouldStop = true;

        //    this.Status = Enums.Status.Stop;

        //    if (m_doorTimer != null)
        //        m_doorTimer.Stop();
        //}


        internal readonly object zozo = new object();
        public void StartProcess(object o)
        {

            try
            {

                //ManageMessageSendTimer();
                //ManageUIRefreshTimer();



                //while (!m_shouldStop)
                {
                    //lock (zozo)
                    {
                        //if (Math.Round(FrontOfTrainLocationWithFootPrintInRoute) <= m_route.Length)

                        //if (FrontOfTrainNextTrack != MovementAuthorityTrack)

                        if(movementTrack.Count > 0)

                        {
                            //routeCompleted = true;


                            //Direction = FindDirection(m_route.Route_Tracks, Direction);
                            Direction = FindDirection(movementTrack, Direction);


                            //trenin arkası ve önü için bir sonraki tracki verir
                            FrontOfTrainNextTrack = FindNextTrack(ActualFrontOfTrainCurrent.Track, Direction);
                            RearOfTrainNextTrack = FindNextTrack(ActualRearOfTrainCurrent.Track, Direction);

                            //trenin frenleme mesafesi hesaplaması
                            Vehicle.BrakingDistance = CalculateBrakingDistance(this.Vehicle.MaxTrainDeceleration, this.Vehicle.CurrentTrainSpeedCMS);


                            //trenin konumundaki hata payını bulma
                            this.FrontOfTrainLocationWithFootPrintInRoute = FindPositionFootPrintFront(ActualFrontOfTrainCurrent.Track, Direction, movementTrack) - 200;
                            //this.RearOfTrainLocationWithFootPrintInRoute = FindPositionFootPrintRear(RearOfTrainCurrentTrack, RearDirection, m_route.Route_Tracks);
                            this.RearOfTrainLocationWithFootPrintInRoute = FindPositionFootPrintRear(ActualRearOfTrainCurrent.Track, Direction, movementTrack);

                            //sanal meşguliyet hesaplama
                            this.FrontOfTrainVirtualOccupation = FindFrontVirtualOccupation(movementTrack, Direction);
                            //this.RearOfTrainVirtualOccupation = FindRearVirtualOccupation(m_route.Route_Tracks, RearDirection, MainForm.allTracks);
                            this.RearOfTrainVirtualOccupation = FindRearVirtualOccupation(movementTrack, Direction);


                            double? isTargetSpeed = FindTargetSpeed(movementTrack.ToList(), ActualFrontOfTrainCurrent.Track, ActualRearOfTrainCurrent.Track, this.Vehicle);

                            if (isTargetSpeed.HasValue)
                                this.Vehicle.TargetSpeedKMH = isTargetSpeed.Value;
                            //else
                            //    this.Vehicle.TargetSpeedKMH = 0;


                            this.Vehicle.CurrentAcceleration = ManageAcceleration(this.Vehicle.CurrentTrainSpeedCMS, this.Vehicle, ActualFrontOfTrainCurrent.Track.MaxTrackSpeedCMS);

                            this.Vehicle.CurrentTrainSpeedCMS = CalculateSpeed(this.Vehicle);

                            this.ActualFrontOfTrainCurrent = CalculateLocationInTrack(ActualFrontOfTrainCurrent.Track, FrontOfTrainNextTrack, Direction, Vehicle, this.ActualFrontOfTrainCurrent.Location);

                            //Tuple<double, Track> rearCurrent = CalculateLocation(RearOfTrainCurrentTrack, RearOfTrainNextTrack, RearDirection, Vehicle, ActualRearOfTrainCurrentLocation);
                            this.ActualRearOfTrainCurrent = CalculateLocationInTrack(ActualRearOfTrainCurrent.Track, RearOfTrainNextTrack, Direction, Vehicle, this.ActualRearOfTrainCurrent.Location);//ActualRearOfTrainCurrentLocation);




                            #region WSATC
                            //WSATC method
                            CheckBerthingStatus();
                            CheckTrainAbsoluteZeroSpeed();
                            #endregion



                            //hareket listesinden silmek için deneme yapıldığı kısım burada başlıyor
                            //DenemeRearOfTrainCurrentTrack = ActualRearOfTrainCurrent.Track;






                            //MainForm.m_trainObserver.TrainMovementUI(this, new OBATPUIAdapter(this));


                            //Thread.Sleep( m_settings.OBATCWorkingCycle);
                        }




                        if (m_messageSendStopwatch.Elapsed.TotalMilliseconds >= m_settings.MessageSendWorkingCycle)
                            m_messageSendTimer.Change(0, Timeout.Infinite);


                        if (m_UIRefreshStopwatch.Elapsed.TotalMilliseconds >= m_settings.UIRefreshWorkingCycle)
                            m_UIRefreshTimer.Change(0, Timeout.Infinite);




                        //m_messageSendTimer.Change(0, Timeout.Infinite);


                        //MainForm.m_trainObserver.TrainMovementSendMessageCreated(this);

                        //ui göstermek için ui propertyleri burada set ediliyor
                        //bu kısım vakit olduğunda farklı bir mekanizma ile tekrar yazılacak

                        this.Speed = Vehicle.CurrentTrainSpeedKMH.ToString();
                        this.Front_Track_ID = ActualFrontOfTrainCurrent.Track.Track_ID.ToString();
                        this.Front_Track_Location = ActualFrontOfTrainCurrent.Location.ToString("0.##");
                        this.Front_Track_Length = ActualFrontOfTrainCurrent.Track.Track_Length.ToString();
                        this.Front_Track_Max_Speed = ActualFrontOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString();
                        this.Rear_Track_ID = ActualRearOfTrainCurrent.Track.Track_ID.ToString();
                        this.Rear_Track_Location = ActualRearOfTrainCurrent.Location.ToString("0.##");
                        this.Rear_Track_Length = ActualRearOfTrainCurrent.Track.Track_Length.ToString();
                        this.Rear_Track_Max_Speed = ActualRearOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString();
                        this.Total_Route_Distance = TotalTrainDistance.ToString("0.##");

                    }


                }
            }
            catch (ThreadInterruptedException ex)
            {
                m_controlOBATCTimer = false;
                ManageOBATCTimer();
                //Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "OBATCTimer1");

            }
            catch (Exception ex)
            {
                m_controlOBATCTimer = false;
                ManageOBATCTimer();
                //Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "OBATCTimer2");
            }
        } 


     
      


        //bu metotun özelliği hem yeni trackı hem konumunu bulması
        public double FindPositionFootPrintFront(Track track, Enums.Direction direction, ThreadSafeList<Track> route)
        {
            double frontTrainLocationInRouteWithFootPrint = -1;

            // FrontTrainLocationFault değeri belirli aralıklarla artacak hata payı ortaya çıkacak, RFID tag okununca sıfırlanacak 
            if (route.Count != 0)
            {
                if (direction == Enums.Direction.Right)
                {
                    // Kendi Track'ini aştığında 
                    //yeni tracke geçtiğinde
                    if (ActualFrontOfTrainCurrent.Location + FrontOfTrainLocationFault >= track.Track_Length)
                    {
                        //FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrent.Location + FrontOfTrainLocationFault - track.Track_End_Position;
                        FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrent.Location + FrontOfTrainLocationFault - track.Track_Length; 
                        FrontOfTrainTrackWithFootPrint.Track = FrontOfTrainNextTrack;
                    }
                    else
                    {
                        //içinde bulunduğu trackte ilerliyorsa
                        FrontOfTrainTrackWithFootPrint.Track = track;
                        //böle değil
                        // FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrentLocation + FrontOfTrainLocationFault;
                        //böle olmalısın
                        FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrent.Location + FrontOfTrainLocationFault;// + track.Track_Start_Position;

                    } 

                    //burası incelenecek çünkü başlangıç pozisyonları sıfırdan başlamıyor
                    frontTrainLocationInRouteWithFootPrint = FrontOfTrainTrackWithFootPrint.Track.StartPositionInRoute + FrontOfTrainTrackWithFootPrint.Location;



                   //FootPrintInTrackFrontOfTrainLocation = ActualFrontOfTrainCurrent.Location + FrontOfTrainLocationFault;

                    //frontTrainLocationInRouteWithFootPrint =  FrontOfTrainTrackWithFootPrint.Location;
                }
                else if (direction == Enums.Direction.Left)
                {
                    // Kendi Track'ini aşmışsa
                    //if (ActualFrontOfTrainCurrent.Location - FrontOfTrainLocationFault <= track.Track_Start_Position)
                    if (ActualFrontOfTrainCurrent.Location - FrontOfTrainLocationFault <= 0)
                    {
                        FrontOfTrainTrackWithFootPrint.Track = FrontOfTrainNextTrack;
                        FrontOfTrainTrackWithFootPrint.Location  = FrontOfTrainTrackWithFootPrint.Track.Track_Length + (ActualFrontOfTrainCurrent.Location - FrontOfTrainLocationFault);
                    }
                    else
                    {
                        FrontOfTrainTrackWithFootPrint.Track = track;
                        FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrent.Location - FrontOfTrainLocationFault;
                    }

                    //burası incelenecek çünkü başlangıç pozisyonları sıfırdan başlamıyor
                    //frontTrainLocationInRouteWithFootPrint = FrontOfTrainTrackWithFootPrint.Track.StartPositionInRoute + FrontOfTrainTrackWithFootPrint.Track.Track_End_Position - FrontOfTrainTrackWithFootPrint.Location;

                    frontTrainLocationInRouteWithFootPrint = FrontOfTrainTrackWithFootPrint.Track.StartPositionInRoute + FrontOfTrainTrackWithFootPrint.Track.Track_Length - FrontOfTrainTrackWithFootPrint.Location;
                }
            }

            return frontTrainLocationInRouteWithFootPrint;

        }

        public double FindPositionFootPrintRear(Track track, Enums.Direction direction, ThreadSafeList<Track> route) //bu incelenecek
        {
            double rearTrainLocationInRouteWithFootPrint = 0;
            // FrontTrainLocationFault değeri belirli aralıklarla artacak hata payı ortaya çıkacak, RFID tag okununca sıfırlanacak

            if (route.Count != 0)
            {
                if (direction == Enums.Direction.Right)
                {

                    if (ActualRearOfTrainCurrent.Location - RearOfTrainLocationFault > track.Track_End_Position)
                    {
                        // Track rearFootPrintTrack = route.Find(x => x == track);

                        Track rearFootPrintTrack = route.Find(x => x == track);


                        if (rearFootPrintTrack != null)
                        {
                            RearOfTrainTrackWithFootPrint.Track = rearFootPrintTrack;
                            RearOfTrainTrackWithFootPrint.Location = RearOfTrainTrackWithFootPrint.Track.Track_End_Position + (ActualRearOfTrainCurrent.Location - RearOfTrainLocationFault);
                            //RearOfTrainTrackWithFootPrint.Location = RearOfTrainTrackWithFootPrint.Track.Track_End_Position + (ActualRearOfTrainCurrent.Location - RearOfTrainLocationFault);
                        }
                    }
                    else
                    {
                        RearOfTrainTrackWithFootPrint.Track = track;
                        //RearOfTrainTrackWithFootPrint.Location = ActualRearOfTrainCurrent.Location - RearOfTrainLocationFault;
                        rearTrainLocationInRouteWithFootPrint = ActualRearOfTrainCurrent.Location - RearOfTrainLocationFault;
                    }
                 


                    //burası
                    rearTrainLocationInRouteWithFootPrint = RearOfTrainTrackWithFootPrint.Track.StartPositionInRoute + RearOfTrainTrackWithFootPrint.Location;


                    //if (ActualRearCurrentLocation - RearOfTrainLocationFault < track.Track_Start_Position)
                    ////if (ActualRearCurrentLocation - RearOfTrainLocationFault < track.StartPositionInRoute)
                    //{
                    //    Track rearFootPrintTrack = route.Find(x => x == track);

                    //    if (rearFootPrintTrack != null)
                    //    {
                    //        RearOfTrainTrackWithFootPrint.Track = rearFootPrintTrack;
                    //        RearOfTrainTrackWithFootPrint.Location = RearOfTrainTrackWithFootPrint.Track.Track_End_Position + (ActualRearCurrentLocation - RearOfTrainLocationFault);
                    //    }
                    //}
                    //else
                    //{
                    //    RearOfTrainTrackWithFootPrint.Track = track;
                    //    rearTrainLocationInRouteWithFootPrint = ActualRearCurrentLocation - RearOfTrainLocationFault;
                    //}
                }
                else if (direction == Enums.Direction.Left)
                {
                    //if (ActualRearOfTrainCurrent.Location + RearOfTrainLocationFault > track.Track_End_Position)
                    if (((ActualRearOfTrainCurrent.Location + RearOfTrainLocationFault) ) > track.Track_Length)
                    { 
                        Track rearFootPrintTrack = route.Find(x => x == track);

                        if (rearFootPrintTrack != null)
                        {
                            rearTrainLocationInRouteWithFootPrint = (ActualRearOfTrainCurrent.Location + RearOfTrainLocationFault) - track.Track_End_Position;
                            RearOfTrainTrackWithFootPrint.Track = rearFootPrintTrack;
                        }
                    }
                    else
                    {
                        RearOfTrainTrackWithFootPrint.Track = track;
                        RearOfTrainTrackWithFootPrint.Location = ActualRearOfTrainCurrent.Location + RearOfTrainLocationFault;
                        rearTrainLocationInRouteWithFootPrint = ActualRearOfTrainCurrent.Location + RearOfTrainLocationFault;
                    }
                }
            }

            return rearTrainLocationInRouteWithFootPrint;
        }



        /// <summary>
        /// Trenin sanal meşguliyet konumunu hesaplar.
        /// </summary>
        public TrackWithPosition FindFrontVirtualOccupation(ThreadSafeList<Track> routeTracks, Enums.Direction direction)
        {
            TrackWithPosition frontOfTrainVirtualOccupation = new TrackWithPosition();


            if (routeTracks.Count != 0)
            {
                if (direction == Enums.Direction.Right)
                {
                    double frontOfTrainVirtualOcc = this.ActualFrontOfTrainCurrent.Location + this.FrontOfTrainLocationFault + this.Vehicle.BrakingDistance;

                    //tren başladığı trackin içindeyse
                    //if (frontOfTrainVirtualOccupation <= FrontOfTrainCurrentTrack.Track_End_Position)
                    if (frontOfTrainVirtualOcc <= ActualFrontOfTrainCurrent.Track.Track_Length)
                    {
                        frontOfTrainVirtualOccupation.Track = ActualFrontOfTrainCurrent.Track;
                        frontOfTrainVirtualOccupation.Location = frontOfTrainVirtualOcc;
                    }
                    else //trenin sanal meşguliyetli uzunluk bilgisi ile yeni tracke geçiyor
                    {

                        //double distanceToFinishCurrentTrack = FrontOfTrainCurrentTrack.Track_End_Position - ActualFrontOfTrainCurrentLocation;
                        double distanceToFinishCurrentTrack = ActualFrontOfTrainCurrent.Track.Track_Length - ActualFrontOfTrainCurrent.Location;
                        double totalMovementDistance = FrontOfTrainNextTrack.Track_Length + distanceToFinishCurrentTrack;
                        double breakingDistanceWithFault = FrontOfTrainLocationFault + Vehicle.BrakingDistance;

                        //hata payı olmadan gidilmesi gereken toplam mesafe DEN hata payı ekli fren mesafesi büyük mü? yani tren bu anda frene bassa durmaz..
                        //frenleyince durma mesafesi kalan mesafeden büyükse
                        if (breakingDistanceWithFault >= totalMovementDistance)
                        {
                            //double de = virtualOccupationFrontTrackLocation - CurrentTrack.TrackEndPosition - NextTrack.TrackEndPosition;
                            //double totalMovementDistanceWithVirtualOccupation = frontOfTrainVirtualOccupation - (FrontOfTrainCurrentTrack.Track_End_Position + FrontOfTrainNextTrack.Track_End_Position);
                            double totalMovementDistanceWithVirtualOccupation = frontOfTrainVirtualOcc - (ActualFrontOfTrainCurrent.Track.Track_Length + FrontOfTrainNextTrack.Track_Length);

                            //trenin sanal meşguliyet uzunluğu kalan tracklerim mesadesinden fazlaysa
                            //trenin gidebileceği kalan mesafe
                            if (totalMovementDistanceWithVirtualOccupation >= 0)
                                frontOfTrainVirtualOccupation.Location = totalMovementDistanceWithVirtualOccupation;
                            else
                                frontOfTrainVirtualOccupation.Location = 0;

                            //frenleme mesafesi current ve nexttrack toplamından daha büyükse nexttrackten bir sonraki tracki next track olarak atıyoruz


                            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            //int nextTrackID = FrontOfTrainNextTrack.Track_Connection_Exit_1;
                            //frontOfTrainVirtualOccupation.Track = MainForm.m_mf.m_allTracks.Find(x => x.Track_ID == nextTrackID);
                            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


                            Track nextOne = FindNextTrack(FrontOfTrainNextTrack, Direction);
                            frontOfTrainVirtualOccupation.Track = nextOne;

                        }
                        else
                        {
                            //double passingDistanceToCurrentTrack = frontOfTrainVirtualOccupation - FrontOfTrainCurrentTrack.Track_End_Position;
                            double passingDistanceToCurrentTrack = frontOfTrainVirtualOcc - ActualFrontOfTrainCurrent.Track.Track_Length;

                            //tren currenttracki ne kadar geçti
                            if (passingDistanceToCurrentTrack >= 0)
                                frontOfTrainVirtualOccupation.Location = passingDistanceToCurrentTrack; //doğal olarak currenttracki geçtiği mesafe nexttrackin içindeki yeni mesafe
                            else
                                frontOfTrainVirtualOccupation.Location = 0;

                            frontOfTrainVirtualOccupation.Track = FrontOfTrainNextTrack;
                        }

                    }
                }
                else if(direction == Enums.Direction.Left)
                {
                    double virtualOccupationFrontTrackLocation = this.ActualFrontOfTrainCurrent.Location - this.FrontOfTrainLocationFault - this.Vehicle.BrakingDistance;

                    //tren başladığı trackın içindeyse
                    //if (virtualOccupationFrontTrackLocation >= ActualFrontOfTrainCurrent.Track.Track_Start_Position)
                    if (virtualOccupationFrontTrackLocation >= 0)
                    {
                        frontOfTrainVirtualOccupation.Track = this.ActualFrontOfTrainCurrent.Track;
                        frontOfTrainVirtualOccupation.Location = virtualOccupationFrontTrackLocation;
                    }
                    else
                    {
                        double breakingDistanceWithFault = FrontOfTrainLocationFault + Vehicle.BrakingDistance;
                        double totalDistance = FrontOfTrainNextTrack.Track_Length + ActualFrontOfTrainCurrent.Location;

                        if (virtualOccupationFrontTrackLocation < 0 && (breakingDistanceWithFault >= totalDistance))
                        {
                            //frenleme mesafesi current ve nexttrack toplamından daha büyükse nexttrackten bir sonraki tracki next track olarak atıyoruz
                            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            //int nextTrackID = FrontOfTrainNextTrack.Track_Connection_Entry_1;
                            //frontOfTrainVirtualOccupation.Track = allTracks.Find(x => x.Track_ID == nextTrackID);//null kontrolü eklemek gerekebilir
                            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                            Track nextOne = FindNextTrack(FrontOfTrainNextTrack, Direction);
                            frontOfTrainVirtualOccupation.Track = nextOne;

                            //frontOfTrainVirtualOccupation.Location = frontOfTrainVirtualOccupation.Track.Track_End_Position - (breakingDistanceWithFault - totalDistance);
                            frontOfTrainVirtualOccupation.Location = frontOfTrainVirtualOccupation.Track.Track_Length - (breakingDistanceWithFault - totalDistance);
                        }
                        else
                        {
                            double passingDistanceToCurrentTrack = ActualFrontOfTrainCurrent.Track.Track_Length + virtualOccupationFrontTrackLocation;

                            frontOfTrainVirtualOccupation.Track = FrontOfTrainNextTrack;
                            frontOfTrainVirtualOccupation.Location = passingDistanceToCurrentTrack;

                            //frontOfTrainVirtualOccupation.Location = frontOfTrainVirtualOccupation.Track.Track_End_Position + virtualOccupationFrontTrackLocation;
                        }
                    }

                }


            }


            return frontOfTrainVirtualOccupation;


        }



        /// <summary>
        /// Trenin sanal meşguliyet konumunu hesaplar.
        /// </summary>
        public TrackWithPosition FindRearVirtualOccupation(ThreadSafeList<Track> routeTracks, Enums.Direction rearDirection)
        {
            TrackWithPosition rearOfTrainVirtualOccupation = new TrackWithPosition();


            if (routeTracks.Count != 0)
            {
                int rearCurrentTrackIndex = routeTracks.IndexOf(ActualRearOfTrainCurrent.Track);

                //int rearCurrentTrackIndex = allTracks.IndexOf(ActualRearOfTrainCurrent.Track);


                if (rearDirection == Enums.Direction.Right)
                {
                    double realRearCurrentLocation = this.ActualRearOfTrainCurrent.Location - this.RearOfTrainLocationFault;

                    //int sddsf = RouteTracks.IndexOf(RearCurrentTrack); 
                    //Track sddsfsdsd = RouteTracks.Find(x => x == RearCurrentTrack); 

                    //if (realRearCurrentLocation < ActualRearOfTrainCurrent.Track.Track_Start_Position)
                    //if (realRearCurrentLocation < RearOfTrainCurrentTrack.StopPositionInRoute)
                    if (realRearCurrentLocation > ActualRearOfTrainCurrent.Track.Track_Length)
                    {
                        if (rearCurrentTrackIndex > 0)//trenin arkası ikinci trackte ise
                        {
                            rearOfTrainVirtualOccupation.Track = routeTracks[rearCurrentTrackIndex - 1];

                            //rearOfTrainVirtualOccupation.Track = allTracks[rearCurrentTrackIndex - 1];
                            rearOfTrainVirtualOccupation.Location = rearOfTrainVirtualOccupation.Track.Track_End_Position + realRearCurrentLocation;
                        }
                    }
                    else
                    {
                        rearOfTrainVirtualOccupation.Track = ActualRearOfTrainCurrent.Track;
                        rearOfTrainVirtualOccupation.Location = realRearCurrentLocation;
                    }
                }
                else if (rearDirection == Enums.Direction.Left)
                {
                    double rearCurrentLocationWithFault = this.ActualRearOfTrainCurrent.Location + this.RearOfTrainLocationFault;

                    //if (rearCurrentLocationWithFault >= ActualRearOfTrainCurrent.Track.Track_End_Position)
                    if (rearCurrentLocationWithFault >= ActualRearOfTrainCurrent.Track.Track_Length)
                    {
                        if (rearCurrentTrackIndex > 0)
                        {
                            rearOfTrainVirtualOccupation.Location = rearCurrentLocationWithFault - ActualRearOfTrainCurrent.Track.Track_End_Position;
                            rearOfTrainVirtualOccupation.Track = routeTracks[rearCurrentTrackIndex - 1];
                        }

                    }
                    else
                    {
                        rearOfTrainVirtualOccupation.Track = ActualRearOfTrainCurrent.Track;
                        rearOfTrainVirtualOccupation.Location = rearCurrentLocationWithFault;
                    }
                }
            }

            return rearOfTrainVirtualOccupation;
        }


     


        /// <summary>
        /// Trenin gitmesi gereken hızı belirler.
        /// </summary>
        public double? FindTargetSpeed(List<Track> routeTracks, Track  frontCurrentTrack, Track rearCurrentTrack, Vehicle vehicle)
        {
            double? targetSpeedKM = null;

            bool IsConnectionOkayToGo = true;
            DoorStatus = ManageTrainDoors();

            Enums.Route isExceed = IsRouteLimitExceeded();


            Enums.Route cani = CanIGO();

            // Gitme izni yoksa tren durmalı
            //if (isExceed == Enums.Route.Out || DoorStatus == Enums.DoorStatus.Open || !string.IsNullOrEmpty(frontCurrentTrack.Station_Name) || (!string.IsNullOrEmpty(frontCurrentTrack.Station_Name) && frontCurrentTrack.Station_Name = && DoorStatus == Enums.DoorStatus.Close))//|| DoorStatus == Enums.DoorStatus.Open)
            //if (isExceed == Enums.Route.Out || DoorStatus==Enums.DoorStatus.Open )// || DoorStatus == Enums.DoorStatus.Open || !string.IsNullOrEmpty(FrontOfTrainCurrentTrack.Station_Name))
            // {
            //     targetSpeedKM = 0;
            // }
            if ((isExceed == Enums.Route.Out))// || (!string.IsNullOrEmpty(ActualFrontOfTrainCurrent.Track.Station_Name) && ActualFrontOfTrainCurrent.Track.Station_Name == openDoorTrackName))// || !string.IsNullOrEmpty(FrontOfTrainCurrentTrack.Station_Name))
            {
                targetSpeedKM = 0;
            }
            //else if ((cani == Enums.Route.Out))// || (!string.IsNullOrEmpty(ActualFrontOfTrainCurrent.Track.Station_Name) && ActualFrontOfTrainCurrent.Track.Station_Name == openDoorTrackName))// || !string.IsNullOrEmpty(FrontOfTrainCurrentTrack.Station_Name))
            //{
            //    targetSpeedKM = 0;
            //}
            else //  if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= frontCurrentTrack.StopPositionInRoute)
            {
                //if (IsRouteLimitExceeded() == Enums.Route.Out)
                //{
                //    targetSpeedKM = 0;
                //}
                //else
                //{
                //int frontCurrentTrackIndex = movementTrack.IndexOf(frontCurrentTrack);

                int frontCurrentTrackIndex = movementTrack.ToList().FindIndex(x => x.Track_ID == frontCurrentTrack.Track_ID);
                //int frontCurrentTrackIndex = m_route.Route_Tracks.ToList().FindIndex(x => x.Track_ID == frontCurrentTrack.Track_ID);
                int remainingTracks = movementTrack.Count - frontCurrentTrackIndex;


                //int de = m_route.Route_Tracks.ToList().FindIndex(x => x.Track_ID == MovementAuthorityTrack.Track_ID);

                //List<Track> restOfMovementTracks = m_route.Route_Tracks.ToList().GetRange(frontCurrentTrackIndex, remainingTracks);

                //var dsf = m_route.Route_Tracks...Take( While(x => x < 10).ToList();

                List<Track> restOfMovementTracks = movementTrack.ToList().GetRange(frontCurrentTrackIndex, remainingTracks);

                //List<Track> restOfMovementTracks = routeTracks.Where((element, index) => (index >= frontCurrentTrackIndex) && (index <= de)).ToList();

                Stack<Track> reverseRestOfMovementTracks = new Stack<Track>(restOfMovementTracks.ToList());

                //if (restOfMovementTracks.Count == 0)
                //    return targetSpeedKM;



                foreach (Track item in restOfMovementTracks)
                    {

                    #region WSATC Movement Speed Arrange




                    #endregion



                    //frenleme mesafesi frontcurrenttrackin konumu geçiyorsa
                    //if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= item.StopPositionInRoute) //item.StartPositionInRoute)
                    {

                        do
                        {
                            Track reverseTracks = reverseRestOfMovementTracks.Pop();

                            double tempSpeed;


                            if (item.MaxTrackSpeedCMS < vehicle.CurrentTrainSpeedCMS)
                                tempSpeed = item.MaxTrackSpeedCMS;
                            else
                                tempSpeed = vehicle.CurrentTrainSpeedCMS;


                            //double differenceOfMaxTrackSpeed = (tempSpeed - reverseTracks.MaxTrackSpeedCMS);


                            double differenceOfMaxTrackSpeed = Math.Abs(tempSpeed - reverseTracks.MaxTrackSpeedCMS);
                            double timeToDifferenceOfMaxTrackSpeed = (differenceOfMaxTrackSpeed / vehicle.MaxTrainDeceleration);
                            double distanceToArrangeTheSpeed = (tempSpeed * timeToDifferenceOfMaxTrackSpeed);

                            //openDoorTrack != FrontOfTrainCurrentTrack.Station_Name


                            #region duraklarda bağımsız yavaşla için yapılan değişiklik
                            int stoppingPoint = TrackStoppingPointForDirection(item, Direction);


                            if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= (item.StopPositionInRoute - stoppingPoint))
                            //if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= (item.StopPositionInRoute - item.Stopping_Point_Position_1))

                            {
                                if (!string.IsNullOrEmpty(item.Station_Name) && item.Station_Name != openDoorTrackName)

                                {

                                    double ahme = this.ActualFrontOfTrainCurrent.Track.Track_Start_Position + this.ActualFrontOfTrainCurrent.Location;

                                    //if (this.ActualFrontOfTrainCurrent.Location >= item.Track_Length   -item.Stopping_Point_Position_1)
                                    {
                                        targetSpeedKM = 0;


                                        //break;
                                        return targetSpeedKM;
                                    }


                                }
                            } 



                            #endregion



                            ////iki track arasındaki hız farkı için toplam alınan mesafe
                            double distanceToSpeedDifference = (differenceOfMaxTrackSpeed) * (timeToDifferenceOfMaxTrackSpeed);

                            ////double vehicledistanceToSpeedDifference = (vehicledifferenceOfMaxTrackSpeed) * (vehicletimeToDifferenceOfMaxTrackSpeed);
                            ///


                            if ((reverseRestOfMovementTracks.Count == 0) && (rearCurrentTrack.Track_ID == movementTrack.Last().Track_ID))
                            {
                                if ((tempSpeed < reverseTracks.MaxTrackSpeedCMS) &&
                              (((FrontOfTrainLocationWithFootPrintInRoute + distanceToArrangeTheSpeed) - (0.5 * distanceToSpeedDifference)) < reverseTracks.StopPositionInRoute))
                                {
                                    targetSpeedKM = 0;

                                    return targetSpeedKM;
                                }
                            }






                            if ((tempSpeed > reverseTracks.MaxTrackSpeedCMS) &&
                                (((FrontOfTrainLocationWithFootPrintInRoute + distanceToArrangeTheSpeed) - (0.5 * distanceToSpeedDifference)) < reverseTracks.StartPositionInRoute))
                            {


                            }
                            //mesafe yoksa
                            else if (tempSpeed > reverseTracks.MaxTrackSpeedCMS)
                            {


                                targetSpeedKM = reverseTracks.MaxTrackSpeedKMH;



                                //double mesafecm = ters.StartPositionInRoute - FrontOfTrainLocationWithFootPrintInRoute;  
                                //double acc = (ters.MaxTrackSpeedCMS - Math.Pow(vehicle.CurrentTrainSpeedCMS, 2)) / (2 * mesafecm);

                                //targetSpeedKM = reverseTracks.MaxTrackSpeedKMH;


                                //break;
                                return targetSpeedKM;

                            }


                        } while (reverseRestOfMovementTracks.Count > 0);


                            if (item == frontCurrentTrack)
                            {

                            //int stopPosition;

                            //if (Direction == Enums.Direction.Right)
                            //    stopPosition = item.Stopping_Point_Positon_2;
                            //else
                            //    stopPosition = item.Stopping_Point_Position_1;

                            //if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= (item.StopPositionInRoute - stopPosition))

                            #region duraklarda bağımsız yavaşla için yapılan değişiklik

                            //int stoppingPoint = TrackStoppingPointForDirection(item, Direction);


                            //if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= (item.StopPositionInRoute - stoppingPoint))
                            ////if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= (item.StopPositionInRoute - item.Stopping_Point_Position_1))

                            //{
                            //    if (!string.IsNullOrEmpty(item.Station_Name) && item.Station_Name != openDoorTrackName)

                            //    {

                            //        double ahme = this.ActualFrontOfTrainCurrent.Track.Track_Start_Position + this.ActualFrontOfTrainCurrent.Location;

                            //        //if (this.ActualFrontOfTrainCurrent.Location >= item.Track_Length   -item.Stopping_Point_Position_1)
                            //        {
                            //            targetSpeedKM = 0;


                            //            //break;
                            //            return targetSpeedKM;
                            //        }


                            //    }
                            //}
                            #endregion

                            //burayı silme deneme maksitl
                            //if (FrontOfTrainLocationWithFootPrintInRoute  + Vehicle.BrakingDistance >= item.StopPositionInRoute )
                            //{
                            //    if (!string.IsNullOrEmpty(item.Station_Name) && item.Station_Name != openDoorTrackName)
                            //    {
                            //        targetSpeedKM = 0;


                            //        //break;
                            //        return targetSpeedKM;
                            //    }
                            //}



                            //trackın maksimum hızı ile trenin hızı arasındakı fark
                            double differenceBetweenTrainAndTrackSpeed = vehicle.CurrentTrainSpeedCMS - item.MaxTrackSpeedCMS; 
                                //trenin hızı trackın maksimum hızına çıkarmak için geçmesi gereken zaman saniye cinsinden
                                double time = Math.Abs(vehicle.CurrentTrainSpeedCMS - item.MaxTrackSpeedCMS) / vehicle.MaxTrainDeceleration; 
                                //trenin trackin maksimum hızına çıkmak için alması gereken yol santimetre cinsinden
                                double distanceToArrangeTheSpeed = vehicle.CurrentTrainSpeedCMS * time; 

                                double kimsin = distanceToArrangeTheSpeed + (Math.Abs(differenceBetweenTrainAndTrackSpeed) * time / 2);

                            // Anlık hız gelecek dikkate alınması gereken sonraki bir Track hızından düşükse
                            //trenin o anda bulunduğu hız haraket izni bulunan trackin maksimum hızından düşükse (hareket izni bulunan tracklerle sırasıyla karşılaştırıyoruz)
                            if (vehicle.CurrentTrainSpeedCMS < item.MaxTrackSpeedCMS) // burası end: ten sonraki koşul karşılaştırması
                            {


                                // O hıza gidebileceği mesafeye geldiyse
                                if (item.StartPositionInRoute - this.ActualFrontOfTrainCurrent.Location <= distanceToArrangeTheSpeed + (Math.Abs(differenceBetweenTrainAndTrackSpeed) * time / 2))
                                {
                                    //List<Track> directionTracks = new List<Track>();

                                    //if (this.Direction == Enums.Direction.Right)
                                    //    directionTracks = MainForm.m_mf.m_YNK1_KIR2;
                                    //else if (this.Direction == Enums.Direction.Left)
                                    //    directionTracks = MainForm.m_mf.m_KIR2_YNK1;


                                    //targetSpeedKM = FindMinSpeedInTracks(item, rearCurrentTrack, directionTracks);


                                    targetSpeedKM = FindMinSpeedInTracks(item, rearCurrentTrack, routeTracks);

                                    //break;
                                    return targetSpeedKM;


                                }
                                //tren önünden itibaren bulunduğu trackın içindeyse tek bir trackin içindedir; o trackın hızını al
                                else if (this.FrontOfTrainLocationWithFootPrintInRoute - vehicle.TrainLength >= item.StopPositionInRoute) //item.StartPositionInRoute)
                                {


                                    targetSpeedKM = item.MaxTrackSpeedKMH;


 

                                    return targetSpeedKM;
                                    //break;
                                    //return;
                                }
                                else if (Math.Round(FrontOfTrainLocationWithFootPrintInRoute) + Vehicle.BrakingDistance <= item.StopPositionInRoute)
                                {

                                    //List<Track> directionTracks = new List<Track>();

                                    //if (this.Direction == Enums.Direction.Right)
                                    //    directionTracks = MainForm.m_mf.m_YNK1_KIR2;
                                    //else if (this.Direction == Enums.Direction.Left)
                                    //    directionTracks = MainForm.m_mf.m_KIR2_YNK1;


                                    //targetSpeedKM = FindMinSpeedInTracks(item, rearCurrentTrack, directionTracks);


                                    targetSpeedKM = FindMinSpeedInTracks(item, rearCurrentTrack, routeTracks);


                                    //break;
                                    return targetSpeedKM;

                                }

                                //sonradan eklendi WSATC
                                //else if (Math.Round(FrontOfTrainLocationWithFootPrintInRoute) + Vehicle.BrakingDistance > item.StopPositionInRoute)
                                //{



                                //    int stoppingPoint = TrackStoppingPointForDirection(item, Enums.Direction.Right);

                                //    if ((ActualFrontOfTrainCurrent.Track.Track_ID == movementTrack.Last().Track_ID) &&
                                //           ((item.Stopping_Point_Positon_2 <= (ActualFrontOfTrainCurrent.Location))))
                                //    {
                                //        targetSpeedKM = 0;
                                //    }

                                //    //List<Track> directionTracks = new List<Track>();

                                //    //if (this.Direction == Enums.Direction.Right)
                                //    //    directionTracks = MainForm.m_mf.m_YNK1_KIR2;
                                //    //else if (this.Direction == Enums.Direction.Left)
                                //    //    directionTracks = MainForm.m_mf.m_KIR2_YNK1;


                                //    //targetSpeedKM = FindMinSpeedInTracks(item, rearCurrentTrack, directionTracks); 


                                //    //break;
                                //    return targetSpeedKM;

                                //}

                                //sonradan eklendi durunca hareket ettirmek için
                                else if (Math.Round(FrontOfTrainLocationWithFootPrintInRoute) + Vehicle.BrakingDistance > item.StopPositionInRoute)
                                {

                                    //List<Track> directionTracks = new List<Track>();

                                    //if (this.Direction == Enums.Direction.Right)
                                    //    directionTracks = MainForm.m_mf.m_YNK1_KIR2;
                                    //else if (this.Direction == Enums.Direction.Left)
                                    //    directionTracks = MainForm.m_mf.m_KIR2_YNK1;


                                    //targetSpeedKM = FindMinSpeedInTracks(item, rearCurrentTrack, directionTracks);



                                    targetSpeedKM = FindMinSpeedInTracks(item, rearCurrentTrack, routeTracks);


                                    //break;
                                    return targetSpeedKM;

                                }






                            }
                            else if (vehicle.CurrentTrainSpeedCMS > item.MaxTrackSpeedCMS) // burası end: ten sonraki koşul karşılaştırması
                            {
                                // O hıza gidebileceği mesafeye geldiyse
                                if (item.StartPositionInRoute - this.ActualFrontOfTrainCurrent.Location <= distanceToArrangeTheSpeed - (Math.Abs(differenceBetweenTrainAndTrackSpeed) * time / 2))
                                {

                               
                                    targetSpeedKM = FindMinSpeedInTracks(item, rearCurrentTrack, routeTracks);
                                    //break;

                                    return targetSpeedKM;


                                }
                                //tren önünden itibaren bulunduğu trackın içindeyse tek bir trackin içindedir; o trackın hızını al
                                else if (this.FrontOfTrainLocationWithFootPrintInRoute - vehicle.TrainLength >= item.StartPositionInRoute)
                                {
                                   
                                    targetSpeedKM = item.MaxTrackSpeedKMH;

 
                                    return targetSpeedKM;
                                    //break;
                                    //return;
                                }
                                else if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance <= item.StopPositionInRoute)
                                {
                                    targetSpeedKM = FindMinSpeedInTracks(item, rearCurrentTrack, routeTracks);


                                    //break;
                                    return targetSpeedKM;

                                }

                            }


                                break;

                            }



                            if (reverseRestOfMovementTracks.Count == 0)
                            {
                                List<Track> tempReverseRestOfMovementTracks = restOfMovementTracks.ToList();
                                tempReverseRestOfMovementTracks.Remove(item);
                                reverseRestOfMovementTracks = new Stack<Track>(tempReverseRestOfMovementTracks);


                            }
                        } 

                    }  
                     
                }

            //}
            //else if (isExceed == Enums.Route.In && !ManageTrainDoors()  )// && DoorStatus == Enums.DoorStatus.Close)

            //{


            //    //frenleme mesafesi frontcurrenttrackin konumu geçiyorsa
            //    if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= frontCurrentTrack.StopPositionInRoute) //item.StartPositionInRoute)
            //    {

            //    }

            //    DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "----------------------------------", System.Drawing.Color.Black);
            //    DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "gitme izni var ve başka koşul yok ", System.Drawing.Color.Black);
            //    DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "trenin içinde bulunduğu tracklerin hız limitlerine uy ", System.Drawing.Color.Black);


            //    targetSpeedKM = FindMinSpeedInTracks(frontCurrentTrack, rearCurrentTrack, routeTracks);


            //}



            return targetSpeedKM;
        }




        public double ManageAcceleration(double currentTrainSpeed, Vehicle vehicle, double maxTrackSpeed)
        { 
            double? acc = null, dec = null, stopAcc = null;
            double acceleration = 0 ;


            if (vehicle.TargetSpeedKMH == 0)
            {
                stopAcc = StopTrain(vehicle);
            } 
            else if ((vehicle.TargetSpeedCMS != 0) && (currentTrainSpeed != vehicle.TargetSpeedCMS))
            {
                acc = SetAcceleration(currentTrainSpeed, vehicle.TargetSpeedCMS, vehicle.MaxTrainAcceleration, maxTrackSpeed);
                dec = SetDeceleration(currentTrainSpeed, vehicle.TargetSpeedCMS, vehicle.MaxTrainDeceleration);
            }

            if (stopAcc.HasValue)
                acceleration = stopAcc.Value;
            if (acc.HasValue)
                acceleration = acc.Value;
            else if (dec.HasValue)
                acceleration = dec.Value;

            return acceleration;
        }


        double totalDistance;
        public double? StopTrain(Vehicle vehicle)
        {
            double? currentAcceleration = null;

            if (vehicle.CurrentTrainSpeedKMH == 0)
            {
                vehicle.CurrentTrainSpeedCMS = 0;
                vehicle.CurrentAcceleration = 0;
            }
            else if (vehicle.CurrentTrainSpeedCMS > 0)
            {
                double distanceToTarget=0;

                Track station = movementTrack.Find(x => (x == ActualFrontOfTrainCurrent.Track) && !string.IsNullOrEmpty(x.Station_Name));

                if (station != null)
                {

                    int stoppingPoint = TrackStoppingPointForDirection(station, Direction);


                    //istasyonda durdurma kısmı burası önemli bu noktaya göre duracak tren
                    //distanceToTarget = station.StopPositionInRoute - FrontOfTrainLocationWithFootPrintInRoute;
                    //this.ActualFrontOfTrainCurrent.Location >= itemstationTrack_Length - item.Stopping_Point_Position_1

                    //if (station.Station_Name == "SGM2")
                    //    distanceToTarget = Math.Abs((station.StopPositionInRoute - station.Stopping_Point_Positon_2 ) - FrontOfTrainLocationWithFootPrintInRoute);
                    //else

                    //wayside
                    distanceToTarget = Math.Abs((station.StopPositionInRoute - stoppingPoint) - FrontOfTrainLocationWithFootPrintInRoute);
                    //distanceToTarget =  ((station.StopPositionInRoute - stoppingPoint) - FrontOfTrainLocationWithFootPrintInRoute);
                    //distanceToTarget = Math.Abs((station.StopPositionInRoute - station.Stopping_Point_Position_1) - FrontOfTrainLocationWithFootPrintInRoute);

                    //int stopPosition;

                    //if (Direction == Enums.Direction.Right)
                    //    stopPosition = station.Stopping_Point_Positon_2;
                    //else
                    //    stopPosition = station.Stopping_Point_Position_1;


                    //distanceToTarget = (station.StopPositionInRoute - stopPosition) - FrontOfTrainLocationWithFootPrintInRoute;
                }
                //else if (ActualFrontOfTrainCurrent.Track == MovementAuthorityTrack)
                //{
                //    int stoppingPoint = TrackStoppingPointForDirection(ActualFrontOfTrainCurrent.Track, Direction);

                //    distanceToTarget = Math.Abs((ActualFrontOfTrainCurrent.Track.StopPositionInRoute - stoppingPoint) - FrontOfTrainLocationWithFootPrintInRoute);
                //}
                else
                {
                    //burayı değiştireceğim
                    //distanceToTarget = m_route.Length - FrontOfTrainLocationWithFootPrintInRoute;

                    //double totalDistance = movementTrack.Sum(x => x.Track_Length);

                    //distanceToTarget = totalDistance - Math.Floor(FrontOfTrainLocationWithFootPrintInRoute);

                    //distanceToTarget = MainForm.m_mf.m_YNK1_KIR2_YNK1.Last().StopPositionInRoute - Math.Floor(FrontOfTrainLocationWithFootPrintInRoute);


                    //distanceToTarget = movementTrack.Last().StopPositionInRoute - FrontOfTrainLocationWithFootPrintInRoute;
                }



                //double distanceToTarget = m_route.Route_Tracks[1].StopPositionInRoute - FrontOfTrainLocationWithFootPrintInRoute;


                //double distanceToTarget = m_route.Length - RealFrontCurrentLocation;// FrontTrainLocationInRouteWithFootPrint;

                // Vs² = Vi² + 2ax
                double acc = (0 - Math.Pow(vehicle.CurrentTrainSpeedCMS, 2)) / (2 * distanceToTarget);

                if (Math.Abs(acc) < (vehicle.MaxTrainDeceleration))
                {
                    currentAcceleration = acc;
                }
                else
                {
                    currentAcceleration = -vehicle.MaxTrainDeceleration;
                }
            }

            return currentAcceleration;
        }

        public double? StopTrain(Vehicle vehicle, double length)
        {
            double? currentAcceleration = null;

            if (vehicle.CurrentTrainSpeedKMH == 0)
            {
                vehicle.CurrentTrainSpeedCMS = 0;
                vehicle.CurrentAcceleration = 0;
            }
            else if (vehicle.CurrentTrainSpeedCMS > 0)
            {
                //burayı değiştireceğim
                double distanceToTarget = length - FrontOfTrainLocationWithFootPrintInRoute;



                //double distanceToTarget = m_route.Route_Tracks[1].StopPositionInRoute - FrontOfTrainLocationWithFootPrintInRoute;


                //double distanceToTarget = m_route.Length - RealFrontCurrentLocation;// FrontTrainLocationInRouteWithFootPrint;

                // Vs² = Vi² + 2ax
                double acc = (0 - Math.Pow(vehicle.CurrentTrainSpeedCMS, 2)) / (2 * distanceToTarget);

                if (Math.Abs(acc) < (vehicle.MaxTrainDeceleration))
                {
                    currentAcceleration = acc;
                }
                else
                {
                    currentAcceleration = -vehicle.MaxTrainDeceleration;
                }
            }

            return currentAcceleration;
        }

        public double? SetAcceleration(double currentTrainSpeed, double targetSpeed, double maxTrainAcceleration, double maxTrackSpeed)
        {
            double? acceleration = null;

            if (currentTrainSpeed < targetSpeed && currentTrainSpeed < maxTrackSpeed)//burada kaldım
                acceleration = maxTrainAcceleration; 

            return acceleration;
        }

        public double? SetDeceleration(double currentTrainSpeed, double targetSpeed, double maxTrainDeceleration)
        {
            double? deceleration = null;

            if (currentTrainSpeed > 0 && currentTrainSpeed > targetSpeed)
                deceleration = -maxTrainDeceleration;

            return deceleration;
        }



        readonly object m_manageDoor = new object();

        double total;
        public Enums.DoorStatus ManageTrainDoors()
        {

            //lock (m_manageDoor)
            {
                DoorTimerCounter = 11 - Convert.ToInt32(m_stopwatch.Elapsed.TotalSeconds);

                if (!string.IsNullOrEmpty(ActualFrontOfTrainCurrent.Track.Station_Name) && !m_doorTimer.Enabled && (openDoorTrackName != ActualFrontOfTrainCurrent.Track.Station_Name) && Vehicle.CurrentTrainSpeedCMS == 0)
                //if (!string.IsNullOrEmpty(ActualFrontOfTrainCurrent.Track.Station_Name) && !m_doorTimerDeneme.Enabled && (openDoorTrackName != ActualFrontOfTrainCurrent.Track.Station_Name) && Vehicle.CurrentTrainSpeedCMS == 0)

                {
                    //m_route = Route.CreateNewRoute(this.ActualRearOfTrainCurrent.Track.Track_ID, MainForm.m_mf.m_allRoute);


                    //if (Vehicle.CurrentTrainSpeedCMS == 0)
                    {
                        DoorStatus = OpenTrainDoors(DoorStatus);

                        if (DoorStatus == Enums.DoorStatus.Open)
                        {
                            m_doorTimer.Start();
                            //System.Threading.Timer STTimer = new System.Threading.Timer(DenemeTimer, null, 0, 1000);

                            //STTimer.

                            //m_doorTimerDeneme.Start();
                            //zongurt = ActualFrontOfTrainCurrent.Track.DwellTime;
                            //this.dwellTime zongurt = 10;

                            DwellTimeFinished = false;
                                





                            DwellTime  = 10;
                            zongurt = 10;

                        }

                        Debug.WriteLine(this.Vehicle.TrainID.ToString() + " " + "Open Doors - " + "Counter : " + DoorTimerCounter.ToString());
                        //DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, this.Vehicle.TrainID.ToString() + " " + "Open Doors", Color.Red);

                        //MainForm.m_mf.m_UILogs.Add(Tuple.Create<string, Color>(this.Vehicle.TrainID.ToString() + " " + "Open Doors", Color.Red));

                        DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBoxTrainsLogs, this.Vehicle.TrainID.ToString() + " " + "Open Doors", Color.Red);

                        m_stopwatch.Start();



                        #region WSATC Movement Test

                        if ((ActualFrontOfTrainCurrent.Track.Track_ID == movementTrack.Last().Track_ID))
                        {
                            movementTrack.Clear();
                        }
                            //int stoppingPoint = TrackStoppingPointForDirection(ActualFrontOfTrainCurrent.Track, Enums.Direction.Right);

                            //if ((ActualFrontOfTrainCurrent.Track.Track_ID == movementTrack.Last().Track_ID) &&
                            //       ((ActualFrontOfTrainCurrent.Location - 40) <= stoppingPoint) && (stoppingPoint <= (ActualFrontOfTrainCurrent.Location + 40))) 
                            //{
                            //    movementTrack.Clear();
                            //}

                            #endregion



















                            //this.DwellTime = DoorTimerCounter;




                            //Wayside hareket yetkisi testi için commentlendi
                            //movementTrack = Route.CreateMovementTracksStationToStation(this.ActualFrontOfTrainCurrent.Track.Track_ID, MainForm.m_mf.m_YNK1_KIR2_YNK1, MainForm.m_mf.m_YNK2_HAV2_YNK2, true);



                            //if ((!string.IsNullOrEmpty(movementTrack.First().Station_Name)) && (movementTrack.First().Station_Name == "KRZ2"))
                            //{
                            //    ActualFrontOfTrainCurrent.Track = movementTrack.First();
                            //    ActualRearOfTrainCurrent.Track = movementTrack.First();

                            //    ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Position_1);
                            //    ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location + this.Vehicle.TrainLength;
                            //}
                            //else if ((!string.IsNullOrEmpty(movementTrack.First().Station_Name)) && (movementTrack.First().Station_Name == "YNK1"))
                            //{
                            //    ActualFrontOfTrainCurrent.Track = movementTrack.First();
                            //    ActualRearOfTrainCurrent.Track = movementTrack.First();

                            //    ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2);
                            //    ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location - this.Vehicle.TrainLength;
                            //}

                            //else if ((!string.IsNullOrEmpty(movementTrack.First().Station_Name)) && (movementTrack.First().Station_Name == "HVL2"))
                            //{
                            //    ActualFrontOfTrainCurrent.Track = movementTrack.First();
                            //    ActualRearOfTrainCurrent.Track = movementTrack.First();

                            //    ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2);
                            //    ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location - this.Vehicle.TrainLength;
                            //}
                            //else if ((!string.IsNullOrEmpty(movementTrack.First().Station_Name)) && (movementTrack.First().Station_Name == "YNK2"))
                            //{
                            //    ActualFrontOfTrainCurrent.Track = movementTrack.First();
                            //    ActualRearOfTrainCurrent.Track = movementTrack.First();

                            //    ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2);
                            //    ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location - this.Vehicle.TrainLength;
                            //}
                            //total = movementTrack.Sum(x => x.Track_Length);


                            return DoorStatus;
                    }

                }

                else
                {
                    //Console.WriteLine("ManageTrainDoors index = 2");
                    //Debug.WriteLine("ManageTrainDoors index = 2 - " + this.Vehicle.TrainID.ToString() + " " + "Doors - " + "Counter : " + DoorTimerCounter.ToString());
                    return DoorStatus;
                }

            }


        }

        int timerCount = 0;

        public void DenemeTimer(object Ahmet)
        {
            //timerCount++;

            Debug.WriteLine(timerCount++.ToString());
        }


        //    public void OnTrainDoorsOpenedEvent(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    //Vehicle.DoorTimerCounter++

        //    //if(m_stopwatch.Elapsed.TotalSeconds > 10)
        //    {
        //        DoorStatus = CloseTrainDoors(DoorStatus);
        //        m_doorTimer.Stop();
        //        m_stopwatch.Reset();
        //        DwellTimeFinished = true;

        //        openDoorTrackName = ActualFrontOfTrainCurrent.Track.Station_Name;

        //        //openDoorTrackName = ActualFrontOfTrainCurrent.Track.Station_Name;

        //        //ActualRearOfTrainCurrent
        //        //DoorTimerCounter = (int)m_stopwatch.Elapsed.TotalSeconds;
        //        DoorTimerCounter = 11 - Convert.ToInt32(m_stopwatch.Elapsed.TotalSeconds);
        //        //Debug.WriteLine("kapı kapandı");
        //        //DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, this.Vehicle.TrainID.ToString() + " " + "Close Doors", Color.Red);

        //        //MainForm.m_mf.m_UILogs.Add(Tuple.Create<string, Color>(this.Vehicle.TrainID.ToString() + " " + "Close Doors", Color.Red));

        //        Debug.WriteLine(this.Vehicle.TrainID.ToString() + " " + "Close Doors - " + "Counter : " + DoorTimerCounter.ToString());


        //        DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBoxTrainsLogs, this.Vehicle.TrainID.ToString() + " " + "Close Doors", Color.Red);




        //        //WSATC
        //        //OBATCtoWSATC_BerthingOk = false;

        //        //if(ActualFrontOfTrainCurrent.Track.Track_ID == 10101)
        //        //{
        //        //    ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2);//FrontOfTrainCurrentTrack.Track_Start_Position + trainLength;
        //        //    ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location - 11200;
        //        //}
        //    } 

        //}
        [Browsable(false)]
        public  int zongurt { get; set; }

        internal readonly object m_OnTrainDoors = new object();

        public void OnTrainDoorsOpenedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            //lock (m_OnTrainDoors)
            {
                //Vehicle.DoorTimerCounter++

                if (zongurt <= 0)
                {
                    DoorStatus = CloseTrainDoors(DoorStatus);
                    m_doorTimer.Stop();
                    m_stopwatch.Reset();
                    DwellTimeFinished = true;

                    openDoorTrackName = ActualFrontOfTrainCurrent.Track.Station_Name;

                    //openDoorTrackName = ActualFrontOfTrainCurrent.Track.Station_Name;

                    //ActualRearOfTrainCurrent
                    //DoorTimerCounter = (int)m_stopwatch.Elapsed.TotalSeconds;
                    DoorTimerCounter = 11 - Convert.ToInt32(m_stopwatch.Elapsed.TotalSeconds);
                    //Debug.WriteLine("kapı kapandı");
                    //DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, this.Vehicle.TrainID.ToString() + " " + "Close Doors", Color.Red);

                    //MainForm.m_mf.m_UILogs.Add(Tuple.Create<string, Color>(this.Vehicle.TrainID.ToString() + " " + "Close Doors", Color.Red));

                    Debug.WriteLine(this.Vehicle.TrainID.ToString() + " " + "Close Doors - " + "Counter : " + DoorTimerCounter.ToString());


                    DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBoxTrainsLogs, this.Vehicle.TrainID.ToString() + " " + "Close Doors", Color.Red);




                    //WSATC
                    //OBATCtoWSATC_BerthingOk = false;

                    //if(ActualFrontOfTrainCurrent.Track.Track_ID == 10101)
                    //{
                    //    ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2);//FrontOfTrainCurrentTrack.Track_Start_Position + trainLength;
                    //    ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location - 11200;
                    //}


                }
                else if (zongurt >= 0)
                {
                    zongurt--;

                    //DwellTimeFinished = true;
                }

            }


        }
















        public Enums.DoorStatus OpenTrainDoors(Enums.DoorStatus doorStatus)
        {

           

            if (doorStatus == Enums.DoorStatus.Close)
            {
                doorStatus = Enums.DoorStatus.Open;

            
            }

            return doorStatus;
        }

        public Enums.DoorStatus CloseTrainDoors(Enums.DoorStatus doorStatus)
        {

            if (doorStatus == Enums.DoorStatus.Open)
            {
                doorStatus = Enums.DoorStatus.Close;
            }

            return doorStatus;
        }






        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    // Dispose time code 
                    //buraya sonlanma için method eklenecek
                }

                // Finalize time code 
                m_disposed = true;
            }


        }

        public void Dispose()
        {
            //if (m_disposed)
            {
                Dispose(true);

                GC.SuppressFinalize(this);
            }
        }


    }
}
