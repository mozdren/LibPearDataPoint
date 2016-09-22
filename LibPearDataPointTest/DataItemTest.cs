using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibPearDataPointTest
{
    [TestClass]
    public class DataItemTest
    {
        /// <summary>
        /// Dataitem initialization test. We would like to ensure, that the data
        /// are stored as is, no changes are done during initialization.
        /// </summary>
        [TestMethod]
        public void DataItemInitializationTest()
        {
            // create dataitem
            var dataItem = new LibPearDataPoint.DataItem
            {
                IsLocal = true,
                IsReliable = true,
                LastUpdateTime = new DateTime(1985, 11, 11),
                Name = "BirthDay",
                Value = new DateTime(1985, 11, 11).ToString("yyyyMMdd")
            };

            Assert.IsTrue(dataItem.IsLocal);
            Assert.IsTrue(dataItem.IsReliable);
            Assert.IsTrue(dataItem.LastUpdateTime.Day == 11 && 
                          dataItem.LastUpdateTime.Month == 11 &&
                          dataItem.LastUpdateTime.Year == 1985);
            Assert.IsTrue(dataItem.Name == "BirthDay");
            Assert.IsFalse(dataItem.Name == "BIRTHDAY");
            Assert.IsTrue(dataItem.Value == "19851111");

            // create second dataitem
            var dataItem2 = new LibPearDataPoint.DataItem
            {
                IsLocal = false,
                IsReliable = false,
                LastUpdateTime = new DateTime(2016, 08, 17),
                Name = "DateOfTestCreation",
                Value = new DateTime(2016, 08, 17).ToString("yyyyMMdd")
            };

            Assert.IsFalse(dataItem2.IsLocal);
            Assert.IsFalse(dataItem2.IsReliable);
            Assert.IsTrue(dataItem2.LastUpdateTime.Day == 17 &&
                          dataItem2.LastUpdateTime.Month == 8 &&
                          dataItem2.LastUpdateTime.Year == 2016);
            Assert.IsTrue(dataItem2.Name == "DateOfTestCreation");
            Assert.IsFalse(dataItem2.Name == "DATEOFTESTCREATION");
            Assert.IsTrue(dataItem2.Value == "20160817");
        }
    }
}
