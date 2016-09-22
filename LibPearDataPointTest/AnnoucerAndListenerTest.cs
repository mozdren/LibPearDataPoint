using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibPearDataPoint;

namespace LibPearDataPointTest
{
    /// <summary>
    /// This class represents test for Announcer and AnnoucmentListener classes
    /// </summary>
    [TestClass]
    public class AnnoucerAndListenerTest
    {
        /// <summary>
        /// Pear object registers two data items and those should be recognized by announcer
        /// </summary>
        [TestMethod]
        public void AnnounceAndListenTest()
        {
            // create dataitems
            Assert.IsTrue(Pear.Data.Create("first1", "1"));
            Assert.IsTrue(Pear.Data.Create("second2", "2"));

            // wait a while until they get announced
            Thread.Sleep(3000);

            // get item names (the items should be already recognized)
            var names = Pear.Data.PearAnnouncementListener.GetNames();
            Assert.IsTrue(names.Contains("first1"));
            Assert.IsTrue(names.Contains("second2"));

            // cleanup
            Pear.Data.Clear();
        }
    }
}
