using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibPearDataPoint;

namespace LibPearDataPointTest
{
    /// <summary>
    /// A test for DataPoint Service
    /// </summary>
    [TestClass]
    public class DataPointServiceTest
    {
        /// <summary>
        /// Service and service client comunication test
        /// </summary>
        [TestMethod]
        public void DataPointServiceAndClientTest()
        {
            Pear.Data.Deinit(); // cleanup before start

            var dataPoint = new LocalDataPoint();

            dataPoint.Create(new DataItem
            {
                Name = "test1",
                Value = "this;is;a;very;specific;string",
                IsLocal = true,
                IsReliable = true,
                LastUpdateTime = new DateTime(1985, 11, 11)
            });

            Assert.IsTrue(dataPoint["test1"].ToString().Equals("this;is;a;very;specific;string"));

            var service = new DataPointService(dataPoint);
            service.StartService(1234);

            Assert.IsTrue(service.ServicePort == 1234);

            var endpoint = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), service.ServicePort);

            var itemFromService = DataPointServiceClient.GetDataItem(endpoint, "xxx");
            Assert.IsTrue(itemFromService == null);

            itemFromService = DataPointServiceClient.GetDataItem(endpoint, "test1");
            Assert.IsTrue(itemFromService != null);

            Assert.IsTrue(itemFromService.Value.Equals("this;is;a;very;specific;string"));
            Assert.IsFalse(itemFromService.IsLocal);
            Assert.IsTrue(itemFromService.Name.Equals("test1"));

            service.StopService();

            Pear.Data.Deinit(); // cleanup after
        }

        /// <summary>
        /// Testting service of a pear data object
        /// </summary>
        [TestMethod]
        public void PearDataServiceTest()
        {
            Pear.Data.Create("PearDataServiceTest", "1234");

            var endpoint = new IPEndPoint(new IPAddress(new byte[] {127, 0, 0, 1}), Pear.ServicePort);
            var itemFromService = DataPointServiceClient.GetDataItem(endpoint, "PearDataServiceTest");

            Assert.IsTrue(itemFromService != null);
            Assert.IsTrue(itemFromService.Name.Equals("PearDataServiceTest"));
            Assert.IsTrue(itemFromService.Value.Equals("1234"));

            Assert.IsTrue(Pear.Data.Update("PearDataServiceTest", "test"));
            itemFromService = DataPointServiceClient.GetDataItem(endpoint, "PearDataServiceTest");
            Assert.IsTrue(itemFromService != null);
            Assert.IsTrue(itemFromService.Name.Equals("PearDataServiceTest"));
            Assert.IsTrue(itemFromService.Value.Equals("test"));

            itemFromService = DataPointServiceClient.GetDataItem(endpoint, "nonexistingdataitem");
            Assert.IsTrue(itemFromService == null);

            Pear.Data.Deinit();
        }
    }
}
