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
using MetroTcpLibrary;

namespace OnBoard
{
    public partial class MainForm : Form, ITrainMovementWatcher
    {
        XMLSerialization m_settings;
        internal static TrainMovementCreate m_trainMovement;
        public static MainForm m_mf;


        public static TcpClient m_conn;


        //public List<Track> rou;
        ConcurrentDictionary<int, OBATP> m_allOBATP;
        public static List<Track> allTracks;
        public static List<Route> allRoute;

        DataTable m_dataTable = GetTable();
        //ListViewHelper listViewHelper;
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

            m_trainMovement = new TrainMovementCreate();

            //excel tablosundan track listesini ve özelliklerini okuyoruz    
            //DataTable dt = FileOperation.ReadTrackTableInExcel();
            DataTable dt = m_settings.RouteTrack;



            Track se = new Track();


            List<Track> asasas = new List<Track>(se.AllTracksAAA(dt));

            allTracks = se.AllTracksAAA(dt);

            //List<Track> asasas = new List<Track>(se.AllTracks(dt));

            //allTracks = se.AllTracks(dt);
            //DataTable rt = FileOperation.ReadRouteTableInExcel();



            //m_conn = new TcpClient(205, "10.2.149.17", 1010);

            //allRoute = Route.AllRoute(rt, asasas);

            //Route route = new Route();
            //allRoute = route.AllRoute(rt, asasas);


            //foreach (Route item in allRoute)
            //{
            //    double routeLength = 0;
            //    foreach (Track track in item.Route_Tracks)
            //    {
            //        track.StartPositionInRoute = routeLength;
            //        track.StopPositionInRoute = routeLength + track.Track_Length;
            //        routeLength += track.Track_Length;
            //    }
            //    item.Length = routeLength;
            //}

            //List<Route> asdasdasdasdasd =  Route.AllRoute(rt, allTracks);

     

           


            typeof(ListView).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
           | BindingFlags.Instance | BindingFlags.NonPublic, null,
           m_listViewFootPrintTracks, new object[] { true });

