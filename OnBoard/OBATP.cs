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
    public class OBATP 
    {
        #region Gloval

        public  Enums.DoorStatus DoorStatus { get; set; }


        //public ThreadSafeList<ushort> VirtualOccupationTracks = new ThreadSafeList<ushort>();
        //public ThreadSafeList<ushort> FootPrintTracks = new ThreadSafeList<ushort>();

        public static TrainOnTracks TrainOnTracks = new TrainOnTracks();


        /// <summary>
        /// Trenin gittiği toplam mesafe (m)
        /// </summary>
        public double TotalTrainDistance { get; set; }

        /// <summary>
        /// aracın structı
        /// </summary>
        public Vehicle Vehicle = new Vehicle();

        TrackWithPosition FrontOfTrainTrackWithFootPrint = new TrackWithPosition();
        public TrackWithPosition FrontOfTrainVirtualOccupation = new TrackWithPosition();

        TrackWithPosition RearOfTrainTrackWithFootPrint = new TrackWithPosition(); 
        TrackWithPosition RearOfTrainVirtualOccupation = new TrackWithPosition(); 
       
        #endregion 

        internal volatile bool m_shouldStop; 

        /// <summary>
        /// Trenin hata payı olmadan net konum bilgisidir. (cm)
        /// </summary>
      
        public double ActualRearCurrentLocation { get; set; } 
        /// <summary>
        /// Trenin toplam rota metrajı içerisindeki ön konum bilgisi (cm)
        /// </summary>
        public double FrontOfTrainLocationWithFootPrintInRoute { get; set; } 
        public double ActualFrontOfTrainCurrentLocation { get; set; }
        public Track FrontOfTrainCurrentTrack { get; set; } 

        public double RearOfTrainLocationWithFootPrintInRoute { get; set; } 
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
        public Track RearOfTrainNextTrack { get; set; }
        public Track MovementAuthorityTrack { get; set; }

        /// <summary>
        /// Trene ait yön bilgisi, 1 veya 2 şeklinde belirtilir.
        /// </summary>
        public Enums.Direction Direction { get; set; }
        public Enums.Direction RearDirection { get; set; }

        public int DwellTime { get; set; }
        public System.Timers.Timer m_doorTimer;
        Stopwatch m_stopwatch;
        public bool DwellTimeFinished { get; set; }
        public int DoorTimerCounter { get; set; }
        double OperationTime = 0.2;

       //public List<Track> m_route;
        public Route m_route;

        
        //List<Track> allTracks;
        public OBATP()
        {
            ////tren parametreleri set ediliyor
            //Vehicle.MaxTrainAcceleration = 0.8 * 100;
            //Vehicle.MaxTrainDeceleration = 1.1 * 100;
            //Vehicle.MaxTrainSpeedKMH = 80;
            //Vehicle.TrainLength = 11200;
            //Vehicle.TrainIndex = 1;

            //DataTable dt = FileOperation.ReadTrackTableInExcel();
            //allTracks = Track.AllTracks(dt);


            //Track startTrack = allTracks.Find(x => x.Track_ID == 10101);
            //m_route = Route.CreateNewRoute(startTrack, 10201, allTracks);

            //FrontTrainLocationFault = 200;
            ////RearTrainLocationFault = 200;

            //FrontCurrentTrack = startTrack;
            //RearCurrentTrack = startTrack;
            //RealFrontCurrentLocation = 11200;
            //RealRearCurrentLocation = 0;

            
            //MainForm.m_trainMovement.AddWatcher(MainForm.m_mf);

            //MainForm.m_trainMovement.TrainMovementRouteCreated(m_route);
        }


        public OBATP(int index)
        {
            ////tren parametreleri set ediliyor
            //Vehicle.MaxTrainAcceleration = 0.8 * 100;
            //Vehicle.MaxTrainDeceleration = 1.1 * 100;
            //Vehicle.MaxTrainSpeedKMH = 80;
            //Vehicle.TrainLength = 11200;
            //Vehicle.TrainIndex = index;

            //DataTable dt = FileOperation.ReadTrackTableInExcel();
            //allTracks = Track.AllTracks(dt);


            //Track startTrack = allTracks.Find(x => x.Track_ID == 10101);
            //m_route = Route.CreateNewRoute(startTrack, 10103, allTracks);

            //FrontTrainLocationFault = 200;
            ////RearTrainLocationFault = 200;

            //FrontCurrentTrack = startTrack;
            //RearCurrentTrack = startTrack;
            //RealFrontCurrentLocation = 11200;
            //RealRearCurrentLocation = 0;


            //MainForm.m_trainMovement.AddWatcher(MainForm.m_mf);

            //MainForm.m_trainMovement.TrainMovementRouteCreated(m_route);
        }

        //public OBATP(int index, double maxTrainAcceleration, double maxTrainDeceleration, int maxTrainSpeedKMH, double trainLength, List<Track> route)
        //{
        //    //tren parametreleri set ediliyor
        //    Vehicle.MaxTrainAcceleration = maxTrainAcceleration * 100;
        //    Vehicle.MaxTrainDeceleration = maxTrainDeceleration * 100;
        //    Vehicle.MaxTrainSpeedKMH = maxTrainSpeedKMH;
        //    Vehicle.TrainLength = trainLength;
        //    Vehicle.TrainIndex = index;


        //    FrontTrainLocationFault = 200;
        //    //RearTrainLocationFault = 200;

        //    FrontCurrentTrack = route[0];// startTrack;
        //    RearCurrentTrack = route[0];// startTrack;startTrack;
        //    RealFrontCurrentLocation = 11200;
        //    RealRearCurrentLocation = 0;

        //    m_route = route;


        //    MainForm.m_trainMovement.AddWatcher(MainForm.m_mf);

        //    MainForm.m_trainMovement.TrainMovementRouteCreated(route);
        //}


        public OBATP(int index, string name, double maxTrainAcceleration, double maxTrainDeceleration, int maxTrainSpeedKMH, double trainLength, Route route)
        {
            //tren parametreleri set ediliyor
            Vehicle.MaxTrainAcceleration = maxTrainAcceleration * 100;
            Vehicle.MaxTrainDeceleration = maxTrainDeceleration * 100;
            Vehicle.MaxTrainSpeedKMH = maxTrainSpeedKMH;
            Vehicle.TrainLength = trainLength;
            Vehicle.TrainIndex = index;
            Vehicle.TrainName = name;


            FrontOfTrainLocationFault = 200;
            //RearTrainLocationFault = 200;

            FrontOfTrainCurrentTrack = route.Entry_Track;// startTrack;
            RearOfTrainCurrentTrack = route.Entry_Track;// startTrack;startTrack;
            
            
            //RealFrontCurrentLocation = 11200;
            ActualFrontOfTrainCurrentLocation =   trainLength;
            ActualRearCurrentLocation = 0;


            DoorStatus = Enums.DoorStatus.Close;
            DoorTimerCounter = 0;
            DwellTimeFinished = false;
            m_stopwatch = new Stopwatch();
            m_doorTimer = new System.Timers.Timer();
            m_doorTimer.Interval = 1000;
            m_doorTimer.Elapsed += OnTrainDoorsOpenedEvent;


            m_route = route;

            //double  routeLength = 0;
            //foreach (Track track in m_route.Route_Tracks)
            //{
            //    track.StartPositionInRoute = routeLength;
            //    track.StopPositionInRoute = routeLength + track.Track_Length;
            //    routeLength += track.Track_Length;
            //}
            //m_route.Length = routeLength;

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
                    FrontOfTrainNextTrack = FindNextTrack(FrontOfTrainCurrentTrack, Direction, m_route.Route_Tracks, MainForm.allTracks);
                    RearOfTrainNextTrack = FindNextTrack(RearOfTrainCurrentTrack, Direction, m_route.Route_Tracks, MainForm.allTracks); 

                    //trenin frenleme mesafesi hesaplaması
                    Vehicle.BrakingDistance = CalculateBrakingDistance(this.Vehicle.MaxTrainDeceleration, this.Vehicle.CurrentTrainSpeedCMS);


                    //trenin konumundaki hata payını bulma
                    this.FrontOfTrainLocationWithFootPrintInRoute = FindPositionFootPrintFront(FrontOfTrainCurrentTrack, Direction, m_route.Route_Tracks); 
                    this.RearOfTrainLocationWithFootPrintInRoute = FindPositionFootPrintRear(RearOfTrainCurrentTrack, RearDirection, m_route.Route_Tracks);

                    //sanal meşguliyet hesaplama
                    this.FrontOfTrainVirtualOccupation = FindFrontVirtualOccupation(m_route.Route_Tracks, Direction, MainForm.allTracks);
                    this.RearOfTrainVirtualOccupation = FindRearVirtualOccupation(m_route.Route_Tracks, RearDirection, MainForm.allTracks);


                    double? isTargetSpeed = FindTargetSpeed(m_route.Route_Tracks, FrontOfTrainCurrentTrack, RearOfTrainCurrentTrack, this.Vehicle) ;

                    if (isTargetSpeed.HasValue)
                        this.Vehicle.TargetSpeedKMH = isTargetSpeed.Value; 
                 


                    this.Vehicle.CurrentAcceleration = ManageAcceleration(this.Vehicle.CurrentTrainSpeedCMS, this.Vehicle, FrontOfTrainCurrentTrack.MaxTrackSpeedCMS);


                    this.Vehicle.CurrentTrainSpeedCMS = CalculateSpeed(this.Vehicle);
                    

                    Tuple<double, Track> current = CalculateLocation(FrontOfTrainCurrentTrack, FrontOfTrainNextTrack, Direction, Vehicle, ActualFrontOfTrainCurrentLocation);
                    ActualFrontOfTrainCurrentLocation = current.Item1;
                    FrontOfTrainCurrentTrack = current.Item2;


                    Tuple<double, Track> rearCurrent = CalculateLocation(RearOfTrainCurrentTrack, RearOfTrainNextTrack, RearDirection, Vehicle, ActualRearCurrentLocation);
                    ActualRearCurrentLocation = rearCurrent.Item1;
                    RearOfTrainCurrentTrack = rearCurrent.Item2;






                    //Message message = new Message();
                    //message.DS = (UInt32)Enums.Message.DS.DS;
                    //message.Size = (UInt32)Enums.Message.Size.Size; //(40 + sourceMessageData.Length); //
                    //message.ID = (UInt32)Enums.Message.ID.OBATO_TO_WSATO;
                    //message.DST = 61001;
                    //message.SRC = 25001;
                    //message.RTC = DateTimeExtensions.GetAllMiliSeconds();
                    //message.NO = 1;

                    //message.CRC = 47851476196393100;
                    ////OBATP_TO_WSATP içerik oluşturma
                    OBATP_TO_WSATP OBATP_TO_WSATP = new OBATP_TO_WSATP();
                    //OBATP_TO_WSATP.EmergencyBrakeApplied = Convert.ToByte(false);
                    //OBATP_TO_WSATP.TrainAbsoluteZeroSpeed = Convert.ToByte(false);
                    //OBATP_TO_WSATP.AllTrainDoorsClosedAndLocked = Convert.ToByte(false);
                    //OBATP_TO_WSATP.EnablePSD1 = Convert.ToByte(255);
                    //OBATP_TO_WSATP.EnablePSD2 = Convert.ToByte(255);
                    ////OBATP_TO_WSATP.ActiveATP = Convert.ToByte("\x02".Substring(2), NumberStyles.HexNumber); // Encoding.ASCII.GetBytes("\x02");
                    //OBATP_TO_WSATP.ActiveATP = Byte.Parse("0x02".Substring(2), NumberStyles.HexNumber); // Encoding.ASCII.GetBytes("\x02");
                    //OBATP_TO_WSATP.ActiveCab = Byte.Parse("0x03".Substring(2), NumberStyles.HexNumber);//Convert.ToByte("\x03");
                    //OBATP_TO_WSATP.TrainDirection = Convert.ToByte(Direction);
                    //OBATP_TO_WSATP.TrainCoupled = Byte.Parse("0x04".Substring(2), NumberStyles.HexNumber); //Convert.ToByte("\x04");
                    //OBATP_TO_WSATP.TrainIntegrity = Convert.ToByte(false);
                    //OBATP_TO_WSATP.TrainLocationDeterminationFault = Convert.ToByte(false);
                    //OBATP_TO_WSATP.TrackDatabaseVersionMajor = Convert.ToByte(1);
                    //OBATP_TO_WSATP.TrackDatabaseVersionMinor = Convert.ToByte(1);
                    //OBATP_TO_WSATP.TrainDerailment = Convert.ToByte(false);


                  

                    ushort[] footPrintTracks = FindTrackRangeInAllTracks(FrontOfTrainTrackWithFootPrint.Track, RearOfTrainTrackWithFootPrint.Track, MainForm.allTracks);


                   


                    Array.Copy(footPrintTracks, OBATP_TO_WSATP.FootPrintTrackSectionID, footPrintTracks.Length);


                    ushort[] virtualOccupationTracks = FindTrackRangeInAllTracks(FrontOfTrainVirtualOccupation.Track, RearOfTrainVirtualOccupation.Track, MainForm.allTracks);
                    Array.Copy(virtualOccupationTracks, OBATP_TO_WSATP.VirtualOccupancyTrackSectionID, virtualOccupationTracks.Length);


                    //this.FootPrintTracks.AddRange(footPrintTracks);


                    //TrainOnTracks.VirtualOccupationTracks.

                    //virtualOccupationTracks. 




                 


                    TrainOnTracks.VirtualOccupationTracks.Clear();
                    TrainOnTracks.FootPrintTracks.Clear();

                    TrainOnTracks.VirtualOccupationTracks.AddRange(virtualOccupationTracks);
                    TrainOnTracks.FootPrintTracks.AddRange(footPrintTracks);





                    //byte[] OBATP_TO_WSATP_ByteArray = OBATP_TO_WSATP.ToByte(); 
                    //message.DATA = OBATP_TO_WSATP_ByteArray;



                    //byte[] Message_OBATP_TO_WSATP_ByteArray = message.ToByte();


                    //MainForm.m_conn.Send(Message_OBATP_TO_WSATP_ByteArray);





                    MainForm.m_trainMovement.TrainMovementCreated(this);

                    Thread.Sleep(20);
                }

            }


        }





        //şimdilik rotaları parametrik olarak alıyor
        //sonrasında yapının şekillenmesine göre ayarlanacak
        public Track FindNextTrack(Track track, Enums.Direction direction,List<Track> route, List<Track> allTracks)// rota zaten bütün trackleri içermiyor mu? burayı inceleyip anlamak lazım
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
                    if (direction == Enums.Direction.One)
                    {
                        //neden sonraki tracki bütün trackler tablosundan arıyoruz?? anlamadım
                        nextTrack = allTracks.Find(x => x.Track_ID == track.Track_Connection_Exit_1);
                    }
                    else if (direction == Enums.Direction.Second)
                    {
                        //neden sonraki tracki bütün trackler tablosundan arıyoruz?? anlamadım
                        // tren geri geldiği için ama emin değilim ters gelişi sormak lazım
                        nextTrack = allTracks.Find(x => x.Track_ID == track.Track_Connection_Entry_1);


                    }
                }
            }
            else
            {
                //şimdilik kal
                FrontOfTrainNextTrack = null;
                RearOfTrainNextTrack = null;
            } 

            return nextTrack;

        }


        public Enums.Direction FindDirection(List<Track> routeTracks, Enums.Direction gloDirection)
        { 
            Enums.Direction direction = Enums.Direction.One;

            List<Track> tracksWithOutSwitches = routeTracks.FindAll(x => x.Track_ID.ToString().Substring(0, 1) != "6"); 

           if(tracksWithOutSwitches.Count >= 2)
            {
                int track1 = tracksWithOutSwitches[0].Track_ID;
                int track2 = tracksWithOutSwitches[1].Track_ID;

                if (track1 < track2)
                    direction = Enums.Direction.One;
                else
                    direction = Enums.Direction.Second;
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
                if (direction == Enums.Direction.One)
                {
                    // Kendi Track'ini aştığında
                    if (ActualFrontOfTrainCurrentLocation + FrontOfTrainLocationFault >= track.Track_End_Position) // yeni trackin içindeki konumu
                        //if (ActualFrontOfTrainCurrentLocation + FrontOfTrainLocationFault >= track.Track_Length) // yeni trackin içindeki konumu
                    {
                        FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrentLocation + FrontOfTrainLocationFault - track.Track_End_Position;
                        //FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrentLocation + FrontOfTrainLocationFault - track.Track_Length;

                        FrontOfTrainTrackWithFootPrint.Track =  FrontOfTrainNextTrack;
                    }
                    else
                    {
                        FrontOfTrainTrackWithFootPrint.Track = track;
                        FrontOfTrainTrackWithFootPrint.Location = ActualFrontOfTrainCurrentLocation + FrontOfTrainLocationFault;
                    }

                    //burası incelenecek çünkü başlangıç pozisyonları sıfırdan başlamıyor
                    frontTrainLocationInRouteWithFootPrint = FrontOfTrainTrackWithFootPrint.Track.StartPositionInRoute + FrontOfTrainTrackWithFootPrint.Location;
                    //frontTrainLocationInRouteWithFootPrint =  FrontOfTrainTrackWithFootPrint.Location;
                }
                else if (direction == Enums.Direction.Second)
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
            double rearTrainLocationInRouteWithFootPrint = -1;
            // FrontTrainLocationFault değeri belirli aralıklarla artacak hata payı ortaya çıkacak, RFID tag okununca sıfırlanacak

            if (route.Count != 0)
            {
                if (direction == Enums.Direction.One)
                {
                    //if (ActualRearCurrentLocation - RearOfTrainLocationFault < track.Track_Start_Position)
                    if (ActualRearCurrentLocation - RearOfTrainLocationFault < track.StartPositionInRoute)
                    {
                        Track rearFootPrintTrack = route.Find(x => x == track);

                        if (rearFootPrintTrack != null)
                        {
                            RearOfTrainTrackWithFootPrint.Track = rearFootPrintTrack;
                            RearOfTrainTrackWithFootPrint.Location = RearOfTrainTrackWithFootPrint.Track.Track_End_Position + (ActualRearCurrentLocation - RearOfTrainLocationFault);
                        }
                    }
                    else
                    {
                        RearOfTrainTrackWithFootPrint.Track = track;
                        rearTrainLocationInRouteWithFootPrint = ActualRearCurrentLocation - RearOfTrainLocationFault;
                    }
                }
                else if (direction == Enums.Direction.Second)
                {
                    if (ActualRearCurrentLocation + RearOfTrainLocationFault > track.Track_End_Position)
                    { 
                        Track rearFootPrintTrack = route.Find(x => x == track);

                        if (rearFootPrintTrack != null)
                        {
                            rearTrainLocationInRouteWithFootPrint = (ActualRearCurrentLocation + RearOfTrainLocationFault) - track.Track_End_Position;
                            RearOfTrainTrackWithFootPrint.Track = rearFootPrintTrack;
                        }
                    }
                    else
                    {
                        RearOfTrainTrackWithFootPrint.Track = track;
                        rearTrainLocationInRouteWithFootPrint = ActualRearCurrentLocation + RearOfTrainLocationFault;
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
                if (direction == Enums.Direction.One)
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
                else if(direction == Enums.Direction.Second)
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

                if (rearDirection == Enums.Direction.One)
                {
                    double realRearCurrentLocation = this.ActualRearCurrentLocation - this.RearOfTrainLocationFault;

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
                else if (rearDirection == Enums.Direction.Second)
                {
                    double rearCurrentLocationWithFault = this.ActualRearCurrentLocation + this.RearOfTrainLocationFault;

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




        double urgentTargetSpeed;

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
            if (isExceed == Enums.Route.Out )//|| DoorStatus == Enums.DoorStatus.Open)
            {
                targetSpeedKM = 0;
            }
            else  if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= frontCurrentTrack.StopPositionInRoute)
            {
                if (IsRouteLimitExceeded() == Enums.Route.Out)
                {
                    targetSpeedKM = 0;
                }
                else
                {
                    int frontCurrentTrackIndex = m_route.Route_Tracks.IndexOf(frontCurrentTrack);
                    int remainingTracks = m_route.Route_Tracks.Count - frontCurrentTrackIndex;

                    List<Track> restOfMovementTracks = m_route.Route_Tracks.GetRange(frontCurrentTrackIndex, remainingTracks);


                    Stack<Track> reverseRestOfMovementTracks = new Stack<Track>(restOfMovementTracks.ToList());



                    foreach (Track item in restOfMovementTracks)
                    {
 
                        //frenleme mesafesi frontcurrenttrackin konumu geçiyorsa
                        if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= item.StopPositionInRoute) //item.StartPositionInRoute)
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

                                        DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "O hıza gidebileceği mesafeye geldiyse", System.Drawing.Color.Black);


                                        targetSpeedKM = FindMinSpeedInTracks(item, rearCurrentTrack, routeTracks); 


                                        //break;
                                        return targetSpeedKM;


                                    }
                                    //tren önünden itibaren bulunduğu trackın içindeyse tek bir trackin içindedir; o trackın hızını al
                                    else if (this.FrontOfTrainLocationWithFootPrintInRoute - vehicle.TrainLength >= item.StopPositionInRoute) //item.StartPositionInRoute)
                                    {
                                        

                                        targetSpeedKM = item.MaxTrackSpeedKMH;

 

                                        DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Train Speed Limit : " + item.MaxTrackSpeedKMH.ToString(), System.Drawing.Color.Black);

 
                                        return targetSpeedKM;
                                        //break;
                                        //return;
                                    }

                                }
                                else if (vehicle.CurrentTrainSpeedCMS > item.MaxTrackSpeedCMS) // burası end: ten sonraki koşul karşılaştırması
                                {
                                    // O hıza gidebileceği mesafeye geldiyse
                                    if (item.StartPositionInRoute - this.ActualFrontOfTrainCurrentLocation <= distanceToArrangeTheSpeed - (Math.Abs(differenceBetweenTrainAndTrackSpeed) * time / 2))
                                    {

                                        DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "O hıza gidebileceği mesafeye geldiyse", System.Drawing.Color.Black);

                                        targetSpeedKM = FindMinSpeedInTracks(item, rearCurrentTrack, routeTracks);
                                        //break;

                                        return targetSpeedKM;


                                    }
                                    //tren önünden itibaren bulunduğu trackın içindeyse tek bir trackin içindedir; o trackın hızını al
                                    else if (this.FrontOfTrainLocationWithFootPrintInRoute - vehicle.TrainLength >= item.StartPositionInRoute)
                                    {
                                        DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "tek bir trackin içindedir", System.Drawing.Color.Black);

                                        targetSpeedKM = item.MaxTrackSpeedKMH;


                                        DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Front Track ID : " + item.Track_ID.ToString(), System.Drawing.Color.Black);
                                        DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Rear Track ID : " + rearCurrentTrack.Track_ID.ToString(), System.Drawing.Color.Black);


                                        DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "Train Speed Limit : " + frontCurrentTrack.MaxTrackSpeedKMH.ToString(), System.Drawing.Color.Black);


                                        return targetSpeedKM;
                                        //break;
                                        //return;
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

            }
            else if (isExceed == Enums.Route.In )// && DoorStatus == Enums.DoorStatus.Close)

            {


                //frenleme mesafesi frontcurrenttrackin konumu geçiyorsa
                if (FrontOfTrainLocationWithFootPrintInRoute + Vehicle.BrakingDistance >= frontCurrentTrack.StopPositionInRoute) //item.StartPositionInRoute)
                {

                }

                DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "----------------------------------", System.Drawing.Color.Black);
                DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "gitme izni var ve başka koşul yok ", System.Drawing.Color.Black);
                DisplayManager.RichTextBoxInvoke(MainForm.m_mf.m_richTextBox, "trenin içinde bulunduğu tracklerin hız limitlerine uy ", System.Drawing.Color.Black);


                targetSpeedKM = FindMinSpeedInTracks(frontCurrentTrack, rearCurrentTrack, routeTracks);


            }



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
                //burayı değiştireceğim
                double distanceToTarget = m_route.Length - FrontOfTrainLocationWithFootPrintInRoute;
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
            if (direction == Enums.Direction.One)
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
            else if (direction == Enums.Direction.Second)
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

            if (!string.IsNullOrEmpty(FrontOfTrainCurrentTrack.Station_Name) && !m_doorTimer.Enabled )
            {

                DoorStatus = OpenTrainDoors(DoorStatus);
                //HoldTrainAtStation();


                m_stopwatch.Start();
                m_doorTimer.Start();

                return DoorStatus;
            }
            else if (DwellTimeFinished)
            {
                Console.WriteLine("ManageTrainDoors index = 1");
                //LeftDoorsOpened = false;
                //RightDoorsOpened = false;
                DoorStatus = CloseTrainDoors(DoorStatus);  
                return DoorStatus;
            }
            else
            {
                Console.WriteLine("ManageTrainDoors index = 2");
                return DoorStatus;
            }
        }
        public void OnTrainDoorsOpenedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            //if (m_stopwatch.Elapsed.TotalSeconds >= DwellTime)
            if (m_stopwatch.Elapsed.TotalSeconds >= DwellTime)
            {
                if (Vehicle.CurrentTrainSpeedKMH > 0)
                {
                    Console.WriteLine("OnTrainDoorsOpenedEvent index = 0");
                    m_doorTimer.Stop();
                    m_stopwatch.Reset();
                    DoorTimerCounter = 0;
                    DwellTimeFinished = false;
                    return;
                }
                else
                {
                    Console.WriteLine("OnTrainDoorsOpenedEvent index = 1");
                    DwellTimeFinished = true;
                    //m_doorTimer.Stop();
                }
            }

            DoorTimerCounter++;
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



        public ushort[] FindTrackRangeInAllTracks(Track frontTrack, Track rearTrack, List<Track> allTracks)
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

    }
}
