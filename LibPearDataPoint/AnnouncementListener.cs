using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LibPearDataPoint
{
    /// <summary>
    /// Class is intended for listening of broadcasts that announce distant datapoints and their endpoints
    /// </summary>
    internal class AnnouncementListener
    {
        #region private fields

        /// <summary>
        /// Distant datapoints and the endpoints under which they are serviced
        /// </summary>
        private readonly Dictionary<string, List<IPEndPoint>> _distantDataPoints = new Dictionary<string, List<IPEndPoint>>();

        /// <summary>
        /// Thread used for listening
        /// </summary>
        private Thread _listeningThread;

        /// <summary>
        /// Lock for synchronous reading and writing of distant datapoints information
        /// </summary>
        private readonly object _lock = new object();

        #endregion

        /// <summary>
        /// returns datapoints for a specific datapoint name
        /// </summary>
        /// <param name="dataItemName"></param>
        /// <returns></returns>
        internal IEnumerable<IPEndPoint> GetEndpoints(string dataItemName)
        {
            lock (_lock)
            {
                if (string.IsNullOrWhiteSpace(dataItemName))
                {
                    return null;
                }

                if (_distantDataPoints.ContainsKey(dataItemName))
                {
                    return _distantDataPoints[dataItemName];
                }

                return null;
            }
        }

        /// <summary>
        /// returns discovered names
        /// </summary>
        /// <returns></returns>
        internal ICollection<string> GetNames()
        {
            return _distantDataPoints.Keys;
        }

        /// <summary>
        /// Starts listening thread
        /// </summary>
        internal void StartListening()
        {

            _listeningThread = new Thread(threadDelegate =>
            {
                using (var client = new UdpClient(Pear.Configuration.BroadcastPortNumber))
                {
                    var endpoint = new IPEndPoint(IPAddress.Any, Pear.Configuration.BroadcastPortNumber);

                    while (true)
                    {
                        // get the data from broadcast using async methods with timeout
                        var dataAsincResult = client.BeginReceive(null, null);
                        dataAsincResult.AsyncWaitHandle.WaitOne(1000);
                        if (!dataAsincResult.IsCompleted) // has the operation completed?
                        {
                            continue;
                        }

                        // result received
                        var data = client.EndReceive(dataAsincResult, ref endpoint);
                        var messageString = Encoding.ASCII.GetString(data);

                        lock (_lock)
                        {
                            if (string.IsNullOrWhiteSpace(messageString))
                            {
                                continue;
                            }

                            var splittedData = messageString.Split('#');
                            if (splittedData.Length != 2)
                            {
                                continue;
                            }

                            List<IPEndPoint> endpoints = new List<IPEndPoint>();

                            // parse endpoints
                            foreach (var endpointData in from endpointString 
                                     in splittedData[0].Split(';')
                                     where !string.IsNullOrWhiteSpace(endpointString)
                                     select endpointString.Split(':') 
                                     into endpointData
                                     where endpointData.Length == 2 &&
                                           !string.IsNullOrWhiteSpace(endpointData[0]) &&
                                           !string.IsNullOrWhiteSpace(endpointData[1])
                                     select endpointData)
                            {
                                IPAddress ipAddress;
                                int port;

                                if (IPAddress.TryParse(endpointData[0], out ipAddress) &&
                                    int.TryParse(endpointData[1], out port))
                                {
                                    endpoints.Add(new IPEndPoint(ipAddress, port));
                                }
                            }

                            var annoucedItems = splittedData[1].Split(';');
                            foreach (var item in annoucedItems)
                            {
                                if (!_distantDataPoints.ContainsKey(item))
                                {
                                    
                                    _distantDataPoints.Add(item, endpoints); // new dataitem discovered
                                }
                                else
                                {
                                    _distantDataPoints[item] = endpoints; // new location of dataitem discovered
                                }
                            }
                        }
                    }
                }
                // ReSharper disable once FunctionNeverReturns
            });

            _listeningThread.Start();
        }

        /// <summary>
        /// Aborts/stops listening theread
        /// </summary>
        internal void StopListening()
        {
            try
            {
                _listeningThread.Abort();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Thread abortion exception {0}", ex.Message));
                // It doesnot matter what happens, but the thread has to be aborted and application
                // should not die
            }
        } 
    }
}
