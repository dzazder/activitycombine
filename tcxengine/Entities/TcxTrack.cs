using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tcxengine.Entities;

namespace tcxengine.Entities
{
    public class TcxTrack
    {
        public List<TcxTrackpoint> TrackPoints { get; set; }

        public void OrderByTime()
        {

        }
    }
}
