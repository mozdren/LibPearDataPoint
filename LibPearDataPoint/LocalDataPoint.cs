using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LibPearDataPoint
{
    internal class LocalDataPoint : IEnumerable<DataItem>
    {
        #region Private Constants

        /// <summary>
        /// A constant for tracer - tracer uses it in traces to identify orign of the traces
        /// </summary>
        private const string TracerConstant = "LocalDataPoint";

        #endregion

        #region Private Fields

        /// <summary>
        /// DataItem collection (Dictionary).
        /// </summary>
        private Dictionary<string, DataItem> _localDataItems = new Dictionary<string, DataItem>();

        /// <summary>
        /// This object is being used for thread safety (creating, updating, and removing should be atomic operations)
        /// </summary>
        private object _dataManipulationLock = new object();

        #endregion

        #region Main Methods

        /// <summary>
        /// Creates an item in a local dataset
        /// </summary>
        /// <param name="item">item with a name set</param>
        /// <returns>true when success</returns>
        internal bool Create(DataItem item)
        {
            lock (_dataManipulationLock)
            {
                // Validate input
                var workItem = GetValidatedDataItemClone(item);
                if (workItem == null)
                {
                    Trace.WriteLine(string.Format("{0} Could not create item, input data are not valid", TracerConstant));
                    return false;
                }

                // Cannot add item with the same identifier twice
                if (_localDataItems.Keys.Contains(workItem.Name))
                {
                    Trace.WriteLine(string.Format("{0} Could not create item, item already exists (Name: {1})", TracerConstant, item.Name));
                    return false;
                }

                // all seems to be OK, let's add our item (the clone)
                workItem.LastUpdateTime = DateTime.Now;
                workItem.IsLocal = true;
                workItem.IsReliable = true;
                _localDataItems[item.Name] = workItem;
                return true;
            }
        }

        /// <summary>
        /// Removes item from a local dataset
        /// </summary>
        /// <param name="item">item to be removed, identified by name</param>
        /// <returns>true if the item was successfuly removed</returns>
        internal bool Remove(DataItem item)
        {
            lock (_dataManipulationLock)
            {
                // Validate input
                var workItem = GetValidatedDataItemClone(item);
                if (workItem == null)
                {
                    Trace.WriteLine(string.Format("{0} Could not remove item, item is not valid (Name: {1})", TracerConstant, item.Name));
                    return false;
                }

                // Cannot remove non-existing item
                if (!_localDataItems.Keys.Contains(workItem.Name))
                {
                    Trace.WriteLine(string.Format("{0} Could not remove item, item does not exists (Name: {1})", TracerConstant, item.Name));
                    return false;
                }

                // Item exists, lets remove it
                _localDataItems.Remove(item.Name);
                return true;
            }
        }

        /// <summary>
        /// Updates an item in the dataset
        /// </summary>
        /// <param name="item">items new form</param>
        /// <returns>true if success</returns>
        internal bool Update(DataItem item)
        {
            lock (_dataManipulationLock)
            {
                // Validate input
                var workItem = GetValidatedDataItemClone(item);
                if (workItem == null)
                {
                    Trace.WriteLine(string.Format("{0} Could not update item, item is not valid (Name: {1})", TracerConstant, item.Name));
                    return false;
                }

                // Cannot update non-existing item
                if (!_localDataItems.Keys.Contains(workItem.Name))
                {
                    Trace.WriteLine(string.Format("{0} Could not update item, item doesn't exist (Name: {1})", TracerConstant, item.Name));
                    return false;
                }

                // all ok and ready, let's update the item
                workItem.LastUpdateTime = DateTime.Now;
                workItem.IsReliable = true;
                workItem.IsLocal = true;
                _localDataItems[workItem.Name] = workItem;
                return true;
            }
        }

        /// <summary>
        /// Provides enumerator over cloned elements of the data item collection
        /// </summary>
        /// <returns>local data item collection enumerator</returns>
        IEnumerator<DataItem> IEnumerable<DataItem>.GetEnumerator()
        {
            return _localDataItems.Values.Select(item => item.Clone() as DataItem).GetEnumerator();
        }

        /// <summary>
        /// Provides enumerator over cloned elements of the data item collection
        /// </summary>
        /// <returns>local data item collection enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _localDataItems.Values.Select(item => item.Clone() as DataItem).GetEnumerator();
        }

        #endregion

        #region Overloaded Operators
        
        /// <summary>
        /// Indexing items using the string key value as the index
        /// </summary>
        /// <param name="key">key identifier of the dataitem</param>
        /// <returns>Data item if found, null othervise</returns>
        internal DataItem this[string key]
        {
            get
            {
                lock (_dataManipulationLock)
                {
                    // Key should have value, and also should exist in _localDataItems
                    if (string.IsNullOrWhiteSpace(key) || !_localDataItems.ContainsKey(key))
                    {
                        return null;
                    }

                    // all OK, let's return the item
                    return _localDataItems[key].Clone() as DataItem;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Validates DataItem object and returns its clone if all mandatory conditions are fulfilled
        /// </summary>
        /// <param name="item">DataIitem instance to be cloned and validated</param>
        /// <returns>DataItem instance clone</returns>
        private static DataItem GetValidatedDataItemClone(DataItem item)
        {
            // providet prototype should exist
            if (item == null)
            {
                Trace.WriteLine(string.Format("{0} Validation Failed - item is null", TracerConstant));
                return null;
            }

            // it should be also of type DataItem so we can create a work clone
            var workItem = item.Clone() as DataItem;
            if (workItem == null)
            {
                // This basically should not happen, but stays in as a procausion for the case something changes in the code
                Trace.WriteLine(string.Format("{0} Validation Failed - item is not of type DataItem", TracerConstant));
                return null;
            }

            // Name is our identifier and therefore cannot be empty
            if (string.IsNullOrWhiteSpace(workItem.Name))
            {
                Trace.WriteLine(string.Format("{0} Validation Failed - item name is null or whitespace", TracerConstant));
                return null;
            }

            // Name is not of a defined format
            if (!Utils.IsNameValid(workItem.Name))
            {
                Trace.WriteLine(string.Format("{0} Validation Failed - item name is not of valid format", TracerConstant));
                return null;
            }

            // validated, let's return validated clone
            return workItem;
        }

        #endregion
    }
}
