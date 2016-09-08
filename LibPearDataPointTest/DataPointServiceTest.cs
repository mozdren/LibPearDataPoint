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
            var dataPoint = new LocalDataPoint();

            dataPoint.Create(new DataItem
            {
                Name = "test1",
                Value = "this;is;a;very;specific;string",
                IsLocal = true,
                IsReliable = true,
                LastUpdateTime = new DateTime(1985,11,11)
            });

            Assert.IsTrue(dataPoint["test1"].ToString().Equals("this;is;a;very;specific;string"));

            var service = new DataPointService(dataPoint);
            service.StartService();

            Assert.IsTrue(service.ServicePort != 0);

            var endpoint = new IPEndPoint(new IPAddress(new byte[] {127, 0, 0, 1}), service.ServicePort);

            var itemFromService = DataPointServiceClient.GetDataItem(endpoint, "xxx");
            Assert.IsTrue(itemFromService == null);

            itemFromService = DataPointServiceClient.GetDataItem(endpoint, "test1");
            Assert.IsTrue(itemFromService != null);

            Assert.IsTrue(itemFromService.Value.Equals("this;is;a;very;specific;string"));
            Assert.IsFalse(itemFromService.IsLocal);
            Assert.IsTrue(itemFromService.Name.Equals("test1"));

            service.StopService();
        }
    }
}
