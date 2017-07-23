using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcxengine.Entities
{
    public class TcxTrackpoint
    {
        public static string ATTR_TIME = "Time";
        public static string ATTR_POSITION = "Position";
        public static string ATTR_ALTITUDE_METERS = "AltitudeMeters";
        public static string ATTR_DISTANCE_METERS = "DistanceMeters";
        public static string ATTR_SENSOR_STATE = "SensorState";
        public static string ATTR_CADENCE = "Cadence";
        public static string ATTR_HEART_RATE_BPM = "HeartRateBpm";
        public static string ATTR_EXTENSIONS = "Extensions";
        public static string ATTR_VALUE = "Value";

        public DateTime Time { get; set; }
        public TcxPosition Position { get; set; }
        public double AltitudeMeters { get; set; }
        public double DistanceMeters { get; set; }
        public string SensorState { get; set; }
        public double HeartRateBpm { get; set; }
        public double Cadence { get; set; }
        public TcxExtensions Extensions { get; set; }

        public bool IsTimeDefined { get; set; }
        public bool IsPositionDefined { get; set; }
        public bool IsAltitudeMetersDefined { get; set; }
        public bool IsDistanceMetersDefined { get; set; }
        public bool IsSensorStateDefined { get; set; }
        public bool IsHeartRateBpmDefined { get; set; }
        public bool IsCadenceDefined { get; set; }
        public bool IsExtensionsDefined { get; set; }

        public TcxTrackpoint()
        {

        }

        public TcxTrackpoint(GpxTrackpoint gpx)
        {
            if (gpx.IsTimeDefined)
            {
                Time = gpx.Time;
            }

            if (gpx.IsPositionDefined)
            {
                Position = new TcxPosition(gpx.Position.Latitude, gpx.Position.Longitude);
            }

            if (gpx.IsAltitudeMetersDefined)
            {
                AltitudeMeters = gpx.AltitudeMeters;
            }

            IsTimeDefined = gpx.IsTimeDefined;
            IsPositionDefined = gpx.IsPositionDefined;
            IsAltitudeMetersDefined = gpx.IsAltitudeMetersDefined;
            IsDistanceMetersDefined = false;
            IsSensorStateDefined = false;
            IsHeartRateBpmDefined = false;
            IsCadenceDefined = false;
            IsExtensionsDefined = false;
        }

        public TcxTrackpoint CombineTrackpoint(TcxTrackpoint prev, List<TcxTrackpoint> next)
        {
            Console.WriteLine("Combine Trackpoint");
            TcxTrackpoint newTrackpoint = new TcxTrackpoint();
            newTrackpoint.Time = Time;
            newTrackpoint.IsTimeDefined = true;

            if (!IsPositionDefined)
            {
                if (!prev.IsPositionDefined)
                {
                    Console.WriteLine("Previous element has not defines position!");
                }
                else
                {
                    TcxTrackpoint nextM = null;
                    int count = 0;
                    foreach (TcxTrackpoint t in next)
                    {
                        count++;
                        if (t.IsPositionDefined)
                        {
                            nextM = t;
                            break;
                        }
                    }
                    if (nextM == null)
                    {
                        Console.WriteLine("Not found next element with position specified!");
                    }
                    else
                    {
                        double fullDiff = (nextM.Time - prev.Time).TotalSeconds;
                        double diff = (Time - prev.Time).TotalSeconds;

                        double latDiff = nextM.Position.Latitude - prev.Position.Latitude;
                        double lonDiff = nextM.Position.Longitude - prev.Position.Longitude;

                        double newLat = prev.Position.Latitude + ((diff / fullDiff) * latDiff);
                        double newLon = prev.Position.Longitude + ((diff / fullDiff) * lonDiff);

                        newTrackpoint.Position = new TcxPosition(newLat, newLon);
                        newTrackpoint.IsPositionDefined = true;
                        Console.WriteLine(string.Format("Diff: {0}, FullDiff: {1}, LatDiff: {2}, LonDiff: {3}, newLat: {4}, newLon: {5}",
                            diff, fullDiff, latDiff, lonDiff, newLat, newLon));
                    }
                }
            }
            else
            {
                newTrackpoint.Position = Position;
            }


            // todo rest of attributes
            return newTrackpoint;
        }

        public TcxTrackpoint CombineTrackpoint(TcxTrackpoint second)
        {
            TcxTrackpoint newTrackpoint = new TcxTrackpoint();
            newTrackpoint.Time = Time;
            newTrackpoint.IsTimeDefined = true;

            if (IsPositionDefined && second.IsPositionDefined)
            {
                newTrackpoint.Position = Position;
                newTrackpoint.IsPositionDefined = true;
            }
            else
            {
                if (IsPositionDefined)
                {
                    newTrackpoint.Position = Position;
                    newTrackpoint.IsPositionDefined = true;
                }
                else if (second.IsPositionDefined)
                {
                    newTrackpoint.Position = second.Position;
                    newTrackpoint.IsPositionDefined = true;
                }
                else
                {
                    newTrackpoint.IsPositionDefined = false;
                }
            }

            if (IsAltitudeMetersDefined && second.IsAltitudeMetersDefined)
            {
                newTrackpoint.AltitudeMeters = AltitudeMeters;
                newTrackpoint.IsAltitudeMetersDefined = true;
            }
            else
            {
                if (IsAltitudeMetersDefined)
                {
                    newTrackpoint.AltitudeMeters = AltitudeMeters;
                    newTrackpoint.IsAltitudeMetersDefined = true;
                }
                else if (second.IsAltitudeMetersDefined)
                {
                    newTrackpoint.AltitudeMeters = second.AltitudeMeters;
                    newTrackpoint.IsAltitudeMetersDefined = true;
                }
                else
                {
                    newTrackpoint.IsAltitudeMetersDefined = false;
                }
            }

            if (IsDistanceMetersDefined && second.IsDistanceMetersDefined)
            {
                newTrackpoint.DistanceMeters = DistanceMeters;
                newTrackpoint.IsDistanceMetersDefined = true;
            }
            else
            {
                if (IsDistanceMetersDefined)
                {
                    newTrackpoint.DistanceMeters = DistanceMeters;
                    newTrackpoint.IsDistanceMetersDefined = true;
                }
                else if (second.IsDistanceMetersDefined)
                {
                    newTrackpoint.DistanceMeters = second.DistanceMeters;
                    newTrackpoint.IsDistanceMetersDefined = true;
                }
                else
                {
                    newTrackpoint.IsDistanceMetersDefined = false;
                }
            }

            if (IsSensorStateDefined && second.IsSensorStateDefined)
            {
                newTrackpoint.SensorState = SensorState;
                newTrackpoint.IsSensorStateDefined = true;
            }
            else
            {
                if (IsSensorStateDefined)
                {
                    newTrackpoint.SensorState = SensorState;
                    newTrackpoint.IsSensorStateDefined = true;
                }
                else if (second.IsSensorStateDefined)
                {
                    newTrackpoint.SensorState = second.SensorState;
                    newTrackpoint.IsSensorStateDefined = true;
                }
                else
                {
                    newTrackpoint.IsSensorStateDefined = false;
                }
            }

            if (IsHeartRateBpmDefined && second.IsHeartRateBpmDefined)
            {
                newTrackpoint.HeartRateBpm = HeartRateBpm;
                newTrackpoint.IsHeartRateBpmDefined = true;
            }
            else
            {
                if (IsHeartRateBpmDefined)
                {
                    newTrackpoint.HeartRateBpm = HeartRateBpm;
                    newTrackpoint.IsHeartRateBpmDefined = true;
                }
                else if (second.IsHeartRateBpmDefined)
                {
                    newTrackpoint.HeartRateBpm = second.HeartRateBpm;
                    newTrackpoint.IsHeartRateBpmDefined = true;
                }
                else
                {
                    newTrackpoint.IsHeartRateBpmDefined = false;
                }
            }

            if (IsCadenceDefined && second.IsCadenceDefined)
            {
                newTrackpoint.Cadence = Cadence;
                newTrackpoint.IsCadenceDefined = true;
            }
            else
            {
                if (IsCadenceDefined)
                {
                    newTrackpoint.Cadence = Cadence;
                    newTrackpoint.IsCadenceDefined = true;
                }
                else if (second.IsCadenceDefined)
                {
                    newTrackpoint.Cadence = second.Cadence;
                    newTrackpoint.IsCadenceDefined = true;
                }
                else
                {
                    newTrackpoint.IsCadenceDefined = false;
                }
            }

            return newTrackpoint;
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<Trackpoint>");

            if (IsTimeDefined)
            {
                sb.Append("<Time>");
                sb.Append(Time.ToString());
                sb.Append("</Time>");
            }

            if (IsPositionDefined)
            {
                sb.Append("<Position>");
                sb.Append("<LatitudeDegrees>");
                sb.Append(Position.Latitude.ToString(new CultureInfo("en-US")));
                sb.Append("</LatitudeDegrees>");
                sb.Append("<LongitudeDegrees>");
                sb.Append(Position.Longitude.ToString(new CultureInfo("en-US")));
                sb.Append("</LongitudeDegrees>");
                sb.Append("</Position>");
            }

            if (IsAltitudeMetersDefined)
            {
                sb.Append("<AltitudeMeters>");
                sb.Append(AltitudeMeters.ToString(new CultureInfo("en-US")));
                sb.Append("</AltitudeMeters>");
            }

            if (IsDistanceMetersDefined)
            {
                sb.Append("<DistanceMeters>");
                sb.Append(DistanceMeters.ToString(new CultureInfo("en-US")));
                sb.Append("</DistanceMeters>");
            }

            if (IsSensorStateDefined)
            {
                sb.Append("<SensorState>");
                sb.Append(SensorState.ToString());
                sb.Append("</SensorState>");
            }

            sb.Append("</Trackpoint>");

            return sb.ToString();
        }
    }
}
