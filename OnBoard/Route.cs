using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    [Serializable]
    public class Route : IDisposable
    {

        public int Route_No { get; set; }

        public int Entry_Track_ID { get; set; }

        public Track Entry_Track { get; set; } = new Track();

        public int Exit_Track_ID { get; set; }


        public Track Exit_Track { get; set; } = new Track();

        public ThreadSafeList<Track> Route_Tracks { get; set; } = new ThreadSafeList<Track>();

        public ThreadSafeList<Track> R_Tracks { get; set; } = new ThreadSafeList<Track>();
        public  double Length { get; set; }

        public Route()
        {
            
        }
        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }


        //static  
        public  List<Route> AllRoute(DataTable dt, ThreadSafeList<Track> allTracks)
        {
          List<Route> routeList = new List<Route>();

            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row.ItemArray[0].ToString()))
                {
                    Route route = new Route();

                  

                    int route_No;

                    if (int.TryParse(row[0].ToString(), out route_No))
                        route.Route_No = route_No; 

                    string[] routeTracks = row[1].ToString().Split('-');



                    Track rtrack;

                    for (int i = 0; i < routeTracks.Length; i++)
                    {
                        int routeTrackID = Convert.ToInt32(routeTracks[i]);

                         rtrack = allTracks.Find(x => x.Track_ID == routeTrackID);
                        //Track track = trackList.Find(x => x.Track_ID == routeTrackID);

                        route.Route_Tracks.Add(rtrack);
                        route.R_Tracks.Add(rtrack);
                        //route.Route_Tracks.Add(track);
                        //trackList.Add(track);
                    }



                    
                    Track track;

                    if (int.TryParse(row[2].ToString(), out int entry_Track_ID))
                    {
                        route.Entry_Track_ID = entry_Track_ID;

                        if (route.Entry_Track_ID != 0)
                        {
                              track = allTracks.Find(x => x.Track_ID == route.Entry_Track_ID);

                            //route.Entry_Track = allTracks.Find(x => x.Track_ID == route.Entry_Track_ID); 
                            //route.Entry_Track = trackList.Find(x => x.Track_ID == route.Entry_Track_ID); 
                            if (track != null)
                            {
                                route.Entry_Track = track;

                                //ilk track ve uzunluğunu ekliyoruz
                                route.Route_Tracks.Insert(0, route.Entry_Track);
                                route.R_Tracks.Insert(0, route.Entry_Track);
                                //route.Route_Tracks.Insert(0, route.Entry_Track);
                                //route.Route_Tracks.Insert(0, route.Entry_Track);
                                //trackList.Insert(0, route.Entry_Track);
                                //route.Length += route.Entry_Track.Track_Length;
                            }

                        }

                    }


                     
                    Track etrack;
                    if (int.TryParse(row[3].ToString(), out int exit_Track_ID))
                    {
                        route.Exit_Track_ID = exit_Track_ID;

                        if (route.Exit_Track_ID != 0)
                        { 
                            etrack = allTracks.Find(x => x.Track_ID == route.Exit_Track_ID);

                            if (etrack != null)
                            {
                                route.Exit_Track = etrack;
                                 
                            }
                        }
                           
                        //route.Exit_Track = allTracks.Find(x => x.Track_ID == route.Exit_Track_ID);
                    }


                    double routeLength = 0;



                    //for (int i = 0; i < routeTrack.Count; i++)
                    //{
                    //    route.Route_Tracks[i].StartPositionInRoute = routeLength;
                    //    route.Route_Tracks[i].StopPositionInRoute = routeLength + route.Route_Tracks[i].Track_Length;
                    //    //routeLength = routeLength + route.Route_Tracks[i].Track_Length;
                    //    //routeLength = routeLength + route.Route_Tracks[i].Track_Length;
                    //}

                    foreach (Track item in route.Route_Tracks)
                    {
                        item.StartPositionInRoute = routeLength;
                        item.StopPositionInRoute = routeLength + item.Track_Length;
                        routeLength += item.Track_Length;
                    }

                    //route.Length = routeLength;

                    foreach (Track item in route.R_Tracks)
                    {
                        item.StartPositionInRoute = routeLength;
                        item.StopPositionInRoute = routeLength + item.Track_Length;
                        routeLength += item.Track_Length;
                    }

                    route.Length = routeLength;



                    routeList.Add(route);
                   
                 
                }

                //routeList.Add(route); 
            }




            //for (int i = 0; i < routeList.Count; i++)
            //{

            //    double routeLength = 0;

            //    foreach (Track track in routeList[i].Route_Tracks)
            //    {
            //        track.StartPositionInRoute = routeLength;
            //        track.StopPositionInRoute = routeLength + track.Track_Length;
            //        routeLength += track.Track_Length;
            //    }

            //}


 


            return routeList;

        }


        public static List<Route> SimulationRoute(DataTable dt)
        { 

            HashSet<string> stationName = new HashSet<string>();
            List<Route> routeList = new List<Route>();

            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row.ItemArray[0].ToString()))
                {
                    stationName.Add(row[0].ToString());

                    Route route = new Route();

                    if (int.TryParse(row[1].ToString(), out int route_No))
                        route.Route_No = route_No;




                    string[] routeTracks = row[2].ToString().Split('-');



                    Track rtrack;

                    for (int i = 0; i < routeTracks.Length; i++)
                    {
                        int routeTrackID = Convert.ToInt32(routeTracks[i]);

                        rtrack = MainForm.m_allTracks.Find(x => x.Track_ID == routeTrackID);
                        //Track track = trackList.Find(x => x.Track_ID == routeTrackID);

                        route.Route_Tracks.Add(rtrack);

                    }




                    Track track;

                    if (int.TryParse(row[3].ToString(), out int entry_Track_ID))
                    {
                        route.Entry_Track_ID = entry_Track_ID;

                        if (route.Entry_Track_ID != 0)
                        {
                            track = MainForm.m_allTracks.Find(x => x.Track_ID == route.Entry_Track_ID);

                            if (track != null)
                            {
                                route.Entry_Track = track;

                                //ilk track ve uzunluğunu ekliyoruz
                                route.Route_Tracks.Insert(0, route.Entry_Track);

                            }

                        }

                    }

                    Track etrack;
                    if (int.TryParse(row[4].ToString(), out int exit_Track_ID))
                    {
                        route.Exit_Track_ID = exit_Track_ID;

                        if (route.Exit_Track_ID != 0)
                        {
                            etrack = MainForm.m_allTracks.Find(x => x.Track_ID == route.Exit_Track_ID);

                            if (etrack != null)
                            {
                                route.Exit_Track = etrack;

                            }
                        }

                        //route.Exit_Track = allTracks.Find(x => x.Track_ID == route.Exit_Track_ID);
                    }



                    routeList.Add(route);
                }
            } 


            return routeList;

        }


        public Track FindLast(Track startTrack, Track stopTrack, List<Track> tracks)
        {
            Track tempTrack = startTrack;

            tempTrack = tracks.Find(x => x.Track_ID == tempTrack.Track_Connection_Exit_1);

            if (tempTrack.Track_ID != stopTrack.Track_ID)
                FindLast(tempTrack, stopTrack, tracks);


            return tempTrack;


        }


        //public static List<Track> CreateNewRoute(Track startTrack, int stopTrackID, List<Track> tracks)
        //{
        //    List<Track> route = new List<Track>();
        //    //double length = 0;

        //    route.Add(startTrack);

        //    Track tempTrack = startTrack;

        //    do
        //    {
        //        if(startTrack.Track_ID < stopTrackID)
        //            tempTrack = tracks.Find(x => x.Track_ID == tempTrack.Track_Connection_Exit_1);
        //        else
        //            tempTrack = tracks.Find(x => x.Track_ID == tempTrack.Track_Connection_Entry_1);

        //        Length += tempTrack.Track_Length;

        //        route.Add(tempTrack); 
        //    }
        //    while (tempTrack.Track_ID != stopTrackID); 


        //    //foreach (Track track in tracks)
        //    //{
        //    //    length += track.Track_Length;
        //    //}


        //    return route;
        //}


        public static Route CreateNewRoute(List<Route> routes)
        {
            Route route = new Route();

            foreach (Route item in routes)
            {
                if (route.Route_Tracks.Count == 0)
                    route.Route_Tracks.AddRange(item.Route_Tracks);

            } 

            return route;
        }


        public static Route CreateNewRoute(int startTrackID, List<Route> routes)
        {
            Route route = new Route(); 

            Route entry = routes.Find(x => x.Entry_Track_ID == startTrackID); 
            int index = routes.FindIndex(x => x == entry); 
            List<Route> routesRange = routes.GetRange(index, routes.Count - index);


            foreach (Route item in routesRange)
            {
                if (route.Route_Tracks.Count == 0)
                {
                    route.Route_Tracks.AddRange(item.Route_Tracks);
                    continue;
                }
                else if (route.Route_Tracks.Count >= 20)
                    break;

                foreach (Track tra in item.Route_Tracks)
                {
                    if ((route.Route_Tracks.Count < 20))
                    {
                        bool hasTrack = route.Route_Tracks.Contains(tra);

                        if (!hasTrack)
                            route.Route_Tracks.Add(tra);
                    }
                    else if ((route.Route_Tracks.Count >= 20) && (tra.Track_ID != item.Exit_Track_ID))
                    {
                        int zindex = item.Route_Tracks.ToList().FindIndex(x => x == tra);
                        List<Track> loloahmet = item.Route_Tracks.ToList().GetRange(zindex, item.Route_Tracks.Count - zindex);

                        route.Route_Tracks.AddRange(loloahmet);

                        break;
                    }
                    else
                        break;
                }

            }


            route.Entry_Track_ID = route.Route_Tracks[0].Track_ID;
            route.Entry_Track = route.Route_Tracks[0];
            route.Exit_Track_ID = route.Route_Tracks[route.Route_Tracks.Count - 1].Track_ID;
            route.Exit_Track = route.Route_Tracks[route.Route_Tracks.Count - 1]; 

            double routeLength = 0;

            foreach (Track track in route.Route_Tracks)
            {
                track.StartPositionInRoute = routeLength;
                track.StopPositionInRoute = routeLength + track.Track_Length;
                routeLength += track.Track_Length;
            } 

            route.Length = routeLength;




            return route;
        }

        //HashSet<Track> hasTe = new HashSet<Track>(); 

        //foreach (Route item in lolo)
        //{
        //    foreach (Track itemTrack in item.Route_Tracks)
        //    {
        //        hasTe.Add(itemTrack);
        //    } 
        //}  

        //foreach (Track item in hasTe)
        //{
        //    if ((route.Route_Tracks.Count < 20) || ((route.Route_Tracks.Count >= 20) && (item.Track_ID.ToString().StartsWith("6"))))
        //    {
        //        Track newpne = MainForm.m_allTracks.Find(x => x == item);

        //        route.Route_Tracks.Add(newpne);

        //    }
        //    else
        //        break;

        //}

        public static Route CreateNewRoute(int startTrackID, int stopTrackID, ThreadSafeList<Track> tracks)
        {
            Route route = new Route();

            //List<Track> routeList = new List<Track>();

            Track startTrack = tracks.Find(x => x.Track_ID == startTrackID);
            route.Route_Tracks.Add(startTrack);

            Track tempTrack = startTrack;

            do
            {
                if (startTrack.Track_ID < stopTrackID)
                    tempTrack = tracks.Find(x => x.Track_ID == tempTrack.Track_Connection_Exit_1);
                else
                    tempTrack = tracks.Find(x => x.Track_ID == tempTrack.Track_Connection_Entry_1);

                //Length += tempTrack.Track_Length;

                route.Route_Tracks.Add(tempTrack);
            }
            while (tempTrack.Track_ID != stopTrackID);


            Track exitTrack = tracks.Find(x => x.Track_ID == stopTrackID);





            double routeLength = 0;

            foreach (Track track in route.Route_Tracks)
            {
                track.StartPositionInRoute = routeLength;
                track.StopPositionInRoute = routeLength + track.Track_Length;
                routeLength += track.Track_Length;
            }

            //foreach (Track track in route.Route_Tracks)
            //{
            //    track.StartPositionInRoute = track.Track_Start_Position;
            //    track.StopPositionInRoute = track.Track_End_Position;
            //    routeLength += track.Track_Length;
            //}

            route.Length = routeLength;



            route.Entry_Track_ID = startTrackID;
            route.Entry_Track = startTrack;
            route.Exit_Track_ID = stopTrackID;
            route.Exit_Track = exitTrack;
            //route.Route_Tracks = route.Route_Tracks;

            return route;
        }




        //public static List<Track> CreateNewRoute(int startTrackID, int stopTrackID, List<Track> tracks)
        //{ 
        //    List<Track> route = new List<Track>();

        //    Track startTrack = tracks.Find(x => x.Track_ID == startTrackID);
        //    route.Add(startTrack);

        //    Track tempTrack = startTrack;

        //    do
        //    {
        //        if (startTrack.Track_ID < stopTrackID)
        //            tempTrack = tracks.Find(x => x.Track_ID == tempTrack.Track_Connection_Exit_1);
        //        else
        //            tempTrack = tracks.Find(x => x.Track_ID == tempTrack.Track_Connection_Entry_1);

        //        //Length += tempTrack.Track_Length;

        //        route.Add(tempTrack);
        //    }
        //    while (tempTrack.Track_ID != stopTrackID);


        //    return route;
        //}

        public double FindTrainLocationInRoute(Enums.Direction direction, Track frontOfTheTrainTrack, double frontOfTheTrainWithFault)
        {
            double frontTrainLocationInRoute = 0;

            //if(direction = Enums.Direction.One)
            //    frontTrainLocationInRoute = frontOfTheTrainTrack.


            return frontTrainLocationInRoute;
        }



        public static void CreateNewRoute(Route route)
        {
            double routeLength = 0;


            foreach (Track track in route.Route_Tracks)
            {
                track.StartPositionInRoute = routeLength;
                track.StopPositionInRoute = routeLength + track.Track_Length;
                routeLength += track.Track_Length;
            }

            route.Length = routeLength;


            routeLength = 0;
        }
    }
}

