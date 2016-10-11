using System.Collections.Generic;
using System.Net;
using System.Diagnostics;
using System;

namespace LibPearDataPoint
{
    internal class Subscriptions
    {
        #region Fields and Properties

        /// <summary>
        /// Local datapoints providing change events
        /// </summary>
        private LocalDataPoint _localDataPoint { get; set; }

        /// <summary>
        /// Subscribed endpoints
        /// </summary>
        private Dictionary<string, List<IPEndPoint>> _subscriptions;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for a subscription object with reference to a localDataPoint object
        /// </summary>
        /// <param name="localDataPoints">reference to local dataitems</param>
        internal Subscriptions(LocalDataPoint localDataPoints)
        {
            _localDataPoint = localDataPoints;
            _subscriptions = new Dictionary<string, List<IPEndPoint>>();
            _localDataPoint.DataItemChanged += OnDataDataItemChange;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Local datapoint event processing method (calls subscribed endpoint using DataPointService)
        /// </summary>
        /// <param name="dataItem"></param>
        internal void OnDataDataItemChange(DataItem dataItem)
        {
            if (dataItem == null || string.IsNullOrWhiteSpace(dataItem.Name))
            {
                // if dataitem is null, or the name is invalid
                return;
            }

            if (_subscriptions.ContainsKey(dataItem.Name))
            {
                var endpoints = _subscriptions[dataItem.Name];

                foreach (var endpoint in endpoints)
                {
                    if (!DataPointServiceClient.ChangeEvent(endpoint, dataItem.Name, dataItem.Value))
                    {
                        Trace.WriteLine(string.Format("Unable to inform about the change of dataitem {0} on endpoint {1}", dataItem.Name, endpoint));
                    }
                }
            }
        }

        /// <summary>
        /// Method for subscription to dataitems
        /// </summary>
        /// <param name="endpoint">endpoint where to announce the change</param>
        /// <param name="name">name of dataitem</param>
        internal void Subscribe(IPEndPoint endpoint, string name)
        {
            if (!_subscriptions.ContainsKey(name))
            {
                _subscriptions.Add(name, new List<IPEndPoint>());
            }

            var subsList = _subscriptions[name];

            if (endpoint != null)
            {
                subsList.Add(endpoint);
            }
        }

        /// <summary>
        /// Clears subscriptions
        /// </summary>
        internal void Clear()
        {
            _subscriptions.Clear();
        }

        #endregion
    }
}
