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
        #region Private Constants

        /// <summary>
        /// String that should be added to tracer output
        /// </summary>
        private const string TracerConstant = "AnnoucementListener";

        #endregion

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
        /// a custom port to listen to
        /// </summary>
        private readonly int _customPort;

        /// <summary>
        /// Lock for synchronous reading and writing of distant datapoints information
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// udp client object
        /// </summary>
        private UdpClient _client;

        /// <summary>
        /// is true if can listen to announcments
        /// </summary>
        private bool _isListening;

        #endregion

        #region Methods

        /// <summary>
        /// Constructor, allowing to set custom port number
        /// </summary>
        /// <param name="customPort"></param>
        internal AnnouncementListener(int customPort = 0)
        {
            _customPort = customPort;
        }

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
            _isListening = true;

            _listeningThread = new Thread(threadDelegate =>
            {
                while (_isListening)
                {
                    try
                    {
                        using (_client = new UdpClient())
                        {
                            var endpoint = _customPort != 0
                                ? new IPEndPoint(Pear.Configuration.BroadcastAddress, _customPort)
                                : new IPEndPoint(Pear.Configuration.BroadcastAddress,
                                    Pear.Configuration.BroadcastPortNumber);

                            _client.ExclusiveAddressUse = false;
                            _client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                            _client.Client.Bind(endpoint);
                            _client.Connect(endpoint);

                            var data = _client.Receive(ref endpoint);
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
                    catch (Exception exception)
                    {
                        Trace.WriteLine(string.Format("{0} listening to messages failed. Exception: {1}", TracerConstant, exception.Message));
                    }
                }

                Trace.WriteLine(string.Format("{0} End of Listening", TracerConstant));
            });

            _listeningThread.Start();
        }

        /// <summary>
        /// Aborts/stops listening theread
        /// </summary>
        internal void StopListening()
        {
            _isListening = false;

            try
            {
                if (_client != null && _client.Client != null)
                {
                    _client.Client.Shutdown(SocketShutdown.Both);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format(" abortion exception {0}", ex.Message));
                // It doesnot matter what happens, but the thread has to be aborted and application
                // should not die
            }
        }

        /// <summary>
        /// Clears list of distant endpoints and its datapoints
        /// </summary>
        internal void Clear()
        {
            lock (_lock)
            {
                _distantDataPoints.Clear();
            }
        }

        #endregion

    }
}
