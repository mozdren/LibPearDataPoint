using System.Collections.Generic;
using System.Linq;

namespace LibPearDataPoint
{
    /// <summary>
    /// Library Facade - Providing the interface for the library
    /// </summary>
    public static class Peer
    {
        /// <summary>
        /// Delegate for dataitem change events
        /// </summary>
        /// <param name="dataItem">dataitem that has been changed</param>
        public delegate void DataItemChangedDelegate(DataItem dataItem);

        /// <summary>
        /// Definition of a dataitem change evenet
        /// </summary>
        public static event DataItemChangedDelegate DataItemChanged;

        /// <summary>
        /// Static constructor
        /// </summary>
        static Peer()
        {
            Pear.Data.DataItemChanged += Subscribe;
        }

        /// <summary>
        /// Configuration of the Peer Application
        /// </summary>
        public static Configuration Configuration
        {
            get
            {
                return Pear.Configuration;
            }
        }

        /// <summary>
        /// Count of local dataitems
        /// </summary>
        public static int CountLocal
        {
            get
            {
                return Pear.CountLocal;
            }
        }

        /// <summary>
        /// Count of distant dataitems
        /// </summary>
        public static int CountDistant
        {
            get
            {
                return Pear.CountDistant;
            }
        }

        /// <summary>
        /// Count of local and distant dataitems combined
        /// </summary>
        public static int Count
        {
            get
            {
                return Pear.Count;
            }
        }

        /// <summary>
        /// Property returning all dataitems
        /// </summary>
        public static ICollection<DataItem> Items
        {
            get
            {
                return Pear.Data.GetAllDataItems();
            }
        }

        /// <summary>
        /// Property returning local dataitems only
        /// </summary>
        public static ICollection<DataItem> LocalItems
        {
            get
            {
                return Pear.Data.ToArray();
            }
        }

        /// <summary>
        /// Property returning distant dataitem
        /// </summary>
        public static ICollection<DataItem> DistantItems
        {
            get
            {
                return Pear.Data.GetDistantDataItems();
            }
        }

        /// <summary>
        /// Property returning local names
        /// </summary>
        public static ICollection<string> LocalNames
        {
            get
            {
                return Pear.Data.GetLocalNames();
            }
        }

        /// <summary>
        /// Property returning distant names
        /// </summary>
        public static ICollection<string> DistantNames
        {
            get
            {
                return Pear.Data.GetDistantNames();
            }
        }

        /// <summary>
        /// Prorperty returning all names (local and distant combined)
        /// </summary>
        public static ICollection<string> Names
        {
            get
            {
                return Pear.Data.GetNames();
            }
        }

        /// <summary>
        /// Initialization of Peer
        /// </summary>
        public static void Start()
        {
            Pear.Start();
        }

        /// <summary>
        /// Creates a new dataitem with a specific name
        /// </summary>
        /// <param name="name">name of the dataitem</param>
        /// <returns>true if success</returns>
        public static bool Create(string name)
        {
            return Pear.Data.Create(name);
        }

        /// <summary>
        /// Creates a new dataitem with specific name and value
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="name">Name of the dataitem</param>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static bool Create<T>(string name, T value)
        {
            return Pear.Data.Create(name, value);
        }

        /// <summary>
        /// Creates a new dataitem with specific name and array
        /// </summary>
        /// <typeparam name="T">Type of the array values</typeparam>
        /// <param name="name">Name of the dataitem</param>
        /// <param name="array">The array</param>
        /// <returns></returns>
        public static bool Create<T>(string name, T[] array)
        {
            return Pear.Data.Create(name, array);
        }

        /// <summary>
        /// Updates dataitem value
        /// </summary>
        /// <typeparam name="T">type of a value</typeparam>
        /// <param name="name">name of the dataitem</param>
        /// <param name="value">new value</param>
        /// <returns>true if success</returns>
        public static bool Update<T>(string name, T value)
        {
            return Pear.Data.Update(name, value);
        }

        /// <summary>
        /// Removed dataitem with specific name (restricted to local dataitems by design)
        /// </summary>
        /// <param name="name">name of the dataitem</param>
        /// <returns>true if successfuly removed</returns>
        public static bool Remove(string name)
        {
            return Pear.Data.Remove(name);
        }

