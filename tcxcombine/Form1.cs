using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tcxengine;
using tcxengine.Entities;

namespace tcxcombine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var firstTcx = XmlToTcx.Convert("07_17.tcx");
            var gpx = XmlToGpx.Convert("07_17.gpx");
            var tcx = gpx[0].ConvertToTcxActivity();

            firstTcx[0].Laps[0].Track.Concat(tcx.Laps[0].Track);

            File.WriteAllText("result.tcx", firstTcx[0].ToString());
            Console.WriteLine("END");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            TcxTrackpoint c = new TcxTrackpoint();
            c.IsTimeDefined = true;
            c.Time = new DateTime(2017, 07, 01, 11, 10, 10);
            c.IsPositionDefined = false;

            TcxTrackpoint p = new TcxTrackpoint();
            c.IsTimeDefined = true;
            c.Time = new DateTime(2017, 07, 01, 11, 10, 09);
            c.IsPositionDefined = true;
            c.Position = new TcxPosition(51.085341, 17.043303);

            TcxTrackpoint n = new TcxTrackpoint();
            c.IsTimeDefined = true;
            c.Time = new DateTime(2017, 07, 01, 11, 10, 11);
            c.IsPositionDefined = true;
            c.Position = new TcxPosition(51.085286, 17.043645);

            List<TcxTrackpoint> next = new List<TcxTrackpoint>();
            next.Add(n);
            var result = c.CombineTrackpoint(p, next);

            double lat = (51.085341 + 51.085286) / 2;
            double lon = (17.043303 + 17.043645) / 2;
        }
    }
}
