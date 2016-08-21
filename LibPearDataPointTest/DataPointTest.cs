using System;
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
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "longMax", Value = long.MaxValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "longValue", Value = 1985.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "longMin", Value = long.MinValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "longFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "ulongMax", Value = ulong.MaxValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "ulongValue", Value = 1985.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "ulongMin", Value = ulong.MinValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "ulongFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "intMax", Value = int.MaxValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "intValue", Value = 1985.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "intMin", Value = int.MinValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "intFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "uintMax", Value = uint.MaxValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "uintValue", Value = 1985.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "uintMin", Value = uint.MinValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "uintFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "shortMax", Value = short.MaxValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "shortValue", Value = 1985.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "shortMin", Value = short.MinValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "shortFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "ushortMax", Value = ushort.MaxValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "ushortValue", Value = 1985.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "ushortMin", Value = ushort.MinValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "ushortFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "charMax", Value = char.MaxValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "charValue", Value = 'u'.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "charMin", Value = char.MinValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "charFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "byteMax", Value = byte.MaxValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "byteValue", Value = 11.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "byteMin", Value = byte.MinValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "byteFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "sbyteMax", Value = sbyte.MaxValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "sbyteValue", Value = 11.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "sbyteMin", Value = sbyte.MinValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "sbyteFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "floatMax", Value = float.MaxValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "floatValue", Value = 11.11.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "floatMin", Value = float.MinValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "floatFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "doubleMax", Value = double.MaxValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "doubleValue", Value = 11.11.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "doubleMin", Value = double.MinValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "doubleFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "decimalMax", Value = decimal.MaxValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "decimalValue", Value = 11.11.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "decimalMin", Value = decimal.MinValue.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "decimalFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "boolTrue", Value = true.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "boolFalse", Value = false.ToString() }));
            Assert.IsTrue(dataPoint.Create(new DataItem { Name = "boolFail", Value = "asd" }));

            Assert.IsTrue(dataPoint.Count() == 51);

            // TODO: Check extended methods for casting
        }
    }
}
