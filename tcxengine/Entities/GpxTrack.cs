using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcxengine.Entities
{
    public class GpxTrack
    {
        public List<GpxTrackSegment> Track { get; set; }
        public string StartTime { get; set; }
    }
}
