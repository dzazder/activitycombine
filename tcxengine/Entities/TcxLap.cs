using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcxengine.Entities
{
    public class TcxLap
    {
        public string StartTime { get; set; }
        public double TotalTimeSeconds { get; set; }
        public double DistanceMeters { get; set; }
        public double MaximumSpeed { get; set; }
        public double Calories { get; set; }
        public string Intensity { get; set; }
        public string TriggerMethod { get; set; }
        public TcxTrack Track { get; set; }
    }
}
