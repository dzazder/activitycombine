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

        public TcxActivity ConvertToTcxActivity()
        {
            TcxActivity tcxActivity = new TcxActivity();
            tcxActivity.Id = StartTime;
            tcxActivity.Laps = new List<TcxLap>();

            foreach (GpxTrackSegment segment in Track)
            {
                TcxLap lap = new TcxLap();
                lap.Track = new TcxTrack();
                lap.Track.TrackPoints = new List<TcxTrackpoint>();
                foreach (GpxTrackpoint trackpoint in segment.Track)
                {
                    TcxTrackpoint tcxTrackpoint = new TcxTrackpoint(trackpoint);
                    lap.Track.TrackPoints.Add(tcxTrackpoint);
                }

                tcxActivity.Laps.Add(lap);
            }

            return tcxActivity;
        }
    }
}
