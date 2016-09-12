using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibPearDataPoint;

namespace LibPearDataPointTest
{
    [TestClass]
    public class AnnoucerAndListenerTest
    {
        [TestMethod]
        public void AnnounceAndListenTest()
        {
            Pear.Data.Deinit();

            var dataPoint = new LocalDataPoint();
            Assert.IsTrue(dataPoint.Create(new DataItem {Name = "first1", Value = "1"}));
            Assert.IsTrue(dataPoint.Create(new DataItem {Name = "second2", Value = "2" }));
            var announcer = new Announcer(dataPoint);
            var listener = new AnnouncementListener();

            listener.StartListening();
            announcer.Announce();
            Thread.Sleep(100);
            listener.StopListening();

            var names = listener.GetNames();
            Assert.IsTrue(names.Count == 2);
            Assert.IsTrue(names.Contains("first1"));
            Assert.IsTrue(names.Contains("second2"));

            Pear.Data.Deinit();
        }
    }
}
