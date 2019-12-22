using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnBoard
{
    public partial class TrainSimModal : Form, ITrainMovementWatcher
    {
        private MainForm m_mf;
        XMLSerialization m_settings;
        private Rectangle m_rect;
        private Graphics m_bitmapGraphics;
        private Bitmap m_bitmapScreen;
        private Point m_prevPoint;

        const double RouteScaleRatio = 0.005;
        const double TrainLengthCM = 11300;

        const double TrainLengthCMReScale = (TrainLengthCM * RouteScaleRatio);

        const int offsetX = 0;
          int offsetY = 20;

        public TrainSimModal()
        {
            InitializeComponent();

            //ayarları okuma
            m_settings = XMLSerialization.Singleton();
            m_settings = m_settings.DeSerialize(m_settings);

            MainForm.m_trainMovement.AddWatcher(this);

       

            m_rect = new Rectangle(offsetX, 100, Convert.ToInt32(TrainLengthCMReScale), 20);
            //m_bitmapScreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            m_bitmapGraphics = m_panel.CreateGraphics();// Graphics.FromImage(m_bitmapScreen);
            //m_bitmapScreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //m_bitmapGraphics = m_panel.CreateGraphics();// Graphics.FromImage(m_bitmapScreen);

            //m_bitmapGraphics.FillRectangle(Brushes.Blue, m_rect);

            //Invalidate();

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            m_panel, new object[] { true });


        }


        public TrainSimModal(MainForm mf) : this()
        {
            m_mf = mf;
        }

        Route m_route;

        private void TrainSimModal_Load(object sender, EventArgs e)
        {
            //m_route = Route.CreateNewRoute(10103, 10301, MainForm.allTracks);
            m_route = Route.CreateNewRoute(10101, 10211, MainForm.allTracks);
            //m_route = Route.CreateNewRoute(10101, 10103, MainForm.allTracks);



          


            m_prevPoint = m_rect.Location;
            //m_panel.Invalidate();
        }


        string trainSpeedKMH = "0";

        #region createdevent
        public void TrainMovementCreated(OBATP OBATP)
        {

            m_rect.Location = new Point(Convert.ToInt32(OBATP.FrontOfTrainLocationWithFootPrintInRoute * RouteScaleRatio) - Convert.ToInt32(TrainLengthCMReScale) + offsetX, 100);


            trainSpeedKMH = OBATP.Vehicle.CurrentTrainSpeedKMH.ToString("0.##");


            //int centerXPointOfRect = Convert.ToInt32(((m_rect.Location.X + m_rect.Width) - m_rect.Location.X) / 2);

            //m_bitmapGraphics.DrawString(trainSpeedKMH + " km/sa", new Font("Arial", 8), new SolidBrush(Color.Black), new Point(centerXPointOfRect, 55));

            m_bitmapGraphics.DrawString(trainSpeedKMH + " km/sa", new Font("Arial", 8), new SolidBrush(Color.Black), new Point(m_rect.Location.X, m_rect.Location.Y));



            m_panel.Invalidate(Rectangle.Inflate(m_rect, 150, 150));
            //m_panel.Invalidate(Rectangle.Inflate(m_rect, 100, 100));

            m_prevPoint = m_rect.Location;
        }

        public void TrainMovementRouteCreated(Route route)
        {
          
        }
        #endregion

        private void m_panel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen redPen = new Pen(Color.Red, 1);
            Pen bluePen = new Pen(Color.Blue, 5);
            Pen blueThinPen = new Pen(Color.Blue, 1);
            Pen redThinPen = new Pen(Color.Red, 1);
            //dikeyçizgi
            g.DrawLine(redThinPen, new Point(offsetX, 50), new Point(offsetX, 60));
            //track çizgisi
            g.DrawLine(redPen, new Point(Convert.ToInt32(m_route.Route_Tracks[0].StartPositionInRoute * RouteScaleRatio) + offsetX, 55), new Point(Convert.ToInt32(m_route.Route_Tracks[0].StopPositionInRoute * RouteScaleRatio) + 100, 55 ));

            g.DrawString(m_route.Route_Tracks[0].Track_ID.ToString() + "\n" + m_route.Route_Tracks[0].MaxTrackSpeedKMH.ToString("0.##") + " km/sa", new Font("Arial", 8), new SolidBrush(Color.Red), new Point(Convert.ToInt32((m_route.Route_Tracks[0].StopPositionInRoute / 2) * RouteScaleRatio) + offsetX, 55));


           
            for (int i = 1; i < m_route.Route_Tracks.Count; i++)
            {
                Pen pen;
                Pen thinPen;
                Color color;


                if (i % 2 == 0)
                {
                    pen = redPen;
                    offsetY = 65;
                    color = Color.Red;
                    thinPen = redThinPen;
                }
                  
                else
                {
                    offsetY = 25;
                    pen = bluePen;
                    color = Color.Blue;
                    thinPen = blueThinPen;
                }


              
              
                //dikeyçizgi
                g.DrawLine(thinPen, new Point(Convert.ToInt32(m_route.Route_Tracks[i].StartPositionInRoute * RouteScaleRatio) + offsetX, 50), new Point(Convert.ToInt32(m_route.Route_Tracks[i].StartPositionInRoute * RouteScaleRatio) + offsetX, 60));
                //track çizgisi
                g.DrawLine(pen, new Point(Convert.ToInt32(m_route.Route_Tracks[i].StartPositionInRoute * RouteScaleRatio) + offsetX, 55), new Point(Convert.ToInt32(m_route.Route_Tracks[i].StopPositionInRoute * RouteScaleRatio) + offsetX, 55));

                g.DrawString(m_route.Route_Tracks[i].Track_ID.ToString() + "\n" + m_route.Route_Tracks[i].MaxTrackSpeedKMH.ToString("0.##") + " km/sa", new Font("Arial", 8), new SolidBrush(color),
                    new Point(Convert.ToInt32((m_route.Route_Tracks[i].StartPositionInRoute ) * RouteScaleRatio) + Convert.ToInt32((m_route.Route_Tracks[i].Track_Length/ 2) * RouteScaleRatio) + offsetX, offsetY));


                if(i == m_route.Route_Tracks.Count-1)
                {
                    //dikeyçizgi
                    //track çizgisi
                    g.DrawLine(pen, new Point(Convert.ToInt32(m_route.Route_Tracks[i].StopPositionInRoute * RouteScaleRatio) + offsetX, 50), new Point(Convert.ToInt32(m_route.Route_Tracks[i].StopPositionInRoute * RouteScaleRatio) + offsetX, 60));


                }
            }
            g.FillRectangle(Brushes.Blue, m_rect);
          

            g.DrawString(trainSpeedKMH + " km/sa", new Font("Arial", 8), new SolidBrush(Color.Black), new Point((m_rect.Width / 2) + m_rect.Location.X , m_rect.Bottom  ));

        }

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    //...e.


        //}

        //private void TrainSimModal_Paint(object sender, PaintEventArgs e)
        //{
        //    e.Graphics.DrawImage(m_bitmapScreen, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
        //}
    }
}
