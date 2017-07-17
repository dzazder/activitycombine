using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcxengine.Entities
{
    public class TcxActivity
    {
        public string Id { get; set; }
        public List<TcxLap> Laps { get; set; }
        public string Sport { get; set; }

        public string ToString()
        {
            double totalTime = 0;
            double distanceMeters = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"no\"?><TrainingCenterDatabase xsi:schemaLocation=\"http://www.garmin.com/xmlschemas/ActivityExtension/v1 http://www.garmin.com/xmlschemas/ActivityExtensionv1.xsd http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2 http://www.garmin.com/xmlschemas/TrainingCenterDatabasev2.xsd\" xmlns=\"http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2\" xmlns:extv2=\"http://www.garmin.com/xmlschemas/ActivityExtension/v2\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><Activities>");
            sb.Append("<Activity Sport=" + Sport + ">");

            foreach (TcxLap lap in Laps)
            {
                totalTime = lap.Track.TotalTimeInSeconds();
                distanceMeters = lap.Track.DistanceMeters();
                sb.Append("<Lap>");
                sb.Append("<Track>");

                foreach (TcxTrackpoint tr in lap.Track.TrackPoints)
                {
                    sb.Append(tr.ToString());
                }

                sb.Append("</Track>");
                sb.Append("<TotalTimeSeconds>" + totalTime + "</TotalTimeSeconds><DistanceMeters>" + distanceMeters + "</DistanceMeters><MaximumSpeed>10.780555555555557</MaximumSpeed><Calories>0</Calories><Intensity>Active</Intensity><Cadence>87</Cadence><TriggerMethod>Manual</TriggerMethod><Notes/><Extensions><extv2:LX><extv2:AvgSpeed>6.675</extv2:AvgSpeed></extv2:LX></Extensions>");
                sb.Append("</Lap>");
            }

            sb.Append("<TotalTimeSeconds>" + totalTime + "</TotalTimeSeconds><DistanceMeters>" + distanceMeters + "</DistanceMeters><MaximumSpeed>10.780555555555557</MaximumSpeed><Calories>0</Calories><Intensity>Active</Intensity><Cadence>87</Cadence><TriggerMethod>Manual</TriggerMethod><Notes/><Extensions><extv2:LX><extv2:AvgSpeed>6.675</extv2:AvgSpeed></extv2:LX></Extensions></Lap><Creator xsi:type=\"Device_t\"><Name>ROX 6.0</Name><UnitId>4294967295</UnitId><ProductID>65535</ProductID><Version><VersionMajor>0</VersionMajor><VersionMinor>0</VersionMinor></Version></Creator>");
            sb.Append("</Activity>");
            sb.Append("</Activities>");
            sb.Append("<Author xsi:type=\"Application_t\"><Name>Sigma Data Center</Name><Build><Version><VersionMajor>5</VersionMajor><VersionMinor>28</VersionMinor><BuildMajor>0</BuildMajor><BuildMinor>0</BuildMinor></Version><Type>Release</Type><Time>Sep 1 2015, 14:19:54</Time></Build><LangID>EN</LangID><PartNumber>XXX-XXXXX-XX</PartNumber></Author></TrainingCenterDatabase>");

            return sb.ToString();
        }
    }
}
