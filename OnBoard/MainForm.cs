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
    public partial class MainForm : Form, ITrainMovementWatcher
    {
        XMLSerialization m_settings;
        SocketCommunication m_socketCommunication;
        //List<OBATPUIAdapter> m_ListList;
        List<OBATP> m_ListList;
        internal static TrainCreate m_trainCreate;
        internal static TrainMovementCreate m_trainMovement;
        internal static WSATP_TO_OBATPMessageInComing m_WSATP_TO_OBATPMessageInComing;
        internal static ATS_TO_OBATO_InitMessageInComing m_ATS_TO_OBATO_InitMessageInComing;
        internal static ATS_TO_OBATO_MessageInComing m_ATS_TO_OBATO_MessageInComing;
        public static MainForm m_mf;
        internal static ConcurrentDictionary<int, OBATP> m_allOBATP;
        public static List<Track> m_allTracks;
        public static List<Route> m_allRoute;
        internal static DataTable m_fromFileTracks; 
        internal static Route m_route;
        readonly object m_lolo = new object();



        //List<UIOBATP> m_ListList; 
        //public List<Track> rou; 
        //DataTable m_dataTable = GetTable(); 
        //DataTable ahmet = new DataTable("VirOcc");

       
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
            m_trainMovement = new TrainMovementCreate();
            m_trainCreate = new TrainCreate();
            m_WSATP_TO_OBATPMessageInComing = new WSATP_TO_OBATPMessageInComing();
            m_ATS_TO_OBATO_InitMessageInComing = new ATS_TO_OBATO_InitMessageInComing();
            m_ATS_TO_OBATO_MessageInComing = new ATS_TO_OBATO_MessageInComing();
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

            //m_route = Route.CreateNewRoute(m_settings.EndTrackID, m_settings.StartTrackID,  allTracks);
            //m_route = Route.CreateNewRoute(m_settings.StartTrackID, 10309, allTracks);

            //DataTable rt = FileOperation.ReadRouteTableInExcel();  
            //allRoute = Route.AllRoute(rt, asasas);

            //Route route = new Route();
            //allRoute = route.AllRoute(rt, asasas); 
            //List<Route> asdasdasdasdasd =  Route.AllRoute(rt, allTracks); 
            //ahmet.Columns.Add("ID"); 


            //m_socketCommunication.Start(SocketCommunication.CommunicationType.Client, "10.2.149.17", 205);
            //m_socketCommunication.Start(SocketCommunication.CommunicationType.Client, m_settings.OBATPToWSATCIPAddress, Convert.ToInt32(m_settings.OBATPToWSATCPort)); 
            m_socketCommunication.Start(SocketCommunication.CommunicationType.Client, "127.0.0.1", 5050);


            foreach (int index in m_settings.Trains)
            {
                int trainIndex = index;
                string trainName = "Train" + (trainIndex + 1).ToString();

                OBATP OBATP = new OBATP((Enums.Train_ID)trainIndex, trainName, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration, m_settings.TrainSpeedLimit, m_settings.TrainLength, m_route);

                m_allOBATP.TryAdd(index, OBATP);
            }

            #region controls doublebuffered
            UIOperation.SetDoubleBuffered(m_listViewFootPrintTracks);
            UIOperation.SetDoubleBuffered(m_listViewVirtualOccupation);
            UIOperation.SetDoubleBuffered(m_dataGridViewAllTrains);
            #endregion
        }

 


        private void MainForm_Load(object sender, EventArgs e)
        {

          //  Enums.TrainCoupled efeefefefeffe = Enums.TrainCoupled.MD1AndMD2Coupled;


          //  byte r = efeefefefeffe.TrainCoupledToHex();

          //  bool[] bools = new bool[8];


          //  bools[0] = true;
          //  bools[1] = true;
          //  bools[2] = true;
          //  bools[3] = true;


          //  BitArray a = new BitArray(bools);
          //  byte[] bytes = new byte[a.Length / 8];
          //  a.CopyTo(bytes, 0);

          //int safd =  Marshal.SizeOf(bytes[0]); 


            //byte osman =Byte.Parse("0xAA".Substring(2), NumberStyles.HexNumber);
            //byte sd = HelperClass.BoolToHex(true);
            //byte sd2 = HelperClass.BoolToHex(false);


            //byte lolo = false.BoolToHex();
            //byte lolo1 = true.BoolToHex();

     

          

            MainForm.m_trainMovement.AddWatcher(MainForm.m_mf);



            TrainSimModal trainSimModal = new TrainSimModal();
            trainSimModal.Owner = this;
            trainSimModal.Show();


            for (int i = 1; i < 31; i++)
            {
                m_comboBoxTrain.Items.Add("Train" + i.ToString());
            }

            m_comboBoxTrain.SelectedIndex = 0; 

     
            m_bindingSourceTrains.DataSource = m_ListList;
            m_dataGridViewAllTrains.DataSource = m_bindingSourceTrains;

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


        #region createdevent

        public void TrainMovementUI(UIOBATP UIOBATP)
        {
            //try
            //{
            //    if (UIOBATP != null)
            //    {
            //        lock (UIOBATP)
            //        {
            //            int index = m_ListList.FindIndex(x => x == UIOBATP);

            //            //int index = ListList.FindIndex(x => x == new UIOBATP()
            //            //{
            //            //    //ID = OBATP.Vehicle.TrainID.ToString(),
            //            //    ID = OBATP.Vehicle.TrainIndex.ToString(),
            //            //    Train_Name = OBATP.Vehicle.TrainName,
            //            //    Front_Track_ID = OBATP.ActualFrontOfTrainCurrent.Track.Track_ID.ToString(),
            //            //    Front_Track_Location = OBATP.ActualFrontOfTrainCurrent.Location.ToString("0.##"),
            //            //    Front_Track_Length = OBATP.ActualFrontOfTrainCurrent.Track.Track_Length.ToString(),
            //            //    Front_Track_Max_Speed = OBATP.ActualFrontOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString(),
            //            //    Rear_Track_ID = OBATP.ActualRearOfTrainCurrent.Track.Track_ID.ToString(),
            //            //    Rear_Track_Location = OBATP.ActualRearOfTrainCurrent.Location.ToString("0.##"),
            //            //    Rear_Track_Length = OBATP.ActualRearOfTrainCurrent.Track.Track_Length.ToString(),
            //            //    Rear_Track_Max_Speed = OBATP.ActualRearOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString(),
            //            //    Total_Route_Distance = OBATP.TotalTrainDistance.ToString("0.##")
            //            //});



            //            if (index != -1)
            //            {
            //                m_bindingSourceTrains.ResetItem(index);
            //            }
            //            else
            //            {
            //                m_ListList.Add(UIOBATP);


            //                m_bindingSourceTrains.ResetBindings(false);
            //            }
            //        }

            //    }
            //}
            //catch
            //{

            //}
       
        }

      
        //OBATPUIAdapter osman;

        public void TrainMovementCreated(OBATP OBATP)
        {
            try
            {



                lock (m_lolo)
                {

                    //List<int> listValuesVirtualOccupation = m_listViewVirtualOccupation.Items.Cast<ListViewItem>().Select(item => Convert.ToInt32(item.Text)).ToList();
                    //bool difflistValuesVirtualOccupation = OBATP.TrainOnTracks.VirtualOccupationTracks.Select(x => x.Track_ID).ToList().SequenceEqual(listValuesVirtualOccupation);


                    //if (!difflistValuesVirtualOccupation)
                    //{
                    //    m_listViewVirtualOccupation.Items.Clear();
                    //    //OBATP.TrainOnTracks.VirtualOccupationTracks.ForEach(x => m_listViewVirtualOccupation.Items.Add(x.Track_ID.ToString()));
                    //    foreach (var item in OBATP.TrainOnTracks.VirtualOccupationTracks)
                    //    {
                    //        m_listViewVirtualOccupation.Items.Add(item.Track_ID.ToString());
                    //    }  
                    //}
                    ////new ListViewItem(new string[] { x.Track_ID.ToString(), x.Station_Name })

                    //List<int> listValuesFootPrintTracks = m_listViewFootPrintTracks.Items.Cast<ListViewItem>().Select(item => Convert.ToInt32(item.Text)).ToList();
                    //bool difflistValuesFootPrintTracks = OBATP.TrainOnTracks.FootPrintTracks.Select(x => x.Track_ID).ToList().SequenceEqual(listValuesFootPrintTracks);

                    //if (!difflistValuesFootPrintTracks)
                    //{
                    //    m_listViewFootPrintTracks.Items.Clear();
                    //    //OBATP.TrainOnTracks.FootPrintTracks.ForEach(x => m_listViewFootPrintTracks.Items.Add(x.Track_ID.ToString()));

                    //    foreach (var item in OBATP.TrainOnTracks.FootPrintTracks)
                    //    {
                    //        m_listViewFootPrintTracks.Items.Add(item.Track_ID.ToString());
                    //    }
                    //}



                    //OBATPUIAdapter osman = new OBATPUIAdapter();
                    //osman.AdaptData1(OBATP);


                    int index = m_ListList.FindIndex(x => x == OBATP);

                    //var idsfndex = m_ListList.Find(x => x.ID == osman.ID);

                    if (index != -1)
                    {
                        m_bindingSourceTrains.ResetItem(index);
                    }


                    //OBATPUIAdapter osman =  OBATPUIAdapter.Singleton();
                    //OBATPUIAdapter osman = new OBATPUIAdapter();
                    //osman.AdaptData1(OBATP);


                    //int index = m_ListList.FindIndex(x => x.ID == osman.ID);

                    //var idsfndex = m_ListList.Find(x => x.ID == osman.ID);

                    //if (index != -1)
                    //{
                    //    m_bindingSourceTrains.ResetItem(index);
                    //}
                    //else
                    //{



                    //    //m_ListList.Add(osman);


                    //    m_bindingSourceTrains.ResetBindings(false);
                    //}



                    //int index = ListList.FindIndex(x => x.ID == UIOBATP.ID);



                    //int index = ListList.FindIndex(x => x == OBATP);

                    //int index = ListList.FindIndex(x => x == denemem);

                    //int index = ListList.FindIndex(x => x == new UIOBATP()
                    //{
                    //    //ID = OBATP.Vehicle.TrainID.ToString(),
                    //    ID = OBATP.Vehicle.TrainIndex.ToString(),
                    //    Train_Name = OBATP.Vehicle.TrainName,
                    //    Front_Track_ID = OBATP.ActualFrontOfTrainCurrent.Track.Track_ID.ToString(),
                    //    Front_Track_Location = OBATP.ActualFrontOfTrainCurrent.Location.ToString("0.##"),
                    //    Front_Track_Length = OBATP.ActualFrontOfTrainCurrent.Track.Track_Length.ToString(),
                    //    Front_Track_Max_Speed = OBATP.ActualFrontOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString(),
                    //    Rear_Track_ID = OBATP.ActualRearOfTrainCurrent.Track.Track_ID.ToString(),
                    //    Rear_Track_Location = OBATP.ActualRearOfTrainCurrent.Location.ToString("0.##"),
                    //    Rear_Track_Length = OBATP.ActualRearOfTrainCurrent.Track.Track_Length.ToString(),
                    //    Rear_Track_Max_Speed = OBATP.ActualRearOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString(),
                    //    Total_Route_Distance = OBATP.TotalTrainDistance.ToString("0.##")
                    //});

                    //int index = ListList.FindIndex(x => x.ID == 1.ToString());


                    //if (index != -1)
                    //{
                    //    bindingSource1.ResetItem(index);
                    //}
                    //else
                    //{

                    //    //UIOBATP UIOBATP = new UIOBATP()
                    //    //{
                    //    //    //ID = OBATP.Vehicle.TrainID.ToString(),
                    //    //    ID = OBATP.Vehicle.TrainIndex.ToString(),
                    //    //    Train_Name = OBATP.Vehicle.TrainName,
                    //    //    Front_Track_ID = OBATP.ActualFrontOfTrainCurrent.Track.Track_ID.ToString(),
                    //    //    Front_Track_Location = OBATP.ActualFrontOfTrainCurrent.Location.ToString("0.##"),
                    //    //    Front_Track_Length = OBATP.ActualFrontOfTrainCurrent.Track.Track_Length.ToString(),
                    //    //    Front_Track_Max_Speed = OBATP.ActualFrontOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString(),
                    //    //    Rear_Track_ID = OBATP.ActualRearOfTrainCurrent.Track.Track_ID.ToString(),
                    //    //    Rear_Track_Location = OBATP.ActualRearOfTrainCurrent.Location.ToString("0.##"),
                    //    //    Rear_Track_Length = OBATP.ActualRearOfTrainCurrent.Track.Track_Length.ToString(),
                    //    //    Rear_Track_Max_Speed = OBATP.ActualRearOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString(),
                    //    //    Total_Route_Distance = OBATP.TotalTrainDistance.ToString("0.##")
                    //    //};




                    //    //ListList.Add(UIOBATP);

                    //    //ListList.Add(OBATP);



                    //    denemem.ID = OBATP.Vehicle.TrainIndex.ToString();
                    //    denemem.Train_Name = OBATP.Vehicle.TrainName;
                    //    denemem.Front_Track_ID = OBATP.ActualFrontOfTrainCurrent.Track.Track_ID.ToString();
                    //    denemem.Front_Track_Location = OBATP.ActualFrontOfTrainCurrent.Location.ToString("0.##");
                    //    denemem.Front_Track_Length = OBATP.ActualFrontOfTrainCurrent.Track.Track_Length.ToString();
                    //    denemem.Front_Track_Max_Speed = OBATP.ActualFrontOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString();
                    //    denemem.Rear_Track_ID = OBATP.ActualRearOfTrainCurrent.Track.Track_ID.ToString();
                    //    denemem.Rear_Track_Location = OBATP.ActualRearOfTrainCurrent.Location.ToString("0.##");
                    //    denemem.Rear_Track_Length = OBATP.ActualRearOfTrainCurrent.Track.Track_Length.ToString();
                    //    denemem.Rear_Track_Max_Speed = OBATP.ActualRearOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString();
                    //    denemem.Total_Route_Distance = OBATP.TotalTrainDistance.ToString("0.##");



                    //    ListList.Add(denemem);

                    //    bindingSource1.ResetBindings(true);
                    //}

                    //OBATP sdvsdffs = ListList.Where(x => x.OBATP_ID == OBATP.OBATP_ID).Select(x => x).Single();//.ToList();

                    //if(sdvsdffs != null )
                    //{
                    //    int index = ListList.FindIndex(x=> x =sdvsdffs);


                    //}




                    //binding.RaiseListChangedEvents = false;
                    //binding.Clear();
                    ////var dsfsdf = binding.AllowEdit;
                    //binding.Add(OBATP);
                    //binding.RaiseListChangedEvents = true;
                    //binding.ResetItem(0);

                    //binding.ResetBindings();

                    //foreach (DataRow row in m_dataTable.Rows)
                    //{
                    //    if (row["Train_Name"].ToString() == OBATP.Vehicle.TrainName)
                    //    {
                    //        row.SetField("Speed", OBATP.Vehicle.CurrentTrainSpeedKMH.ToString());

                    //        row.SetField("Front_Track", OBATP.ActualFrontOfTrainCurrent.Track.Track_ID.ToString());
                    //        //row.SetField("Front_Track_Location", OBATP.ActualFrontOfTrainCurrentLocation.ToString("0.##"));
                    //        row.SetField("Front_Track_Location", OBATP.ActualFrontOfTrainCurrent.Location.ToString("0.##"));
                    //        row.SetField("Front_Track_Length", OBATP.ActualFrontOfTrainCurrent.Track.Track_Length.ToString());
                    //        row.SetField("Front_Track_Max_Speed", OBATP.ActualFrontOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString());


                    //        row.SetField("Rear_Track", OBATP.ActualRearOfTrainCurrent.Track.Track_ID.ToString());
                    //        //row.SetField("Rear_Track_Location", OBATP.ActualRearOfTrainCurrentLocation.ToString("0.##"));
                    //        row.SetField("Rear_Track_Location", OBATP.ActualRearOfTrainCurrent.Location.ToString("0.##"));
                    //        row.SetField("Rear_Track_Length", OBATP.ActualRearOfTrainCurrent.Track.Track_Length.ToString());
                    //        row.SetField("Rear_Track_Max_Speed", OBATP.ActualRearOfTrainCurrent.Track.MaxTrackSpeedKMH.ToString());

                    //        row.SetField("Total_Route_Distance", OBATP.TotalTrainDistance.ToString("0.##"));
                    //    }

                    //}


                    //foreach (ListViewItem li in m_listView.Items)
                    //{

                    //    int itemText = Convert.ToInt32(li.Text);


                    //    Track lolo = OBATP.TrainOnTracks.ActualLocationTracks.Find(x => x.Track_ID == itemText);

                    //    if (lolo != null)
                    //    {
                    //        //li.ForeColor = Color.Red;
                    //        li.BackColor = Color.Red;
                    //    }
                    //    else
                    //    {
                    //        li.BackColor = Color.White;
                    //    }
                    //}


                    //foreach (Track item in OBATP.TrainOnTracks.ActualLocationTracks)
                    //{
                    //   ListViewItem sdffsd = m_listView.FindItemWithText(item.Track_ID.ToString());

                    //    sdffsd.ForeColor = Color.Red;
                    //}
                    //foreach (Track item in OBATP.fdgkldfgkljdlfkgf)
                    //{
                    //    ListViewItem sdffsd = m_listView.FindItemWithText(item.Track_ID.ToString());

                    //    sdffsd.ForeColor = Color.Red;
                    //}



                    //if (m_comboBoxTrain.SelectedIndex == OBATP.Vehicle.TrainIndex - 1)
                    //{

                    //    //general train
                    //    DisplayManager.TextBoxInvoke(m_textBoxCurrentTrainSpeedKM, OBATP.Vehicle.CurrentTrainSpeedKMH.ToString());
                    //    DisplayManager.TextBoxInvoke(m_textBoxCurrentLocation, OBATP.ActualFrontOfTrainCurrent.Location.ToString("0.##"));
                    //    DisplayManager.TextBoxInvoke(m_textBoxRearCurrentLocation, OBATP.ActualRearOfTrainCurrent.Location.ToString("0.##"));
                    //    DisplayManager.TextBoxInvoke(m_textBoxCurrentAcceleration, (OBATP.Vehicle.CurrentAcceleration / 100).ToString("0.##"));

                    //    if (OBATP.DoorStatus == Enums.DoorStatus.Open)
                    //        m_pictureBoxDoorStatus.Image = Properties.Resources.dooropen;
                    //    else
                    //        m_pictureBoxDoorStatus.Image = Properties.Resources.doorclose;





                    //    //DisplayManager.TextBoxInvoke(m_textBoxDoorTimerCounter, OBATP.DoorTimerCounter.ToString());

                    //    //if (OBATP.DoorStatus == Enums.DoorStatus.Open)
                    //    //    DisplayManager.TextBoxInvoke(m_textBoxDoorStatus, "Open");
                    //    //else
                    //    //    DisplayManager.TextBoxInvoke(m_textBoxDoorStatus, "Close");


                    //}


                }

            }
            catch
            {

            }

        }

        public void TrainMovementRouteCreated(Route route)
        {
            //if (m_comboBoxTrain.SelectedIndex == OBATP.Vehicle.TrainIndex)
            //{
            m_listView.Items.Clear();
            route.Route_Tracks.ForEach(x => m_listView.Items.Add(new ListViewItem(new string[] { x.Track_ID.ToString(), x.Station_Name, x.SpeedChangeVMax.ToString() })));
            //}
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

                MainForm.m_WSATP_TO_OBATPMessageInComing.AddWatcher(OBATP);
                MainForm.m_ATS_TO_OBATO_InitMessageInComing.AddWatcher(OBATP);
                MainForm.m_ATS_TO_OBATO_MessageInComing.AddWatcher(OBATP);



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

                m_ListList.Add(OBATP);
                m_bindingSourceTrains.ResetBindings(false);
                //}



                OBATP.RequestStartProcess();



                #region waitingstart start next train
                //int sleepTime = Convert.ToInt32(m_settings.TrainFrequency);
                //sw.Start();

                //while (sw.Elapsed.TotalSeconds < (sleepTime))
                //{

                //}

                //sw.Reset();
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



        static DataTable GetTable()
        {
             DataTable table = new DataTable("OBATP");

            table.Columns.Add("ID");
            table.Columns.Add("Train_Name");
            table.Columns.Add("Speed");
            table.Columns.Add("Front_Track");
            table.Columns.Add("Front_Track_Location");
            table.Columns.Add("Front_Track_Length");
            
            table.Columns.Add("Front_Track_Max_Speed");
            table.Columns.Add("Rear_Track");
            table.Columns.Add("Rear_Track_Location");
            table.Columns.Add("Rear_Track_Length");
            table.Columns.Add("Rear_Track_Max_Speed");
            table.Columns.Add("Total_Route_Distance"); 

            //table.Columns["ID"].AutoIncrement = true;
            //table.Columns["ID"].AutoIncrementSeed = 1;
            //table.Columns["ID"].AutoIncrementStep = 1;

            return table;
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //g.DrawLine(Pens.Blue, new Point(50, 100), new Point(100, 40));

            //g.Dispose();

            float dsf = g.DpiX;

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
    }
}
