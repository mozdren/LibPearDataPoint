using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibPearDataPoint;

namespace LibPearDataPointTest
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        public void TestStringUtils()
        {
            // Unhappy Test Cases
            Assert.IsFalse(Utils.IsNameValid(string.Empty));
            Assert.IsFalse(Utils.IsNameValid(null));
            Assert.IsFalse(Utils.IsNameValid(" "));
            Assert.IsFalse(Utils.IsNameValid("123"));
            Assert.IsFalse(Utils.IsNameValid("11karel"));
            Assert.IsFalse(Utils.IsNameValid("karel."));
            Assert.IsFalse(Utils.IsNameValid("karel.1"));
            Assert.IsFalse(Utils.IsNameValid("karel.mozdren.12"));
            Assert.IsFalse(Utils.IsNameValid("karel.mozdren."));
            Assert.IsFalse(Utils.IsNameValid("a#b"));

            // Happy Test Cases
            Assert.IsTrue(Utils.IsNameValid("a"));
            Assert.IsTrue(Utils.IsNameValid("a1"));
            Assert.IsTrue(Utils.IsNameValid("karel"));
            Assert.IsTrue(Utils.IsNameValid("karel11"));
            Assert.IsTrue(Utils.IsNameValid("karel.mozdren"));
            Assert.IsTrue(Utils.IsNameValid("karel.mozdren11"));
            Assert.IsTrue(Utils.IsNameValid("karel11.mozdren11"));
            var testStr = "a.b.c.d.e.f.g.h.i.j.k.l.m.n.o.p.q.r.s.t.u.v.w.x.y.z";
            Assert.IsTrue(Utils.IsNameValid(testStr));
            Assert.IsTrue(Utils.IsNameValid(testStr.ToUpper()));

            var splittedArray = "***text1***text2***text3*** ***".Split("***");
            Assert.IsTrue(splittedArray.Count() == 6);
            Assert.IsTrue(splittedArray[0] == "");
            Assert.IsTrue(splittedArray[1] == "text1");
            Assert.IsTrue(splittedArray[2] == "text2");
            Assert.IsTrue(splittedArray[3] == "text3");
            Assert.IsTrue(splittedArray[4] == " ");
            Assert.IsTrue(splittedArray[5] == "");

            splittedArray = "*** ".Split("***");
            Assert.IsTrue(splittedArray.Count() == 2);
            Assert.IsTrue(splittedArray[0] == "");
            Assert.IsTrue(splittedArray[1] == " ");

            splittedArray = " ".Split("***");
            Assert.IsTrue(splittedArray.Count() == 1);
            Assert.IsTrue(splittedArray[0] == " ");
        }

        [TestMethod]
        public void TestNetUtils()
        {
            // IP addresses might change, but is there is a network, there would be at least some address
            // This test might fail if no network is available
            var ips = Utils.GetLocalIpAddresses();
            Assert.IsTrue(ips.Any());
        }
    }
}
