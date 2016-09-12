﻿using System;
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
                Trace.WriteLine(string.Format("Exception when getting item {0} from {1} (Exception: {2})", name ?? string.Empty, endpoint, ex));
                return null;
            }
        }
    }
}