            typeof(ListView).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
          | BindingFlags.Instance | BindingFlags.NonPublic, null,
          m_listViewVirtualOccupation, new object[] { true });

           
            //ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint
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

       

            m_backgroundWorker.RunWorkerAsync();


           
        }


      
        readonly object m_lolo = new object();

        #region createdevent
        public void TrainMovementCreated(OBATP OBATP)
        { 

            lock (m_lolo)
            {


                m_listViewVirtualOccupation.Items.Clear();
                //m_listViewVirtualOccupation.Items.AddRange(OBATP.TrainOnTracks.VirtualOccupationTracks);
                OBATP.TrainOnTracks.VirtualOccupationTracks.ForEach(x => m_listViewVirtualOccupation.Items.Add( x.ToString() )  );

                m_listViewFootPrintTracks.Items.Clear();
                OBATP.TrainOnTracks.FootPrintTracks.ForEach(x => m_listViewFootPrintTracks.Items.Add( x.ToString()  ));



                //foreach (DataRow row in m_dataTable.Rows)
                //{
                //    if (row["Train_Name"].ToString() == OBATP.Vehicle.TrainName)
                //    {
                //        row.SetField("Speed", OBATP.Vehicle.CurrentTrainSpeedKMH.ToString()); 

                //        row.SetField("Front_Track", OBATP.FrontOfTrainCurrentTrack.Track_ID.ToString());
                //        row.SetField("Front_Track_Location", OBATP.ActualFrontOfTrainCurrentLocation.ToString("0.##"));
                //        row.SetField("Front_Track_Length", OBATP.FrontOfTrainCurrentTrack.Track_Length.ToString());
                //        row.SetField("Front_Track_Max_Speed", OBATP.FrontOfTrainCurrentTrack.MaxTrackSpeedKMH.ToString());


                //        row.SetField("Rear_Track", OBATP.RearOfTrainCurrentTrack.Track_ID.ToString());
                //        row.SetField("Rear_Track_Location", OBATP.ActualRearCurrentLocation.ToString("0.##"));
                //        row.SetField("Rear_Track_Length", OBATP.RearOfTrainCurrentTrack.Track_Length.ToString());
                //        row.SetField("Rear_Track_Max_Speed", OBATP.RearOfTrainCurrentTrack.MaxTrackSpeedKMH.ToString());

                //        row.SetField("Total_Route_Distance", OBATP.TotalTrainDistance.ToString("0.##"));
                //    }

                //}





                //string[] row = { textBox1.Text, textBox2.Text, textBox3.Text };
                //var satir = new ListViewItem(row);

                //foreach (var item in OBATP.TrainOnTracks.VirtualOccupationTracks)
                //{
                //    //m_listViewVirtualOccupation.Items.Add(new ListViewItem(new string[] { item.ToString() }));

                //    bool hjhjhj = m_listViewVirtualOccupation.Items.Contains(new ListViewItem( item.ToString() ));

                //    if (hjhjhj == false)
                //        m_listViewVirtualOccupation.Items.Add(new ListViewItem( item.ToString() ));
                //    else
                //    {

                //    }
                //}




               


                //var sdf = m_dataGridViewAllTrains.SelectedRows[0].Cells;


                int rowindex = m_dataGridViewAllTrains.CurrentCell.RowIndex;
            int columnindex = m_dataGridViewAllTrains.CurrentCell.ColumnIndex;

            string id = m_dataGridViewAllTrains.SelectedCells[1].Value.ToString();


                if (m_comboBoxTrain.SelectedIndex == OBATP.Vehicle.TrainIndex)
                {

                    //general train
                    DisplayManager.TextBoxInvoke(m_textBoxCurrentTrainSpeedKM, OBATP.Vehicle.CurrentTrainSpeedKMH.ToString());
                    DisplayManager.TextBoxInvoke(m_textBoxCurrentLocation, OBATP.ActualFrontOfTrainCurrentLocation.ToString("0.##"));
                    DisplayManager.TextBoxInvoke(m_textBoxRearCurrentLocation, OBATP.ActualRearCurrentLocation.ToString("0.##"));
                    DisplayManager.TextBoxInvoke(m_textBoxCurrentAcceleration, (OBATP.Vehicle.CurrentAcceleration / 100).ToString("0.##"));

                    //front current track
                    DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackID, OBATP.FrontOfTrainCurrentTrack.Track_ID.ToString());
                    DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackLength, OBATP.FrontOfTrainCurrentTrack.Track_Length.ToString());
                    //DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackDwellTime, );
                    DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackSpeedChangeVMax, OBATP.FrontOfTrainCurrentTrack.SpeedChangeVMax.ToString());
                    DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackStation, OBATP.FrontOfTrainCurrentTrack.Station_Name);
                    DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackMaxSpeedKM, OBATP.FrontOfTrainCurrentTrack.MaxTrackSpeedKMH.ToString());
                    DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackVirtualOccupationTrackID, OBATP.FrontOfTrainVirtualOccupation.Track.Track_ID.ToString());
                    DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentVirtualOccupationTrackLocation, OBATP.FrontOfTrainVirtualOccupation.Location.ToString("0.##"));

                    //rear current track
                    DisplayManager.TextBoxInvoke(m_textBoxRearCurrentTrackID, OBATP.RearOfTrainCurrentTrack.Track_ID.ToString());
                    DisplayManager.TextBoxInvoke(m_textBoxRearCurrentTrackSpeedChangeVMax, OBATP.RearOfTrainCurrentTrack.SpeedChangeVMax.ToString());
                    //DisplayManager.TextBoxInvoke(m_textBoxRearCurrentTrackVirtualOccupationTrackID, OBATP.VirtualOccupationRearTrack.Track_ID.ToString());
                    //DisplayManager.TextBoxInvoke(m_textBoxRearCurrentVirtualOccupationTrackLocation, OBATP.VirtualOccupationRearTrackLocation.ToString());

                    DisplayManager.TextBoxInvoke(m_textBoxFrontCurrentTrackDwellTime, OBATP.DwellTime.ToString());
                    DisplayManager.TextBoxInvoke(m_textBoxDoorTimerCounter, OBATP.DoorTimerCounter.ToString());

                    if(OBATP.DoorStatus == Enums.DoorStatus.Open)
                        DisplayManager.TextBoxInvoke(m_textBoxDoorStatus, "Open");
                    else
                        DisplayManager.TextBoxInvoke(m_textBoxDoorStatus, "Close");

                   
                }

             
            }



        }

        public void TrainMovementRouteCreated(Route route)
        {
            //if (m_comboBoxTrain.SelectedIndex == OBATP.Vehicle.TrainIndex)
            //{
            m_listView.Items.Clear();
            route.Route_Tracks.ForEach(x => m_listView.Items.Add(new ListViewItem(new string[] { x.Track_ID.ToString(), x.Station_Name })));
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


            foreach (var item in m_settings.Trains)
            {
                counter++;
                //Route route = allRoute.Find(x => x.Entry_Track_ID == 10105 && x.Exit_Track_ID == 10107);
                //Route route = allRoute.Find(x => x.Entry_Track_ID == 10107 && x.Exit_Track_ID == 10201);
                //Route route = allRoute.Find(x => x.Entry_Track_ID == 10103 && x.Exit_Track_ID == 10201);

                //Route.CreateNewRoute(route);

                //10107, 10203
                Route route = Route.CreateNewRoute(10101, 10211, allTracks);

                //Route route = Route.CreateNewRoute(10101, 10211, allTracks);
                //Route route = Route.CreateNewRoute(10103, 10301, allTracks);



                int trainIndex = item;
                string trainName  = "Train" + (trainIndex + 1).ToString();

                DataRow row2 = m_dataTable.NewRow();
                row2["ID"] = counter;
                row2["Train_Name"] = trainName;
               m_dataTable.Rows.Add(row2);

                DisplayManager.RichTextBoxInvoke(m_richTextBox, trainName + " is Created...", Color.Black);



                OBATP oBATP = new OBATP(trainIndex, trainName, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration, m_settings.TrainSpeedLimit, m_settings.TrainLength, route);

                //m_allOBATP.TryAdd(trainIndex, oBATP);
                //m_allOBATP.AddOrUpdate(trainIndex, oBATP, (s, i) => oBATP);

                oBATP.RequestStartProcess();


                int sleepTime = Convert.ToInt32(m_settings.TrainFrequency);

                sw.Start();



                while (sw.Elapsed.TotalSeconds < (sleepTime))
                {

                }

                sw.Reset();


            }



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

        private void button1_Click(object sender, EventArgs e)
        {


            int saf =  CentimeterToPixel(1);

            //Graphics g = panel1.CreateGraphics();




            ////Graphics g = e.Graphics;

            //g.DrawLine(Pens.Blue, new Point(50, 100), new Point(100, 40));

            //g.Dispose();
        }

        int CentimeterToPixel(double Centimeter)
        {
            double pixel = -1;
            using (Graphics g = this.CreateGraphics())
            {
                pixel = Centimeter * g.DpiY / 2.54d;
            }
            return (int)pixel;
        }

        private void m_trainSimItem_Click(object sender, EventArgs e)
        {
            TrainSimModal trainSimModal = new TrainSimModal();
            trainSimModal.Owner = this;
            trainSimModal.ShowDialog();
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
