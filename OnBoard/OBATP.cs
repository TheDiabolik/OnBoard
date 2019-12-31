using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnBoard
{
    public class OBATP : IWSATP_TO_OBATPMessageWatcher, IATS_TO_OBATO_InitMessageWatcher, IATS_TO_OBATO_MessageWatcher
    {
        #region Gloval

        bool DwellTimeFinished = false;



        string openDoorTrackName;

        public Enums.DoorStatus DoorStatus { get; set; }
        public Enums.OBATP_ID OBATP_ID { get; set; }

        //public ThreadSafeList<ushort> VirtualOccupationTracks = new ThreadSafeList<ushort>();
        //public ThreadSafeList<ushort> FootPrintTracks = new ThreadSafeList<ushort>();

        public static TrainOnTracks TrainOnTracks = new TrainOnTracks();

        public ushort[] footPrintTracks = new ushort[15];

        public ushort[] virtualOccupationTracks = new ushort[20];
        /// <summary>
        /// Trenin gittiği toplam mesafe (m)
        /// </summary>
        public double TotalTrainDistance { get; set; }

        /// <summary>
        /// aracın structı
        /// </summary>
        public Vehicle Vehicle = new Vehicle();

        public TrackWithPosition FrontOfTrainTrackWithFootPrint = new TrackWithPosition();
        public TrackWithPosition FrontOfTrainVirtualOccupation = new TrackWithPosition();

        public TrackWithPosition RearOfTrainTrackWithFootPrint = new TrackWithPosition();
        public TrackWithPosition RearOfTrainVirtualOccupation = new TrackWithPosition(); 
       
        #endregion 

        internal volatile bool m_shouldStop;

        /// <summary>
        /// Trenin hata payı olmadan net konum bilgisidir. (cm)
        /// </summary>
        public double ActualFrontOfTrainCurrentLocation { get; set; }
        public double ActualRearOfTrainCurrentLocation { get; set; } 
        /// <summary>
        /// Trenin toplam rota metrajı içerisindeki ön konum bilgisi (cm)
        /// </summary>
        public double FrontOfTrainLocationWithFootPrintInRoute { get; set; }
        public double RearOfTrainLocationWithFootPrintInRoute { get; set; }

        public Track FrontOfTrainCurrentTrack { get; set; }
        public Track RearOfTrainNextTrack { get; set; }

        /// <summary>
        /// Tren önüne eklenen hata payı (cm)
        /// </summary>
        public double FrontOfTrainLocationFault { get; set; }
        /// <summary>
        /// Tren arkasına eklenen hata payı (cm)
        /// </summary>
        public double RearOfTrainLocationFault { get; set; }

      
        public Track FrontOfTrainNextTrack { get; set; }
        public Track RearOfTrainCurrentTrack { get; set; }



        //public Track RearOfTrainCurrentTrack { get; set; }



        private Track denemeRearOfTrainCurrentTrack;
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


    
        public Track MovementAuthorityTrack { get; set; }

        /// <summary>
        /// Trene ait yön bilgisi, 1 veya 2 şeklinde belirtilir.
        /// </summary>
        public Enums.Direction Direction { get; set; }
        public Enums.Direction RearDirection { get; set; }

      
        public System.Timers.Timer m_doorTimer;
        Stopwatch m_stopwatch;
       
        public int DoorTimerCounter { get; set; }
        double OperationTime = 0.2;

       //public List<Track> m_route;
        public Route m_route; 
      

        public OBATP()
        {

        }

        public OBATP(Enums.Train_ID trainID, string name, double maxTrainAcceleration, double maxTrainDeceleration, int maxTrainSpeedKMH, double trainLength, Route route)
        {
            //tren parametreleri set ediliyor
            Vehicle.MaxTrainAcceleration = maxTrainAcceleration * 100;
            Vehicle.MaxTrainDeceleration = maxTrainDeceleration * 100;
            Vehicle.MaxTrainSpeedKMH = maxTrainSpeedKMH;
            Vehicle.TrainLength = trainLength;
            Vehicle.TrainIndex = (int)trainID+1;
            Vehicle.TrainID = trainID;
            Vehicle.TrainName = name;

            OBATP_ID = GetOBATP_ID(name);




            FrontOfTrainLocationFault = 200;
            RearOfTrainLocationFault = 0;

            FrontOfTrainCurrentTrack = route.Entry_Track;// startTrack;
            RearOfTrainCurrentTrack = route.Entry_Track;// startTrack;startTrack;


            //RealFrontCurrentLocation = 11200;
            ActualFrontOfTrainCurrentLocation = trainLength;//FrontOfTrainCurrentTrack.Track_Start_Position + trainLength;
            ActualRearOfTrainCurrentLocation = 1;


            DoorStatus = Enums.DoorStatus.Close;
            DoorTimerCounter = 0;
            //DwellTimeFinished = false;
            m_stopwatch = new Stopwatch();
            m_doorTimer = new System.Timers.Timer();
            m_doorTimer.Interval = 10000;
            m_doorTimer.Elapsed += OnTrainDoorsOpenedEvent;


            m_route = route;

            //DwellTime = 20;

  
            MainForm.m_trainMovement.AddWatcher(MainForm.m_mf);

            MainForm.m_trainMovement.TrainMovementRouteCreated(route);



          

        }


        public void RequestStartProcess()
        {
            m_shouldStop = false;


            Thread thread = new Thread(new ParameterizedThreadStart(StartProcess));
            thread.IsBackground = true;
            thread.Start();

            //StartProcess(null);
        }
        public void RequestStopProcess()
        {
            m_shouldStop = true;
        }



        public void StartProcess(object o)
        {

            
            while (!m_shouldStop)
            {

                if (Math.Round(FrontOfTrainLocationWithFootPrintInRoute) <= m_route.Length)
                {
                    //routeCompleted = true;

                   
                    Direction = FindDirection(m_route.Route_Tracks, Direction);


                    //trenin arkası ve önü için bir sonraki tracki verir
                    FrontOfTrainNextTrack = FindNextTrack(FrontOfTrainCurrentTrack, Direction);
                    RearOfTrainNextTrack = FindNextTrack(RearOfTrainCurrentTrack, Direction); 

                    //trenin frenleme mesafesi hesaplaması
                    Vehicle.BrakingDistance = CalculateBrakingDistance(this.Vehicle.MaxTrainDeceleration, this.Vehicle.CurrentTrainSpeedCMS);


                    //trenin konumundaki hata payını bulma
                    this.FrontOfTrainLocationWithFootPrintInRoute = FindPositionFootPrintFront(FrontOfTrainCurrentTrack, Direction, m_route.Route_Tracks);
                    //this.RearOfTrainLocationWithFootPrintInRoute = FindPositionFootPrintRear(RearOfTrainCurrentTrack, RearDirection, m_route.Route_Tracks);
                    this.RearOfTrainLocationWithFootPrintInRoute = FindPositionFootPrintRear(RearOfTrainCurrentTrack, Direction, m_route.Route_Tracks);

                    //sanal meşguliyet hesaplama
                    this.FrontOfTrainVirtualOccupation = FindFrontVirtualOccupation(m_route.Route_Tracks, Direction, MainForm.allTracks);
                    //this.RearOfTrainVirtualOccupation = FindRearVirtualOccupation(m_route.Route_Tracks, RearDirection, MainForm.allTracks);
                    this.RearOfTrainVirtualOccupation = FindRearVirtualOccupation(m_route.Route_Tracks, Direction, MainForm.allTracks);


                    double? isTargetSpeed = FindTargetSpeed(m_route.Route_Tracks, FrontOfTrainCurrentTrack, RearOfTrainCurrentTrack, this.Vehicle) ;

                    if (isTargetSpeed.HasValue)
                        this.Vehicle.TargetSpeedKMH = isTargetSpeed.Value; 
                 


                    this.Vehicle.CurrentAcceleration = ManageAcceleration(this.Vehicle.CurrentTrainSpeedCMS, this.Vehicle, FrontOfTrainCurrentTrack.MaxTrackSpeedCMS);


                    this.Vehicle.CurrentTrainSpeedCMS = CalculateSpeed(this.Vehicle);
                    

                    Tuple<double, Track> current = CalculateLocation(FrontOfTrainCurrentTrack, FrontOfTrainNextTrack, Direction, Vehicle, ActualFrontOfTrainCurrentLocation);
                    ActualFrontOfTrainCurrentLocation = current.Item1;
                    FrontOfTrainCurrentTrack = current.Item2;


                    //Tuple<double, Track> rearCurrent = CalculateLocation(RearOfTrainCurrentTrack, RearOfTrainNextTrack, RearDirection, Vehicle, ActualRearOfTrainCurrentLocation);
                    Tuple<double, Track> rearCurrent = CalculateLocation(RearOfTrainCurrentTrack, RearOfTrainNextTrack, Direction, Vehicle, ActualRearOfTrainCurrentLocation);
                    ActualRearOfTrainCurrentLocation = rearCurrent.Item1;
                    RearOfTrainCurrentTrack = rearCurrent.Item2;



                    //hareket listesinden silmek için deneme yapıldığı kısım burada başlıyor
                    DenemeRearOfTrainCurrentTrack = RearOfTrainCurrentTrack;


                    footPrintTracks = FindTrackRangeInAllTracks(FrontOfTrainTrackWithFootPrint.Track, RearOfTrainTrackWithFootPrint.Track, MainForm.allTracks);  
                    virtualOccupationTracks = FindTrackRangeInAllTracks(FrontOfTrainVirtualOccupation.Track, RearOfTrainVirtualOccupation.Track, MainForm.allTracks);  

                 

                    //arayüzde göstermek için liste
                    TrainOnTracks.VirtualOccupationTracks.Clear();
                    TrainOnTracks.FootPrintTracks.Clear();

                    foreach (ushort item in virtualOccupationTracks)
                        TrainOnTracks.VirtualOccupationTracks.Add(MainForm.allTracks.Find(x => x.Track_ID == item));

                    foreach (ushort item in footPrintTracks)
                        TrainOnTracks.FootPrintTracks.Add(MainForm.allTracks.Find(x => x.Track_ID == item));



                    TrainOnTracks.ActualLocationTracks.Clear();

                  ushort[] actual = FindTrackRangeInAllTracks(FrontOfTrainCurrentTrack, RearOfTrainCurrentTrack, MainForm.allTracks);


                    foreach (ushort item in actual)
                        TrainOnTracks.ActualLocationTracks.Add(MainForm.allTracks.Find(x => x.Track_ID == item));



                fdgkldfgkljdlfkgf = m_route.Route_Tracks.Except(TrainOnTracks.ActualLocationTracks).ToList();

                   
                    MainForm.m_trainMovement.TrainMovementCreated(this);

                    Thread.Sleep(10);
                }

            }


        }


        public List<Track> fdgkldfgkljdlfkgf = new List<Track>();


        //şimdilik rotaları parametrik olarak alıyor
        //sonrasında yapının şekillenmesine göre ayarlanacak
        public Track FindNextTrack(Track track, Enums.Direction direction)// rota zaten bütün trackleri içermiyor mu? burayı inceleyip anlamak lazım
        {
            // Tekrar bir gözden geçir
            Track nextTrack = null;

            if (direction == Enums.Direction.Right)
            { 
                nextTrack = MainForm.allTracks.Find(x => x.Track_ID == track.Track_Connection_Exit_1);
            }
            else if (direction == Enums.Direction.Left)
            { 
                nextTrack = MainForm.allTracks.Find(x => x.Track_ID == track.Track_Connection_Entry_1); 
            }
             

            return nextTrack;

        }


        public Enums.Direction FindDirection(List<Track> routeTracks, Enums.Direction gloDirection)
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
        public double FindPositionFootPrintFront(Track track, Enums.Direction direction, List<Track> route)
        {
            double frontTrainLocationInRouteWithFootPrint = -1;

            // FrontTrainLocationFault değeri belirli aralıklarla artacak hata payı ortaya çıkacak, RFID tag okununca sıfırlanacak 
            if (route.Count != 0)
            {
                if (direction == Enums.Direction.Right)
                {
                    // Kendi Track'ini aştığında
                    if (ActualFrontOfTrainCurrentLocation + FrontOfTrainLocationFault >= track.Track_End_Position) // yeni trackin içindeki konumu
                                                                                                                   //if (ActualFrontOfTrainCurrentLocation + FrontOfTrainLocationFault >= track.Track_Length) // yeni trackin içindeki konumu
                    {
                        FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrentLocation + FrontOfTrainLocationFault - track.Track_End_Position;
                        //FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrentLocation + FrontOfTrainLocationFault - track.Track_Length;

                        FrontOfTrainTrackWithFootPrint.Track = FrontOfTrainNextTrack;
                    }
                    else
                    {
                        FrontOfTrainTrackWithFootPrint.Track = track;
                        //böle değil
                        // FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrentLocation + FrontOfTrainLocationFault;
                        //böle olmalısın
                        FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrentLocation + FrontOfTrainLocationFault;// + track.Track_Start_Position;
                    }

                    //burası incelenecek çünkü başlangıç pozisyonları sıfırdan başlamıyor
                    frontTrainLocationInRouteWithFootPrint = FrontOfTrainTrackWithFootPrint.Track.StartPositionInRoute + FrontOfTrainTrackWithFootPrint.Location;
                    //frontTrainLocationInRouteWithFootPrint =  FrontOfTrainTrackWithFootPrint.Location;
                }
                else if (direction == Enums.Direction.Left)
                {
                    // Kendi Track'ini aşmışsa
                    if (ActualFrontOfTrainCurrentLocation - FrontOfTrainLocationFault <= track.Track_Start_Position)
                    {
                        FrontOfTrainTrackWithFootPrint.Track = FrontOfTrainNextTrack;
                        FrontOfTrainTrackWithFootPrint.Location  = FrontOfTrainTrackWithFootPrint.Track.Track_End_Position + (ActualFrontOfTrainCurrentLocation - FrontOfTrainLocationFault);
                    }
                    else
                    {
                        FrontOfTrainTrackWithFootPrint.Track = track;
                        FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrentLocation - FrontOfTrainLocationFault;
                    }

                    //burası incelenecek çünkü başlangıç pozisyonları sıfırdan başlamıyor
                    frontTrainLocationInRouteWithFootPrint = FrontOfTrainTrackWithFootPrint.Track.StartPositionInRoute + FrontOfTrainTrackWithFootPrint.Track.Track_End_Position - FrontOfTrainTrackWithFootPrint.Location;
                }
            }

            return frontTrainLocationInRouteWithFootPrint;

        }

        public double FindPositionFootPrintRear(Track track, Enums.Direction direction, List<Track> route) //bu incelenecek
        {
            double rearTrainLocationInRouteWithFootPrint = 0;
            // FrontTrainLocationFault değeri belirli aralıklarla artacak hata payı ortaya çıkacak, RFID tag okununca sıfırlanacak

            if (route.Count != 0)
            {
                if (direction == Enums.Direction.Right)
                {


                    if (ActualRearOfTrainCurrentLocation + RearOfTrainLocationFault >= track.Track_End_Position)
                    {
                        Track rearFootPrintTrack = route.Find(x => x == track);

                        //if (rearFootPrintTrack != null)
                        {
                            rearTrainLocationInRouteWithFootPrint = (ActualRearOfTrainCurrentLocation + RearOfTrainLocationFault) - track.Track_End_Position;


                            RearOfTrainTrackWithFootPrint.Location = (ActualRearOfTrainCurrentLocation + RearOfTrainLocationFault) - track.Track_End_Position;
                            RearOfTrainTrackWithFootPrint.Track = rearFootPrintTrack;

                        }
                    }
                    else
                    {
                        RearOfTrainTrackWithFootPrint.Track = track;
                        RearOfTrainTrackWithFootPrint.Location = ActualRearOfTrainCurrentLocation + RearOfTrainLocationFault;




                    }

                    //rearTrainLocationInRouteWithFootPrint = ActualRearCurrentLocation + RearOfTrainLocationFault;

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
                    if (ActualRearOfTrainCurrentLocation + RearOfTrainLocationFault > track.Track_End_Position)
                    { 
                        Track rearFootPrintTrack = route.Find(x => x == track);

                        if (rearFootPrintTrack != null)
                        {
                            rearTrainLocationInRouteWithFootPrint = (ActualRearOfTrainCurrentLocation + RearOfTrainLocationFault) - track.Track_End_Position;
                            RearOfTrainTrackWithFootPrint.Track = rearFootPrintTrack;
                        }
                    }
                    else
                    {
                        RearOfTrainTrackWithFootPrint.Track = track;
                        rearTrainLocationInRouteWithFootPrint = ActualRearOfTrainCurrentLocation + RearOfTrainLocationFault;
                    }
                }
            }

            return rearTrainLocationInRouteWithFootPrint;
        }



        /// <summary>
        /// Trenin sanal meşguliyet konumunu hesaplar.
        /// </summary>
        public TrackWithPosition FindFrontVirtualOccupation(List<Track> routeTracks, Enums.Direction direction, List<Track> allTracks)
        {
            TrackWithPosition frontOfTrainVirtualOccupation = new TrackWithPosition();


            if (routeTracks.Count != 0)
            {
                if (direction == Enums.Direction.Right)
                {
                    double frontOfTrainVirtualOcc = this.ActualFrontOfTrainCurrentLocation + this.FrontOfTrainLocationFault + this.Vehicle.BrakingDistance;

                    //tren başladığı trackin içindeyse
                    //if (frontOfTrainVirtualOccupation <= FrontOfTrainCurrentTrack.Track_End_Position)
                    if (frontOfTrainVirtualOcc <= FrontOfTrainCurrentTrack.Track_Length)
                    {
                        frontOfTrainVirtualOccupation.Track = FrontOfTrainCurrentTrack;
                        frontOfTrainVirtualOccupation.Location = frontOfTrainVirtualOcc;
                    }
                    else //trenin sanal meşguliyetli uzunluk bilgisi ile yeni tracke geçiyor
                    {

                        //double distanceToFinishCurrentTrack = FrontOfTrainCurrentTrack.Track_End_Position - ActualFrontOfTrainCurrentLocation;
                        double distanceToFinishCurrentTrack = FrontOfTrainCurrentTrack.Track_Length - ActualFrontOfTrainCurrentLocation;
                        double totalMovementDistance = FrontOfTrainNextTrack.Track_Length + distanceToFinishCurrentTrack;
                        double breakingDistanceWithFault = FrontOfTrainLocationFault + Vehicle.BrakingDistance;

                        //hata payı olmadan gidilmesi gereken toplam mesafe DEN hata payı ekli fren mesafesi büyük mü? yani tren bu anda frene bassa durmaz..
                        //frenleyince durma mesafesi kalan mesafeden büyükse
                        if (breakingDistanceWithFault >= totalMovementDistance)
                        {
                            //double de = virtualOccupationFrontTrackLocation - CurrentTrack.TrackEndPosition - NextTrack.TrackEndPosition;
                            //double totalMovementDistanceWithVirtualOccupation = frontOfTrainVirtualOccupation - (FrontOfTrainCurrentTrack.Track_End_Position + FrontOfTrainNextTrack.Track_End_Position);
                            double totalMovementDistanceWithVirtualOccupation = frontOfTrainVirtualOcc - (FrontOfTrainCurrentTrack.Track_Length + FrontOfTrainNextTrack.Track_Length);

                            //trenin sanal meşguliyet uzunluğu kalan tracklerim mesadesinden fazlaysa
                            //trenin gidebileceği kalan mesafe
                            if (totalMovementDistanceWithVirtualOccupation >= 0)
                                frontOfTrainVirtualOccupation.Location = totalMovementDistanceWithVirtualOccupation;
                            else
                                frontOfTrainVirtualOccupation.Location = 0;

                            //frenleme mesafesi current ve nexttrack toplamından daha büyükse nexttrackten bir sonraki tracki next track olarak atıyoruz
                            int nextTrackID = FrontOfTrainNextTrack.Track_Connection_Exit_1;
                            frontOfTrainVirtualOccupation.Track = MainForm.allTracks.Find(x => x.Track_ID == nextTrackID);

                        }
                        else
                        {
                            //double passingDistanceToCurrentTrack = frontOfTrainVirtualOccupation - FrontOfTrainCurrentTrack.Track_End_Position;
                            double passingDistanceToCurrentTrack = frontOfTrainVirtualOcc - FrontOfTrainCurrentTrack.Track_Length;

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
                    double virtualOccupationFrontTrackLocation = this.ActualFrontOfTrainCurrentLocation - this.FrontOfTrainLocationFault - this.Vehicle.BrakingDistance;

                    //tren başladığı trackın içindeyse
                    if (virtualOccupationFrontTrackLocation >= FrontOfTrainCurrentTrack.Track_Start_Position)
                    {
                        frontOfTrainVirtualOccupation.Track = this.FrontOfTrainCurrentTrack;
                        frontOfTrainVirtualOccupation.Location = virtualOccupationFrontTrackLocation;
                    }
                    else
                    {
                        double breakingDistanceWithFault = FrontOfTrainLocationFault + Vehicle.BrakingDistance;
                        double totalDistance = FrontOfTrainNextTrack.Track_Length + ActualFrontOfTrainCurrentLocation;

                        if (virtualOccupationFrontTrackLocation < 0 && (breakingDistanceWithFault >= totalDistance))
                        {
                            //frenleme mesafesi current ve nexttrack toplamından daha büyükse nexttrackten bir sonraki tracki next track olarak atıyoruz
                            int nextTrackID = FrontOfTrainNextTrack.Track_Connection_Entry_1;
                            frontOfTrainVirtualOccupation.Track = allTracks.Find(x => x.Track_ID == nextTrackID);//null kontrolü eklemek gerekebilir

                            frontOfTrainVirtualOccupation.Location = frontOfTrainVirtualOccupation.Track.Track_End_Position - (breakingDistanceWithFault - totalDistance);
                        }
                        else
                        {
                            frontOfTrainVirtualOccupation.Track = FrontOfTrainNextTrack;
                            frontOfTrainVirtualOccupation.Location = frontOfTrainVirtualOccupation.Track.Track_End_Position + virtualOccupationFrontTrackLocation;
                        }
                    }

                }


            }


            return frontOfTrainVirtualOccupation;


        }



        /// <summary>
        /// Trenin sanal meşguliyet konumunu hesaplar.
        /// </summary>
        public TrackWithPosition FindRearVirtualOccupation(List<Track> routeTracks, Enums.Direction rearDirection, List<Track> allTracks)
        {
            TrackWithPosition rearOfTrainVirtualOccupation = new TrackWithPosition();


            if (routeTracks.Count != 0)
            {
                int rearCurrentTrackIndex = routeTracks.IndexOf(RearOfTrainCurrentTrack);

                if (rearDirection == Enums.Direction.Right)
                {
                    double realRearCurrentLocation = this.ActualRearOfTrainCurrentLocation - this.RearOfTrainLocationFault;

                    //int sddsf = RouteTracks.IndexOf(RearCurrentTrack); 
                    //Track sddsfsdsd = RouteTracks.Find(x => x == RearCurrentTrack); 

                    if (realRearCurrentLocation < RearOfTrainCurrentTrack.Track_Start_Position)
                        //if (realRearCurrentLocation < RearOfTrainCurrentTrack.StopPositionInRoute)
                    {
                        if (rearCurrentTrackIndex > 0)//trenin arkası ikinci trackte ise
                        {
                            rearOfTrainVirtualOccupation.Track = routeTracks[rearCurrentTrackIndex - 1];
                            rearOfTrainVirtualOccupation.Location = rearOfTrainVirtualOccupation.Track.Track_End_Position + realRearCurrentLocation;
                        }
                    }
                    else
                    {
                        rearOfTrainVirtualOccupation.Track = RearOfTrainCurrentTrack;
                        rearOfTrainVirtualOccupation.Location = realRearCurrentLocation;
                    }
                }
                else if (rearDirection == Enums.Direction.Left)
                {
                    double rearCurrentLocationWithFault = this.ActualRearOfTrainCurrentLocation + this.RearOfTrainLocationFault;

                    if (rearCurrentLocationWithFault >= RearOfTrainCurrentTrack.Track_End_Position)
                    {
                        if (rearCurrentTrackIndex > 0)
                        {
                            rearOfTrainVirtualOccupation.Location = rearCurrentLocationWithFault - RearOfTrainCurrentTrack.Track_End_Position;
                            rearOfTrainVirtualOccupation.Track = routeTracks[rearCurrentTrackIndex - 1];
                        }

                    }
                    else
                    {
                        rearOfTrainVirtualOccupation.Track = RearOfTrainCurrentTrack;
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


            // Gitme izni yoksa tren durmalı
            //if (isExceed == Enums.Route.Out || DoorStatus == Enums.DoorStatus.Open || !string.IsNullOrEmpty(frontCurrentTrack.Station_Name) || (!string.IsNullOrEmpty(frontCurrentTrack.Station_Name) && frontCurrentTrack.Station_Name = && DoorStatus == Enums.DoorStatus.Close))//|| DoorStatus == Enums.DoorStatus.Open)
           if (isExceed == Enums.Route.Out)// || ManageTrainDoors() )// || DoorStatus == Enums.DoorStatus.Open || !string.IsNullOrEmpty(FrontOfTrainCurrentTrack.Station_Name))
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
                    int remainingTracks = m_route.Route_Tracks.Count - frontCurrentTrackIndex;

                    List<Track> restOfMovementTracks = m_route.Route_Tracks.GetRange(frontCurrentTrackIndex, remainingTracks);


                    Stack<Track> reverseRestOfMovementTracks = new Stack<Track>(restOfMovementTracks.ToList());



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

                            //if (!string.IsNullOrEmpty(item.Station_Name))// && (item.Station_Name != openDoorTrackName))// DwellTimeFinished == false)// && (openDoorTrack != item.Station_Name))
                            //{

                            //    //this.Vehicle.CurrentAcceleration = (double)StopTrain(vehicle, item.StopPositionInRoute);

                            //    //targetSpeedKM = reverseTracks.MaxTrackSpeedKMH;
                            //    targetSpeedKM = 0;
                            //    //StopTrain(Vehicle);
                            //    return targetSpeedKM;
                            //    //return targetSpeedKM;
                            //}

                          

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


                            if (FrontOfTrainLocationWithFootPrintInRoute  + Vehicle.BrakingDistance >= item.StopPositionInRoute )
                            {
                                if (!string.IsNullOrEmpty(item.Station_Name) && item.Station_Name != openDoorTrackName)
                                {
                                    targetSpeedKM = 0;


                                    //break;
                                    return targetSpeedKM;
                                }
                            }

                              

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
                                if (item.StartPositionInRoute - this.ActualFrontOfTrainCurrentLocation <= distanceToArrangeTheSpeed + (Math.Abs(differenceBetweenTrainAndTrackSpeed) * time / 2))
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







                            }
                            else if (vehicle.CurrentTrainSpeedCMS > item.MaxTrackSpeedCMS) // burası end: ten sonraki koşul karşılaştırması
                            {
                                // O hıza gidebileceği mesafeye geldiyse
                                if (item.StartPositionInRoute - this.ActualFrontOfTrainCurrentLocation <= distanceToArrangeTheSpeed - (Math.Abs(differenceBetweenTrainAndTrackSpeed) * time / 2))
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
                double distanceToTarget;

                Track station = m_route.Route_Tracks.Find(x => (x == FrontOfTrainCurrentTrack) && !string.IsNullOrEmpty(x.Station_Name));

                if (station != null)
                {
                    distanceToTarget = station.StopPositionInRoute - FrontOfTrainLocationWithFootPrintInRoute;
                   

                }
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


        public Tuple<double, Track> CalculateLocation(Track track, Track nextTrack, Enums.Direction direction, Vehicle vehicle, double location)
        {
            Tuple<double, Track> returnValues;// = Tuple.Create<double, Track>[];

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
                    //location = location - track.Track_End_Position;
                    location = location - track.Track_Length;
                    track = nextTrack;
                }


            }
            else if (direction == Enums.Direction.Left)
            {
                location = location - (0.5 * (vehicle.CurrentTrainSpeedCMS + vehicle.PreviousSpeedCMS) * OperationTime);

                if (location <= track.Track_Start_Position - 0.1)
                {
                    track = nextTrack;
                    location = track.Track_End_Position + location;
                }

            }

            currentLocation = location;

            //bu değişecek
            //CurrentTrack = track;


            returnValues = Tuple.Create<double, Track>(currentLocation, track);


            TotalTrainDistance += (0.5 * (this.Vehicle.CurrentTrainSpeedCMS + this.Vehicle.PreviousSpeedCMS) * OperationTime);

            return returnValues;


        }

   
        /// <summary>
        /// Trenin rota sınırını durma mesafesiyle birlikte aşıp aşmadığının kontrolü yapılır.
        /// </summary>
        public Enums.Route IsRouteLimitExceeded()
        {
            if ((FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance  < m_route.Length))
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


            //if (!string.IsNullOrEmpty(FrontOfTrainCurrentTrack.Station_Name) && FrontOfTrainCurrentTrack.Station_Name == openDoorTrackName)
            //{
            //    return DoorStatus;
            //}


            if (!string.IsNullOrEmpty(FrontOfTrainCurrentTrack.Station_Name) && !m_doorTimer.Enabled && (openDoorTrackName != FrontOfTrainCurrentTrack.Station_Name) && Vehicle.CurrentTrainSpeedCMS == 0)
            {

                //if (Vehicle.CurrentTrainSpeedCMS == 0)
                {
                    DoorStatus = OpenTrainDoors(DoorStatus);

                    if (DoorStatus == Enums.DoorStatus.Open)
                    {
                        m_doorTimer.Start();
                    }

                    //openDoorTrackName = FrontOfTrainCurrentTrack.Station_Name;
                    Debug.WriteLine("kapı açıldı");

                    //m_stopwatch.Start();


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
            //Vehicle.DoorTimerCounter++;

            DoorStatus = CloseTrainDoors(DoorStatus);
            m_doorTimer.Stop();
            DwellTimeFinished = true;

            openDoorTrackName = FrontOfTrainCurrentTrack.Station_Name;

            Debug.WriteLine("kapı kapandı");
          
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

            List<Track> findMinSpeed = routeTracks.Where((element, index) => (index <= frontTrackIndex) && (index >= rearTrackIndex)).ToList();

            double minSpeed = 0;

            minSpeed = findMinSpeed.Min(x => x.MaxTrackSpeedKMH);


            return minSpeed;
        }



        public static ushort[] FindTrackRangeInAllTracks(Track frontTrack, Track rearTrack, List<Track> allTracks)
        {
            ushort[] trackRangeList = new ushort[15];

            int frontTrackIndex = allTracks.FindIndex(x => x == frontTrack);
            int rearTrackIndex = allTracks.FindIndex(x => x == rearTrack);

            if (frontTrackIndex != -1 && rearTrackIndex != -1)
                trackRangeList = allTracks.Where((element, index) => (index <= frontTrackIndex) && (index >= rearTrackIndex)).Select(x => (ushort)x.Track_ID).ToList().ToArray();
            else if (frontTrackIndex != -1 && rearTrackIndex == -1)
                trackRangeList = allTracks.Where((element, index) => (index <= frontTrackIndex) && (index >= frontTrackIndex-1)).Select(x => (ushort)x.Track_ID).ToList().ToArray();

            return trackRangeList;
        }





        public Enums.OBATP_ID GetOBATP_ID(string trainName)
        {
            Enums.OBATP_ID OBATP_ID  = Enums.OBATP_ID.Train1;

            switch (trainName)
            {
                case "Train1" :
                    {
                        OBATP_ID = Enums.OBATP_ID.Train1;
                        break;
                    }
                case "Train2":
                    {
                        OBATP_ID = Enums.OBATP_ID.Train2;
                        break;
                    }
                case "Train3":
                    {
                        OBATP_ID = Enums.OBATP_ID.Train3;
                        break;
                    }
                case "Train4":
                    {
                        OBATP_ID = Enums.OBATP_ID.Train4;
                        break;
                    }
                case "Train5":
                    {
                        OBATP_ID = Enums.OBATP_ID.Train5;
                        break;
                    }
                case "Train6":
                    {
                        OBATP_ID = Enums.OBATP_ID.Train6;
                        break;
                    }
                case "Train7":
                    {
                        OBATP_ID = Enums.OBATP_ID.Train7;
                        break;
                    }
                case "Train8":
                    {
                        OBATP_ID = Enums.OBATP_ID.Train8;
                        break;
                    }
                case "Train9":
                    {
                        OBATP_ID = Enums.OBATP_ID.Train9;
                        break;
                    }
                case "Train10":
                    {
                        OBATP_ID = Enums.OBATP_ID.Train10;
                        break;
                    }
            }

            return OBATP_ID;

        }


        //waysidedan gelen haraket yetkisi tracklerini işleyen metot
        public void WSATP_TO_OBATPMessageInComing(Enums.OBATP_ID OBATP_ID, WSATP_TO_OBATPAdapter WSATP_TO_OBATPAdapter)
        {
            if (this.OBATP_ID == OBATP_ID)
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


                m_route = Route.CreateNewRoute(m_route.Route_Tracks[0].Track_ID, m_route.Route_Tracks[m_route.Route_Tracks.Count - 1].Track_ID, MainForm.allTracks);

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

        public void ATS_TO_OBATO_InitMessageInComing(Enums.OBATP_ID OBATP_ID, ATS_TO_OBATO_InitAdapter ATS_TO_OBATO_InitAdapter)
        {
            if (this.OBATP_ID == OBATP_ID)
            { 



            }
        }
        public void ATS_TO_OBATO_MessageInComing(Enums.OBATP_ID OBATP_ID, ATS_TO_OBATOAdapter ATS_TO_OBATOAdapter)
        {
            if (this.OBATP_ID == OBATP_ID)
            {



            }
        }

    }
}
