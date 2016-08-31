using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace LibPearDataPoint
{
    internal partial class Utils
    {
        /// <summary>
        /// Initialization of Net component of utils
        /// </summary>
        internal static void InitNet()
        {
        }

        /// <summary>
        /// Getting local IP address
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<IPAddress> GetLocalIpAddresses()
        {
            try
            {
                var hostName = Dns.GetHostName();
                var ips = Dns.GetHostAddresses(hostName);
                return ips.Where(item => item.AddressFamily == AddressFamily.InterNetwork).ToArray();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception when getting Host IP addresses: {0}", ex.Message));
                return new List<IPAddress>();
            }
        }
    }
}
