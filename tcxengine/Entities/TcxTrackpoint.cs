using System;
using System.Collections.Generic;
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
    }
}
