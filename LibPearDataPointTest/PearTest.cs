using System;
using System.Linq;
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
            Pear.Data.Deinit();

            Assert.IsTrue(Pear.Data.Create("boolTrue", true));
            Assert.IsTrue(Pear.Data.Create("boolFalse", false));
            Assert.IsTrue(Pear.Data.Create("boolCreate"));

            Assert.IsTrue(Pear.Data.Create("byteMax", byte.MaxValue));
            Assert.IsTrue(Pear.Data.Create("byteMin", byte.MinValue));
            Assert.IsTrue(Pear.Data.Create("byteCreate"));

            Assert.IsTrue(Pear.Data.Create("charMax", char.MaxValue));
            Assert.IsTrue(Pear.Data.Create("charMin", char.MinValue));
            Assert.IsTrue(Pear.Data.Create("charCreate"));

            Assert.IsTrue(Pear.Data.Create("decimalPi",(decimal)Math.PI));
            Assert.IsTrue(Pear.Data.Create("decimalCreate"));

            Assert.IsTrue(Pear.Data.Create("doublePi", Math.PI));
            Assert.IsTrue(Pear.Data.Create("doubleCreate"));

            Assert.IsTrue(Pear.Data.Create("floatPi", (float)Math.PI));
            Assert.IsTrue(Pear.Data.Create("floatCreate"));

            Assert.IsTrue(Pear.Data.Create("intMax", int.MaxValue));
            Assert.IsTrue(Pear.Data.Create("intMin", int.MinValue));
            Assert.IsTrue(Pear.Data.Create("intCreate"));

            Assert.IsTrue(Pear.Data.Create("longMax", long.MaxValue));
            Assert.IsTrue(Pear.Data.Create("longMin", long.MinValue));
            Assert.IsTrue(Pear.Data.Create("longCreate"));

            Assert.IsTrue(Pear.Data.Create("sbyteMax", sbyte.MaxValue));
            Assert.IsTrue(Pear.Data.Create("sbyteMin", sbyte.MinValue));
            Assert.IsTrue(Pear.Data.Create("sbyteCreate"));

            Assert.IsTrue(Pear.Data.Create("shortMax", short.MaxValue));
            Assert.IsTrue(Pear.Data.Create("shortMin", short.MinValue));
            Assert.IsTrue(Pear.Data.Create("shortCreate"));

            Assert.IsTrue(Pear.Data.Create("stringValue", "test"));
            Assert.IsTrue(Pear.Data.Create("stringCreate"));

            Assert.IsTrue(Pear.Data.Create("uintMax", uint.MaxValue));
            Assert.IsTrue(Pear.Data.Create("uintMin", uint.MinValue));
            Assert.IsTrue(Pear.Data.Create("uintCreate"));

            Assert.IsTrue(Pear.Data.Create("ulongMax", ulong.MaxValue));
            Assert.IsTrue(Pear.Data.Create("ulongMin", ulong.MinValue));
            Assert.IsTrue(Pear.Data.Create("ulongCreate"));

            Assert.IsTrue(Pear.Data.Create("ushortMax", ushort.MaxValue));
            Assert.IsTrue(Pear.Data.Create("ushortMin", ushort.MinValue));
            Assert.IsTrue(Pear.Data.Create("ushortCreate"));

            Assert.IsTrue(Pear.CountLocal == 38);

            bool boolTrue = Pear.Data["boolTrue"]; Assert.IsTrue(boolTrue);
            bool boolFalse = Pear.Data["boolFalse"]; Assert.IsTrue(boolFalse == false);
            byte byteMax = Pear.Data["byteMax"]; Assert.IsTrue(byteMax == byte.MaxValue);
            byte byteMin = Pear.Data["byteMin"]; Assert.IsTrue(byteMin == byte.MinValue);
            char charMax = Pear.Data["charMax"]; Assert.IsTrue(charMax == char.MaxValue);
            char charMin = Pear.Data["charMin"]; Assert.IsTrue(charMin == char.MinValue);
            decimal decimalPi = Pear.Data["decimalPi"]; Assert.IsTrue(Math.Abs(decimalPi - (decimal)Math.PI) < 0.000001m);
            double doublePi = Pear.Data["doublePi"]; Assert.IsTrue(Math.Abs(doublePi - Math.PI) < 0.000001d);
            float floatPi = Pear.Data["floatPi"]; Assert.IsTrue(Math.Abs(floatPi - (float)Math.PI) < 0.000001f);
            int intMax = Pear.Data["intMax"]; Assert.IsTrue(intMax == int.MaxValue);
            int intMin = Pear.Data["intMin"]; Assert.IsTrue(intMin == int.MinValue);
            long longMax = Pear.Data["longMax"]; Assert.IsTrue(longMax == long.MaxValue);
            long longMin = Pear.Data["longMin"]; Assert.IsTrue(longMin == long.MinValue);
            sbyte sbyteMax = Pear.Data["sbyteMax"]; Assert.IsTrue(sbyteMax == sbyte.MaxValue);
            sbyte sbyteMin = Pear.Data["sbyteMin"]; Assert.IsTrue(sbyteMin == sbyte.MinValue);
            short shortMax = Pear.Data["shortMax"]; Assert.IsTrue(shortMax == short.MaxValue);
            short shortMin = Pear.Data["shortMin"]; Assert.IsTrue(shortMin == short.MinValue);
            string stringValue = Pear.Data["stringValue"]; Assert.IsTrue(stringValue.Equals("test"));
            uint uintMax = Pear.Data["uintMax"]; Assert.IsTrue(uintMax == uint.MaxValue);
            uint uintMin = Pear.Data["uintMin"]; Assert.IsTrue(uintMin == uint.MinValue);
            ulong ulongMax = Pear.Data["ulongMax"]; Assert.IsTrue(ulongMax == ulong.MaxValue);
            ulong ulongMin = Pear.Data["ulongMin"]; Assert.IsTrue(ulongMin == ulong.MinValue);
            ushort ushortMax = Pear.Data["ushortMax"]; Assert.IsTrue(ushortMax == ushort.MaxValue);
            ushort ushortMin = Pear.Data["ushortMin"]; Assert.IsTrue(ushortMin == ushort.MinValue);

            var maxValues = Pear.Data.Where(item => item.Name.Contains("Max"));
            Assert.IsTrue(maxValues.Count() == 9);

            Assert.IsTrue(Pear.Data.Remove("boolTrue"));
            Assert.IsTrue(Pear.Data.Remove("boolFalse"));
            Assert.IsTrue(Pear.Data.Remove("boolCreate"));
            Assert.IsTrue(Pear.Data.Remove("byteMax"));
            Assert.IsTrue(Pear.Data.Remove("byteMin"));
            Assert.IsTrue(Pear.Data.Remove("byteCreate"));
            Assert.IsTrue(Pear.Data.Remove("charMax"));
            Assert.IsTrue(Pear.Data.Remove("charMin"));
            Assert.IsTrue(Pear.Data.Remove("charCreate"));
            Assert.IsTrue(Pear.Data.Remove("decimalPi"));
            Assert.IsTrue(Pear.Data.Remove("decimalCreate"));
            Assert.IsTrue(Pear.Data.Remove("doublePi"));
            Assert.IsTrue(Pear.Data.Remove("doubleCreate"));
            Assert.IsTrue(Pear.Data.Remove("floatPi"));
            Assert.IsTrue(Pear.Data.Remove("floatCreate"));
            Assert.IsTrue(Pear.Data.Remove("intMax"));
            Assert.IsTrue(Pear.Data.Remove("intMin"));
            Assert.IsTrue(Pear.Data.Remove("intCreate"));
            Assert.IsTrue(Pear.Data.Remove("longMax"));
            Assert.IsTrue(Pear.Data.Remove("longMin"));
            Assert.IsTrue(Pear.Data.Remove("longCreate"));
            Assert.IsTrue(Pear.Data.Remove("sbyteMax"));
            Assert.IsTrue(Pear.Data.Remove("sbyteMin"));
            Assert.IsTrue(Pear.Data.Remove("sbyteCreate"));
            Assert.IsTrue(Pear.Data.Remove("shortMax"));
            Assert.IsTrue(Pear.Data.Remove("shortMin"));
            Assert.IsTrue(Pear.Data.Remove("shortCreate"));
            Assert.IsTrue(Pear.Data.Remove("stringValue"));
            Assert.IsTrue(Pear.Data.Remove("stringCreate"));
            Assert.IsTrue(Pear.Data.Remove("uintMax"));
            Assert.IsTrue(Pear.Data.Remove("uintMin"));
            Assert.IsTrue(Pear.Data.Remove("uintCreate"));
            Assert.IsTrue(Pear.Data.Remove("ulongMax"));
            Assert.IsTrue(Pear.Data.Remove("ulongMin"));
            Assert.IsTrue(Pear.Data.Remove("ulongCreate"));
            Assert.IsTrue(Pear.Data.Remove("ushortMax"));
            Assert.IsTrue(Pear.Data.Remove("ushortMin"));
            Assert.IsTrue(Pear.Data.Remove("ushortCreate"));

            Assert.IsTrue(Pear.CountLocal == 0);

            Pear.Data.Deinit();
        }
    }
}
