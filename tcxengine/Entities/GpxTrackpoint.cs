using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcxengine.Entities
{
    public class GpxTrackpoint
    {
        public static string ATTR_TIME = "time";
        public static string ATTR_POSITION_LAT = "lat";
        public static string ATTR_POSITION_LON = "lon";
        public static string ATTR_ALTITUDE_METERS = "ele";

        public DateTime Time { get; set; }
        public TcxPosition Position { get; set; }
        public double AltitudeMeters { get; set; }

        public bool IsTimeDefined { get; set; }
        public bool IsPositionDefined { get; set; }
        public bool IsAltitudeMetersDefined { get; set; }
    }
}
