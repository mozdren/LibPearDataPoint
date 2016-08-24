﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using LibPearDataPoint;

namespace LibPearDataPointTest
{
    [TestClass]
    public class DataPointTest
    {
        /// <summary>
        /// Testing local datapoints
        /// </summary>
        [TestMethod]
        public void LocalDataPointTest()
        {
            var dataPoint = new LocalDataPoint();
            Assert.IsTrue(dataPoint.Count() == 0);

            // Unhappy Flow Create
            DataItem dataItem = null;
            Assert.IsFalse(dataPoint.Create(dataItem));

            dataItem = new DataItem();
            Assert.IsFalse(dataPoint.Create(dataItem));

            dataItem.Name = "    ";
            Assert.IsFalse(dataPoint.Create(dataItem));

            // Happy Flow - create first data item 
            dataItem.Name = "TheBestNameEver";
            Assert.IsTrue(dataPoint.Create(dataItem));
            Assert.IsTrue(dataPoint.Count() == 1);
            Assert.IsNotNull(dataPoint.FirstOrDefault(item => item.Name.Equals("TheBestNameEver")));

            // Unhappy Flow - create the same again
            Assert.IsFalse(dataPoint.Create(dataItem));

            // validation is not tested any more - it is tested in previous test cases
            // Happy Flow removeItem
            Assert.IsTrue(dataPoint.Remove(new DataItem { Name = "TheBestNameEver" }));
            Assert.IsTrue(dataPoint.Count() == 0);

            // Unhappy Flow removeItem
            Assert.IsFalse(dataPoint.Remove(new DataItem()));
            Assert.IsFalse(dataPoint.Remove(new DataItem { Name = "TheBestNameEver" }));

            // Check that the data in dataItem are not influenced by writing to returned item
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "item1", Value = "OriginalValue1" }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "item2", Value = "OriginalValue2" }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "item3", Value = "OriginalValue2" }));

            var dataItem2 = dataPoint.Where(item => item.Name.Equals("item2")).FirstOrDefault();
            Assert.IsNotNull(dataItem2);
            dataItem2.Value = "NewValue1";

            var dataItem2Again = dataPoint.FirstOrDefault(item => item.Name.Equals("item2"));
            Assert.IsNotNull(dataItem2Again);
            Assert.IsTrue(dataItem2Again.Value.Equals("OriginalValue2"));

            // Unhappy Update Flows
            Assert.IsFalse(dataPoint.Update(new DataItem()));
            Assert.IsFalse(dataPoint.Update(new DataItem { Name = "notExistingItem" }));

            // Happy Update Flow - get item, change data
            var itemToChange = dataPoint.FirstOrDefault(item => item.Name.Equals("item2"));
            Assert.IsNotNull(itemToChange);
            var oldValue = itemToChange.Value;
            itemToChange.Value = "ChangedValue2";
            Assert.IsTrue(dataPoint.Update(itemToChange));
            var changedItem = dataPoint.FirstOrDefault(item => item.Name.Equals("item2"));
            Assert.IsNotNull(changedItem);
            Assert.IsFalse(changedItem.Name.Equals("ChagedValue2"));
        }

        [TestMethod]
        public void DataPointDataItemConversionTest()
        {
            var dataPoint = new LocalDataPoint();

            var item = new DataItem();

            item.Name = "longMax"; item.Set(long.MaxValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "longValue"; item.Set(1985L); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "longMin"; item.Set(long.MinValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "longFail"; item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            item.Name = "ulongMax";   item.Set(ulong.MaxValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "ulongValue"; item.Set((ulong)1985); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "ulongMin";   item.Set(ulong.MinValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "ulongFail";  item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            item.Name = "intMax"; item.Set(int.MaxValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "intValue"; item.Set(1985); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "intMin"; item.Set(int.MinValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "intFail"; item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            item.Name = "uintMax"; item.Set(uint.MaxValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "uintValue"; item.Set(1985U); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "uintMin"; item.Set(uint.MinValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "uintFail"; item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            item.Name = "shortMax"; item.Set(short.MaxValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "shortValue"; item.Set((short)1985); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "shortMin"; item.Set(short.MinValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "shortFail"; item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            item.Name = "ushortMax"; item.Set(ushort.MaxValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "ushortValue"; item.Set((ushort)1985); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "ushortMin"; item.Set(ushort.MinValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "ushortFail"; item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            item.Name = "charMax"; item.Set(char.MaxValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "charValue"; item.Set('u'); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "charMin"; item.Set(char.MinValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "charFail"; item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            item.Name = "byteMax"; item.Set(byte.MaxValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "byteValue"; item.Set((byte)11); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "byteMin"; item.Set(byte.MinValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "byteFail"; item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            item.Name = "sbyteMax"; item.Set(sbyte.MaxValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "sbyteValue"; item.Set((sbyte)11); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "sbyteMin"; item.Set(sbyte.MinValue); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "sbyteFail"; item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            item.Name = "floatPi"; item.Set((float)Math.PI); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "floatValue"; item.Set(11.11f); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "floatMinusPi"; item.Set((float)-Math.PI); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "floatFail"; item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            item.Name = "doublePi"; item.Set(Math.PI); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "doubleValue"; item.Set(11.11d); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "doubleMinusPi"; item.Set(-Math.PI); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "doubleFail"; item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            item.Name = "decimalPi"; item.Set((decimal)Math.PI); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "decimalValue"; item.Set(11.11m); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "decimalMinusPi"; item.Set((decimal)-Math.PI); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "decimalFail"; item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            item.Name = "boolTrue"; item.Set(true); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "boolFalse"; item.Set(false); Assert.IsTrue(dataPoint.Create(item));
            item.Name = "boolFail"; item.Set("asd"); Assert.IsTrue(dataPoint.Create(item));

            Assert.IsTrue(dataPoint.Count() == 51);

            Assert.IsNotNull(dataPoint["longMax"].AsLong());
            Assert.IsTrue(dataPoint["longMax"] == long.MaxValue);
            Assert.IsNotNull(dataPoint["longMin"].AsLong());
            Assert.IsTrue(dataPoint["longMin"] == long.MinValue);
            Assert.IsNotNull(dataPoint["longValue"].AsLong());
            Assert.IsTrue(dataPoint["longValue"] == (long)1985);
            Assert.IsNull(dataPoint["longFail"].AsLong());

            Assert.IsNotNull(dataPoint["ulongMax"].AsULong());
            Assert.IsTrue(dataPoint["ulongMax"] == ulong.MaxValue);
            Assert.IsNotNull(dataPoint["ulongMin"].AsULong());
            Assert.IsTrue(dataPoint["ulongMin"] == ulong.MinValue);
            Assert.IsNotNull(dataPoint["ulongValue"].AsULong());
            Assert.IsTrue(dataPoint["ulongValue"] == (ulong)1985);
            Assert.IsNull(dataPoint["ulongFail"].AsULong());

            Assert.IsNotNull(dataPoint["intMax"].AsInt());
            Assert.IsTrue(dataPoint["intMax"] == int.MaxValue);
            Assert.IsNotNull(dataPoint["intMin"].AsInt());
            Assert.IsTrue(dataPoint["intMin"] == int.MinValue);
            Assert.IsNotNull(dataPoint["intValue"].AsInt());
            Assert.IsTrue(dataPoint["intValue"] == 1985);
            Assert.IsNull(dataPoint["intFail"].AsInt());

            Assert.IsNotNull(dataPoint["uintMax"].AsUInt());
            Assert.IsTrue(dataPoint["uintMax"] == uint.MaxValue);
            Assert.IsNotNull(dataPoint["uintMin"].AsUInt());
            Assert.IsTrue(dataPoint["uintMin"] == uint.MinValue);
            Assert.IsNotNull(dataPoint["uintValue"].AsUInt());
            Assert.IsTrue(dataPoint["uintValue"] == (uint)1985);
            Assert.IsNull(dataPoint["uintFail"].AsUInt());

            Assert.IsNotNull(dataPoint["shortMax"].AsShort());
            Assert.IsTrue(dataPoint["shortMax"] == short.MaxValue);
            Assert.IsNotNull(dataPoint["shortMin"].AsShort());
            Assert.IsTrue(dataPoint["shortMin"] == short.MinValue);
            Assert.IsNotNull(dataPoint["shortValue"].AsShort());
            Assert.IsTrue(dataPoint["shortValue"] == (short)1985);
            Assert.IsNull(dataPoint["shortFail"].AsShort());

            Assert.IsNotNull(dataPoint["ushortMax"].AsUShort());
            Assert.IsTrue(dataPoint["ushortMax"] == ushort.MaxValue);
            Assert.IsNotNull(dataPoint["ushortMin"].AsUShort());
            Assert.IsTrue(dataPoint["ushortMin"] == ushort.MinValue);
            Assert.IsNotNull(dataPoint["ushortValue"].AsUShort());
            Assert.IsTrue(dataPoint["ushortValue"] == (ushort)1985);
            Assert.IsNull(dataPoint["ushortFail"].AsUShort());

            Assert.IsNotNull(dataPoint["charMax"].AsChar());
            Assert.IsTrue((char)dataPoint["charMax"] == char.MaxValue);
            Assert.IsNotNull(dataPoint["charMin"].AsChar());
            Assert.IsTrue((char)dataPoint["charMin"] == char.MinValue);
            Assert.IsNotNull(dataPoint["charValue"].AsChar());
            Assert.IsTrue((char)dataPoint["charValue"] == 'u');
            Assert.IsNull(dataPoint["charFail"].AsChar());

            Assert.IsNotNull(dataPoint["byteMax"].AsByte());
            Assert.IsTrue(dataPoint["byteMax"] == byte.MaxValue);
            Assert.IsNotNull(dataPoint["byteMin"].AsByte());
            Assert.IsTrue(dataPoint["byteMin"] == byte.MinValue);
            Assert.IsNotNull(dataPoint["byteValue"].AsByte());
            Assert.IsTrue(dataPoint["byteValue"] == (byte)11);
            Assert.IsNull(dataPoint["byteFail"].AsByte());

            Assert.IsNotNull(dataPoint["sbyteMax"].AsSByte());
            Assert.IsTrue(dataPoint["sbyteMax"] == sbyte.MaxValue);
            Assert.IsNotNull(dataPoint["sbyteMin"].AsSByte());
            Assert.IsTrue(dataPoint["sbyteMin"] == sbyte.MinValue);
            Assert.IsNotNull(dataPoint["sbyteValue"].AsSByte());
            Assert.IsTrue(dataPoint["sbyteValue"] == (sbyte)11);
            Assert.IsNull(dataPoint["sbyteFail"].AsSByte());

            Assert.IsNotNull(dataPoint["floatPi"].AsFloat());
            Assert.IsTrue(Math.Abs(dataPoint["floatPi"] - (float)Math.PI) < 0.000001f);
            Assert.IsNotNull(dataPoint["floatMinusPi"].AsFloat());
            Assert.IsTrue(Math.Abs(dataPoint["floatMinusPi"] + (float)Math.PI) < 0.000001f);
            Assert.IsNotNull(dataPoint["floatValue"].AsFloat());
            Assert.IsTrue(dataPoint["floatValue"] == 11.11f);
            Assert.IsNull(dataPoint["floatFail"].AsFloat());

            Assert.IsNotNull(dataPoint["doublePi"].AsDouble());
            Assert.IsTrue(Math.Abs(dataPoint["doublePi"] - Math.PI) < 0.00000000000001);
            Assert.IsNotNull(dataPoint["doubleMinusPi"].AsDouble());
            Assert.IsTrue(Math.Abs(dataPoint["doubleMinusPi"] + Math.PI) < 0.00000000000001);
            Assert.IsNotNull(dataPoint["doubleValue"].AsDouble());
            Assert.IsTrue(dataPoint["doubleValue"] == 11.11d);
            Assert.IsNull(dataPoint["doubleFail"].AsDouble());

            Assert.IsNotNull(dataPoint["decimalPi"].AsDecimal());
            Assert.IsTrue(Math.Abs(dataPoint["decimalPi"] - (decimal)Math.PI) < 0.00000000000001m);
            Assert.IsNotNull(dataPoint["decimalMinusPi"].AsDecimal());
            Assert.IsTrue(Math.Abs(dataPoint["decimalMinusPi"] + (decimal)Math.PI) < 0.00000000000001m);
            Assert.IsNotNull(dataPoint["decimalValue"].AsDecimal());
            Assert.IsTrue(dataPoint["decimalValue"] == 11.11m);
            Assert.IsNull(dataPoint["decimalFail"].AsDecimal());

            Assert.IsNotNull(dataPoint["boolTrue"].AsBool());
            Assert.IsTrue(dataPoint["boolTrue"] == true);
            Assert.IsNotNull(dataPoint["boolFalse"].AsBool());
            Assert.IsTrue(dataPoint["boolFalse"] == false);
            Assert.IsNull(dataPoint["boolFail"].AsBool());
        }
    }
}
