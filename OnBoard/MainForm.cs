using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace OnBoard
{
    public partial class MainForm : Form, ITrainMovementUIWatcher, ISettingsWindowsWatcher, ITrainNewMovementAuthorityCreatedWatcher //ITrainMovementCreatedSendMessageWatcher, ITrainMovementUIWatcher, ISettingsWindowsWatcher
    {
        XMLSerialization m_settings;
        //SocketCommunication m_OBATPToWSATCSocket;
        //SocketCommunication m_OBATPToATSSocket;
        //SocketCommunication m_ATSToOBATPSocket;
        //SocketCommunication m_WSATCToOBATPSocket;
        internal SocketCommunication m_ATSSocket;
        internal SocketCommunication m_WSATCSocket;

        //List<OBATPUIAdapter> m_ListList;

        //internal   BindingList<OBATP> m_allTrains;
        private System.Timers.Timer STTimer;
        private System.Threading.Timer AllTrainTimer;



        internal ThreadedBindingList<OBATP> m_allTrains;

        Stopwatch stopwatch = new Stopwatch();

        //internal BlockingCollection<Message> m_communicationLogs;

        internal BlockingCollection<Tuple<DateTime, string, string>> m_communicationLogs;
        internal BlockingCollection<Tuple<string, Color>> m_UILogs;

        //internal BlockingCollection<Tuple<DateTime, string, string>> m_communicationLogssdsd;

        internal static TrainObserver m_trainObserver;

        //internal static TRAİNmove ATS_TO_OBATO_MessageInComing m_ATS_TO_OBATO_MessageInComing;


        internal static WSATP_TO_OBATPMessageInComing m_WSATP_TO_OBATPMessageInComing;
        internal static ATS_TO_OBATO_InitMessageInComing m_ATS_TO_OBATO_InitMessageInComing;
        internal static ATS_TO_OBATO_MessageInComing m_ATS_TO_OBATO_MessageInComing;
         
        internal static SettingsWindowsObserver m_settingsWindowsObserver;



        public static MainForm m_mf;
        internal static ConcurrentDictionary<int, OBATP> m_allOBATP;
        public ThreadSafeList<Track> m_allTracks;

        //public List<Track> m_ToYenikapıTracks;
        //public List<Track> m_FromYenikapıTracks;

        public List<Track> m_KIR2_YNK1;
        public List<Track> m_YNK1_KIR2;
        public List<Track> m_YNK1_KIR2_YNK1;


        public ThreadSafeList<Track> m_WSATCMovement_YNK1_KIR2_YNK1;

        public List<Track> m_YNK2_HAV2_YNK2;

        public ThreadSafeList<Track> m_simulationAllTracks;
        //public  List<Route> m_allRoute;
        internal static DataTable m_fromFileTracks;
        //internal static DataTable m_simulationRouteTracks;
        //internal static Route m_route;
        readonly object m_movement = new object();
        readonly object m_newRoute = new object();
        readonly object m_movementUI = new object();

        public MainForm()
        {
            InitializeComponent();

            m_mf = this;

            #region ayarları okuma
            m_settings = XMLSerialization.Singleton();
            m_settings = m_settings.DeSerialize(m_settings);
            #endregion

            #region SocketCreation
            //m_OBATPToWSATCSocket = new SocketCommunication();
            //m_WSATCToOBATPSocket = new SocketCommunication();

            //m_OBATPToATSSocket = new SocketCommunication();
            //m_ATSToOBATPSocket = new SocketCommunication();

            m_ATSSocket = new SocketCommunication();
            m_WSATCSocket = new SocketCommunication();
            #endregion


            m_communicationLogs = new BlockingCollection<Tuple<DateTime, string, string>>();
            m_UILogs = new BlockingCollection<Tuple<string, Color>>();

            AllTrainTimer = new System.Threading.Timer(ManageAllTrain);


            STTimer = new System.Timers.Timer();
            STTimer.Elapsed += OnTimerElapsed;

            //m_allTrains = new BindingList<OBATP>();
            m_allTrains = new ThreadedBindingList<OBATP>();

            //m_ToYenikapıTracks = new List<Track>();
            //m_FromYenikapıTracks = new List<Track>();

            m_KIR2_YNK1 = new List<Track>();
            m_YNK1_KIR2 = new List<Track>();
            m_YNK1_KIR2_YNK1 = new List<Track>();

            m_YNK2_HAV2_YNK2 = new List<Track>();

            m_WSATCMovement_YNK1_KIR2_YNK1 = new ThreadSafeList<Track>();




            m_simulationAllTracks = new ThreadSafeList<Track>();

            m_allOBATP = new ConcurrentDictionary<int, OBATP>();
            m_trainObserver = new TrainObserver();

            m_WSATP_TO_OBATPMessageInComing = new WSATP_TO_OBATPMessageInComing();
            m_ATS_TO_OBATO_InitMessageInComing = new ATS_TO_OBATO_InitMessageInComing();
            m_ATS_TO_OBATO_MessageInComing = new ATS_TO_OBATO_MessageInComing();


            m_settingsWindowsObserver = new SettingsWindowsObserver();

            //MainForm.m_trainObserver.AddTrainMovementCreatedSendMessageWatcher(this);

            MainForm.m_trainObserver.AddTrainNewMovementAuthorityCreatedWatcher(this);

            MainForm.m_trainObserver.AddTrainMovementUIWatcher(this);

         
            //MainForm.m_trainObserver.AddTrainMovementUIall TracksListWatcher(this);

            MainForm.m_settingsWindowsObserver.AddSettingsWindowWatcher(this);



            //excel tablosundan track listesini ve özelliklerini okuyoruz    
            m_fromFileTracks = FileOperation.Singleton().EPPlusReadTrackTableInExcel();


            if (m_settings.TrackInput == Enums.TrackInput.Manuel)
            {
                m_allTracks = Track.AllTracksAAA(m_settings.RouteTrack);
            }
            else if (m_settings.TrackInput == Enums.TrackInput.FromFile)
            {
                m_allTracks = Track.AllTracks(m_fromFileTracks);
            }

            #region veritabanı için commentlendi


            //m_simulationRouteTracks = FileOperation.Singleton().ReadSimulationRouteTableInExcel();
            //m_simulationRouteTracks = FileOperation.Singleton().EPPlusReadSimulationRouteTableInExcel();
            //m_allRoute =  Route.SimulationRoute(m_simulationRouteTracks);
            //m_allRoute = Route.SimulationRouteStationToStation(m_simulationRouteTracks);


            //m_simulationAllTracks = Track.SimulationTrack(m_simulationRouteTracks);

            //Track lastStation = m_simulationAllTracks.Find(x => x.Track_ID == 11502);
            //int indexLastStation = m_simulationAllTracks.IndexOf(lastStation);

            //Track len = m_simulationAllTracks.ToList().FindLastIndex(x => x.Track_ID == 10101);
            //int indexlen = m_simulationAllTracks.IndexOf(len);

            //int indexlen = m_simulationAllTracks.ToList().FindLastIndex(x => x.Track_ID == 10101);

            //m_FromYenikapıTracks = m_simulationAllTracks.Where((element, index) => (index >= 0) && (index <= indexLastStation)).ToList();

            //m_ToYenikapıTracks = m_simulationAllTracks.Where((element, index) => (index >= indexLastStation) && (index <= indexlen)).ToList();

            #endregion 
 


            veri veri = new veri(); 


            Task<List<Track>> taskSelect2 = veri.AsycSelectYNK1_KIR2_YNK1();
            taskSelect2.Wait();  
            m_YNK1_KIR2_YNK1 = taskSelect2.Result;


            m_WSATCMovement_YNK1_KIR2_YNK1.AddRange(m_YNK1_KIR2_YNK1);


            //Task<List<Track>> taskSelect3 = veri.AsycSelectYNK2_HAV2_YNK2();
            //taskSelect3.Wait(); 

            //m_YNK2_HAV2_YNK2 = taskSelect3.Result;




            //m_OBATPToATSSocket.Start(m_settings.OBATPToATSCommunicationType, "127.0.0.1", Convert.ToInt32(m_settings.OBATPToATSPort));
            //m_ATSToOBATPSocket.Start(m_settings.ATSToOBATPCommunicationType, "127.0.0.1", Convert.ToInt32(m_settings.ATSToOBATPPort));

            //m_OBATPToWSATCSocket.Start(m_settings.OBATPToWSATCCommunicationType, "127.0.0.1", Convert.ToInt32(m_settings.OBATPToWSATCPort));
            //m_WSATCToOBATPSocket.Start(m_settings.WSATCToOBATPCommunicationType, "127.0.0.1", Convert.ToInt32(m_settings.WSATCToOBATPPort)); 

            //m_OBATPToATSSocket.Start(m_settings.OBATPToATSCommunicationType, m_settings.OBATPToATSIPAddress, Convert.ToInt32(m_settings.OBATPToATSPort));
            //m_ATSToOBATPSocket.Start(m_settings.ATSToOBATPCommunicationType, m_settings.ATSToOBATPIPAddress, Convert.ToInt32(m_settings.ATSToOBATPPort));

            //m_OBATPToWSATCSocket.Start(m_settings.OBATPToWSATCCommunicationType, m_settings.OBATPToWSATCIPAddress, Convert.ToInt32(m_settings.OBATPToWSATCPort));
            //m_WSATCToOBATPSocket.Start(m_settings.WSATCToOBATPCommunicationType, m_settings.WSATCToOBATPIPAddress, Convert.ToInt32(m_settings.WSATCToOBATPPort));

            m_ATSSocket.Start(m_settings.ATSCommunicationType, SocketCommunication.ClientType.ATS, m_settings.ATSIPAddress, Convert.ToInt32(m_settings.ATSPort));
            m_WSATCSocket.Start(m_settings.WSATCCommunicationType, SocketCommunication.ClientType.WSATC, m_settings.WSATCIPAddress, Convert.ToInt32(m_settings.WSATCPort));



            foreach (int index in m_settings.Trains)
            {
                int trainIndex = index + 1;
                Enums.Train_ID train_ID = (Enums.Train_ID)trainIndex;

                //OBATP OBATP = new OBATP(train_ID, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration, m_settings.TrainSpeedLimit, m_settings.TrainLength, 10101);
                OBATP OBATP = new OBATP(train_ID, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration, m_settings.TrainSpeedLimit, m_settings.TrainLength);

                #region WSATC Movement Yetkisi Testi
                //OBATP OBATP = new OBATP(train_ID, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration);
                #endregion

                ////wayside testi için var test sonrası silinebilir
                //if (OBATP.Status == Enums.Status.Create)
                //{

                //    //Track sdf =  MainForm.m_mf.m_YNK1_KIR2_YNK1.Find(x => x.Track_ID == 11402);

                //    OBATP.SetStart(10101, true);

                //    //OBATP.ActualFrontOfTrainCurrent.Track.Track_ID = 11402;
                //    //OBATP.m_startTrackID = 11402;


                //    //break;
                //}

                m_allOBATP.TryAdd(trainIndex, OBATP);

                m_comboBoxTrain.Items.Add(train_ID.ToString());

                #region Communication Add Watchers
                MainForm.m_WSATP_TO_OBATPMessageInComing.AddWatcher(OBATP);
                MainForm.m_ATS_TO_OBATO_InitMessageInComing.AddWatcher(OBATP);
                MainForm.m_ATS_TO_OBATO_MessageInComing.AddWatcher(OBATP);
                #endregion
            }

            #region controls doublebuffered
            UIOperation.SetDoubleBuffered(m_listView);
            UIOperation.SetDoubleBuffered(m_listViewFootPrintTracks);
            UIOperation.SetDoubleBuffered(m_listViewVirtualOccupation);
            UIOperation.SetDoubleBuffered(m_dataGridViewAllTrains);
            #endregion 

        }




        private void MainForm_Load(object sender, EventArgs e)
        {

             


            m_labelDoorCounter.Text = "";
            m_labelDoorCounter.BorderStyle = BorderStyle.None;

            //TrainSimModal trainSimModal = new TrainSimModal();
            //trainSimModal.Owner = this;
            //trainSimModal.Show();

            if(m_settings.Trains.Count > 0)
                m_comboBoxTrain.SelectedIndex = 0;

            m_allTrains.SynchronizationContext = SynchronizationContext.Current;
            m_dataGridViewAllTrains.DataSource = m_allTrains;


            //general
            m_dataGridViewAllTrains.Columns[0].Width = 50;
            m_dataGridViewAllTrains.Columns[1].Width = 90;
            m_dataGridViewAllTrains.Columns[2].Width = 100;
            //front
            m_dataGridViewAllTrains.Columns[3].Width = 110;
            m_dataGridViewAllTrains.Columns[4].Width = 160;
            m_dataGridViewAllTrains.Columns[5].Width = 150;
            //rear
            m_dataGridViewAllTrains.Columns[6].Width = 110;
            m_dataGridViewAllTrains.Columns[7].Width = 160;
            m_dataGridViewAllTrains.Columns[8].Width = 150;
            //total route
            //m_dataGridViewAllTrains.Columns[9].Width = 150;


            if (!m_backgroundWorkerCommunicationLogs.IsBusy)
                m_backgroundWorkerCommunicationLogs.RunWorkerAsync();

            if (!m_backgroundWorkerUILogs.IsBusy)
                m_backgroundWorkerUILogs.RunWorkerAsync();


        } 
        private void m_buttonStart_Click(object sender, EventArgs e)
        {
            //if (!m_backgroundWorker.IsBusy)
            //    m_backgroundWorker.RunWorkerAsync(); 

            int waitingMinuteToMiliSecond = Convert.ToInt32(m_settings.TrainFrequencyMinute) * 60000;
            int waitingSecondToMiliSecond = Convert.ToInt32(m_settings.TrainFrequencySecond) * 1000;
            int waitingMiliSecond = waitingMinuteToMiliSecond + waitingSecondToMiliSecond;


            AllTrainTimer.Change(0, waitingMiliSecond);
        }

        #region TrainMovementUIRefreshAllTrainList  

        public void TrainMovementUIRefreshAllTrainList(OBATP OBATP)
        {
            try
            {
                lock (m_movement)
                {
                    int index = m_allTrains.ToList().FindIndex(x => x == OBATP);
                    m_allTrains.ResetItem(index);

                    if ((DisplayManager.ComboBoxGetSelectedItemInvoke(m_comboBoxTrain) != null) && (DisplayManager.ComboBoxGetSelectedItemInvoke(m_comboBoxTrain).ToString() == OBATP.Vehicle.TrainName))
                    {
                        //general train
                        DisplayManager.TextBoxInvoke(m_textBoxCurrentTrainSpeedKM, OBATP.Vehicle.CurrentTrainSpeedKMH.ToString());
                        DisplayManager.TextBoxInvoke(m_textBoxCurrentLocation, OBATP.ActualFrontOfTrainCurrent.Location.ToString("0.##"));
                        DisplayManager.TextBoxInvoke(m_textBoxRearCurrentLocation, OBATP.ActualRearOfTrainCurrent.Location.ToString("0.##"));
                        DisplayManager.TextBoxInvoke(m_textBoxCurrentAcceleration, (OBATP.Vehicle.CurrentAcceleration / 100).ToString("0.##"));

                        if (OBATP.DoorStatus == Enums.DoorStatus.Open)
                        {
                            DisplayManager.PanelInvoke(panel1, Color.Red);
                            DisplayManager.PictureBoxInvoke(m_pictureBoxDoorStatus, Properties.Resources.dooropen);
                        }
                        else
                        {
                            DisplayManager.PanelInvoke(panel1, default(Color));
                            DisplayManager.PictureBoxInvoke(m_pictureBoxDoorStatus, Properties.Resources.doorclose);
                        }


                        ////if(OBATP.DoorTimerCounter.ToString() == "11")
                        ////{
                        ////    DisplayManager.LabelInvoke(m_labelDoorCounter, "");
                        ////}
                        ////else
                        ////{
                        ////DisplayManager.LabelInvoke(m_labelDoorCounter, OBATP.DoorTimerCounter.ToString()); 
                        ////}
                        ///

                        if (OBATP.zongurt.ToString() == "0")
                        {
                            DisplayManager.LabelInvoke(m_labelDoorCounter, "");
                        }
                        else
                        {
                            DisplayManager.LabelInvoke(m_labelDoorCounter, OBATP.zongurt.ToString());
                        }


                        DisplayManager.TextBoxInvoke(m_textBoxDoorTimerCounter, OBATP.DoorTimerCounter.ToString());

                        //if (OBATP.DoorStatus == Enums.DoorStatus.Open)
                        //    DisplayManager.TextBoxInvoke(m_textBoxDoorStatus, "Open");
                        //else
                        //    DisplayManager.TextBoxInvoke(m_textBoxDoorStatus, "Close"); 
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "TrainMovementCreated");

            }

        }
        //public void TrainMovementCreatedSendMessage(OBATP OBATP)
        //{
        //    try
        //    {
        //        lock (m_movement)
        //        {
        //            int index = m_allTrains.ToList().FindIndex(x => x == OBATP);
        //            m_allTrains.ResetItem(index);

        //            if ((DisplayManager.ComboBoxGetSelectedItemInvoke(m_comboBoxTrain) != null) && (DisplayManager.ComboBoxGetSelectedItemInvoke(m_comboBoxTrain).ToString() == OBATP.Vehicle.TrainName))
        //            {
        //                //general train
        //                DisplayManager.TextBoxInvoke(m_textBoxCurrentTrainSpeedKM, OBATP.Vehicle.CurrentTrainSpeedKMH.ToString());
        //                DisplayManager.TextBoxInvoke(m_textBoxCurrentLocation, OBATP.ActualFrontOfTrainCurrent.Location.ToString("0.##"));
        //                DisplayManager.TextBoxInvoke(m_textBoxRearCurrentLocation, OBATP.ActualRearOfTrainCurrent.Location.ToString("0.##"));
        //                DisplayManager.TextBoxInvoke(m_textBoxCurrentAcceleration, (OBATP.Vehicle.CurrentAcceleration / 100).ToString("0.##"));

        //                if (OBATP.DoorStatus == Enums.DoorStatus.Open)
        //                {
        //                    DisplayManager.PanelInvoke(panel1, Color.Red);
        //                    DisplayManager.PictureBoxInvoke(m_pictureBoxDoorStatus, Properties.Resources.dooropen);
        //                }
        //                else
        //                {
        //                    DisplayManager.PanelInvoke(panel1, default(Color));
        //                    DisplayManager.PictureBoxInvoke(m_pictureBoxDoorStatus, Properties.Resources.doorclose);
        //                }


        //                ////if(OBATP.DoorTimerCounter.ToString() == "11")
        //                ////{
        //                ////    DisplayManager.LabelInvoke(m_labelDoorCounter, "");
        //                ////}
        //                ////else
        //                ////{
        //                ////DisplayManager.LabelInvoke(m_labelDoorCounter, OBATP.DoorTimerCounter.ToString()); 
        //                ////}
        //                ///

        //                if (OBATP.zongurt.ToString() == "0")
        //                {
        //                    DisplayManager.LabelInvoke(m_labelDoorCounter, "");
        //                }
        //                else
        //                {
        //                    DisplayManager.LabelInvoke(m_labelDoorCounter, OBATP.zongurt.ToString());
        //                }


        //                DisplayManager.TextBoxInvoke(m_textBoxDoorTimerCounter, OBATP.DoorTimerCounter.ToString());

        //                //if (OBATP.DoorStatus == Enums.DoorStatus.Open)
        //                //    DisplayManager.TextBoxInvoke(m_textBoxDoorStatus, "Open");
        //                //else
        //                //    DisplayManager.TextBoxInvoke(m_textBoxDoorStatus, "Close"); 
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "TrainMovementCreated");

        //    }

        //}
        #endregion

        #region TrainMovementUI 
        public void TrainMovementUIRefreshTracksList(OBATP OBATP, UIOBATP UIOBATP)
        {
            try
            {
                lock (OBATP)
                {
                    if ((DisplayManager.ComboBoxGetSelectedItemInvoke(m_comboBoxTrain) != null) && (DisplayManager.ComboBoxGetSelectedItemInvoke(m_comboBoxTrain).ToString() == OBATP.Vehicle.TrainName))
                    {
                        //List<int> listValueRoute_Tracks = m_listView.Items.Cast<ListViewItem>().Select(item => Convert.ToInt32(item.Text)).ToList();
                        //bool difflistValuesRoute_Tracks = OBATP.m_route.Route_Tracks.Select(x => x.Track_ID).ToList().SequenceEqual(listValueRoute_Tracks);


                        //if (!difflistValuesRoute_Tracks)
                        if (UIOBATP.RefreshRouteTracks)
                        {

                            //DisplayManager.ListViewItemsClearInvoke(m_listView);

                            ////foreach (var item in OBATP.m_route.Route_Tracks)
                            //foreach (var item in OBATP.movementTrack)
                            //{
                            //    DisplayManager.ListViewItemsAddInvoke(m_listView, new ListViewItem(new string[] { item.Track_ID.ToString(), item.Station_Name, item.SpeedChangeVMax.ToString() })); 
                            //}

                            DisplayManager.ListViewItemBackColorInvoke(m_listView, 0, Color.Red);

                            

                        }


                        //List<int> listValuesVirtualOccupation = m_listViewVirtualOccupation.Items.Cast<ListViewItem>().Select(item => Convert.ToInt32(item.Text)).ToList();
                        //bool difflistValuesVirtualOccupation = UIOBATP.TrainOnTracks.VirtualOccupationTracks.Select(x => x.Track_ID).ToList().SequenceEqual(listValuesVirtualOccupation);

                        //if (!difflistValuesVirtualOccupation)

                        if (UIOBATP.RefreshVirtualOccupationTracks)
                        {

                            DisplayManager.ListViewItemsClearInvoke(m_listViewVirtualOccupation);

                            foreach (var item in UIOBATP.TrainOnTracks.VirtualOccupationTracks)
                            {
                                DisplayManager.ListViewItemsAddInvoke(m_listViewVirtualOccupation, new ListViewItem(new string[] { item.Track_ID.ToString() }));
                            }
                             

                            //m_listViewVirtualOccupation.Invoke((Action)(() =>
                            //{
                            //    m_listViewVirtualOccupation.Items.Clear();
                            ////OBATP.TrainOnTracks.VirtualOccupationTracks.ForEach(x => m_listViewVirtualOccupation.Items.Add(x.Track_ID.ToString()));
                            //foreach (var item in UIOBATP.TrainOnTracks.VirtualOccupationTracks)
                            //{
                            //    m_listViewVirtualOccupation.Items.Add(item.Track_ID.ToString());
                            //}
                            //}));
                        }
                        //new ListViewItem(new string[] { x.Track_ID.ToString(), x.Station_Name })

                        //List<int> listValuesFootPrintTracks = m_listViewFootPrintTracks.Items.Cast<ListViewItem>().Select(item => Convert.ToInt32(item.Text)).ToList();
                        //bool difflistValuesFootPrintTracks = UIOBATP.TrainOnTracks.FootPrintTracks.Select(x => x.Track_ID).ToList().SequenceEqual(listValuesFootPrintTracks);

                        //if (!difflistValuesFootPrintTracks)

                        if (UIOBATP.RefreshFootPrintTracks)
                        { 

                            DisplayManager.ListViewItemsClearInvoke(m_listViewFootPrintTracks);

                            foreach (var item in UIOBATP.TrainOnTracks.FootPrintTracks)
                            {
                                DisplayManager.ListViewItemsAddInvoke(m_listViewFootPrintTracks, new ListViewItem(new string[] { item.Track_ID.ToString() }));
                            } 
 
                        }

                        if (UIOBATP.RefreshActualLocationTracks)
                        {
                            m_listView.Invoke((Action)(() =>
                            {
                                //trenin tracklerini kırmızıya boyama
                                foreach (ListViewItem li in m_listView.Items)
                                {
                                    int itemText = Convert.ToInt32(li.Text);

                                    Track lolo = UIOBATP.TrainOnTracks.ActualLocationTracks.Find(x => x.Track_ID == itemText);

                                    if (lolo != null)
                                    {
                                        //li.ForeColor = Color.Red;
                                        li.BackColor = Color.Red;
                                    }
                                    else
                                    {
                                        li.BackColor = Color.White;
                                    }
                                }
                            }));
                        }

                        

                    }
                }
            }
            catch (Exception ex)
            {
                Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "TrainMovementUI");

            }
        }
        #endregion

        #region TrainMovementRouteCreated 
        public void TrainMovementRouteCreated(Route route)
        {
            //lock(m_newRoute)
            //{
            //    //if ((m_comboBoxTrain.SelectedItem != null) && (m_comboBoxTrain.SelectedItem.ToString() == OBATP.Vehicle.TrainName))
            //    {

            //        var ds = m_comboBoxTrain.SelectedItem;

            //        var dssadasd = m_comboBoxTrain.Text;
            //        var asdasdsaddssadasd = m_comboBoxTrain.SelectedText;

            //        if(dssadasd == "Train8")
            //        {

            //        }
            //        //m_listView.Items.Clear();
            //        //route.Route_Tracks.ForEach(x => m_listView.Items.Add(new ListViewItem(new string[] { x.Track_ID.ToString(), x.Station_Name, x.SpeedChangeVMax.ToString() }))); 

            //        List<int> listValueRoute_Tracks = m_listView.Items.Cast<ListViewItem>().Select(item => Convert.ToInt32(item.Text)).ToList();
            //        bool difflistValuesRoute_Tracks =  route.Route_Tracks.Select(x => x.Track_ID).ToList().SequenceEqual(listValueRoute_Tracks);


            //        if (!difflistValuesRoute_Tracks)
            //        {
            //            m_listView.Items.Clear();
                      
            //            foreach (var item in  route.Route_Tracks)
            //            {
            //                m_listView.Items.Add(new ListViewItem(new string[] { item.Track_ID.ToString(), item.Station_Name, item.SpeedChangeVMax.ToString() }));
            //            }
            //        }




            //    }
            //} 
        }

        #endregion

        #region TrainNewMovementAuthorityCreated

        public void TrainNewMovementAuthorityCreated(ThreadSafeList<Track> newMovementAuthorityList)
        {
            DisplayManager.ListViewItemsClearInvoke(m_listView);

            //foreach (var item in OBATP.m_route.Route_Tracks)
            foreach (var item in newMovementAuthorityList)
            {
                DisplayManager.ListViewItemsAddInvoke(m_listView, new ListViewItem(new string[] { item.Track_ID.ToString(), item.Station_Name, item.SpeedChangeVMax.ToString() }));
            }
        }

        #endregion

        private void m_trainItem_Click(object sender, EventArgs e)
        {
            TrainSettingsModal trainSettingsModal = new TrainSettingsModal();
            trainSettingsModal.Owner = this;
            trainSettingsModal.ShowDialog();
        }

        private void m_generalItem_Click(object sender, EventArgs e)
        {
            //m_ATSSocket.Stop(Enums.CommunicationType.Client, SocketCommunication.ClientType.ATS);


            GeneralSettingsModal generalSettingsModal = new GeneralSettingsModal(this);
            generalSettingsModal.Owner = this;
            generalSettingsModal.ShowDialog();
        }

     
        //OBATPUIAdapter osman = new OBATPUIAdapter();
        private void m_backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //try
            //{

            this.STTimer.Start();

            //m_settings = m_settings.DeSerialize(m_settings);

            //int waitingTime = Convert.ToInt32(m_settings.TrainFrequency) * 1000;

            //this.STTimer = new System.Threading.Timer(DeleteInValidImages, null, 0, waitingTime);


            //}
            ////catch
            //{

            //}



            //try
            //{

            //this.STTimer.Start();

            //m_settings = m_settings.DeSerialize(m_settings);
            //int waitingTime = Convert.ToInt32(m_settings.TrainFrequency) * 1000;


            //foreach (KeyValuePair<int, OBATP> item in m_allOBATP)
            //{
            //    OBATP OBATP = item.Value;

            //    if (!m_allTrains.Contains(OBATP))
            //        m_allTrains.Add(OBATP);


            //    if (OBATP.Status == Enums.Status.Create)
            //    {
            //        OBATP.RequestStartProcess();

            //        Thread.Sleep(waitingTime);

            //        //break;
            //    }


            //}
        }

        int counter = 0;

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {



            foreach (KeyValuePair<int, OBATP> item in m_allOBATP)
            {
                OBATP OBATP = item.Value;

                if (!m_allTrains.Contains(OBATP))
                    m_allTrains.Add(OBATP);


                if (OBATP.Status == Enums.Status.Create)
                {

                    //Track sdf =  MainForm.m_mf.m_YNK1_KIR2_YNK1.Find(x => x.Track_ID == 11402);

                    OBATP.SetStart(10101, true);

                    //OBATP.ActualFrontOfTrainCurrent.Track.Track_ID = 11402;
                    //OBATP.m_startTrackID = 11402;

                    OBATP.RequestStartProcess();
                    break;
                }
                else if (OBATP.Status == Enums.Status.Stop)
                {

                    OBATP.RequestStartProcess();
                }
            }

            int waitingMinuteToMiliSecond = Convert.ToInt32(m_settings.TrainFrequencyMinute) * 60000;
            int waitingSecondToMiliSecond = Convert.ToInt32(m_settings.TrainFrequencySecond) * 1000;
            int waitingMiliSecond = waitingMinuteToMiliSecond + waitingSecondToMiliSecond;

            this.STTimer.Interval = waitingMiliSecond;


            //var dsfıjop = m_allOBATP.Values.ToList();

        }


        public void ManageAllTrain(object o)
        {
            //try
            //{


            foreach (KeyValuePair<int, OBATP> item in m_allOBATP)
            {
                OBATP OBATP = item.Value;

                if (!m_allTrains.Contains(OBATP))
                    m_allTrains.Add(OBATP);


                if (OBATP.Status == Enums.Status.Create)
                {

                    //Track sdf =  MainForm.m_mf.m_YNK1_KIR2_YNK1.Find(x => x.Track_ID == 11402);

                    OBATP.SetStart(10101, true);

                    //OBATP.ActualFrontOfTrainCurrent.Track.Track_ID = 11402;
                    //OBATP.m_startTrackID = 11402;

                    OBATP.RequestStartProcess();
                    break;
                }
                else if (OBATP.Status == Enums.Status.Stop)
                {

                    OBATP.RequestStartProcess();
                }
            }


            int startOBATCCount = m_allOBATP.Values.ToList().Where(x => x.Status == Enums.Status.Start).Count();

            if (startOBATCCount == m_allOBATP.Count())
                AllTrainTimer.Change(Timeout.Infinite, Timeout.Infinite);

            //int waitingMinuteToMiliSecond = Convert.ToInt32(m_settings.TrainFrequencyMinute) * 60000;
            //int waitingSecondToMiliSecond = Convert.ToInt32(m_settings.TrainFrequencySecond) * 1000;
            //int waitingMiliSecond = waitingMinuteToMiliSecond + waitingSecondToMiliSecond;

            //this.STTimer.Interval = waitingMiliSecond;


            //var dsfıjop = m_allOBATP.Values.ToList();


            //}
            //catch (ThreadInterruptedException ex)
            //{
            //    kontrol = false;
            //    sda();
            //    //Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "socket1");

            //}
            //catch (Exception ex)
            //{
            //    kontrol = false;
            //    sda();
            //    //Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "socket2");
            //}
        }


        public void DeleteInValidImages(object o)
        {
            //try
            //{

                m_settings = m_settings.DeSerialize(m_settings);
                //List<Track> route = Route.CreateNewRoute(10101, 10103, allTracks);


                Stopwatch sw = new Stopwatch();


                int counter = 0;


                foreach (KeyValuePair<int, OBATP> item in m_allOBATP)
                { 
                    OBATP OBATP = item.Value; 

                    //lock(OBATP)
                    {
                        if (!m_allTrains.Contains(OBATP))
                            m_allTrains.Add(OBATP);
                    }


                    //}

                    if(OBATP.Status == Enums.Status.Create)
                        OBATP.RequestStartProcess(); 

                }

            
               


                #region old train start
                //foreach (var item in m_settings.Trains)
                //{
                //    counter++;


                //    int trainIndex = item;
                //    string trainName = "Train" + (trainIndex + 1).ToString();

                //    //DataRow row2 = m_dataTable.NewRow();
                //    //row2["ID"] = counter;
                //    //row2["Train_Name"] = trainName;
                //    //m_dataTable.Rows.Add(row2);

                //    // DisplayManager.RichTextBoxInvoke(m_richTextBox, trainName + " is Created...", Color.Black);





                //    //OBATP oBATP = new OBATP(Enums.Train_ID.Train1, trainName, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration, m_settings.TrainSpeedLimit, m_settings.TrainLength, m_route);
                //    OBATP oBATP = new OBATP((Enums.Train_ID)trainIndex, trainName, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration, m_settings.TrainSpeedLimit, m_settings.TrainLength, m_route);


                //    MainForm.m_WSATP_TO_OBATPMessageInComing.AddWatcher(oBATP);
                //    MainForm.m_ATS_TO_OBATO_InitMessageInComing.AddWatcher(oBATP);
                //    MainForm.m_ATS_TO_OBATO_MessageInComing.AddWatcher(oBATP);

                //    // oba  m_ListList.Add(osman);


                //    //m_bindingSourceTrains.ResetBindings(false);



                //    //pool.PutObject(oBATP);



                //    // //m_allOBATP.TryAdd(trainIndex, oBATP);
                //    // //m_allOBATP.AddOrUpdate(trainIndex, oBATP, (s, i) => oBATP);

                //    oBATP.RequestStartProcess();


                //    int sleepTime = Convert.ToInt32(m_settings.TrainFrequency);

                //    sw.Start();



                //    while (sw.Elapsed.TotalSeconds < (sleepTime))
                //    {

                //    }

                //    sw.Reset();


                //}
                #endregion



            //}
            //catch (ThreadInterruptedException ex)
            //{
            //    kontrol = false;
            //    sda();
            //    //Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "socket1");

            //}
            //catch (Exception ex)
            //{
            //    kontrol = false;
            //    sda();
            //    //Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "socket2");
            //}
        }




        private void m_dataGridViewAllTrains_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void m_dataGridViewAllTrains_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

           
        }

        private void m_richTextBox_TextChanged(object sender, EventArgs e)
        {
            if (m_richTextBoxCommunicationLogs.Text.Length > 5000)
            {
                
                m_richTextBoxCommunicationLogs.ResetText();
            }

            m_richTextBoxCommunicationLogs.ScrollToCaret();
        }

        private void m_mainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void m_communicationItem_Click(object sender, EventArgs e)
        {
            CommunicationSettingsModal communicationSettingsModal = new CommunicationSettingsModal();
            communicationSettingsModal.Owner = this;
            communicationSettingsModal.ShowDialog();
        }
         
        private void m_trainSimItem_Click(object sender, EventArgs e)
        {
            TrainSimModal trainSimModal = new TrainSimModal();
            trainSimModal.Owner = this;
            trainSimModal.Show();
        }

        private void m_routeItem_Click(object sender, EventArgs e)
        {
            TrackSettingsModal trackSettingsModal = new TrackSettingsModal();
            trackSettingsModal.Owner = this;
            trackSettingsModal.ShowDialog();
        }

        private void m_listViewVirtualOccupation_SelectedIndexChanged(object sender, EventArgs e)
        {

            int coun = 0;
            foreach (ListViewItem lvw in m_listViewVirtualOccupation.Items)
            {


                if (coun % 2 == 0)// lvw.SubItems[coun].ToString() == "True")
                {
                    lvw.ForeColor = Color.Red;
                }

                coun++;
            }

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void m_dataGridViewAllTrains_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            //e.Cancel = false;
        }

        private void m_dataGridViewAllTrains_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void m_comboBoxTrain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void m_buttonStop_Click(object sender, EventArgs e)
        {
            //if(m_comboBoxTrain.SelectedItem != null)
            //{ 
            //    bool isGetValue = MainForm.m_allOBATP.TryGetValue(Convert.ToInt32(m_comboBoxTrain.SelectedIndex + 1), out OBATP OBATP);

            //    if (isGetValue)
            //        OBATP.RequestStopProcess();
            //}  

            if(STTimer.Enabled)
            {
                STTimer.Stop();
                STTimer.Interval = 1;
            }

            foreach (KeyValuePair<int, OBATP> item in m_allOBATP)
            {
                OBATP OBATP = item.Value;

                OBATP.RequestStopProcess();

            }
        }

        private void m_backgroundWorkerCommunicationLogs_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (Tuple<DateTime, string, string> messageToWriteLog in m_communicationLogs.GetConsumingEnumerable())
            {

                StringBuilder messageToWriteLogStringBuilder = new StringBuilder(messageToWriteLog.Item2);
                messageToWriteLogStringBuilder.Replace("*", messageToWriteLog.Item3);

                Logging.WriteCommunicationLog(messageToWriteLog.Item1, messageToWriteLogStringBuilder);

            }
        }

        private void m_backgroundWorkerUILogs_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (Tuple<string, Color> messageToWriteLog in m_UILogs.GetConsumingEnumerable())
            {

                DisplayManager.RichTextBoxInvoke(m_richTextBoxCommunicationLogs, messageToWriteLog.Item1, messageToWriteLog.Item2);

               

            }
        }

        public void SettingsWindowStatus(Enums.SettingsWindowStatus settingsWindowStatus, Enums.SettingsWindow settingsWindow)
        {
            if(settingsWindowStatus == Enums.SettingsWindowStatus.Open)
            {
                if (STTimer.Enabled)
                {
                    STTimer.Stop();
                    STTimer.Interval = 1;
                }
                    


                m_ATSSocket.Stop(m_settings.ATSCommunicationType, SocketCommunication.ClientType.ATS);
                m_WSATCSocket.Stop(m_settings.WSATCCommunicationType, SocketCommunication.ClientType.WSATC);



                foreach (KeyValuePair<int, OBATP> item in m_allOBATP)
                {
                    OBATP OBATP = item.Value;  

                    if (m_allTrains.Contains(OBATP))
                        m_allTrains.Remove(OBATP); 

                    if (OBATP.Status == Enums.Status.Start)
                    { 
                        OBATP.RequestStopProcess();
                      
                    }

                    OBATP.Dispose();

                    MainForm.m_WSATP_TO_OBATPMessageInComing.RemoveWatcher(OBATP);
                    MainForm.m_ATS_TO_OBATO_InitMessageInComing.RemoveWatcher(OBATP);
                    MainForm.m_ATS_TO_OBATO_MessageInComing.RemoveWatcher(OBATP); 

                }

                m_allOBATP.Clear();
                m_comboBoxTrain.Items.Clear();

                DisplayManager.ListViewItemsClearInvoke(m_listView);
                DisplayManager.ListViewItemsClearInvoke(m_listViewVirtualOccupation);
                DisplayManager.ListViewItemsClearInvoke(m_listViewFootPrintTracks);

                DisplayManager.TextBoxClearInvoke(m_textBoxCurrentTrainSpeedKM);
                DisplayManager.TextBoxClearInvoke(m_textBoxCurrentLocation);
                DisplayManager.TextBoxClearInvoke(m_textBoxRearCurrentLocation);
                DisplayManager.TextBoxClearInvoke(m_textBoxCurrentAcceleration);


                DisplayManager.PanelInvoke(panel1, default(Color));
                DisplayManager.PictureBoxInvoke(m_pictureBoxDoorStatus, Properties.Resources.doorclose);

                m_labelDoorCounter.Text = "";

                m_richTextBoxCommunicationLogs.Clear();
                m_richTextBoxTrainsLogs.Clear();

            }
            else
            {
                m_settings = m_settings.DeSerialize(m_settings);

                m_ATSSocket.Start(m_settings.ATSCommunicationType, SocketCommunication.ClientType.ATS, m_settings.ATSIPAddress, Convert.ToInt32(m_settings.ATSPort));
                m_WSATCSocket.Start(m_settings.WSATCCommunicationType, SocketCommunication.ClientType.WSATC, m_settings.WSATCIPAddress, Convert.ToInt32(m_settings.WSATCPort));


                foreach (int index in m_settings.Trains)
                {
                    int trainIndex = index + 1;
                    Enums.Train_ID train_ID = (Enums.Train_ID)trainIndex;
                     
                    OBATP OBATP = new OBATP(train_ID, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration, m_settings.TrainSpeedLimit, m_settings.TrainLength);

 
                    m_allOBATP.TryAdd(trainIndex, OBATP);

                    m_comboBoxTrain.Items.Add(train_ID.ToString());

                    MainForm.m_WSATP_TO_OBATPMessageInComing.AddWatcher(OBATP);
                    MainForm.m_ATS_TO_OBATO_InitMessageInComing.AddWatcher(OBATP);
                    MainForm.m_ATS_TO_OBATO_MessageInComing.AddWatcher(OBATP);
                }

                if (m_comboBoxTrain.Items.Count > 0)
                    m_comboBoxTrain.SelectedIndex = 0;





            }



        }

        private void m_richTextBoxTrainsLogs_TextChanged(object sender, EventArgs e)
        {
            if (m_richTextBoxTrainsLogs.Text.Length > 5000)
            {

                m_richTextBoxTrainsLogs.ResetText();
            }

            m_richTextBoxTrainsLogs.ScrollToCaret();
        }
    }
}
