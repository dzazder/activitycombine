using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcxengine.Entities
{
    public class TcxTrackpoint
    {
        public string Time { get; set; }
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
