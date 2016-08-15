using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibPearDataPointTest
{
    [TestClass]
    public class ConfigurationTest
    {
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

        [TestMethod]
        public void GetValuesTest()
        {
            var configuration = new LibPearDataPoint.Configuration("Configuration.Test.xml");
            Assert.IsNotNull(configuration);

            // returns empty collection
            Assert.IsTrue(configuration.GetValues(null).Count() == 0);
            Assert.IsTrue(configuration.GetValues(string.Empty).Count() == 0);
            Assert.IsTrue(configuration.GetValues("nonexistingkey").Count() == 0);
            Assert.IsTrue(configuration.GetValues("empty").Count() == 0);

            // Happy flows
            var singleValue = configuration.GetValues("singlevalue");
            Assert.IsTrue(singleValue.Count() == 1);
            Assert.IsTrue(singleValue.FirstOrDefault() == "testValue");

            var multiValue = configuration.GetValues("multivalue");
            Assert.IsTrue(multiValue.Count() == 4);
            Assert.IsTrue(multiValue.FirstOrDefault() == "testValue1");
            Assert.IsTrue(multiValue.LastOrDefault() == "testValue4");
        }
    }
}
