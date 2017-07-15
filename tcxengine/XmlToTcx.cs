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
    public class XmlToTcx
    {
        /// <summary>
        /// Returns all children with specified name
        /// </summary>
        /// <param name="parentNode">Parent node</param>
        /// <param name="name">Child name</param>
        /// <returns></returns>
        private static List<XmlNode> GetChildren(XmlNode parentNode, string name)
        {
            var result = new List<XmlNode>();

            foreach (XmlNode child in parentNode.ChildNodes)
            {
                if (child.Name.Equals(name))
                {
                    result.Add(child);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns first child node with specified name or null
        /// </summary>
        /// <param name="parentNode">Parent node</param>
        /// <param name="name">Child name</param>
        /// <returns></returns>
        private static XmlNode GetChild(XmlNode parentNode, string name)
        {
            var children = GetChildren(parentNode, name);
            return children.Count > 0 ? children[0] : null;
        }

        /// <summary>
        /// Reteurns value of node with specified name when value is saved like as <Node>value</Node>
        /// </summary>
        /// <param name="parentNode">Parent node</param>
        /// <param name="name">Name of node with value</param>
        /// <returns>Value of node or null</returns>
        private static string GetChildValue(XmlNode parentNode, string name)
        {
            var childNode = GetChild(parentNode, name);
            if (childNode != null)
            {
                var childNodes = childNode.ChildNodes;
                if (childNodes != null && childNodes.Count > 0)
                {
                    return childNodes[0].Value;
                }
            }

            return null;
        }

        private static double? GetDoubleChildValue(XmlNode parentNode, string name)
        {
            var val = GetChildValue(parentNode, name);
            if (!string.IsNullOrEmpty(val))
            {
                double d;
                if (Double.TryParse(val, out d))
                {
                    return d;
                }
            }

            return null;
        }

        private static TcxTrackpoint LoadTrackpoint(XmlNode trackpointNode)
        {
            TcxTrackpoint trackpoint = new TcxTrackpoint();

            var time = GetChildValue(trackpointNode, TcxTrackpoint.ATTR_TIME);
            trackpoint.IsTimeDefined = (time != null);
            if (time != null)
            {
                trackpoint.Time = time;
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
                            tcxAct.Lap = new List<TcxLap>();

                            foreach (XmlNode lapNode in lapChild)
                            {
                                TcxLap lap = new TcxLap();
                                lap.StartTime = lapNode.Attributes["StartTime"].Value;
                                lap.Track = new TcxTrack();
                                lap.Track.Track = new List<TcxTrackpoint>();

                                var trackNodes = GetChildren(lapNode, "Track");
                                foreach (var trackNode in trackNodes)
                                {
                                    var trackPointsNodes = GetChildren(trackNode, "Trackpoint");
                                    foreach (var trackPointNode in trackPointsNodes)
                                    {
                                        TcxTrackpoint point = LoadTrackpoint(trackPointNode);

                                        lap.Track.Track.Add(point);
                                    }
                                }

                                tcxAct.Lap.Add(lap);
                            }

                            result.Add(tcxAct);
                        }
                        
                    }
                }
            }

            //XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

            //XmlNodeList activities;

            //if (doc.DocumentElement.Attributes["xmlns"] != null)
            //{
            //    string xmlns = doc.DocumentElement.Attributes["xmlns"].Value;
                
            //    nsmgr.AddNamespace("Training", xmlns);

            //    activities = doc.SelectNodes("/Training:TrainingCenterDatabase/Training:Activities/Training:Activity", nsmgr);
            //}
            //else
            //{
            //    activities = doc.SelectNodes("/TrainingCenterDatabase/Activities/Activity");
            //}
            
            //foreach (XmlNode node in activities)
            //{
            //    TcxActivity activity = new TcxActivity();
            //    activity.Sport = node.Attributes["Sport"].Value;
            //    var id = node.SelectNodes("/Training:Id", nsmgr);
            //    //activity.Id = id;
            //}
            
            //Console.WriteLine("abc");

            return result;
        }


    }
}
