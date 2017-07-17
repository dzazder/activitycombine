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
    }
}
