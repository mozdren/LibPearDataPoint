using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LibPearDataPoint
{
    /// <summary>
    /// This class is the entry poind for data to be shared localy and over network
    /// </summary>
    public class Pear : IEnumerable<DataItem>
    {
        #region private fields

        /// <summary>
        /// Local Datapoint - this is an entry point for data that are stored localy
        /// </summary>
        private LocalDataPoint _localDataPoint = new LocalDataPoint();

        /// <summary>
        /// Basic Local Configuration - configuration stored in Configuration.Basic.xml
        /// </summary>
        private readonly Configuration _basicConfiguration = new Configuration("Configuration.xml");

        /// <summary>
        /// Service providing data for datapoint clients
        /// </summary>
        private DataPointService _dataPointService;

        /// <summary>
        /// announcer - announcing local data items
        /// </summary>
        private Announcer _announcer;

        /// <summary>
        /// Annoucment listener listens to annoucments of distant datapoints and keeps track of them
        /// </summary>
        private AnnouncementListener _announcementListener;

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
        public static Pear Data {
            get
            {
                if (_pearData._dataPointService == null)
                {
                    _pearData.Init();
                }
                return _pearData;
            }
        }

        /// <summary>
        /// count of dataitems stored localy
        /// </summary>
        public static int CountLocal { get { return Data._localDataPoint.Count(); } }

        #endregion

        #region Internal Properties

        /// <summary>
        /// This propertie should return a service port on which the data are provided.
        /// Eeach DataPoint should have only one service.
        /// </summary>
        internal static int ServicePort {
            get { return Data._dataPointService.ServicePort; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// There can be only one instance of this class
        /// and it has to initialize components in a specific order
        /// </summary>
        private Pear()
        {
        }

        /// <summary>
        /// Initialization of the internal modules
        /// </summary>
        internal void Init()
        {
            _dataPointService = new DataPointService(_localDataPoint);
            _announcer = new Announcer(_localDataPoint);
            _announcementListener = new AnnouncementListener();
            _announcer.StartAnnouncing();
            _dataPointService.StartService();
        }

        /// <summary>
        /// Deinitialization of the internal modules
        /// </summary>
        internal void Deinit()
        {
            try
            {
                // Stop all modules first
                if (_announcer != null)
                {
                    _announcer.StopAnnouncing();
                }

                if (_announcementListener != null)
                {
                    _announcementListener.StopListening();
                }

                if (_dataPointService != null)
                {
                    _dataPointService.StopService();
                }

                // Set all modules to null, so the GC can collect and next init works properly
                _announcer = null;
                _announcementListener = null;
                _dataPointService = null;
            }
            catch (KeyNotFoundException ex)
            {
                Trace.WriteLine(string.Format("Exception when shutting down modules: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Destructor - stops services
        /// </summary>
        ~Pear()
        {
            Deinit();
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
        /// Updates the value localy if the item is local, or sends a command to update the value in distant datapoint
        /// </summary>
        /// <typeparam name="T">generic type of the value to be set</typeparam>
        /// <param name="key">key value of the dataitem</param>
        /// <param name="value">value to be set</param>
        /// <returns>true if successful</returns>
        public bool Update<T>(string key, T value)
        {
            // check if the input values make sense
            if (string.IsNullOrWhiteSpace(key) || value == null)
            {
                return false;
            }

            // check if exists localy
            if (_localDataPoint[key] != null)
            {
                var dataItem = new DataItem { Name = key };
                dataItem.SetSupported(value);
                return _localDataPoint.Update(dataItem);
            }

            // TODO: if not exists localy, then check existence in distant endpoints

            return false;
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
                // exists localy?
                var localValue = _localDataPoint[key];
                if (localValue != null)
                {
                    return localValue;
                }

                // exists in distant datapoint?
                if (_announcementListener.GetNames().Contains(key))
                {
                    return 
                        _announcementListener
                        .GetEndpoints(key)
                        .Select(ipEndPoint => DataPointServiceClient.GetDataItem(ipEndPoint, key))
                        .FirstOrDefault(dataItem => dataItem != null);
                }

                return null;
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

        /// <summary>
        /// Returns enumerator enumerating trough local dataitems (external/network dataitems are not included)
        /// </summary>
        /// <returns>local dataitems enumerator</returns>
        public IEnumerator<DataItem> GetEnumerator()
        {
            return ((IEnumerable<DataItem>)_localDataPoint).GetEnumerator();
        }

        /// <summary>
        /// Returns enumerator enumerating trough local dataitems (external/network dataitems are not included)
        /// </summary>
        /// <returns>local dataitems enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<DataItem>)_localDataPoint).GetEnumerator();
        }
    }
}
