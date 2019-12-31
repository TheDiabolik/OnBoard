using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace OnBoard
{
    public partial class MainForm : Form, ITrainMovementWatcher
    {
        XMLSerialization m_settings;
        internal static TrainMovementCreate m_trainMovement;
        internal static WSATP_TO_OBATPMessageInComing m_WSATP_TO_OBATPMessageInComing;
        internal static ATS_TO_OBATO_InitMessageInComing m_ATS_TO_OBATO_InitMessageInComing;
        internal static ATS_TO_OBATO_MessageInComing m_ATS_TO_OBATO_MessageInComing;
        public static MainForm m_mf;
        readonly object m_lolo = new object();

        SocketCommunication m_socketCommunication;


        //public List<Track> rou;
        ConcurrentDictionary<int, OBATP> m_allOBATP;
        public static List<Track> allTracks;
        public static List<Route> allRoute;

        DataTable m_dataTable = GetTable();

        DataTable ahmet = new DataTable("VirOcc");

        internal static DataTable m_fromFileTracks;

        internal static Route m_route;
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
            m_WSATP_TO_OBATPMessageInComing = new WSATP_TO_OBATPMessageInComing();
            m_ATS_TO_OBATO_InitMessageInComing = new  ATS_TO_OBATO_InitMessageInComing();
            m_ATS_TO_OBATO_MessageInComing = new ATS_TO_OBATO_MessageInComing();
            //excel tablosundan track listesini ve özelliklerini okuyoruz    
            m_fromFileTracks = FileOperation.ReadTrackTableInExcel(); 
  

            if (m_settings.TrackInput == Enums.TrackInput.Manuel)
            {
                allTracks = Track.AllTracksAAA(m_settings.RouteTrack);
            }
            else if (m_settings.TrackInput == Enums.TrackInput.FromFile)
            {
                allTracks = Track.AllTracks(m_fromFileTracks);
            }




            m_route = Route.CreateNewRoute(m_settings.StartTrackID, m_settings.EndTrackID, allTracks);
            //m_route = Route.CreateNewRoute(m_settings.StartTrackID, 10309, allTracks);

            //DataTable rt = FileOperation.ReadRouteTableInExcel();  
            //allRoute = Route.AllRoute(rt, asasas);

            //Route route = new Route();
            //allRoute = route.AllRoute(rt, asasas);


            //List<Route> asdasdasdasdasd =  Route.AllRoute(rt, allTracks);



            ahmet.Columns.Add("ID");


            UIOperation.SetDoubleBuffered(m_listViewFootPrintTracks);
            UIOperation.SetDoubleBuffered(m_listViewVirtualOccupation);


            //m_socketCommunication.Start(SocketCommunication.CommunicationType.Client, "10.2.149.17", 205);
            m_socketCommunication.Start(SocketCommunication.CommunicationType.Client, "127.0.0.1", 5050);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

           


            TrainSimModal trainSimModal = new TrainSimModal();
            trainSimModal.Owner = this;
            trainSimModal.Show();


            for (int i = 1; i < 31; i++)
            {
                m_comboBoxTrain.Items.Add("Train" + i.ToString());
            }  

            m_comboBoxTrain.SelectedIndex = 0;



            //m_dataGridViewAllTrains.DataSource = m_allOBATP.;


            m_dataGridViewAllTrains.DataSource = m_dataTable;

            //m_listViewVirtualOccupation.DataBindings = ahmet;

            //m_dataGridViewAllTrains.ColumnCount = 7;


            //table.Columns.Add("ID");
            //table.Columns.Add("Train_Name");
            //table.Columns.Add("Speed");
            //table.Columns.Add("Front_Track");
            //table.Columns.Add("Front_Track_Location");
            //table.Columns.Add("Front_Track_Max_Speed");
            //table.Columns.Add("Rear_Track");
            //table.Columns.Add("Rear_Track_Location");
            //table.Columns.Add("Rear_Track_Max_Speed");
            //table.Columns.Add("Total_Route_Distance");




            m_dataGridViewAllTrains.Columns[0].HeaderText = "ID";
            m_dataGridViewAllTrains.Columns[1].HeaderText = "Train Name";
            m_dataGridViewAllTrains.Columns[2].HeaderText = "Speed(km/h)";
            m_dataGridViewAllTrains.Columns[3].HeaderText = "Front Track ID";
            m_dataGridViewAllTrains.Columns[4].HeaderText = "Front Track Location";
            m_dataGridViewAllTrains.Columns[5].HeaderText = "Front Track Length";

            m_dataGridViewAllTrains.Columns[6].HeaderText = "Front Track Max Speed";
            m_dataGridViewAllTrains.Columns[7].HeaderText = "Rear Track ID";
            m_dataGridViewAllTrains.Columns[8].HeaderText = "Rear Track Location";
            m_dataGridViewAllTrains.Columns[9].HeaderText = "Rear Track Length";
            m_dataGridViewAllTrains.Columns[10].HeaderText = "Rear Track Max Speed";
            m_dataGridViewAllTrains.Columns[11].HeaderText = "Total Route Distance(cm)";

            m_dataGridViewAllTrains.Columns[0].Width = 50;
            m_dataGridViewAllTrains.Columns[1].Width = 70;




           

        }

        private void m_buttonSave_Click(object sender, EventArgs e)
        {

            

        }

        private void m_buttonStart_Click(object sender, EventArgs e)
        {

       
            if(!m_backgroundWorker.IsBusy)
                m_backgroundWorker.RunWorkerAsync();


           
        }



        #region createdevent
        public void TrainMovementCreated(OBATP OBATP)
        {

            lock (m_lolo)
            { 

                List<int> listValuesVirtualOccupation = m_listViewVirtualOccupation.Items.Cast<ListViewItem>().Select(item => Convert.ToInt32(item.Text)).ToList();
                bool difflistValuesVirtualOccupation = OBATP.TrainOnTracks.VirtualOccupationTracks.Select(x => x.Track_ID).ToList().SequenceEqual(listValuesVirtualOccupation);


                if (!difflistValuesVirtualOccupation)
                {
                    m_listViewVirtualOccupation.Items.Clear();
                    OBATP.TrainOnTracks.VirtualOccupationTracks.ForEach(x => m_listViewVirtualOccupation.Items.Add(x.Track_ID.ToString()));
                }
                //new ListViewItem(new string[] { x.Track_ID.ToString(), x.Station_Name })

                List<int> listValuesFootPrintTracks = m_listViewFootPrintTracks.Items.Cast<ListViewItem>().Select(item => Convert.ToInt32(item.Text)).ToList();
                bool difflistValuesFootPrintTracks = OBATP.TrainOnTracks.FootPrintTracks.Select(x => x.Track_ID).ToList().SequenceEqual(listValuesFootPrintTracks);

                if (!difflistValuesFootPrintTracks)
                {
                    m_listViewFootPrintTracks.Items.Clear();
                    OBATP.TrainOnTracks.FootPrintTracks.ForEach(x => m_listViewFootPrintTracks.Items.Add(x.Track_ID.ToString()));
                }






                foreach (DataRow row in m_dataTable.Rows)
                {
                    if (row["Train_Name"].ToString() == OBATP.Vehicle.TrainName)
                    {
                        row.SetField("Speed", OBATP.Vehicle.CurrentTrainSpeedKMH.ToString());

                        row.SetField("Front_Track", OBATP.FrontOfTrainCurrentTrack.Track_ID.ToString());
                        row.SetField("Front_Track_Location", OBATP.ActualFrontOfTrainCurrentLocation.ToString("0.##"));
                        row.SetField("Front_Track_Length", OBATP.FrontOfTrainCurrentTrack.Track_Length.ToString());
                        row.SetField("Front_Track_Max_Speed", OBATP.FrontOfTrainCurrentTrack.MaxTrackSpeedKMH.ToString());


                        row.SetField("Rear_Track", OBATP.RearOfTrainCurrentTrack.Track_ID.ToString());
                        row.SetField("Rear_Track_Location", OBATP.ActualRearOfTrainCurrentLocation.ToString("0.##"));
                        row.SetField("Rear_Track_Length", OBATP.RearOfTrainCurrentTrack.Track_Length.ToString());
                        row.SetField("Rear_Track_Max_Speed", OBATP.RearOfTrainCurrentTrack.MaxTrackSpeedKMH.ToString());

                        row.SetField("Total_Route_Distance", OBATP.TotalTrainDistance.ToString("0.##"));
                    }

                }


                foreach (ListViewItem li in m_listView.Items)
                {

                    int itemText = Convert.ToInt32(li.Text);


                   Track lolo = OBATP.TrainOnTracks.ActualLocationTracks.Find(x => x.Track_ID == itemText);

                    if (lolo != null )
                    {
                        //li.ForeColor = Color.Red;
                        li.BackColor = Color.Red;
                    }
                    else
                    {
                        li.BackColor = Color.White;
                    }
                }


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



                if (m_comboBoxTrain.SelectedIndex == OBATP.Vehicle.TrainIndex - 1)
                {

                    //general train
                    DisplayManager.TextBoxInvoke(m_textBoxCurrentTrainSpeedKM, OBATP.Vehicle.CurrentTrainSpeedKMH.ToString());
                    DisplayManager.TextBoxInvoke(m_textBoxCurrentLocation, OBATP.ActualFrontOfTrainCurrentLocation.ToString("0.##"));
                    DisplayManager.TextBoxInvoke(m_textBoxRearCurrentLocation, OBATP.ActualRearOfTrainCurrentLocation.ToString("0.##"));
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


                }


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

        private void m_backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            m_settings = m_settings.DeSerialize(m_settings);
            //List<Track> route = Route.CreateNewRoute(10101, 10103, allTracks);


            Stopwatch sw = new Stopwatch();


            int counter = 0;


            OBATPPool<OBATP> pool = new OBATPPool<OBATP>(() => new OBATP());


            foreach (var item in m_settings.Trains)
            {
                counter++;


                int trainIndex = item;
                string trainName = "Train" + (trainIndex + 1).ToString();

                DataRow row2 = m_dataTable.NewRow();
                row2["ID"] = counter;
                row2["Train_Name"] = trainName;
                m_dataTable.Rows.Add(row2);

                // DisplayManager.RichTextBoxInvoke(m_richTextBox, trainName + " is Created...", Color.Black);


              
               


                OBATP oBATP = new OBATP(Enums.Train_ID.Train1, trainName, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration, m_settings.TrainSpeedLimit, m_settings.TrainLength, m_route);

                MainForm.m_WSATP_TO_OBATPMessageInComing.AddWatcher(oBATP);
                MainForm.m_ATS_TO_OBATO_InitMessageInComing.AddWatcher(oBATP);
                MainForm.m_ATS_TO_OBATO_MessageInComing.AddWatcher(oBATP);

                //pool.PutObject(oBATP);


                // //m_allOBATP.TryAdd(trainIndex, oBATP);
                // //m_allOBATP.AddOrUpdate(trainIndex, oBATP, (s, i) => oBATP);

                oBATP.RequestStartProcess();


                // int sleepTime = Convert.ToInt32(m_settings.TrainFrequency);

                // sw.Start();



                // while (sw.Elapsed.TotalSeconds < (sleepTime))
                // {

                // }

                // sw.Reset();


            }


            //OBATP dsfregr = pool.GetObject();
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

            var dsfs= m_dataGridViewAllTrains.SelectedRows[0];

            //DisplayManager.TextBoxInvoke(m_textBoxCurrentTrainSpeedKM, OBATP.Vehicle.CurrentTrainSpeedKMH.ToString());
            //DisplayManager.TextBoxInvoke(m_textBoxCurrentLocation, OBATP.RealFrontCurrentLocation.ToString());
            //DisplayManager.TextBoxInvoke(m_textBoxRearCurrentLocation, OBATP.RealRearCurrentLocation.ToString());
            //DisplayManager.TextBoxInvoke(m_textBoxCurrentAcceleration, (OBATP.Vehicle.CurrentAcceleration / 100).ToString("0.##"));

            ////front current track
            //DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackID, OBATP.FrontCurrentTrack.Track_ID.ToString());
            //DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackLength, OBATP.FrontCurrentTrack.Track_Length.ToString());
            ////DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackDwellTime, );
            //DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackSpeedChangeVMax, OBATP.FrontCurrentTrack.SpeedChangeVMax.ToString());
            //DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackStation, OBATP.FrontCurrentTrack.Station_Name);
            //DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackMaxSpeedKM, OBATP.FrontCurrentTrack.MaxTrackSpeedKMH.ToString());
            //DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackVirtualOccupationTrackID, OBATP.VirtualOccupationFrontTrack.Track_ID.ToString());
            //DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentVirtualOccupationTrackLocation, OBATP.VirtualOccupationFrontTrackLocation.ToString());

            ////rear current track
            //DisplayManager.TextBoxInvoke(m_textBoxRearCurrentTrackID, OBATP.RearCurrentTrack.Track_ID.ToString());
            //DisplayManager.TextBoxInvoke(m_textBoxRearCurrentTrackSpeedChangeVMax, OBATP.RearCurrentTrack.SpeedChangeVMax.ToString());
            ////DisplayManager.TextBoxInvoke(m_textBoxRearCurrentTrackVirtualOccupationTrackID, OBATP.VirtualOccupationRearTrack.Track_ID.ToString());
            ////DisplayManager.TextBoxInvoke(m_textBoxRearCurrentVirtualOccupationTrackLocation, OBATP.VirtualOccupationRearTrackLocation.ToString());
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
    }
}
