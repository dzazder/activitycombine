using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using tcxengine.Entities;

namespace tcxengine
{
    public class XmlToGpx : XmlConvertBase
    {
        private static GpxTrackpoint LoadTrackpoint(XmlNode trackpointNode)
        {
            GpxTrackpoint trackpoint = new GpxTrackpoint();

            var time = GetDateTimeChildValue(trackpointNode, GpxTrackpoint.ATTR_TIME);
            trackpoint.IsTimeDefined = time.HasValue;
            if (time != null)
            {
                trackpoint.Time = time.Value;
            }

            var altitudeMeters = GetDoubleChildValue(trackpointNode, GpxTrackpoint.ATTR_ALTITUDE_METERS);
            trackpoint.IsAltitudeMetersDefined = altitudeMeters.HasValue;
            if (altitudeMeters.HasValue)
            {
                trackpoint.AltitudeMeters = altitudeMeters.Value;
            }

            trackpoint.Position = new TcxPosition();
            var lat = GetDoubleAttributeValue(trackpointNode, GpxTrackpoint.ATTR_POSITION_LAT);
            if (lat.HasValue)
            {
                trackpoint.Position.Latitude = lat.Value;
            }
            var lon = GetDoubleAttributeValue(trackpointNode, GpxTrackpoint.ATTR_POSITION_LON);
            if (lon.HasValue)
            {
                trackpoint.Position.Longitude = lon.Value;
            }

            return trackpoint;
        }

        public static List<GpxTrack> Convert(string xmlFilePath)
        {
            List<GpxTrack> result = new List<GpxTrack>();

            string xmlContent = File.ReadAllText(xmlFilePath);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);    // full document
            if (doc.HasChildNodes)
            {
                var gpx = doc.GetElementsByTagName("gpx");
                foreach (XmlNode gpxNode in gpx)
                {
                    GpxTrack track = new GpxTrack();
                    track.Track = new List<GpxTrackSegment>();
                    XmlNode metadata = GetChild(gpxNode, "metadata");
                    track.StartTime = GetChildValue(metadata, "time");

                    var tracks = GetChildren(gpxNode, "trk");
                    foreach (XmlNode trkNode in tracks)
                    {
                        var seg = GetChildren(trkNode, "trkseg");
                        foreach (XmlNode segNode in seg)
                        {
                            GpxTrackSegment segment = new GpxTrackSegment();
                            segment.Track = new List<GpxTrackpoint>();
                            var trackpoints = GetChildren(segNode, "trkpt");

                            foreach (XmlNode trackpointNode in trackpoints)
                            {
                                var trackpoint = LoadTrackpoint(trackpointNode);
                                segment.Track.Add(trackpoint);
                            }

                            track.Track.Add(segment);
                        }
                    }
                    result.Add(track);
                }
            }

            return result;
        }
    }
}
