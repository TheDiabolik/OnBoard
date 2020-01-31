using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard 
{
    public partial class OBATP : IWSATP_TO_OBATPMessageWatcher, IATS_TO_OBATO_InitMessageWatcher, IATS_TO_OBATO_MessageWatcher, IATS, IWSATC, IDisposable
    {

        private bool m_disposed = false;


        #region Global
        [Browsable(false)]
        bool DwellTimeFinished = false;


        [Browsable(false)]
        volatile string openDoorTrackName;

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


        [Browsable(false)]
        public double FrontOfTrainLocationWithFootPrintInRoute { get; set; }


        [Browsable(false)]
        public double RearOfTrainLocationWithFootPrintInRoute { get; set; }

        [Browsable(false)]
        public Track FrontOfTrainNextTrack { get; set; }
        [Browsable(false)]
        public Track RearOfTrainNextTrack { get; set; }

        [Browsable(false)]
        public double FrontOfTrainLocationFault { get; set; }

        [Browsable(false)]
        public double RearOfTrainLocationFault { get; set; }



        private System.Threading.Timer m_messageSendTimer;
        private System.Threading.Timer m_OBATCTimer;
        private System.Threading.Timer m_UIRefreshTimer;


        Stopwatch m_messageSendStopwatch;
        Stopwatch m_UIRefreshStopwatch;




        bool m_controlUIRefreshTimer = false;
        bool  m_controlMessageSendTimer = false;
        bool m_controlOBATCTimer = false;
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
                    if (denemeRearOfTrainCurrentTrack == null)
                        denemeRearOfTrainCurrentTrack = value;
                    else
                    {
                        //bool isDeleted = m_route.Route_Tracks.Remove(denemeRearOfTrainCurrentTrack);


                        movementTrack.Remove(denemeRearOfTrainCurrentTrack);


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


        [Browsable(false)]
        public System.Timers.Timer m_doorTimerDeneme;



        Stopwatch m_stopwatch;
        [Browsable(false)]
        public int DoorTimerCounter { get; set; }
        double OperationTime = 0.2;

        XMLSerialization m_settings;
        //public List<Track> m_route;
        [Browsable(false)]
        //public Route m_route = new Route();
        public Enums.Status Status;


        public int m_startTrackID;


        public ThreadSafeList<Track> movementTrack = new ThreadSafeList<Track>();

    }
}

