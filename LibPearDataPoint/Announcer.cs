using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Text;

namespace LibPearDataPoint
{
    /// <summary>
    /// This class is a announcer which announces dataitems of a datapoint together with the ipaddress and port under
    /// which is the datapoint presented/provided using broadcasting.
    /// </summary>
    internal class Announcer
    {
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
        }

        /// <summary>
        /// Send announce message
        /// </summary>
        internal void Announce()
        {
            var dataItemsToAnnounce = string.Join(";", DataPointToAnnounce.Select(item => item.Name));
            var endpointsToAnnounce = string.Join(";", Utils.GetLocalIpAddresses().Select(item => string.Format("{0}:{1}", item, Pear.ServicePort)));
            var dataToAnnounce = string.Format("{0}#{1}", endpointsToAnnounce, dataItemsToAnnounce);

            using (var udpClient = new UdpClient())
            {
                var broadcastEndpoint = new IPEndPoint(Pear.Configuration.BroadcastAddress, Pear.Configuration.BroadcastPortNumber);
                udpClient.Connect(broadcastEndpoint);
                var bytes = Encoding.ASCII.GetBytes(dataToAnnounce);
                udpClient.Send(bytes, bytes.Length);
            }
        }
    }
}
