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
        
        public void Concat(TcxTrack newTrack)
        {
            TrackPoints.AddRange(newTrack.TrackPoints);
            RemoveDuplicates();
        }

        protected void RemoveDuplicates()
        {
            OrderByTime();

            TcxTrackpoint prev = null;
            List<TcxTrackpoint> newTrack = new List<TcxTrackpoint>();
            foreach (TcxTrackpoint tp in TrackPoints)
            {
                if (prev == null)
                {
                    prev = tp;
                }
                else
                {
                    if (tp.Time == prev.Time)
                    {
                        prev = tp.CombineTrackpoint(prev);
                        newTrack.Add(prev);
                        prev = null;
                    }
                    else
                    {
                        newTrack.Add(prev);
                        prev = tp;
                    }
                }
            }
            if (TrackPoints.Count > 0)
            {
                newTrack.Add(TrackPoints.Last());
            }

            TrackPoints = newTrack;
        }

        protected void Normalize()
        {
            TcxTrackpoint prev = null;
            TcxTrackpoint current = null;
            List<TcxTrackpoint> next = null;
            List<TcxTrackpoint> newTrack = new List<TcxTrackpoint>();

            for (int i = 1; i < TrackPoints.Count - 1; i++)
            {
                prev = TrackPoints[i - 1];
                current = TrackPoints[i];
                next = TrackPoints.GetRange(i + 1, TrackPoints.Count - i - 1);

                current.CombineTrackpoint(prev, next);
            }
        }

        public void OrderByTime()
        {
            TrackPoints = TrackPoints.OrderBy(x => x.Time).ToList();
        }

        public double TotalTimeInSeconds()
        {
            return (TrackPoints.Last().Time - TrackPoints.First().Time).TotalSeconds;
        }

        public double DistanceMeters()
        {
            return TrackPoints.Last().DistanceMeters;
        }
        
    }
}
