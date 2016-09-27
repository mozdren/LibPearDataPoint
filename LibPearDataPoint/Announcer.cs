using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LibPearDataPoint
{
    /// <summary>
    /// This class is a announcer which announces dataitems of a datapoint together with the ipaddress and port under
    /// which is the datapoint presented/provided using broadcasting.
    /// </summary>
    internal class Announcer
    {
        #region Private Constants

        /// <summary>
        /// String that should be added to tracer output
        /// </summary>
        private const string TracerConstant = "Announcer";

        #endregion

        #region Fields and Properties

        /// <summary>
        /// Thread to be used for announcing
        /// </summary>
        private Thread _announcingThread;

        /// <summary>
        /// Says if should be announcing or not
        /// </summary>
        private bool _isAnnouncing;

        /// <summary>
        /// Custom port for announcing
        /// </summary>
        private readonly int _customPort;

        /// <summary>
        /// UdpClient field
        /// </summary>
        private UdpClient _client;

        /// <summary>
        /// Randomizer for random announcing times
        /// </summary>
        private Random _random = new Random();

        /// <summary>
        /// Reference to datapoint with dataitems to be anounced in broadcast
        /// </summary>
        private LocalDataPoint DataPointToAnnounce { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creating a instance of an announcer with respective LocalDatapoint
        /// </summary>
        /// <param name="datapoint">datapoint to be announced</param>
        /// <param name="customPort">custom port to be used for announcing</param>
        internal Announcer(LocalDataPoint datapoint, int customPort = 0)
        {
            DataPointToAnnounce = datapoint;
            _isAnnouncing = false;
            _customPort = customPort;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts announcing
        /// </summary>
        internal void StartAnnouncing()
        {
            _isAnnouncing = true;
            _announcingThread = new Thread(threadCode =>
            {
                while (_isAnnouncing)
                {
                    try
                    {
                        Thread.Sleep(200 + _random.Next(200));
                        Announce();
                    }
                    catch (Exception exception)
                    {
                        Trace.WriteLine(string.Format("{0} announcing failed. Exception: {1}", TracerConstant, exception.Message));
                        // we do not want it to stop working, just try again
                    }
                }

                Trace.WriteLine(string.Format("{0} End of Announcing", TracerConstant));
            });

            _announcingThread.Start();
        }

        /// <summary>
        /// Stops announcing
        /// </summary>
        internal void StopAnnouncing()
        {
            _isAnnouncing = false;

            try
            {
                if (_announcingThread != null)
                {
                    if (_client != null && _client.Client != null)
                    {
                        _client.Client.Shutdown(SocketShutdown.Both);
                    }
                }
            }catch(Exception ex){
                // the application should fail because we were not able to abort the thread
                // just trace the problem and continue.
                Trace.WriteLine(string.Format("Exception when aborting the announcing thread. Exception: {0}", ex));
            }
        }

        /// <summary>
        /// Send announce message (example: "127.0.0.1:1234;127.0.0.1:1234#dataitem1;dataitem2;dataitem3")
        /// </summary>
        internal void Announce()
        {
            try
            {
                var itemNames = DataPointToAnnounce.Select(item => item.Name).Where(item => !string.IsNullOrWhiteSpace(item)).ToArray();

                if (!itemNames.Any()) return; // nothing to announce

                var dataItemsToAnnounce = string.Join(";", itemNames);
                var endpointsToAnnounce = string.Join(";", Utils.GetLocalIpAddresses().Select(item => string.Format("{0}:{1}", item, Pear.ServicePort)));
                var dataToAnnounce = string.Format("{0}#{1}", endpointsToAnnounce, dataItemsToAnnounce);


                using (_client = new UdpClient())
                {
                    _client.ExclusiveAddressUse = false;
                    _client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    var broadcastEndpoint = new IPEndPoint(
                        Pear.Configuration.BroadcastAddress, 
                        _customPort != 0 
                            ? _customPort 
                            : Pear.Configuration.BroadcastPortNumber);
                    _client.Client.Bind(broadcastEndpoint);
                    _client.Connect(broadcastEndpoint);
                    var bytes = Encoding.ASCII.GetBytes(dataToAnnounce);
                    _client.Send(bytes, bytes.Length);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(string.Format("{0} Exception when announcing dataitems: {1}", TracerConstant,
                    exception.Message));
            }
        }

        #endregion
    }
}
