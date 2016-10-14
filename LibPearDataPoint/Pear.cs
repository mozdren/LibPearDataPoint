using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

namespace LibPearDataPoint
{
    /// <summary>
    /// This class is the entry poind for data to be shared localy and over network
    /// </summary>
    public class Pear : IEnumerable<DataItem>
    {
        #region Private Constants

        /// <summary>
        /// String that should be added to tracer output
        /// </summary>
        private const string TracerConstant = "Pear";

        #endregion

        #region Delegates and Events

        /// <summary>
        /// Public delegate describing interface of method that can process change of the dataitem
        /// </summary>
        /// <param name="changedDataItem">dataitem that was changed</param>
        public delegate void ProcessDataItemChangeDelegate(DataItem changedDataItem);

        /// <summary>
        /// An Event that shows that announces a change of dataitem
        /// </summary>
        public event ProcessDataItemChangeDelegate DataItemChanged;

        #endregion

        #region Fields and Properties

        /// <summary>
        /// Local Datapoint - this is an entry point for data that are stored localy
        /// </summary>
        internal LocalDataPoint PearLocalDataPoint = new LocalDataPoint();

        /// <summary>
        /// Basic Local Configuration - configuration stored in Configuration.Basic.xml
        /// </summary>
        internal readonly Configuration PearBasicConfiguration = new Configuration("Configuration.xml");

        /// <summary>
        /// Service providing data for datapoint clients
        /// </summary>
        internal DataPointService PearDataPointService;

        /// <summary>
        /// announcer - announcing local data items
        /// </summary>
        internal Announcer PearAnnouncer;

        /// <summary>
        /// Annoucment listener listens to annoucments of distant datapoints and keeps track of them
        /// </summary>
        internal AnnouncementListener PearAnnouncementListener;

        /// <summary>
        /// Subscriptions to dataitems
        /// </summary>
        internal Subscriptions PearSubscriptions;

        /// <summary>
        /// Singleton of a pear object
        /// </summary>
        internal static Pear PearData = new Pear();

        /// <summary>
        /// Basic configuration property
        /// </summary>
        public static Configuration Configuration { get { return Data.PearBasicConfiguration; } }

        /// <summary>
        /// A singleton getter - there is only one instance of this class in a whole application
        /// </summary>
        public static Pear Data {
            get
            {
                if (PearData.PearDataPointService == null)
                {
                    PearData.Init();
                }
                return PearData;
            }
        }

        /// <summary>
        /// Users do not have to create endpoints in some cases and just want to read data.
        /// This method is then used for initializing.
        /// </summary>
        public static void Start()
        {
            if (PearData.PearDataPointService == null)
            {
                PearData.Init();
            }
        }

        /// <summary>
        /// count of dataitems stored localy
        /// </summary>
        public static int CountLocal { get { return Data.PearLocalDataPoint.Count(); } }

        /// <summary>
        /// Total count of unique dataitems - local and discovered
        /// </summary>
        public static int CountTotal { get { return 0; } }

        /// <summary>
        /// This propertie should return a service port on which the data are provided.
        /// Eeach DataPoint should have only one service.
        /// </summary>
        internal static int ServicePort {
            get { return Data.PearDataPointService.ServicePort; }
        }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// There can be only one instance of this class
        /// and it has to initialize components in a specific order
        /// </summary>
        private Pear()
        {
        }

        /// <summary>
        /// Destructor - stops services
        /// </summary>
        ~Pear()
        {
            Deinit();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialization of the internal modules
        /// </summary>
        internal void Init()
        {
            PearSubscriptions = new Subscriptions(PearLocalDataPoint);
            PearDataPointService = new DataPointService(PearLocalDataPoint, PearSubscriptions);
            PearAnnouncer = new Announcer(PearLocalDataPoint);
            PearAnnouncementListener = new AnnouncementListener();
            PearDataPointService.DataItemChanged += OnDataItemChanged; // propagate changes of distant dataitems
            PearLocalDataPoint.DataItemChanged += OnDataItemChanged; // propagate changes of local dataitems
            PearAnnouncer.StartAnnouncing();
            PearDataPointService.StartService();
            PearAnnouncementListener.StartListening();
        }

        /// <summary>
        /// Reaction on change of dataitem, distant or local
        /// </summary>
        /// <param name="changedDataItem">changed dataitem</param>
        private void OnDataItemChanged(DataItem changedDataItem)
        {
            if (DataItemChanged != null)
            {
                DataItemChanged(changedDataItem);
            }
        }

        /// <summary>
        /// Deinitialization of the internal modules
        /// </summary>
        internal void Deinit()
        {
            try
            {
                // Stop all modules first
                if (PearAnnouncer != null)
                {
                    PearAnnouncer.StopAnnouncing();
                }

                if (PearAnnouncementListener != null)
                {
                    PearAnnouncementListener.StopListening();
                }

                if (PearDataPointService != null)
                {
                    PearDataPointService.StopService();
                }

                // Set all modules to null, so the GC can collect and next init works properly
                PearAnnouncer = null;
                PearAnnouncementListener = null;
                PearDataPointService = null;
                PearSubscriptions = null;
            }
            catch (KeyNotFoundException ex)
            {
                Trace.WriteLine(string.Format("{0} Exception when shutting down modules: {1}", TracerConstant, ex.Message));
            }
        }

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
            if (PearLocalDataPoint[key] != null)
            {
                return false;
            }

            // if someone already announced having item with specified key, then we cannot create new
            if (PearAnnouncementListener.GetNames().Contains(key))
            {
                return false;
            }

            // if it doesn't exist locally or even over network, then we are allowed to create data item in local datapoint
            var dataItem = new DataItem { Name = key };
            dataItem.SetSupported(value);

            return PearLocalDataPoint.Create(dataItem);
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
            if (PearLocalDataPoint[key] != null)
            {
                var dataItem = new DataItem { Name = key };
                dataItem.SetSupported(value);
                return PearLocalDataPoint.Update(dataItem);
            }

            // if distant data item exists, we have to update it
            if (PearAnnouncementListener.GetNames().Contains(key))
            {
                var dataItem = new DataItem {Name = key};
                dataItem.SetSupported(value);

                return PearAnnouncementListener
                       .GetEndpoints(key)
                       .Select(ipEndPoint => DataPointServiceClient.UpdateDataItem(ipEndPoint, key, dataItem.Value))
                       .FirstOrDefault(result => result);
            }

            return false;
        }

