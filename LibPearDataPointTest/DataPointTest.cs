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
    }
}