        /// <summary>
        /// Gets dataitem with specified name
        /// </summary>
        /// <param name="name">name of the dataitem to be retrieved</param>
        /// <returns>specific dataitem or null on error</returns>
        public static DataItem Get(string name)
        {
            return Pear.Data[name];
        }

        /// <summary>
        /// Subscribes to a specific dataitem (no need to subscribe to a loca dataitem)
        /// </summary>
        /// <param name="name">name of the dataitem we want to subscribe to</param>
        /// <returns>true if successfuly subscribed</returns>
        public static bool Subscribe(string name)
        {
            return Pear.Data.Subscribe(name);
        }

        /// <summary>
        /// Waits for occurance of a specific dataitem
        /// </summary>
        /// <param name="name">name of dataitem we should wait for</param>
        /// <returns>true if found</returns>
        public static bool WaitFor(string name)
        {
            return Pear.Data.WaitFor(name);
        }

        /// <summary>
        /// Waits for occurance of a specific dataitem
        /// </summary>
        /// <param name="name">name of dataitem we should wait for</param>
        /// <param name="timeout">timeout in seconds</param>
        /// <returns>true if found</returns>
        public static bool WaitFor(string name, int timeout)
        {
            return Pear.Data.WaitFor(name, timeout);
        }

        /// <summary>
        /// Waits for occurance of a specific dataitems
        /// </summary>
        /// <param name="names">names of dataitems we should wait for</param>
        /// <returns>true if found</returns>
        public static bool WaitFor(string[] names)
        {
            return Pear.Data.WaitFor(names);
        }

        /// <summary>
        /// Waits for occurance of a specific dataitems
        /// </summary>
        /// <param name="names">names of dataitems we should wait for</param>
        /// <param name="timeout">timeout in seconds</param>
        /// <returns>true if found</returns>
        public static bool WaitFor(string[] names, int timeout)
        {
            return Pear.Data.WaitFor(names, timeout);
        }

        /// <summary>
        /// Waits for occurance of any distant dataitem
        /// </summary>
        /// <returns>true if found</returns>
        public static bool WaitForDistant()
        {
            return Pear.Data.WaitForDistant();
        }

        /// <summary>
        /// Waits for occurance of any distant dataitem with specific timeout
        /// </summary>
        /// <returns>true if found</returns>
        public static bool WaitForDistant(int timeout)
        {
            return Pear.Data.WaitForDistant(timeoutSeconds: timeout);
        }

        /// <summary>
        /// Waits for occurance of a specific distant dataitem
        /// </summary>
        /// <param name="name">name of dataitem we should wait for</param>
        /// <returns>true if found</returns>
        public static bool WaitForDistant(string name)
        {
            return Pear.Data.WaitForDistant(name);
        }

        /// <summary>
        /// Waits for occurance of a specific distant dataitem with timeout
        /// </summary>
        /// <param name="name">name of dataitem we should wait for</param>
        /// <param name="timeout">timeout in seconds</param>
        /// <returns>true if found</returns>
        public static bool WaitForDistant(string name, int timeout)
        {
            return Pear.Data.WaitForDistant(name, timeout);
        }

        /// <summary>
        /// Waits for occurance of a specific distant dataitems
        /// </summary>
        /// <param name="name">names of dataitems we should wait for</param>
        /// <param name="timeout">timeout in seconds</param>
        /// <returns>true if found</returns>
        public static bool WaitForDistant(string[] names)
        {
            return Pear.Data.WaitForDistant(names);
        }

        /// <summary>
        /// Waits for occurance of specific distant dataitems with timeout
        /// </summary>
        /// <param name="names">names of dataitems we should wait for</param>
        /// <param name="timeout">timeout in seconds</param>
        /// <returns>true if found</returns>
        public static bool WaitForDistant(string[] names, int timeout)
        {
            return Pear.Data.WaitForDistant(names, timeout);
        }

        /// <summary>
        /// Clears local Peer
        /// </summary>
        public static void Clear()
        {
            Pear.Data.Clear();
        }

        /// <summary>
        /// Private method used to pass subscriptions
        /// </summary>
        /// <param name="dataItem"></param>
        private static void Subscribe(DataItem dataItem)
        {
            if (DataItemChanged != null)
            {
                DataItemChanged(dataItem);
            }
        }
    }
}
