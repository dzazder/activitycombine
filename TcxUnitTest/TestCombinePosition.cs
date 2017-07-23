using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tcxengine.Entities;
using System.Collections.Generic;

namespace TcxUnitTest
{
    [TestClass]
    public class TestCombinePosition
    {
        [TestMethod]
        public void Test1Second()
        {
            TcxTrackpoint c = new TcxTrackpoint();
            c.IsTimeDefined = true;
            c.Time = new DateTime(2017, 07, 01, 11, 10, 10);
            c.IsPositionDefined = false;

            TcxTrackpoint p = new TcxTrackpoint();
            p.IsTimeDefined = true;
            p.Time = new DateTime(2017, 07, 01, 11, 10, 09);
            p.IsPositionDefined = true;
            p.Position = new TcxPosition(51.085341, 17.043303);

            TcxTrackpoint n = new TcxTrackpoint();
            n.IsTimeDefined = true;
            n.Time = new DateTime(2017, 07, 01, 11, 10, 11);
            n.IsPositionDefined = true;
            n.Position = new TcxPosition(51.085286, 17.043645);

            List<TcxTrackpoint> next = new List<TcxTrackpoint>();
            next.Add(n);
            var result = c.CombineTrackpoint(p, next);

            double lat = (51.085341 + 51.085286) / 2;
            double lon = (17.043303 + 17.043645) / 2;

            Assert.AreEqual(lat, result.Position.Latitude, 0.000001);
            Assert.AreEqual(lon, result.Position.Longitude, 0.000001);
        }

        [TestMethod]
        public void TestPrevMoreDiff()
        {
            TcxTrackpoint c = new TcxTrackpoint();
            c.IsTimeDefined = true;
            c.Time = new DateTime(2017, 07, 01, 11, 10, 10);
            c.IsPositionDefined = false;

            TcxTrackpoint p = new TcxTrackpoint();
            p.IsTimeDefined = true;
            p.Time = new DateTime(2017, 07, 01, 11, 10, 06);
            p.IsPositionDefined = true;
            p.Position = new TcxPosition(51.085341, 17.043303);

            TcxTrackpoint n = new TcxTrackpoint();
            n.IsTimeDefined = true;
            n.Time = new DateTime(2017, 07, 01, 11, 10, 11);
            n.IsPositionDefined = true;
            n.Position = new TcxPosition(51.085286, 17.043645);

            List<TcxTrackpoint> next = new List<TcxTrackpoint>();
            next.Add(n);
            var result = c.CombineTrackpoint(p, next);

            double lat = 51.085297;
            double lon = 17.0435766;

            Assert.AreEqual(lat, result.Position.Latitude, 0.000001);
            Assert.AreEqual(lon, result.Position.Longitude, 0.000001);
        }

        [TestMethod]
        public void TestNextMoreDiff()
        {
            TcxTrackpoint c = new TcxTrackpoint();
            c.IsTimeDefined = true;
            c.Time = new DateTime(2017, 07, 01, 11, 10, 10);
            c.IsPositionDefined = false;

            TcxTrackpoint p = new TcxTrackpoint();
            p.IsTimeDefined = true;
            p.Time = new DateTime(2017, 07, 01, 11, 10, 09);
            p.IsPositionDefined = true;
            p.Position = new TcxPosition(51.085341, 17.043303);

            TcxTrackpoint n = new TcxTrackpoint();
            n.IsTimeDefined = true;
            n.Time = new DateTime(2017, 07, 01, 11, 10, 15);
            n.IsPositionDefined = true;
            n.Position = new TcxPosition(51.085286, 17.043645);

            List<TcxTrackpoint> next = new List<TcxTrackpoint>();
            next.Add(n);
            var result = c.CombineTrackpoint(p, next);

            double lat = 51.0853318;
            double lon = 17.04336;

            Assert.AreEqual(lat, result.Position.Latitude, 0.000001);
            Assert.AreEqual(lon, result.Position.Longitude, 0.000001);
        }

        [TestMethod]
        public void TestSelectCorrectNext()
        {
            TcxTrackpoint c = new TcxTrackpoint();
            c.IsTimeDefined = true;
            c.Time = new DateTime(2017, 07, 01, 11, 10, 10);
            c.IsPositionDefined = false;

            TcxTrackpoint c2 = new TcxTrackpoint();
            c2.IsTimeDefined = true;
            c2.Time = new DateTime(2017, 07, 01, 11, 10, 11);
            c2.IsPositionDefined = false;

            TcxTrackpoint c3 = new TcxTrackpoint();
            c3.IsTimeDefined = true;
            c3.Time = new DateTime(2017, 07, 01, 11, 10, 13);
            c3.IsPositionDefined = false;

            TcxTrackpoint c4 = new TcxTrackpoint();
            c4.IsTimeDefined = true;
            c4.Time = new DateTime(2017, 07, 01, 11, 10, 14);
            c4.IsPositionDefined = false;

            TcxTrackpoint p = new TcxTrackpoint();
            p.IsTimeDefined = true;
            p.Time = new DateTime(2017, 07, 01, 11, 10, 09);
            p.IsPositionDefined = true;
            p.Position = new TcxPosition(51.085341, 17.043303);

            TcxTrackpoint n = new TcxTrackpoint();
            n.IsTimeDefined = true;
            n.Time = new DateTime(2017, 07, 01, 11, 10, 15);
            n.IsPositionDefined = true;
            n.Position = new TcxPosition(51.085286, 17.043645);

            List<TcxTrackpoint> next = new List<TcxTrackpoint>();
            next.Add(c2);
            next.Add(c3);
            next.Add(c4);
            next.Add(n);
            var result = c.CombineTrackpoint(p, next);

            double lat = 51.0853318;
            double lon = 17.04336;

            Assert.AreEqual(lat, result.Position.Latitude, 0.000001);
            Assert.AreEqual(lon, result.Position.Longitude, 0.000001);
        }
    }
}
