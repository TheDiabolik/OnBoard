using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
   public class Route : IDisposable
    {

        public int Route_No { get; set; }

        public int Entry_Track_ID { get; set; }

        public Track Entry_Track { get; set; } = new Track();

        public int Exit_Track_ID { get; set; }


        public Track Exit_Track { get; set; } = new Track();

        public List<Track> Route_Tracks { get; set; } = new List<Track>();

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
        public  List<Route> AllRoute(DataTable dt, List<Track> allTracks)
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

                    //foreach (Track item in route.Route_Tracks)
                    //{
                    //    item.StartPositionInRoute = routeLength;
                    //    item.StopPositionInRoute = routeLength + item.Track_Length;
                    //    routeLength += item.Track_Length;
                    //}

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



            //CreateNewRoute()

            //double routeLength = 0;

            //foreach (var item in routeList)
            //{

            //    CreateNewRoute(item);
            //    //foreach (var item1 in item.Route_Tracks)
            //    //{
            //    //    item1.StartPositionInRoute = routeLength;
            //    //    item1.StopPositionInRoute = routeLength + item1.Track_Length;
            //    //    routeLength += item1.Track_Length;
            //    //}

            //    //item.Length = routeLength;

            //    //routeLength = 0;
            //}


            return routeList;

        }



        //public  static List<Route> AllRoute(DataTable dt, List<Track> allTracks)
        //{
        //    List<Route> routeList = new List<Route>();


        //    List<Track> trackList = new List<Track>(allTracks);

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        if (!string.IsNullOrEmpty(row.ItemArray[0].ToString()))
        //        {
        //            Route route = new Route();
        //            //List<Track> trackList = new List<Track>();

        //            int route_No;

        //            if (int.TryParse(row[0].ToString(), out route_No))
        //                route.Route_No = route_No;


        //            string[] routeTracks = row[1].ToString().Split('-'); 


        //            for (int i = 0; i < routeTracks.Length; i++)
        //            {
        //                int routeTrackID = Convert.ToInt32(routeTracks[i]);

        //                //Track track = allTracks.Find(x => x.Track_ID == routeTrackID);
        //                Track track = trackList.Find(x => x.Track_ID == routeTrackID);



        //                route.Route_Tracks.Add(track);
        //                //trackList.Add(track);
        //            }



        //            int entry_Track_ID;

        //            if (int.TryParse(row[2].ToString(), out entry_Track_ID))
        //            {
        //                route.Entry_Track_ID = entry_Track_ID;

        //                if (route.Entry_Track_ID != 0)
        //                {
        //                    //route.Entry_Track = allTracks.Find(x => x.Track_ID == route.Entry_Track_ID);


        //                    route.Entry_Track = trackList.Find(x => x.Track_ID == route.Entry_Track_ID);



        //                    if (route.Entry_Track != null)
        //                    {
        //                        //ilk track ve uzunluğunu ekliyoruz
        //                        route.Route_Tracks.Insert(0, route.Entry_Track);
        //                        //trackList.Insert(0, route.Entry_Track);
        //                        //route.Length += route.Entry_Track.Track_Length;
        //                    }

        //                }

        //            }


        //            int exit_Track_ID;

        //            if (int.TryParse(row[3].ToString(), out exit_Track_ID))
        //            {
        //                route.Exit_Track_ID = exit_Track_ID;

        //                if (route.Exit_Track_ID != 0)
        //                    route.Exit_Track = trackList.Find(x => x.Track_ID == route.Exit_Track_ID);  

        //                    //route.Exit_Track = allTracks.Find(x => x.Track_ID == route.Exit_Track_ID);
        //            }


        //            double routeLength = 0;



        //            //for (int i = 0; i < trackList.Count; i++)
        //            //{
        //            //   trackList[i].StartPositionInRoute = routeLength;
        //            //    trackList[i].StopPositionInRoute = routeLength + trackList[i].Track_Length;
        //            //    routeLength += trackList[i].Track_Length;
        //            //}

        //            foreach (Track track in route.Route_Tracks)
        //            {
        //                track.StartPositionInRoute = routeLength;
        //                track.StopPositionInRoute = routeLength + track.Track_Length;
        //                routeLength += track.Track_Length;
        //            }

        //            route.Length = routeLength;

        //            //route.Route_Tracks = trackList;

        //            routeList.Add(route);
        //        }

        //    }

        //    return routeList;

        //}






        //public static List<Route> AllRoute(DataTable dt, List<Track> allTracks)
        //{ 
        //    List<Route> routeList = new List<Route>();  

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        Route route = new Route();


        //        int route_No;

        //        if (int.TryParse(row[0].ToString(), out route_No))
        //            route.Route_No = route_No;


        //        int entry_Track_ID;

        //        if (int.TryParse(row[2].ToString(), out entry_Track_ID))
        //        {
        //            route.Entry_Track_ID = entry_Track_ID;


        //            if (route.Entry_Track_ID != 0)
        //            {
        //                Track entry = allTracks.Find(x => x.Track_ID == route.Entry_Track_ID);

        //                //entr
        //            }



        //        }


        //        int exit_Track_ID;

        //        if (int.TryParse(row[3].ToString(), out exit_Track_ID))
        //            route.Exit_Track_ID = exit_Track_ID;





        //        //route.Route_No = Convert.ToInt32(row[0]);


        //        //route.Entry_Track_ID = Convert.ToInt32(row[2]);
        //        //route.Exit_Track_ID = Convert.ToInt32(row[3]);  

        //        if(route_No != 0)
        //            routeList.Add(route);

        //    } 

        //        return routeList;

        //}



        public Track FindLast(Track startTrack, Track stopTrack, List<Track> tracks)
        {
            Track tempTrack = startTrack;

           tempTrack =  tracks.Find(x => x.Track_ID == tempTrack.Track_Connection_Exit_1);

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


        public static Route CreateNewRoute(int startTrackID, int stopTrackID, List<Track> tracks)
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

