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
    public partial class OBATP : IWSATP_TO_OBATPMessageWatcher, IATS_TO_OBATO_InitMessageWatcher, IATS_TO_OBATO_MessageWatcher
    {
        #region Gloval
        [Browsable(false)]
        bool DwellTimeFinished = false;


        [Browsable(false)]
        string openDoorTrackName;

        [Browsable(false)]
        public Enums.DoorStatus DoorStatus { get; set; }
       

        //public ThreadSafeList<ushort> VirtualOccupationTracks = new ThreadSafeList<ushort>();
        //public ThreadSafeList<ushort> FootPrintTracks = new ThreadSafeList<ushort>();
     
        //[Browsable(false)]
        //public ushort[] footPrintTracks = new ushort[15];
        //[Browsable(false)]
        //public ushort[] virtualOccupationTracks = new ushort[20];
        /// <summary>
        /// Trenin gittiği toplam mesafe (m)
        /// </summary>
        [Browsable(false)]
        public double TotalTrainDistance { get; set; }

        /// <summary>
        /// aracın structı
        /// </summary>
        [Browsable(false)]
        public Vehicle Vehicle = new Vehicle();



        //actuallocation
        [Browsable(false)]
        public TrackWithPosition ActualFrontOfTrainCurrent = new TrackWithPosition();
        [Browsable(false)]
        public TrackWithPosition ActualRearOfTrainCurrent = new TrackWithPosition();


        //virtual occ
        [Browsable(false)]
        public TrackWithPosition FrontOfTrainVirtualOccupation = new TrackWithPosition();
        [Browsable(false)]
        public TrackWithPosition RearOfTrainVirtualOccupation = new TrackWithPosition();

        //footprint         
        [Browsable(false)]
        public TrackWithPosition FrontOfTrainTrackWithFootPrint = new TrackWithPosition();

        [Browsable(false)]
        public TrackWithPosition RearOfTrainTrackWithFootPrint = new TrackWithPosition();


        #endregion

        internal volatile bool m_shouldStop;

        /// <summary>
        /// Trenin hata payı olmadan net konum bilgisidir. (cm)
        /// </summary>
        //public double ActualFrontOfTrainCurrentLocation { get; set; }
        //public double ActualRearOfTrainCurrentLocation { get; set; } 
        /// <summary>
        /// Trenin toplam rota metrajı içerisindeki ön konum bilgisi (cm)
        /// </summary>
        [Browsable(false)]
        public double FrontOfTrainLocationWithFootPrintInRoute { get; set; }

        public double FootPrintInTrackFrontOfTrainLocation { get; set; }

        [Browsable(false)]
        public double RearOfTrainLocationWithFootPrintInRoute { get; set; }

        //public Track FrontOfTrainCurrentTrack { get; set; }
        //public Track RearOfTrainCurrentTrack { get; set; }


        [Browsable(false)]
        public Track FrontOfTrainNextTrack { get; set; }
        [Browsable(false)]
        public Track RearOfTrainNextTrack { get; set; }

        /// <summary>
        /// Tren önüne eklenen hata payı (cm)
        /// </summary>
        [Browsable(false)]
        public double FrontOfTrainLocationFault { get; set; }
        /// <summary>
        /// Tren arkasına eklenen hata payı (cm)
        /// </summary>
        [Browsable(false)]
        public double RearOfTrainLocationFault { get; set; }

      
       
        



        //public Track RearOfTrainCurrentTrack { get; set; }



        private Track denemeRearOfTrainCurrentTrack;

        [Browsable(false)]
        public Track DenemeRearOfTrainCurrentTrack
        {
            get { return denemeRearOfTrainCurrentTrack; }

            set
            {
                if (value != denemeRearOfTrainCurrentTrack)
                {
                    //rota listesi içinde eski içinde olanı silecez
                    if(denemeRearOfTrainCurrentTrack == null)
                        denemeRearOfTrainCurrentTrack = value;
                    else
                    {
                       bool isDeleted = m_route.Route_Tracks.Remove(denemeRearOfTrainCurrentTrack);


                        denemeRearOfTrainCurrentTrack = value;
                    }


                    
                    //TargetSpeedKMH = UnitConversion.CentimeterSecondToKilometerHour(targetSpeedCMS);
                }
            }
        }


        [Browsable(false)]
        public Track MovementAuthorityTrack { get; set; }

        /// <summary>
        /// Trene ait yön bilgisi, 1 veya 2 şeklinde belirtilir.
        /// </summary>
       [Browsable(false)]
        public Enums.Direction Direction { get; set; }
        [Browsable(false)]
        public Enums.Direction RearDirection { get; set; }

        [Browsable(false)]
        public System.Timers.Timer m_doorTimer;
        Stopwatch m_stopwatch;
        [Browsable(false)]
        public int DoorTimerCounter { get; set; }
        double OperationTime = 0.2;

        //public List<Track> m_route;
        [Browsable(false)]
        public Route m_route = new Route();
        public Enums.Status Status;


        public List<Track> movementTrack = new List<Track>();


        public OBATP()
        {

        } 
 

        public OBATP DeepCopy()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (OBATP)formatter.Deserialize(ms);
            }
        }

        public OBATP MemberwiseCopy()
        { 

            return (OBATP)this.MemberwiseClone();
          
        }
     
        public OBATP(Enums.Train_ID trainID, double maxTrainAcceleration, double maxTrainDeceleration, int maxTrainSpeedKMH, double trainLength, Route route)
        {
            //tren parametreleri set ediliyor
            Vehicle.MaxTrainAcceleration = maxTrainAcceleration * 100;
            Vehicle.MaxTrainDeceleration = maxTrainDeceleration * 100;
            Vehicle.MaxTrainSpeedKMH = maxTrainSpeedKMH;
            Vehicle.TrainLength = trainLength;
            Vehicle.TrainIndex = (int)trainID;
            Vehicle.TrainID = trainID;
            Vehicle.TrainName = trainID.ToString();

            //MovementAuthorityTrack = route.Route_Tracks.Find(x => x.Track_ID == 11101);


            FrontOfTrainLocationFault = 200;
            RearOfTrainLocationFault = 0;



            //Track startTrack = MainForm.m_mf.m_simulationAllTracks.Find(x => x.Track_ID == route.Entry_Track.Track_ID);

            ActualFrontOfTrainCurrent.Track = route.Entry_Track;// startTrack;
            ActualRearOfTrainCurrent.Track = route.Entry_Track;// startTrack;startTrack;


            //ActualFrontOfTrainCurrent.Track =  startTrack;
            //ActualRearOfTrainCurrent.Track =   startTrack;

            //ActualFrontOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Track.Track_Length - ((ActualFrontOfTrainCurrent.Track.Track_End_Position - ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2) - ActualFrontOfTrainCurrent.Track.Track_Start_Position);//FrontOfTrainCurrentTrack.Track_Start_Position + trainLength;
            //ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location - trainLength;

            Direction = FindDirection(route.Route_Tracks, Direction);

            if(Direction == Enums.Direction.Right)
            {
                ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2);//FrontOfTrainCurrentTrack.Track_Start_Position + trainLength;
                ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location - trainLength;
            }
           else
            {
                ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Position_1);//FrontOfTrainCurrentTrack.Track_Start_Position + trainLength;
                ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location + trainLength;
            }

            //RealFrontCurrentLocation = 11200;
            //ActualFrontOfTrainCurrent.Location = trainLength;//FrontOfTrainCurrentTrack.Track_Start_Position + trainLength;
            //ActualRearOfTrainCurrent.Location = 0;



            //DwellTimeFinished = false;
            m_stopwatch = new Stopwatch();
            m_doorTimer = new System.Timers.Timer();
            m_doorTimer.Interval = 11000;
            m_doorTimer.Elapsed += OnTrainDoorsOpenedEvent;


            DoorStatus = Enums.DoorStatus.Close;
            DoorTimerCounter = 11 - Convert.ToInt32(m_stopwatch.Elapsed.TotalSeconds);

            //m_route = new Route();
            m_route.Entry_Track = route.Entry_Track;
            m_route.Entry_Track_ID = route.Entry_Track_ID;
            m_route.Exit_Track = route.Exit_Track;
            m_route.Exit_Track_ID = route.Exit_Track_ID;
            m_route.Route_No = route.Route_No;
            m_route.Length = route.Length;
            m_route.Route_Tracks = route.Route_Tracks;

            //DwellTime = 20;


            //MainForm.m_trainMovement.AddWatcher(MainForm.m_mf);



            //ui göstermek için ui propertyleri burada set ediliyor
            //bu kısım vakit olduğunda farklı bir mekanizma ile tekrar yazılacak
            this.ID = Convert.ToString(Vehicle.TrainIndex);
            this.Train_Name = Vehicle.TrainName;


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


            this.Status = Enums.Status.Create;

            MainForm.m_trainObserver.TrainCreated(this); 
        }


        public void RequestStartProcess()
        {
            m_shouldStop = false; 

            Thread thread = new Thread(new ParameterizedThreadStart(StartProcess));
            thread.IsBackground = true;
            thread.Start();

            this.Status = Enums.Status.Start;

            //StartProcess(null);
        }
        public void RequestStopProcess()
        {
            m_shouldStop = true;

            this.Status = Enums.Status.Stop;
        }


        readonly object zozo = new object();
        public void StartProcess(object o)
        {

            //try
            {


                while (!m_shouldStop)
                {
                    lock (zozo)
                    {
                        //if (Math.Round(FrontOfTrainLocationWithFootPrintInRoute) <= m_route.Length)

                        //if (FrontOfTrainNextTrack != MovementAuthorityTrack)
                        {
                            //routeCompleted = true;


                            Direction = FindDirection(m_route.Route_Tracks, Direction);


                            //trenin arkası ve önü için bir sonraki tracki verir
                            FrontOfTrainNextTrack = FindNextTrack(ActualFrontOfTrainCurrent.Track, Direction);
                            RearOfTrainNextTrack = FindNextTrack(ActualRearOfTrainCurrent.Track, Direction);

                            //trenin frenleme mesafesi hesaplaması
                            Vehicle.BrakingDistance = CalculateBrakingDistance(this.Vehicle.MaxTrainDeceleration, this.Vehicle.CurrentTrainSpeedCMS);


                            //trenin konumundaki hata payını bulma
                            this.FrontOfTrainLocationWithFootPrintInRoute = FindPositionFootPrintFront(ActualFrontOfTrainCurrent.Track, Direction, m_route.Route_Tracks) - 200;
                            //this.RearOfTrainLocationWithFootPrintInRoute = FindPositionFootPrintRear(RearOfTrainCurrentTrack, RearDirection, m_route.Route_Tracks);
                            this.RearOfTrainLocationWithFootPrintInRoute = FindPositionFootPrintRear(ActualRearOfTrainCurrent.Track, Direction, m_route.Route_Tracks);

                            //sanal meşguliyet hesaplama
                            this.FrontOfTrainVirtualOccupation = FindFrontVirtualOccupation(m_route.Route_Tracks, Direction, MainForm.m_mf.m_allTracks);
                            //this.RearOfTrainVirtualOccupation = FindRearVirtualOccupation(m_route.Route_Tracks, RearDirection, MainForm.allTracks);
                            this.RearOfTrainVirtualOccupation = FindRearVirtualOccupation(m_route.Route_Tracks, Direction, MainForm.m_mf.m_allTracks);


                            double? isTargetSpeed = FindTargetSpeed(m_route.Route_Tracks.ToList(), ActualFrontOfTrainCurrent.Track, ActualRearOfTrainCurrent.Track, this.Vehicle);

                            if (isTargetSpeed.HasValue)
                                this.Vehicle.TargetSpeedKMH = isTargetSpeed.Value;
                            else
                                this.Vehicle.TargetSpeedKMH = 0;


                            this.Vehicle.CurrentAcceleration = ManageAcceleration(this.Vehicle.CurrentTrainSpeedCMS, this.Vehicle, ActualFrontOfTrainCurrent.Track.MaxTrackSpeedCMS);

                            this.Vehicle.CurrentTrainSpeedCMS = CalculateSpeed(this.Vehicle);

                            this.ActualFrontOfTrainCurrent = CalculateLocationInTrack(ActualFrontOfTrainCurrent.Track, FrontOfTrainNextTrack, Direction, Vehicle, this.ActualFrontOfTrainCurrent.Location);

                            //Tuple<double, Track> rearCurrent = CalculateLocation(RearOfTrainCurrentTrack, RearOfTrainNextTrack, RearDirection, Vehicle, ActualRearOfTrainCurrentLocation);
                            this.ActualRearOfTrainCurrent = CalculateLocationInTrack(ActualRearOfTrainCurrent.Track, RearOfTrainNextTrack, Direction, Vehicle, this.ActualRearOfTrainCurrent.Location);//ActualRearOfTrainCurrentLocation);




                            //hareket listesinden silmek için deneme yapıldığı kısım burada başlıyor
                            //DenemeRearOfTrainCurrentTrack = ActualRearOfTrainCurrent.Track;





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


                            MainForm.m_trainObserver.TrainMovementCreated(this);
                            MainForm.m_trainObserver.TrainMovementUI(this, new OBATPUIAdapter(this));


                            Thread.Sleep(5);
                        }
                    }


                }
            }
            //catch(Exception ex)
            //{

            //}

        } 


        //şimdilik rotaları parametrik olarak alıyor
        //sonrasında yapının şekillenmesine göre ayarlanacak
        public Track FindNextTrack(Track track, Enums.Direction direction)// rota zaten bütün trackleri içermiyor mu? burayı inceleyip anlamak lazım
        {


            Track nextTrack = null;

            if (direction == Enums.Direction.Right)
            {

                //int index =  m_route.Route_Tracks.ToList().FindIndex(x => x == track);

                int index = m_route.Route_Tracks.IndexOf(track); 


                //if(track == MovementAuthorityTrack)
                //{
                //    nextTrack = track;
                //}
                //else
             if (index + 1 < m_route.Route_Tracks.Count)
                    nextTrack = m_route.Route_Tracks[index + 1];
                else
                {

                    nextTrack = m_route.Route_Tracks[index];
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


                int index = m_route.Route_Tracks.IndexOf(track);

                //if (track == MovementAuthorityTrack)
                //{
                //    nextTrack = track;
                //}
                //else
                if (index + 1 < m_route.Route_Tracks.Count)
                    nextTrack = m_route.Route_Tracks[index + 1];
                else
                {
                    nextTrack = m_route.Route_Tracks[index];
                }



            }





            return nextTrack;

        }


        public Enums.Direction FindDirection(ThreadSafeList<Track> routeTracks, Enums.Direction gloDirection)
        { 
            Enums.Direction direction = Enums.Direction.Right;

            List<Track> tracksWithOutSwitches = routeTracks.FindAll(x => x.Track_ID.ToString().Substring(0, 1) != "6"); 

           if(tracksWithOutSwitches.Count >= 2)
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

        /// <summary>
        /// Trenin o anki hızına göre yavaşlama ivmesiyle duracağı mesafeyi bulur.
        /// </summary>
          public double CalculateBrakingDistance(double maxTrainDeceleration, double currentTrainSpeedCMS)
        {
            double brakingDistance = ((0.5) * maxTrainDeceleration * Math.Pow(currentTrainSpeedCMS / maxTrainDeceleration, 2)); 

            return brakingDistance;
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
        public TrackWithPosition FindFrontVirtualOccupation(ThreadSafeList<Track> routeTracks, Enums.Direction direction, ThreadSafeList<Track> allTracks)
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
        public TrackWithPosition FindRearVirtualOccupation(ThreadSafeList<Track> routeTracks, Enums.Direction rearDirection, ThreadSafeList<Track> allTracks)
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
        /// Trenin gitmesi gereken hızı belirler.
        /// </summary>
        public double? FindTargetSpeed(List<Track> routeTracks, Track  frontCurrentTrack, Track rearCurrentTrack, Vehicle vehicle)
        {
            double? targetSpeedKM = null;

            bool IsConnectionOkayToGo = true;
            DoorStatus = ManageTrainDoors();

            Enums.Route isExceed = IsRouteLimitExceeded();


            // Gitme izni yoksa tren durmalı
            //if (isExceed == Enums.Route.Out || DoorStatus == Enums.DoorStatus.Open || !string.IsNullOrEmpty(frontCurrentTrack.Station_Name) || (!string.IsNullOrEmpty(frontCurrentTrack.Station_Name) && frontCurrentTrack.Station_Name = && DoorStatus == Enums.DoorStatus.Close))//|| DoorStatus == Enums.DoorStatus.Open)
           if (isExceed == Enums.Route.Out || DoorStatus==Enums.DoorStatus.Open )// || DoorStatus == Enums.DoorStatus.Open || !string.IsNullOrEmpty(FrontOfTrainCurrentTrack.Station_Name))
            {
                targetSpeedKM = 0;
            }
            else //  if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= frontCurrentTrack.StopPositionInRoute)
            {
                //if (IsRouteLimitExceeded() == Enums.Route.Out)
                //{
                //    targetSpeedKM = 0;
                //}
                //else
                //{
                int frontCurrentTrackIndex = m_route.Route_Tracks.IndexOf(frontCurrentTrack);
                //int frontCurrentTrackIndex = m_route.Route_Tracks.ToList().FindIndex(x => x.Track_ID == frontCurrentTrack.Track_ID);
                int remainingTracks = m_route.Route_Tracks.Count - frontCurrentTrackIndex;


                //int de = m_route.Route_Tracks.ToList().FindIndex(x => x.Track_ID == MovementAuthorityTrack.Track_ID);

                //List<Track> restOfMovementTracks = m_route.Route_Tracks.ToList().GetRange(frontCurrentTrackIndex, remainingTracks);

                //var dsf = m_route.Route_Tracks...Take( While(x => x < 10).ToList();

                List<Track> restOfMovementTracks = m_route.Route_Tracks.ToList().GetRange(frontCurrentTrackIndex, remainingTracks);

                //List<Track> restOfMovementTracks = routeTracks.Where((element, index) => (index >= frontCurrentTrackIndex) && (index <= de)).ToList();

                Stack<Track> reverseRestOfMovementTracks = new Stack<Track>(restOfMovementTracks.ToList());

                //if (restOfMovementTracks.Count == 0)
                //    return targetSpeedKM;



                foreach (Track item in restOfMovementTracks)
                    {
 
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


                            double differenceOfMaxTrackSpeed = (tempSpeed - reverseTracks.MaxTrackSpeedCMS);
                            double timeToDifferenceOfMaxTrackSpeed = (differenceOfMaxTrackSpeed / vehicle.MaxTrainDeceleration);
                            double distanceToArrangeTheSpeed = (tempSpeed * timeToDifferenceOfMaxTrackSpeed);

                            //openDoorTrack != FrontOfTrainCurrentTrack.Station_Name


                            #region duraklarda bağımsız yavaşla için yapılan değişiklik
                            //if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= (item.StopPositionInRoute - item.Stopping_Point_Position_1))

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



                            ////iki track arasındaki hız farkı için toplam alınan mesafe
                            double distanceToSpeedDifference = (differenceOfMaxTrackSpeed) * (timeToDifferenceOfMaxTrackSpeed);

                            ////double vehicledistanceToSpeedDifference = (vehicledifferenceOfMaxTrackSpeed) * (vehicletimeToDifferenceOfMaxTrackSpeed);

                            if ((tempSpeed > reverseTracks.MaxTrackSpeedCMS) &&
                                    (((FrontOfTrainLocationWithFootPrintInRoute + distanceToArrangeTheSpeed) - (0.5 * distanceToSpeedDifference)) < reverseTracks.StartPositionInRoute))
                            {

                                
                            }
                            //mesafe yoksa
                            else if (tempSpeed > reverseTracks.MaxTrackSpeedCMS)
                            {


                                //double mesafecm = ters.StartPositionInRoute - FrontOfTrainLocationWithFootPrintInRoute;  
                                //double acc = (ters.MaxTrackSpeedCMS - Math.Pow(vehicle.CurrentTrainSpeedCMS, 2)) / (2 * mesafecm);

                                targetSpeedKM = reverseTracks.MaxTrackSpeedKMH;


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
                                    targetSpeedKM = FindMinSpeedInTracks(item, rearCurrentTrack, routeTracks);


                                    //break;
                                    return targetSpeedKM;

                                }
                                //sonradan eklendi durunca hareket ettirmek için
                                else if (Math.Round(FrontOfTrainLocationWithFootPrintInRoute) + Vehicle.BrakingDistance > item.StopPositionInRoute)
                                {
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

                Track station = m_route.Route_Tracks.Find(x => (x == ActualFrontOfTrainCurrent.Track) && !string.IsNullOrEmpty(x.Station_Name));

                if (station != null)
                {

                    int stoppingPoint = TrackStoppingPointForDirection(station, Direction);


                    //istasyonda durdurma kısmı burası önemli bu noktaya göre duracak tren
                    //distanceToTarget = station.StopPositionInRoute - FrontOfTrainLocationWithFootPrintInRoute;
                    //this.ActualFrontOfTrainCurrent.Location >= itemstationTrack_Length - item.Stopping_Point_Position_1

                    //if (station.Station_Name == "SGM2")
                    //    distanceToTarget = Math.Abs((station.StopPositionInRoute - station.Stopping_Point_Positon_2 ) - FrontOfTrainLocationWithFootPrintInRoute);
                    //else
                    distanceToTarget = Math.Abs((station.StopPositionInRoute - stoppingPoint) - FrontOfTrainLocationWithFootPrintInRoute);
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
                    distanceToTarget = m_route.Length - FrontOfTrainLocationWithFootPrintInRoute;
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


        public double CalculateSpeed(Vehicle vehicle)
        {
            //double currentTrainSpeed = 0;

            double currentTrainSpeed = vehicle.CurrentTrainSpeedCMS;

            vehicle.PreviousSpeedCMS = vehicle.CurrentTrainSpeedCMS;

            if (vehicle.CurrentAcceleration == 0)  // ivme yoksa hız değişmez hız değişmiyorsa hiç ellemesek daha iyi değil mi? previousspeed değerini başka yerlerde kullanıyor muyuz?
            {
                currentTrainSpeed = vehicle.PreviousSpeedCMS;
            }
            else if (vehicle.CurrentAcceleration != 0)  // ivme varsa mevcut hıza ivme zaman çarpımı eklenir
            {
                currentTrainSpeed += (vehicle.CurrentAcceleration * OperationTime);
            }

            return currentTrainSpeed;
        }


        public TrackWithPosition CalculateLocationInTrack(Track track, Track nextTrack, Enums.Direction direction, Vehicle vehicle, double location)
        {
            TrackWithPosition returnValues = new TrackWithPosition();// = Tuple.Create<double, Track>[];

            double currentLocation = -1;

            //if (RouteTracks.Count == 0)
            //    return -1;

            // Bir önceki konum ile son zaman aralığında gidilen konum toplanır.
            if (direction == Enums.Direction.Right)
            {
                 
                location = location + (0.5 * (vehicle.CurrentTrainSpeedCMS + vehicle.PreviousSpeedCMS) * OperationTime);

                //if (location >= track.Track_End_Position + 0.1)
                if (location >= track.Track_Length + 0.1)
                {
                    double cation = location - track.Track_End_Position;
                    location = location - track.Track_Length;
                    track = nextTrack;
                }


            }
            else if (direction == Enums.Direction.Left)
            {
                location = location - (0.5 * (vehicle.CurrentTrainSpeedCMS + vehicle.PreviousSpeedCMS) * OperationTime);

                if (location <=  0.1)
                    //if (location <= track.Track_Length + 0.1)
                    //if (location <= track.Track_Start_Position - 0.1)
                    {
                        track = nextTrack;
                        //location = track.Track_End_Position + location;

                        location = track.Track_Length + location;
                    }

            }

            currentLocation = location;

            //bu değişecek
            //CurrentTrack = track;


            returnValues.Track = track;
            returnValues.Location = currentLocation;



            TotalTrainDistance += (0.5 * (this.Vehicle.CurrentTrainSpeedCMS + this.Vehicle.PreviousSpeedCMS) * OperationTime);

            return returnValues;


        }

        //public Tuple<double, Track> CalculateLocation(Track track, Track nextTrack, Enums.Direction direction, Vehicle vehicle, double location)
        //{
        //    Tuple<double, Track> returnValues;// = Tuple.Create<double, Track>[];

        //    double currentLocation = -1;

        //    //if (RouteTracks.Count == 0)
        //    //    return -1;

        //    // Bir önceki konum ile son zaman aralığında gidilen konum toplanır.
        //    if (direction == Enums.Direction.Right)
        //    {



        //        location = location + (0.5 * (vehicle.CurrentTrainSpeedCMS + vehicle.PreviousSpeedCMS) * OperationTime);

        //        //if (location >= track.Track_End_Position + 0.1)
        //        if (location >= track.Track_Length + 0.1)
        //        {
        //            double cation = location - track.Track_End_Position;
        //            location = location - track.Track_Length;
        //            track = nextTrack;
        //        }


        //    }
        //    else if (direction == Enums.Direction.Left)
        //    {
        //        location = location - (0.5 * (vehicle.CurrentTrainSpeedCMS + vehicle.PreviousSpeedCMS) * OperationTime);


        //        //if (location <= track.Track_Length + 0.1)
        //        if (location <= track.Track_Start_Position - 0.1)
        //        {
        //            track = nextTrack;
        //            //location = track.Track_End_Position + location;

        //            location = track.Track_Length + location;
        //        }

        //    }

        //    currentLocation = location;

        //    //bu değişecek
        //    //CurrentTrack = track;


        //    returnValues = Tuple.Create<double, Track>(currentLocation, track);


        //    TotalTrainDistance += (0.5 * (this.Vehicle.CurrentTrainSpeedCMS + this.Vehicle.PreviousSpeedCMS) * OperationTime);

        //    return returnValues;


        //}


        /// <summary>
        /// Trenin rota sınırını durma mesafesiyle birlikte aşıp aşmadığının kontrolü yapılır.
        /// </summary>
        public Enums.Route IsRouteLimitExceeded()
        {
            if ((FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance < m_route.Length))
            //if ((ActualFrontOfTrainCurrentLocation + Vehicle.BrakingDistance < m_route.Length))
            {
                return Enums.Route.In; //false;
            }
            else
            {
                return Enums.Route.Out;//true;
            }
        } 

        public Enums.DoorStatus ManageTrainDoors()
        {
            //DoorTimerCounter = (int)m_stopwatch.Elapsed.TotalSeconds;
            DoorTimerCounter = 11 - Convert.ToInt32(m_stopwatch.Elapsed.TotalSeconds);
            //if (!string.IsNullOrEmpty(FrontOfTrainCurrentTrack.Station_Name) && FrontOfTrainCurrentTrack.Station_Name == openDoorTrackName)
            //{
            //    return DoorStatus;
            //}


            //  if (!string.IsNullOrEmpty(ActualFrontOfTrainCurrent.Track.Station_Name) && !m_doorTimer.Enabled && (openDoorTrackName != ActualFrontOfTrainCurrent.Track.Station_Name) && Vehicle.CurrentTrainSpeedCMS == 0)

            if (!string.IsNullOrEmpty(ActualFrontOfTrainCurrent.Track.Station_Name) && !m_doorTimer.Enabled && (openDoorTrackName != ActualFrontOfTrainCurrent.Track.Station_Name) && Vehicle.CurrentTrainSpeedCMS == 0)

            {

                //m_route = Route.CreateNewRoute(this.ActualRearOfTrainCurrent.Track.Track_ID, MainForm.m_mf.m_allRoute);


                //if (Vehicle.CurrentTrainSpeedCMS == 0)
                {
                    DoorStatus = OpenTrainDoors(DoorStatus);

                    if (DoorStatus == Enums.DoorStatus.Open)
                    {
                        m_doorTimer.Start();
                    }

                    //openDoorTrackName = FrontOfTrainCurrentTrack.Station_Name;
                    Debug.WriteLine("kapı açıldı");
                    DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, this.Vehicle.TrainID.ToString() + " " + "Open Doors", Color.Red);
                    m_stopwatch.Start();



                    //m_route = Route.CreateNewRouteStationToStation(this.ActualFrontOfTrainCurrent.Track.Track_ID, MainForm.m_mf.m_allRoute);

                    if (Direction == Enums.Direction.Right)
                    {

                        m_route = Route.CreateNewRouteToStation(this.ActualFrontOfTrainCurrent.Track.Track_ID, MainForm.m_mf.m_YNK1_KIR2_YNK1);



                        //m_route = Route.CreateNewRoute(this.ActualFrontOfTrainCurrent.Track.Track_ID, MainForm.m_mf.m_FromYenikapıTracks);

                        if ((m_route.Route_Tracks.Count == 1) && (!string.IsNullOrEmpty(m_route.Route_Tracks.First().Station_Name)) && (m_route.Route_Tracks.First().Station_Name == "KRZ2"))
                        {
                            m_route = Route.CreateNewRoute(this.ActualFrontOfTrainCurrent.Track.Track_ID, MainForm.m_mf.m_ToYenikapıTracks);

                            ActualFrontOfTrainCurrent.Track = m_route.Entry_Track;// startTrack;
                            ActualRearOfTrainCurrent.Track = m_route.Entry_Track;// startTrack;startTrack;

                            ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Position_1);//FrontOfTrainCurrentTrack.Track_Start_Position + trainLength;
                            ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location + 11200;
                        }
                    }
                    //else
                    //{
                    //    m_route = Route.CreateNewRoute(this.ActualFrontOfTrainCurrent.Track.Track_ID, MainForm.m_mf.m_ToYenikapıTracks);

                    //    if ((m_route.Route_Tracks.Count == 1) && (!string.IsNullOrEmpty(m_route.Route_Tracks.First().Station_Name)) && (m_route.Route_Tracks.First().Station_Name == "YNK1"))
                    //    {
                    //        m_route = Route.CreateNewRoute(this.ActualFrontOfTrainCurrent.Track.Track_ID, MainForm.m_mf.m_FromYenikapıTracks);


                    //        ActualFrontOfTrainCurrent.Track = m_route.Entry_Track;// startTrack;
                    //        ActualRearOfTrainCurrent.Track = m_route.Entry_Track;// startTrack;startTrack;

                    //        ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2);//FrontOfTrainCurrentTrack.Track_Start_Position + trainLength;
                    //        ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location - 11200;

                    //    }
                    //}


                    return DoorStatus;
                }

                //return DoorStatus;


            }

            else
            {
                Console.WriteLine("ManageTrainDoors index = 2");
                return DoorStatus;
            } 

        } 
         

        public void OnTrainDoorsOpenedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Vehicle.DoorTimerCounter++

            //if(m_stopwatch.Elapsed.TotalSeconds > 10)
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
                Debug.WriteLine("kapı kapandı");
                DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, this.Vehicle.TrainID.ToString() + " " + "Close Doors", Color.Red);



                //if(ActualFrontOfTrainCurrent.Track.Track_ID == 10101)
                //{
                //    ActualFrontOfTrainCurrent.Location = (ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2);//FrontOfTrainCurrentTrack.Track_Start_Position + trainLength;
                //    ActualRearOfTrainCurrent.Location = ActualFrontOfTrainCurrent.Location - 11200;
                //}
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



        public double FindMinSpeedInTracks(Track frontTrack, Track rearTrack, List<Track> routeTracks)
        {
            int frontTrackIndex = routeTracks.FindIndex(x => x == frontTrack);
            int rearTrackIndex = routeTracks.FindIndex(x => x == rearTrack);

            List<Track> findMinSpeed = new List<Track>();

            //if (Direction == Enums.Direction.Right)
            //    findMinSpeed = routeTracks.Where((element, index) => (index >= frontTrackIndex) && (index <= rearTrackIndex)).ToList();
            //else
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
 


        //waysidedan gelen haraket yetkisi tracklerini işleyen metot
        public void WSATP_TO_OBATPMessageInComing(Enums.Train_ID train_ID, WSATP_TO_OBATPAdapter WSATP_TO_OBATPAdapter)
        {
            if (this.Vehicle.TrainID == train_ID)
            {

                if(m_route.Route_Tracks.Count < 20)
                {
                    List<Track> newMovementTracks = WSATP_TO_OBATPAdapter.MovementAuthorityTracks.Except(m_route.Route_Tracks).ToList();


                    for (int i = 0; i < newMovementTracks.Count; i++)
                    {
                        if (m_route.Route_Tracks.Count < 20)
                            m_route.Route_Tracks.Add(newMovementTracks[i]);
                        else
                            break; 
                    } 

                }


                m_route = Route.CreateNewRoute(m_route.Route_Tracks[0].Track_ID, m_route.Route_Tracks[m_route.Route_Tracks.Count - 1].Track_ID, MainForm.m_mf.m_allTracks);

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

        public void ATS_TO_OBATO_InitMessageInComing(Enums.Train_ID train_ID, ATS_TO_OBATO_InitAdapter ATS_TO_OBATO_InitAdapter)
        {
            lock(ATS_TO_OBATO_InitAdapter)
            {
                if (this.Vehicle.TrainID == train_ID)
                {

                    if (this.Status != Enums.Status.Create)
                        return;




                    Debug.WriteLine(train_ID.ToString() + " created");
                   


                    bool isGetValue = MainForm.m_allOBATP.TryGetValue(Convert.ToInt32(train_ID), out OBATP OBATP);

                    if (isGetValue)
                    {

                        Track track = MainForm.m_mf.m_allTracks.Find(x => x.Track_ID == ATS_TO_OBATO_InitAdapter.TrackSectionID); 
                        //Route route = new Route();
                        //route = Route.CreateNewRoute(track.Track_ID, 10311, MainForm.m_mf.m_allTracks);

                        //List<Route> ahmet = Route.SimulationRoute(MainForm.m_simulationRouteTracks);
                        //Route newRoute = Route.CreateNewRoute(ATS_TO_OBATO_InitAdapter.TrackSectionID, ahmet);

                        Route newRoute;
                        
                        if(ATS_TO_OBATO_InitAdapter.ATStoOBATO_TrainDirection == Enums.Direction.Left)
                        {
                            newRoute = Route.CreateNewRoute(ATS_TO_OBATO_InitAdapter.TrackSectionID, MainForm.m_mf.m_ToYenikapıTracks);

                            OBATP.m_route = newRoute;

                            OBATP.ActualFrontOfTrainCurrent.Track = OBATP.m_route.Entry_Track; ;// startTrack;
                            OBATP.ActualRearOfTrainCurrent.Track = OBATP.m_route.Entry_Track; ;// startTrack;startTrack; 

                            OBATP.ActualFrontOfTrainCurrent.Location = (OBATP.ActualFrontOfTrainCurrent.Track.Stopping_Point_Position_1);//FrontOfTrainCurrentTrack.Track_Start_Position + trainLength;
                            OBATP.ActualRearOfTrainCurrent.Location = OBATP.ActualFrontOfTrainCurrent.Location + OBATP.Vehicle.TrainLength;
                        }
                           
                        else
                        {
                            newRoute = Route.CreateNewRoute(ATS_TO_OBATO_InitAdapter.TrackSectionID, MainForm.m_mf.m_FromYenikapıTracks);

                            OBATP.m_route = newRoute;

                            OBATP.ActualFrontOfTrainCurrent.Track = OBATP.m_route.Entry_Track; ;// startTrack;
                            OBATP.ActualRearOfTrainCurrent.Track = OBATP.m_route.Entry_Track; ;// startTrack;startTrack; 

                            OBATP.ActualFrontOfTrainCurrent.Location = (OBATP.ActualFrontOfTrainCurrent.Track.Stopping_Point_Positon_2);//FrontOfTrainCurrentTrack.Track_Start_Position + trainLength;
                            OBATP.ActualRearOfTrainCurrent.Location = OBATP.ActualFrontOfTrainCurrent.Location - OBATP.Vehicle.TrainLength;
                        }
                            


                     

                    

                    

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

                        MainForm.m_mf.m_allTrains.Add(OBATP);
                        //MainForm.m_mf.m_bindingSourceTrains.ResetBindings(false);

                        OBATP.RequestStartProcess();
                    }



                }
            }

         
        }
        public void ATS_TO_OBATO_MessageInComing(Enums.Train_ID train_ID, ATS_TO_OBATOAdapter ATS_TO_OBATOAdapter)
        {
            if (this.Vehicle.TrainID == train_ID)
            {



            }
        }



    }
}