        public bool Subscribe(string name)
        {
            if (!Utils.IsNameValid(name))
            {
                return false;
            }

            var localData = PearLocalDataPoint[name];
            if (localData != null)
            {
                // local dataitems changes are subscribed automatically
                return true;
            }

            var endpoints = PearAnnouncementListener.GetEndpoints(name);
            if (endpoints == null)
            {
                // there is not such distant dataitem
                return false;
            }

            // With current implementation, each dataitem should be associated with ONE endpoint.
            var subscribed = false;
            foreach (var endpoint in endpoints)
            {
                if (DataPointServiceClient.Subscribe(endpoint, name)) subscribed = true;
            }

            return subscribed; // return true if at least one enpoint states that we are successfully subscribed
        }

        /// <summary>
        /// A synchronization method - waits for occurance of the dataitem with specific name
        /// </summary>
        /// <param name="name">name of the dataitem we are waiting for</param>
        /// <param name="timeoutSeconds">timeout in seconds</param>
        /// <returns>true if dataitem appears</returns>
        public bool WaitFor(string name, int timeoutSeconds = 0)
        {
            var startTime = DateTime.Now;

            while (Data[name] == null)
            {
                Thread.Sleep(50);
                if (timeoutSeconds != 0  && DateTime.Now - startTime > new TimeSpan(0, 0, timeoutSeconds))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// A synchronization method - waits for occurance of the dataitems with specific names
        /// </summary>
        /// <param name="names">names of dataitems we are waiting for</param>
        /// <param name="timeoutSeconds">timeout in seconds</param>
        /// <returns></returns>
        public bool WaitFor(string[] names, int timeoutSeconds = 0)
        {
            var startTime = DateTime.Now;

            while (names.Any(name => Data[name] == null))
            {
                Thread.Sleep(50);
                if (timeoutSeconds != 0 && DateTime.Now - startTime > new TimeSpan(0, 0, timeoutSeconds))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Waits for distant dataitem discovery with a specified name. If the name is not specied, then
        /// the method waits until it finds any distant dataitem.
        /// </summary>
        /// <param name="name">name of the specified distant dataitem</param>
        /// <param name="timeoutSeconds">timeout after which the method returns false</param>
        /// <returns>true if distant dataitem was discovered before timeout</returns>
        public bool WaitForDistant(string name = null, int timeoutSeconds = 0)
        {
            var uniqueDataPoints = new List<IPEndPoint>();
            var startTime = DateTime.Now;

            while (DateTime.Now - startTime < new TimeSpan(0, 0, timeoutSeconds))
            {
                var allNames = GetNames();
                var localNames = GetLocalNames();
                var distantNames = allNames.Where(n => !localNames.Contains(n));

                if (Utils.IsNameValid(name) && distantNames.Contains(name))
                {
                    return true;
                }
                else if (distantNames.Any())
                {
                    return true;
                }

                Thread.Sleep(50);
            }

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
                var localValue = PearLocalDataPoint[key];
                if (localValue != null)
                {
                    return localValue;
                }

                // exists in distant datapoint?
                if (PearAnnouncementListener.GetNames().Contains(key))
                {
                    return 
                        PearAnnouncementListener
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
            var item = PearLocalDataPoint[key];
            if (item == null)
            {
                return false;
            }

            return PearLocalDataPoint.Remove(item);
        }

        /// <summary>
        /// Clears collection of local datapoints and distant datapoints
        /// </summary>
        public void Clear()
        {
            PearLocalDataPoint.Clear();
            PearAnnouncementListener.Clear();
            PearSubscriptions.Clear();
            Data.DataItemChanged = null;
        }

        /// <summary>
        /// returns names of localy created dataitems
        /// </summary>
        /// <returns>collection of names</returns>
        public ICollection<string> GetLocalNames()
        {
            return PearLocalDataPoint.GetNames();
        }

        /// <summary>
        /// returns names of local and discovered datapoints
        /// </summary>
        /// <returns>collection of names</returns>
        public ICollection<string> GetNames()
        {
            var local = GetLocalNames();
            var distant = PearAnnouncementListener.GetNames();
            return distant.Where(item => !local.Contains(item)).Concat(local).ToArray();
        }

        /// <summary>
        /// Returns enumerator enumerating trough local dataitems (external/network dataitems are not included)
        /// </summary>
        /// <returns>local dataitems enumerator</returns>
        public IEnumerator<DataItem> GetEnumerator()
        {
            return ((IEnumerable<DataItem>)PearLocalDataPoint).GetEnumerator();
        }

        /// <summary>
        /// Returns enumerator enumerating trough local dataitems (external/network dataitems are not included)
        /// </summary>
        /// <returns>local dataitems enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<DataItem>)PearLocalDataPoint).GetEnumerator();
        }

        #endregion

    }
}
