using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tcxengine.Entities;

namespace TcxUnitTest
{
    [TestClass]
    public class TestCombineElevation
    {
        [TestMethod]
        public void Test1Second()
        {
            TcxTrackpoint c = new TcxTrackpoint();
            c.IsTimeDefined = true;
            c.Time = new DateTime(2017, 07, 01, 11, 10, 10);
            c.IsAltitudeMetersDefined = false;

            TcxTrackpoint p = new TcxTrackpoint();
            p.IsTimeDefined = true;
            p.Time = new DateTime(2017, 07, 01, 11, 10, 09);
            p.IsAltitudeMetersDefined = true;
            p.AltitudeMeters = 100;

            TcxTrackpoint n = new TcxTrackpoint();
            n.IsTimeDefined = true;
            n.Time = new DateTime(2017, 07, 01, 11, 10, 11);
            n.IsAltitudeMetersDefined = true;
            n.AltitudeMeters = 103;

            List<TcxTrackpoint> next = new List<TcxTrackpoint>();
            next.Add(n);
            var result = c.CombineTrackpoint(p, next);

            Assert.AreEqual(101, result.AltitudeMeters, 0.5);
        }
    }
}
