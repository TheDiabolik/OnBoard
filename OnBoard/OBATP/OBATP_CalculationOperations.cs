using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard 
{
    public partial class OBATP : IWSATP_TO_OBATPMessageWatcher, IATS_TO_OBATO_InitMessageWatcher, IATS_TO_OBATO_MessageWatcher, IATS, IWSATC, IDisposable
    {

        /// <summary>
        /// Trenin o anki hızına göre yavaşlama ivmesiyle duracağı mesafeyi bulur.
        /// </summary>
        public double CalculateBrakingDistance(double maxTrainDeceleration, double currentTrainSpeedCMS)
        {
            double brakingDistance = ((0.5) * maxTrainDeceleration * Math.Pow(currentTrainSpeedCMS / maxTrainDeceleration, 2));

            return brakingDistance;
        }


        public TrackWithPosition CalculateLocationInTrack(Track track, Track nextTrack, Enums.Direction direction, Vehicle vehicle, double location)
        {
            TrackWithPosition returnValues = new TrackWithPosition();// = Tuple.Create<double, Track>[];

            double currentLocation = -1;

            //if (RouteTracks.Count == 0)
            //    return -1;

            // Bir önceki konum ile son zaman aralığında gidilen konum toplanır.
            if (direction == Enums.Direction.Right)
            {

                location = location + (0.5 * (vehicle.CurrentTrainSpeedCMS + vehicle.PreviousSpeedCMS) * OperationTime);

                //if (location >= track.Track_End_Position + 0.1)
                if (location >= track.Track_Length + 0.1)
                {
                    double cation = location - track.Track_End_Position;
                    location = location - track.Track_Length;
                    track = nextTrack;
                }


            }
            else if (direction == Enums.Direction.Left)
            {
                location = location - (0.5 * (vehicle.CurrentTrainSpeedCMS + vehicle.PreviousSpeedCMS) * OperationTime);

                if (location <= 0.1)
                //if (location <= track.Track_Length + 0.1)
                //if (location <= track.Track_Start_Position - 0.1)
                {
                    track = nextTrack;
                    //location = track.Track_End_Position + location;

                    location = track.Track_Length + location;
                }

            }

            currentLocation = location;

            //bu değişecek
            //CurrentTrack = track;


            returnValues.Track = track;
            returnValues.Location = currentLocation;



            TotalTrainDistance += (0.5 * (this.Vehicle.CurrentTrainSpeedCMS + this.Vehicle.PreviousSpeedCMS) * OperationTime);

            return returnValues;


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


            //if (currentTrainSpeed = vehicle.TargetSpeedCMS)
            //    currentTrainSpeed = vehicle.TargetSpeedCMS;

            //if (currentTrainSpeed > 40)
            //    currentTrainSpeed = vehicle.TargetSpeedCMS;

            return currentTrainSpeed;
        }


        //public Tuple<double, Track> CalculateLocation(Track track, Track nextTrack, Enums.Direction direction, Vehicle vehicle, double location)
        //{
        //    Tuple<double, Track> returnValues;// = Tuple.Create<double, Track>[];

        //    double currentLocation = -1;

        //    //if (RouteTracks.Count == 0)
        //    //    return -1;

        //    // Bir önceki konum ile son zaman aralığında gidilen konum toplanır.
        //    if (direction == Enums.Direction.Right)
        //    {



        //        location = location + (0.5 * (vehicle.CurrentTrainSpeedCMS + vehicle.PreviousSpeedCMS) * OperationTime);

        //        //if (location >= track.Track_End_Position + 0.1)
        //        if (location >= track.Track_Length + 0.1)
        //        {
        //            double cation = location - track.Track_End_Position;
        //            location = location - track.Track_Length;
        //            track = nextTrack;
        //        }


        //    }
        //    else if (direction == Enums.Direction.Left)
        //    {
        //        location = location - (0.5 * (vehicle.CurrentTrainSpeedCMS + vehicle.PreviousSpeedCMS) * OperationTime);


        //        //if (location <= track.Track_Length + 0.1)
        //        if (location <= track.Track_Start_Position - 0.1)
        //        {
        //            track = nextTrack;
        //            //location = track.Track_End_Position + location;

        //            location = track.Track_Length + location;
        //        }

        //    }

        //    currentLocation = location;

        //    //bu değişecek
        //    //CurrentTrack = track;


        //    returnValues = Tuple.Create<double, Track>(currentLocation, track);


        //    TotalTrainDistance += (0.5 * (this.Vehicle.CurrentTrainSpeedCMS + this.Vehicle.PreviousSpeedCMS) * OperationTime);

        //    return returnValues;


        //}


    }
}
