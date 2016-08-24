using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibPearDataPoint;

namespace LibPearDataPointTest
{
    [TestClass]
    public class PearTest
    {
        /// <summary>
        /// Test for Pear Entry point
        /// </summary>
        [TestMethod]
        public void PearDataTest()
        {
            // TODO: make the test more rigorous

            Assert.IsTrue(Pear.Data.Create("myEmptyTestValue"));
            Assert.IsFalse(Pear.Data.Create("myEmptyTestValue"));
            Assert.IsTrue(Pear.Data.Create("myNonEmptyTestValue", "test"));
            Assert.IsTrue(Pear.Data.Create("myIntegerTestValue", 10));

            string emptyString = Pear.Data["myEmptyTestValue"];
            string nonEmptyString = Pear.Data["myNonEmptyTestValue"];
            int integerValue = Pear.Data["myIntegerTestValue"];

            Assert.IsTrue(string.IsNullOrWhiteSpace(emptyString));
            Assert.IsTrue(nonEmptyString.Equals("test"));
            Assert.IsTrue(integerValue == 10);
        }
    }
}
