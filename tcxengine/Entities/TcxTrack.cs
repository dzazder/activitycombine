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

            TrackPoints = newTrack;
        }

        public void OrderByTime()
        {
            TrackPoints = TrackPoints.OrderBy(x => x.Time).ToList();
        }
    }
}
