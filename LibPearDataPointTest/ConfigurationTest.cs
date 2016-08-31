using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibPearDataPointTest
{
    /// <summary>
    /// Test for configuration classes
    /// </summary>
    [TestClass]
    public class ConfigurationTest
    {
        /// <summary>
        /// Configuration.getValue method test.
        /// </summary>
        [TestMethod]
        public void GetValueTest()
        {
            var configuration = new LibPearDataPoint.Configuration("Configuration.Test.xml");
            Assert.IsNotNull(configuration);

            // returns empty string
            Assert.IsTrue(configuration.GetValue(null) == string.Empty);
            Assert.IsTrue(configuration.GetValue(string.Empty) == string.Empty);
            Assert.IsTrue(configuration.GetValue("nonexistingkey") == string.Empty);
            Assert.IsTrue(configuration.GetValue("empty") == string.Empty);

            // Happy flows
            Assert.IsTrue(configuration.GetValue("singlevalue") == "testValue");
            Assert.IsTrue(configuration.GetValue("multivalue") == "testValue1");
        }


        /// <summary>
        /// Configuration.getValues method test.
        /// </summary>
        [TestMethod]
        public void GetValuesTest()
        {
            var configuration = new LibPearDataPoint.Configuration("Configuration.Test.xml");
            Assert.IsNotNull(configuration);

            // returns empty collection
            Assert.IsTrue(!configuration.GetValues(null).Any());
            Assert.IsTrue(!configuration.GetValues(string.Empty).Any());
            Assert.IsTrue(!configuration.GetValues("nonexistingkey").Any());
            Assert.IsTrue(!configuration.GetValues("empty").Any());

            // Happy flows
            var singleValue = configuration.GetValues("singlevalue").ToArray();
            Assert.IsTrue(singleValue.Length == 1);
            Assert.IsTrue(singleValue.FirstOrDefault() == "testValue");

            var multiValue = configuration.GetValues("multivalue").ToArray();
            Assert.IsTrue(multiValue.Length == 4);
            Assert.IsTrue(multiValue.FirstOrDefault() == "testValue1");
            Assert.IsTrue(multiValue.LastOrDefault() == "testValue4");
        }

        /// <summary>
        /// Configuration.Helper - specific configuraion values test.
        /// </summary>
        [TestMethod]
        public void ConfigurationHelperTest()
        {
            var configuration = new LibPearDataPoint.Configuration();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(configuration.Version));
            Assert.IsTrue(configuration.BroadcastPortNumber != 0);
            Assert.IsTrue(configuration.PortNumberRange.Item1 != 0);
            Assert.IsTrue(configuration.PortNumberRange.Item2 != 0);
            Assert.IsTrue(configuration.PortNumberRange.Item1 <=
                          configuration.PortNumberRange.Item2);
            Assert.IsTrue(configuration.MaxPortNumber != 0);
            Assert.IsTrue(configuration.MinPortNumber != 0);
            Assert.IsTrue(configuration.AnnouncingInterval != 0);
            Assert.IsTrue(!Equals(configuration.BroadcastAddress, new System.Net.IPAddress(new byte[] { 0, 0, 0, 0 })));
        }

    }
}
