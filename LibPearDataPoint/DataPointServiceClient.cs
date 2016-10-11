using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace LibPearDataPoint
{
    /// <summary>
    /// A static class providing a simple interface to datapoint service
    /// </summary>
    internal static class DataPointServiceClient
    {
        /// <summary>
        /// Gets dataitem from a service
        /// </summary>
        /// <param name="endpoint">endpoint from which the dataitem should be taken</param>
        /// <param name="name">name of the requested dataitem</param>
        /// <returns></returns>
        internal static DataItem GetDataItem(IPEndPoint endpoint, string name)
        {
            if(!Utils.IsNameValid(name))
            {
                // invalid name was requested
                Trace.WriteLine(string.Format("GetDataItem: Ivalid name {0}", name ?? "null"));
                return null;
            }

            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect(endpoint);
                    socket.NoDelay = true;

                    using (var networkStream = new NetworkStream(socket))
                    using (var streamWriter = new StreamWriter(networkStream))
                    using (var streamReader = new StreamReader(networkStream))
                    {
                        streamWriter.AutoFlush = true;
                        streamWriter.WriteLine("{0};{1}", GlobalConstants.Commands.Get, name);
                        var receivedData = streamReader.ReadLine();

                        if (string.IsNullOrWhiteSpace(receivedData))
                        {
                            Trace.WriteLine("received empty response");
                            return null;
                        }

                        if (receivedData.StartsWith("OK")) // ok message was received
                        {
                            var data = receivedData.Split(';');
                            if (data.Length < 3)
                            {
                                Trace.WriteLine(string.Format("Response format incorrect {0}", receivedData));
                                return null;
                            }

                            DateTime dateTime;
                            if (!DateTime.TryParse(data[1], out dateTime))
                            {
                                Trace.WriteLine(string.Format("Could not parse datetime from response: {0}", receivedData));
                                return null;
                            }

                            var strings = new List<string>();
                            for (var i = 2; i < data.Length; i++)
                            {
                                strings.Add(data[i]);
                            }

                            // if value consisted of semicollons, then it would be put back together after separation
                            var value = string.Join(";", data.Skip(2));

                            // complete dataitem is returned
                            return new DataItem
                            {
                                IsLocal = false,
                                IsReliable = true,
                                LastUpdateTime = dateTime,
                                Name = name,
                                Value = value
                            };
                        }

                        var errorMessageData = receivedData.Split(';');
                        if (errorMessageData.Length == 1)
                        {
                            Trace.WriteLine("Unknown Error when getting dataitem");
                            return null;
                        }

                        if (errorMessageData.Length == 2)
                        {
                            Trace.WriteLine(string.Format("Error when getting dataitem: {0}", errorMessageData[1]));
                            return null;
                        }

                        Trace.WriteLine(string.Format("Response format incorrect {0}", receivedData));
                        return null;
                    }
                }                
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception when getting item {0} from {1} (Exception: {2})", name ?? string.Empty, endpoint, ex.Message));
                return null;
            }
        }

        /// <summary>
        /// Updates distant dataitem
        /// </summary>
        /// <param name="endpoint">enpoint where the dataitem iterface service is provided</param>
        /// <param name="name">name of the dataitem to be updated</param>
        /// <param name="value">new value of the dataitem</param>
        /// <returns></returns>
        internal static bool UpdateDataItem(IPEndPoint endpoint, string name, string value)
        {
            if (!Utils.IsNameValid(name))
            {
                // invalid name was requested
                Trace.WriteLine(string.Format("UpdateDataItem: Ivalid name {0}", name ?? "null"));
                return false;
            }

            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect(endpoint);
                    socket.NoDelay = true;

                    using (var networkStream = new NetworkStream(socket))
                    using (var streamWriter = new StreamWriter(networkStream))
                    using (var streamReader = new StreamReader(networkStream))
                    {
                        streamWriter.AutoFlush = true;
                        streamWriter.WriteLine("{0};{1};{2}", GlobalConstants.Commands.Update, name, value);
                        var receivedData = streamReader.ReadLine();

                        if (string.IsNullOrWhiteSpace(receivedData))
                        {
                            Trace.WriteLine("received empty response");
                            return false;
                        }

                        if (receivedData.StartsWith("OK")) // ok message was received
                        {
                            return true;
                        }

                        var errorMessageData = receivedData.Split(';');
                        if (errorMessageData.Length <= 1)
                        {
                            Trace.WriteLine("Unknown Error when updating dataitem");
                            return false;
                        }

                        if (errorMessageData.Length == 2)
                        {
                            Trace.WriteLine(string.Format("Error when updating dataitem: {0}", errorMessageData[1]));
                            return false;
                        }

                        Trace.WriteLine(string.Format("Response format incorrect {0}", receivedData));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception when updating item {0} from {1} (Exception: {2})", name ?? string.Empty, endpoint, ex.Message));
                return false;
            }
        }

        /// <summary>
        /// This method informs the service, that there was an updata of subscribed data
        /// </summary>
        /// <param name="endpoint">enpoint where the dataitem iterface service is provided</param>
        /// <param name="data item name">updated dataitem name</param>
        /// <param name="data item value">updated dataitem value</param>
        /// <returns>true if update was successful</returns>
        internal static bool ChangeEvent(IPEndPoint endpoint, string name, string value)
        {
            if (!Utils.IsNameValid(name))
            {
                // invalid dataitem
                Trace.WriteLine(string.Format("UpdateDataItem: Ivalid name {0}", name ?? "null"));
                return false;
            }

            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect(endpoint);
                    socket.NoDelay = true;

                    using (var networkStream = new NetworkStream(socket))
                    using (var streamWriter = new StreamWriter(networkStream))
                    using (var streamReader = new StreamReader(networkStream))
                    {
                        streamWriter.AutoFlush = true;
                        streamWriter.WriteLine("{0};{1};{2}", GlobalConstants.Commands.ChangeEvent, name, value);
                        var receivedData = streamReader.ReadLine();

                        if (string.IsNullOrWhiteSpace(receivedData))
                        {
                            Trace.WriteLine("received empty response");
                            return false;
                        }

                        if (receivedData.StartsWith("OK")) // ok message was received
                        {
                            return true;
                        }

                        var errorMessageData = receivedData.Split(';');
                        if (errorMessageData.Length <= 1)
                        {
                            Trace.WriteLine("Unknown Error when informing about change of dataitem");
                            return false;
                        }

                        if (errorMessageData.Length == 2)
                        {
                            Trace.WriteLine(string.Format("Error when informing about change of dataitem: {0}", errorMessageData[1]));
                            return false;
                        }

                        Trace.WriteLine(string.Format("Response format incorrect {0}", receivedData));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception when informing about change of item {0} from {1} (Exception: {2})", name ?? string.Empty, endpoint, ex.Message));
                return false;
            }
        }

        /// <summary>
        /// Subscribe to dataitem in distant endpoint
        /// </summary>
        /// <param name="endpoint">dataitem origin</param>
        /// <param name="name">name of the dataitem</param>
        /// <returns>true if success</returns>
        internal static bool Subscribe(IPEndPoint endpoint, string name)
        {
            if (!Utils.IsNameValid(name))
            {
                // invalid dataitem
                Trace.WriteLine(string.Format("UpdateDataItem: Ivalid name {0}", name ?? "null"));
                return false;
            }

            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect(endpoint);
                    socket.NoDelay = true;

                    using (var networkStream = new NetworkStream(socket))
                    using (var streamWriter = new StreamWriter(networkStream))
                    using (var streamReader = new StreamReader(networkStream))
                    {
                        streamWriter.AutoFlush = true;
                        streamWriter.WriteLine("{0};{1};{2}", GlobalConstants.Commands.Subscribe, name, Pear.ServicePort);
                        var receivedData = streamReader.ReadLine();

                        if (string.IsNullOrWhiteSpace(receivedData))
                        {
                            Trace.WriteLine("received empty response");
                            return false;
                        }

                        if (receivedData.StartsWith("OK")) // ok message was received
                        {
                            return true;
                        }

                        var errorMessageData = receivedData.Split(';');
                        if (errorMessageData.Length <= 1)
                        {
                            Trace.WriteLine("Unknown Error when subscribing to dataitem");
                            return false;
                        }

                        if (errorMessageData.Length == 2)
                        {
                            Trace.WriteLine(string.Format("Error when subscribing to dataitem: {0}", errorMessageData[1]));
                            return false;
                        }

                        Trace.WriteLine(string.Format("Response format incorrect {0}", receivedData));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception when subscribing to item {0} from {1} (Exception: {2})", name ?? string.Empty, endpoint, ex.Message));
                return false;
            }
        }

        /// <summary>
        /// Pinging service - connectivity check
        /// </summary>
        /// <param name="endpoint">service endpoint</param>
        /// <returns>true if success</returns>
        internal static bool Ping(IPEndPoint endpoint)
        {
            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect(endpoint);
                    socket.NoDelay = true;

                    using (var networkStream = new NetworkStream(socket))
                    using (var streamWriter = new StreamWriter(networkStream))
                    using (var streamReader = new StreamReader(networkStream))
                    {
                        streamWriter.AutoFlush = true;
                        streamWriter.WriteLine("{0};0", GlobalConstants.Commands.Ping);
                        var receivedData = streamReader.ReadLine();

                        if (string.IsNullOrWhiteSpace(receivedData))
                        {
                            Trace.WriteLine("received empty response");
                            return false;
                        }

                        if (receivedData.StartsWith("OK")) // ok message was received
                        {
                            return true;
                        }

                        var errorMessageData = receivedData.Split(';');
                        if (errorMessageData.Length <= 1)
                        {
                            Trace.WriteLine("Unknown Error when pinging");
                            return false;
                        }

                        if (errorMessageData.Length == 2)
                        {
                            Trace.WriteLine(string.Format("Error when pinging: {0}", errorMessageData[1]));
                            return false;
                        }

                        Trace.WriteLine(string.Format("Response format incorrect {0}", receivedData));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception when pinging: {0}", ex.Message));
                return false;
            }
        }
    }
}
