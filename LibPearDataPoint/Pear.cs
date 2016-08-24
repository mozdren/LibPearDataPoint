using System;
using System.Linq;

namespace LibPearDataPoint
{
    /// <summary>
    /// This class is the entry poind for data to be shared localy and over network
    /// </summary>
    public class Pear
    {
        #region private fields

        /// <summary>
        /// Local Datapoint - this is an entry point for data that are stored localy
        /// </summary>
        private LocalDataPoint _localDataPoint = new LocalDataPoint();

        /// <summary>
        /// Basic Local Configuration - configuration stored in Configuration.Basic.xml
        /// </summary>
        private readonly Configuration _basicConfiguration = new Configuration("Configuration.Basic");

        /// <summary>
        /// Singleton of a pear object
        /// </summary>
        private static Pear _pearData = new Pear();

        #endregion

        #region Public Properties

        /// <summary>
        /// Basic configuration property
        /// </summary>
        public static Configuration Configuration { get { return Data._basicConfiguration; } }

        /// <summary>
        /// A singleton getter - there is only one instance of this class in a whole application
        /// </summary>
        public static Pear Data { get {return _pearData;} }

        /// <summary>
        /// count of dataitems stored localy
        /// </summary>
        public static int CountLocal { get { return Data._localDataPoint.Count(); } }

        #endregion

        #region Constructors
        
        /// <summary>
        /// There can be only one instance of this class
        /// </summary>
        private Pear()
        {
        }
        
        #endregion

        /// <summary>
        /// Create a datapoint with empty data value
        /// </summary>
        /// <param name="key">key as an identifier of the datapoint</param>
        /// <returns>true if successfuly created</returns>
        public bool Create(string key)
        {
            return Create(key, "");
        }

        /// <summary>
        /// Crate a datapoint with specific value
        /// </summary>
        /// <typeparam name="T">Type to be used</typeparam>
        /// <param name="key">key as an identifier of the dataitem</param>
        /// <param name="value">value to be set</param>
        /// <returns>return true if successfuly created</returns>
        public bool Create<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null)
            {
                return false;
            }
            
            // check if exists localy
            if (_localDataPoint[key] != null)
            {
                return false;
            }

            // TODO: do the same check for network value

            // if it doesn't exist locally or even over network, then we are allowed to create data item in local datapoint
            var dataItem = new DataItem { Name = key };
            dataItem.SetSupported(value);

            return _localDataPoint.Create(dataItem);
        }

        /// <summary>
        /// string indexation of datapoint
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>dataitem stored under specific key</returns>
        public DataItem this[string key]
        {
            get
            {
                var localValue = _localDataPoint[key];
                if (localValue != null)
                {
                    return localValue;
                }

                // TODO: after network datapoint is ready, check data also over network

                return null;
            }

            set
            {
                if (value == null || string.IsNullOrWhiteSpace(key)) 
                {
                    throw new InvalidOperationException("cannot set data item or key to null");
                }

                var valueType = value.GetType().ToString();
                var localValue = _localDataPoint[key];
                if (localValue != null)
                {
                    localValue.SetSupported(value);

                    if (!_localDataPoint.Update(localValue))
                    {
                        throw new Exception("Could not update datapoint");
                    }
                }
                else
                {
                    // TODO: check and eventually update network value
                }
            }
        }

        /// <summary>
        /// Removes a dataitem, but only from local datapoint. It is by design not possible
        /// to remove remote datapoints
        /// </summary>
        /// <param name="key">key with datapoint that should be removed</param>
        /// <returns>true if success</returns>
        public bool Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            // check if exists localy
            var item = _localDataPoint[key];
            if (item == null)
            {
                return false;
            }

            return _localDataPoint.Remove(item);
        }
    }
}
