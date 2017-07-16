using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcxengine.Entities
{
    public class TcxPosition
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public TcxPosition()
        {

        }

        public TcxPosition(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }
    }
}
