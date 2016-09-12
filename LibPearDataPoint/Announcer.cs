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
        /// <summary>
        /// Thread to be used for announcing
        /// </summary>
        private Thread _announcingThread;

        /// <summary>
        /// Says if should be announcing or not
        /// </summary>
        private bool _isAnnouncing;

        /// <summary>
        /// Reference to datapoint with dataitems to be anounced in broadcast
        /// </summary>
        private LocalDataPoint DataPointToAnnounce { get; set; }

        /// <summary>
        /// Creating a instance of an announcer with respective LocalDatapoint
        /// </summary>
        /// <param name="datapoint"></param>
        internal Announcer(LocalDataPoint datapoint)
        {
            DataPointToAnnounce = datapoint;
            _isAnnouncing = false;
        }

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
                    Announce();
                    Thread.Sleep(1000); // should the time be configurable?
                }
            });
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
                    _announcingThread.Abort();
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
            var dataItemsToAnnounce = string.Join(";", DataPointToAnnounce.Select(item => item.Name));
            var endpointsToAnnounce = string.Join(";", Utils.GetLocalIpAddresses().Select(item => string.Format("{0}:{1}", item, Pear.ServicePort)));
            var dataToAnnounce = string.Format("{0}#{1}", endpointsToAnnounce, dataItemsToAnnounce);

            try
            {
                using (var udpClient = new UdpClient())
                {
                    var broadcastEndpoint = new IPEndPoint(Pear.Configuration.BroadcastAddress, Pear.Configuration.BroadcastPortNumber);
                    udpClient.Connect(broadcastEndpoint);
                    var bytes = Encoding.ASCII.GetBytes(dataToAnnounce);
                    udpClient.Send(bytes, bytes.Length);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(string.Format("Exception when announcing dataitems: {0}", exception.Message));
            }
        }
    }
}
