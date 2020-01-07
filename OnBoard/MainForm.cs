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
    public partial class MainForm : Form, ITrainMovementCreatedWatcher// ITrainObserverWatcher
    {
        XMLSerialization m_settings;
        SocketCommunication m_socketCommunication;
        //List<OBATPUIAdapter> m_ListList;
        internal static List<OBATP> m_ListList;
        
        internal static TrainObserver m_trainObserver;
        internal static WSATP_TO_OBATPMessageInComing m_WSATP_TO_OBATPMessageInComing;
        internal static ATS_TO_OBATO_InitMessageInComing m_ATS_TO_OBATO_InitMessageInComing;
        internal static ATS_TO_OBATO_MessageInComing m_ATS_TO_OBATO_MessageInComing;
        public static MainForm m_mf;
        internal static ConcurrentDictionary<int, OBATP> m_allOBATP;
        public static ThreadSafeList<Track> m_allTracks;
        public static List<Route> m_allRoute;
        internal static DataTable m_fromFileTracks; 
        internal static Route m_route;
        readonly object m_movement = new object();
        readonly object m_newRoute = new object();


        //List<UIOBATP> m_ListList; 
        //public List<Track> rou; 
        //DataTable m_dataTable = GetTable(); 
        //DataTable ahmet = new DataTable("VirOcc");

        BindingList<Track> bindingTrack = new BindingList<Track>();
        BindingList<OBATP> bindingAhmet = new BindingList<OBATP>();

        public MainForm()
        {
            InitializeComponent();

            m_mf = this;

            #region ayarları okuma
            m_settings = XMLSerialization.Singleton();
            m_settings = m_settings.DeSerialize(m_settings);
            #endregion

            CheckForIllegalCrossThreadCalls = false;

            m_allOBATP = new ConcurrentDictionary<int, OBATP>();
            m_socketCommunication = SocketCommunication.Singleton();
            m_trainObserver = new TrainObserver();
            
            m_WSATP_TO_OBATPMessageInComing = new WSATP_TO_OBATPMessageInComing();
            m_ATS_TO_OBATO_InitMessageInComing = new ATS_TO_OBATO_InitMessageInComing();
            m_ATS_TO_OBATO_MessageInComing = new ATS_TO_OBATO_MessageInComing();


            MainForm.m_trainObserver.AddTrainMovementCreatedWatcher(this);

            //excel tablosundan track listesini ve özelliklerini okuyoruz    
            m_fromFileTracks = FileOperation.ReadTrackTableInExcel();


            //m_ListList = new List<OBATPUIAdapter>();
            m_ListList = new List<OBATP>();

            if (m_settings.TrackInput == Enums.TrackInput.Manuel)
            {
                m_allTracks = Track.AllTracksAAA(m_settings.RouteTrack);
            }
            else if (m_settings.TrackInput == Enums.TrackInput.FromFile)
            {
                m_allTracks = Track.AllTracks(m_fromFileTracks);
            }




            m_route = Route.CreateNewRoute(m_settings.StartTrackID, m_settings.EndTrackID, m_allTracks);

            
            //DataTable rt = FileOperation.ReadRouteTableInExcel();
            //m_allRoute = Route.AllRoute(rt, m_allTracks);

            DataTable rt = FileOperation.ReadRouteTableInExcel();
            Route route1 = new Route();
            m_allRoute = route1.AllRoute(rt, m_allTracks);
           


            //m_socketCommunication.Start(SocketCommunication.CommunicationType.Client, "10.2.149.17", 205);
            //m_socketCommunication.Start(SocketCommunication.CommunicationType.Client, m_settings.OBATPToWSATCIPAddress, Convert.ToInt32(m_settings.OBATPToWSATCPort)); 
            m_socketCommunication.Start(SocketCommunication.CommunicationType.Client, "127.0.0.1", 5050);




            foreach (int index in m_settings.Trains)
            {
                int trainIndex = index + 1;
                Enums.Train_ID train_ID = (Enums.Train_ID)trainIndex;

                Route route = new Route();
                route = Route.CreateNewRoute(10105, m_settings.EndTrackID, m_allTracks);

                Route route8 = new Route();
                route8 = Route.CreateNewRoute(10103, m_settings.EndTrackID, m_allTracks);


                OBATP OBATP;
                if (trainIndex == 8)
                     OBATP = new OBATP(train_ID, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration, m_settings.TrainSpeedLimit, m_settings.TrainLength, route8);
                else
                     OBATP = new OBATP(train_ID, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration, m_settings.TrainSpeedLimit, m_settings.TrainLength, route);
                //OBATP OBATP = new OBATP(train_ID, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration, m_settings.TrainSpeedLimit, m_settings.TrainLength);

                m_allOBATP.TryAdd(trainIndex, OBATP);

                m_comboBoxTrain.Items.Add(train_ID.ToString());



                MainForm.m_WSATP_TO_OBATPMessageInComing.AddWatcher(OBATP);
                MainForm.m_ATS_TO_OBATO_InitMessageInComing.AddWatcher(OBATP);
                MainForm.m_ATS_TO_OBATO_MessageInComing.AddWatcher(OBATP);
            }

            #region controls doublebuffered
            UIOperation.SetDoubleBuffered(m_listView);
            UIOperation.SetDoubleBuffered(m_listViewFootPrintTracks);
            UIOperation.SetDoubleBuffered(m_listViewVirtualOccupation);
            UIOperation.SetDoubleBuffered(m_dataGridViewAllTrains);
            #endregion


            bindingTrack.ListChanged += new ListChangedEventHandler(listOfParts_ListChanged);
        }

 


        private void MainForm_Load(object sender, EventArgs e)
        {
          

          



            //TrainSimModal trainSimModal = new TrainSimModal();
            //trainSimModal.Owner = this;
            //trainSimModal.Show();


            //for (int i = 1; i < 31; i++)
            //{
            //    m_comboBoxTrain.Items.Add("Train" + i.ToString());
            //}

            m_comboBoxTrain.SelectedIndex = 0;




            //m_bindingSourceTrains.DataSource = m_ListList;
            //m_dataGridViewAllTrains.DataSource = m_bindingSourceTrains;

            //m_bindingSourceTrains.DataSource = m_ListList;
            m_dataGridViewAllTrains.DataSource = bindingAhmet;

            //m_listViewVirtualOccupation.DataBindings = ahmet; 

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
           




        }

        private void m_buttonSave_Click(object sender, EventArgs e)
        {



        }

        private void m_buttonStart_Click(object sender, EventArgs e)
        {


            if (!m_backgroundWorker.IsBusy)
                m_backgroundWorker.RunWorkerAsync();



        }


        UIOBATP denemem = new UIOBATP();


        #region TrainMovementCreated 

        public void TrainMovementCreated(OBATP OBATP)
        {
            try
            { 
                lock (m_movement)
                {
                     

                    if (OBATP.Vehicle.TrainID.ToString() == "Train8")
                    {

                    }

                    int index = bindingAhmet.ToList().FindIndex(x => x == OBATP);

                    bindingAhmet.ResetItem(index);

                    if ((m_comboBoxTrain.SelectedItem != null) &&  (m_comboBoxTrain.SelectedItem.ToString() == OBATP.Vehicle.TrainName))
                    { 
                        //general train
                        DisplayManager.TextBoxInvoke(m_textBoxCurrentTrainSpeedKM, OBATP.Vehicle.CurrentTrainSpeedKMH.ToString());
                        DisplayManager.TextBoxInvoke(m_textBoxCurrentLocation, OBATP.ActualFrontOfTrainCurrent.Location.ToString("0.##"));
                        DisplayManager.TextBoxInvoke(m_textBoxRearCurrentLocation, OBATP.ActualRearOfTrainCurrent.Location.ToString("0.##"));
                        DisplayManager.TextBoxInvoke(m_textBoxCurrentAcceleration, (OBATP.Vehicle.CurrentAcceleration / 100).ToString("0.##"));

                        if (OBATP.DoorStatus == Enums.DoorStatus.Open)
                            m_pictureBoxDoorStatus.Image = Properties.Resources.dooropen;
                        else
                            m_pictureBoxDoorStatus.Image = Properties.Resources.doorclose;

                        //DisplayManager.TextBoxInvoke(m_textBoxDoorTimerCounter, OBATP.DoorTimerCounter.ToString());

                        //if (OBATP.DoorStatus == Enums.DoorStatus.Open)
                        //    DisplayManager.TextBoxInvoke(m_textBoxDoorStatus, "Open");
                        //else
                        //    DisplayManager.TextBoxInvoke(m_textBoxDoorStatus, "Close");

                        var ds = m_comboBoxTrain.SelectedItem;

                        var dssadasd = m_comboBoxTrain.Text;
                        var asdasdsaddssadasd = m_comboBoxTrain.SelectedText;


                        List<int> listValueRoute_Tracks = m_listView.Items.Cast<ListViewItem>().Select(item => Convert.ToInt32(item.Text)).ToList();
                        bool difflistValuesRoute_Tracks = OBATP.m_route.Route_Tracks.Select(x => x.Track_ID).ToList().SequenceEqual(listValueRoute_Tracks);


                        if (!difflistValuesRoute_Tracks)
                        {
                            m_listView.Items.Clear();

                            foreach (var item in OBATP.m_route.Route_Tracks)
                            {
                                m_listView.Items.Add(new ListViewItem(new string[] { item.Track_ID.ToString(), item.Station_Name, item.SpeedChangeVMax.ToString() }));
                            }
                        }



                        List<int> listValuesVirtualOccupation = m_listViewVirtualOccupation.Items.Cast<ListViewItem>().Select(item => Convert.ToInt32(item.Text)).ToList();
                        bool difflistValuesVirtualOccupation = OBATP.TrainOnTracks.VirtualOccupationTracks.Select(x => x.Track_ID).ToList().SequenceEqual(listValuesVirtualOccupation);


                        if (!difflistValuesVirtualOccupation)
                        {
                            m_listViewVirtualOccupation.Items.Clear();
                            //OBATP.TrainOnTracks.VirtualOccupationTracks.ForEach(x => m_listViewVirtualOccupation.Items.Add(x.Track_ID.ToString()));
                            foreach (var item in OBATP.TrainOnTracks.VirtualOccupationTracks)
                            {
                                m_listViewVirtualOccupation.Items.Add(item.Track_ID.ToString());
                            }
                        }
                        //new ListViewItem(new string[] { x.Track_ID.ToString(), x.Station_Name })

                        List<int> listValuesFootPrintTracks = m_listViewFootPrintTracks.Items.Cast<ListViewItem>().Select(item => Convert.ToInt32(item.Text)).ToList();
                        bool difflistValuesFootPrintTracks = OBATP.TrainOnTracks.FootPrintTracks.Select(x => x.Track_ID).ToList().SequenceEqual(listValuesFootPrintTracks);

                        if (!difflistValuesFootPrintTracks)
                        {
                            m_listViewFootPrintTracks.Items.Clear();
                            //OBATP.TrainOnTracks.FootPrintTracks.ForEach(x => m_listViewFootPrintTracks.Items.Add(x.Track_ID.ToString()));

                            foreach (var item in OBATP.TrainOnTracks.FootPrintTracks)
                            {
                                m_listViewFootPrintTracks.Items.Add(item.Track_ID.ToString());
                            }
                        }




                        //int index = m_ListList.FindIndex(x => x == OBATP);

                        ////var idsfndex = m_ListList.Find(x => x.ID == osman.ID);

                        //if (index != -1)
                        //{
                        //    m_bindingSourceTrains.ResetItem(index);
                        //}



                        //trenin tracklerini kırmızıya boyama
                        foreach (ListViewItem li in m_listView.Items)
                        {

                            int itemText = Convert.ToInt32(li.Text);


                            if (OBATP.Vehicle.TrainID.ToString() == "Train8")
                            {

                            }
                            Track lolo = OBATP.TrainOnTracks.ActualLocationTracks.Find(x => x.Track_ID == itemText);

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

                    } 
                }

            }
            catch
            {

            }

        }
        #endregion
        void listOfParts_ListChanged(object sender, ListChangedEventArgs e)
        {
            //if(e.ListChangedType == ListChangedType.Reset)
            //{

            //    List<int> listValuesVirtualOccupation = m_listViewVirtualOccupation.Items.Cast<ListViewItem>().Select(item => Convert.ToInt32(item.Text)).ToList();
            //    bool difflistValuesVirtualOccupation = OBATP.TrainOnTracks.VirtualOccupationTracks.Select(x => x.Track_ID).ToList().SequenceEqual(listValuesVirtualOccupation);


            //    if (!difflistValuesVirtualOccupation)
            //    {
            //        m_listViewVirtualOccupation.Items.Clear();
            //        //OBATP.TrainOnTracks.VirtualOccupationTracks.ForEach(x => m_listViewVirtualOccupation.Items.Add(x.Track_ID.ToString()));
            //        foreach (var item in OBATP.TrainOnTracks.VirtualOccupationTracks)
            //        {
            //            m_listViewVirtualOccupation.Items.Add(item.Track_ID.ToString());
            //        }
            //    }
            //}

            //string dsf = (e.ListChangedType.ToString());

            //var dfsdfsdf = e.PropertyDescriptor;
        }

        public void TrainMovementRouteCreated( Route route)
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
       

        private void m_trainItem_Click(object sender, EventArgs e)
        {
            TrainSettingsModal trainSettingsModal = new TrainSettingsModal();
            trainSettingsModal.Owner = this;
            trainSettingsModal.ShowDialog();
        }

        private void m_generalItem_Click(object sender, EventArgs e)
        {
            GeneralSettingsModal generalSettingsModal = new GeneralSettingsModal();
            generalSettingsModal.Owner = this;
            generalSettingsModal.ShowDialog();
        }

     
        //OBATPUIAdapter osman = new OBATPUIAdapter();
        private void m_backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //try
            //{
            m_settings = m_settings.DeSerialize(m_settings);
            //List<Track> route = Route.CreateNewRoute(10101, 10103, allTracks);


            Stopwatch sw = new Stopwatch();


            int counter = 0;


            //OBATPPool<OBATP> pool = new OBATPPool<OBATP>(() => new OBATP());




            foreach (KeyValuePair<int, OBATP> item in m_allOBATP)
            {

                OBATP OBATP = item.Value;

                //MainForm.m_WSATP_TO_OBATPMessageInComing.AddWatcher(OBATP);
                //MainForm.m_ATS_TO_OBATO_InitMessageInComing.AddWatcher(OBATP);
                //MainForm.m_ATS_TO_OBATO_MessageInComing.AddWatcher(OBATP);



                //OBATPUIAdapter osman = new OBATPUIAdapter();

                //osman.AdaptData1(OBATP);


                //int index = m_ListList.FindIndex(x => x == osman);



                //if (index != -1)
                //{
                //    m_bindingSourceTrains.ResetItem(index);
                //}
                //else
                //{ 
                //m_ListList.Add(osman);

                //m_ListList.Add(OBATP);
                //m_bindingSourceTrains.ResetBindings(false);

                bindingAhmet.Add(OBATP);
                //}



                OBATP.RequestStartProcess();



                #region waitingstart start next train
                int sleepTime = Convert.ToInt32(m_settings.TrainFrequency);
                sw.Start();

                while (sw.Elapsed.TotalSeconds < (sleepTime))
                {

                }

                sw.Reset();
                #endregion

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

            //OBATP dsfregr = pool..GetObject();
            //}
            ////catch
            //{

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
            if (m_richTextBox.Text.Length > 5000)
            {
                
                m_richTextBox.ResetText();
            }

            m_richTextBox.ScrollToCaret();
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
    }
}
