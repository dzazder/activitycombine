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
    public class XmlToTcx : XmlConvertBase
    {
        

        private static TcxTrackpoint LoadTrackpoint(XmlNode trackpointNode)
        {
            TcxTrackpoint trackpoint = new TcxTrackpoint();

            var time = GetDateTimeChildValue(trackpointNode, TcxTrackpoint.ATTR_TIME);
            trackpoint.IsTimeDefined = time.HasValue;
            if (time != null)
            {
                trackpoint.Time = time.Value;
            }

            var altitudeMeters = GetDoubleChildValue(trackpointNode, TcxTrackpoint.ATTR_ALTITUDE_METERS);
            trackpoint.IsAltitudeMetersDefined = altitudeMeters.HasValue;
            if (altitudeMeters.HasValue)
            {
                trackpoint.AltitudeMeters = altitudeMeters.Value;
            }

            var distanceMeters = GetDoubleChildValue(trackpointNode, TcxTrackpoint.ATTR_DISTANCE_METERS);
            trackpoint.IsDistanceMetersDefined = distanceMeters.HasValue;
            if (distanceMeters.HasValue)
            {
                trackpoint.DistanceMeters = distanceMeters.Value;
            }

            var cadence = GetDoubleChildValue(trackpointNode, TcxTrackpoint.ATTR_CADENCE);
            trackpoint.IsCadenceDefined = cadence.HasValue;
            if (cadence.HasValue)
            {
                trackpoint.Cadence = cadence.Value;
            }

            var sensorState = GetChildValue(trackpointNode, TcxTrackpoint.ATTR_SENSOR_STATE);
            trackpoint.IsSensorStateDefined = (sensorState != null);
            if (sensorState != null)
            {
                trackpoint.SensorState = sensorState;
            }

            var heartRateNode = GetChild(trackpointNode, TcxTrackpoint.ATTR_HEART_RATE_BPM);
            trackpoint.IsHeartRateBpmDefined = heartRateNode != null;
            if (heartRateNode != null)
            {
                var heartRateValue = GetDoubleChildValue(heartRateNode, TcxTrackpoint.ATTR_VALUE);
                trackpoint.IsHeartRateBpmDefined = heartRateValue.HasValue;
                if (heartRateValue.HasValue)
                {
                    trackpoint.HeartRateBpm = heartRateValue.Value;
                }
            }

            return trackpoint;
        }

        public static List<TcxActivity> Convert(string xmlFilePath)
        {
            List<TcxActivity> result = new List<TcxActivity>();

            string xmlContent = File.ReadAllText(xmlFilePath);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);    // full document
            if (doc.HasChildNodes)
            {
                var trainingCenterDatabase = doc.GetElementsByTagName("TrainingCenterDatabase");
                foreach (XmlNode trainingCenterDatabaseNode in trainingCenterDatabase)
                {
                    var activities = GetChildren(trainingCenterDatabaseNode, "Activities");
                    foreach (XmlNode act in activities)
                    {
                        var activitiesChildren = GetChildren(act, "Activity");
                        foreach (XmlNode activity in activitiesChildren) {
                            TcxActivity tcxAct = new TcxActivity();

                            var sport = activity.Attributes["Sport"];
                            var idChild = GetChildren(activity, "Id");
                            var lapChild = GetChildren(activity, "Lap");

                            tcxAct.Id = GetChildValue(activity, "Id");
                            tcxAct.Sport = sport.Value;
                            tcxAct.Laps = new List<TcxLap>();

                            foreach (XmlNode lapNode in lapChild)
                            {
                                TcxLap lap = new TcxLap();
                                lap.StartTime = lapNode.Attributes["StartTime"].Value;
                                lap.Track = new TcxTrack();
                                lap.Track.TrackPoints = new List<TcxTrackpoint>();

                                var trackNodes = GetChildren(lapNode, "Track");
                                foreach (var trackNode in trackNodes)
                                {
                                    var trackPointsNodes = GetChildren(trackNode, "Trackpoint");
                                    foreach (var trackPointNode in trackPointsNodes)
                                    {
                                        TcxTrackpoint point = LoadTrackpoint(trackPointNode);

                                        lap.Track.TrackPoints.Add(point);
                                    }
                                }

                                tcxAct.Laps.Add(lap);
                            }

                            result.Add(tcxAct);
                        }
                        
                    }
                }
            }
            

            return result;
        }


    }
}
