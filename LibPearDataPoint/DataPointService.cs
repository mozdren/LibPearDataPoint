using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LibPearDataPoint
{
    /// <summary>
    /// PearDataPointService is a service that should run over every DataPoint and provide data to client (Pear) DataPoints
    /// </summary>
    internal class DataPointService
    {
        #region Private Constants

        /// <summary>
        /// String that should be added to tracer output
        /// </summary>
        private const string TracerConstant = "DataPointService";

        #endregion

        #region Fields and Properties

        /// <summary>
        /// Randomizer
        /// </summary>
        private readonly Random _random = new Random();

        /// <summary>
        /// Thread running the service (should run separatelly from main thread)
        /// </summary>
        private Thread _serviceThread;

        /// <summary>
        /// service socket - the service listens on this socket
        /// </summary>
        private Socket _serviceSocket;

        /// <summary>
        /// A datapoint for which the data are provided
        /// </summary>
        private readonly LocalDataPoint _localDataPoint;

        /// <summary>
        /// is true if service accepts new connections
        /// </summary>
        private bool _isAccepting;

        /// <summary>
        /// Property showing which port is being used to run the service
        /// </summary>
        internal int ServicePort { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// A constructor that expects the datapoint instance as a parameter
        /// </summary>
        /// <param name="localDataPoint">local datapoint</param>
        internal DataPointService(LocalDataPoint localDataPoint)
        {
            _localDataPoint = localDataPoint;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starting service with a specific port, or selects port according to configuration
        /// </summary>
        /// <param name="port">port number to be used for service, or 0 if range should be used</param>
        internal void StartService(int port = 0)
        {
            _isAccepting = true;
            _serviceSocket = GetServiceSocket(port);

            if (_serviceSocket == null)
            {
                Trace.WriteLine(string.Format("{0} Could not create service socket. Aborting!", TracerConstant));
                return;
            }

            _serviceThread = new Thread(threadCode =>
            {
                while (_isAccepting)
                {
                    try
                    {
                        var asyncResult = _serviceSocket.BeginAccept(ProcessServiceClient, null);
                        asyncResult.AsyncWaitHandle.WaitOne(1000);
                    }
                    catch (Exception ex)
                    {
                        // We log the exception, but we continue accept connection no matter what
                        Trace.WriteLine(string.Format("{0} Exception when listening to clients (ex: {1})", TracerConstant, ex.Message));
                    }
                }

                Trace.WriteLine(string.Format("{0} End of Service", TracerConstant));
            });

            _serviceThread.Name = "DataPoint Service Thread";
            _serviceThread.Start();
        }

        /// <summary>
        /// Stops the service
        /// </summary>
        internal void StopService()
        {
            _isAccepting = false;

            try
            {
                if (_serviceSocket != null && _serviceSocket.Connected)
                {
                    _serviceSocket.Shutdown(SocketShutdown.Both);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("{0} Closing service socket exception (ex: {1})", TracerConstant, ex.Message));
            }
        }

        /// <summary>
        /// Returns socket with unique (free) port number on current system
        /// </summary>
        /// <param name="port">specific port or random is selected if 0</param>
        /// <returns></returns>
        private Socket GetServiceSocket(int port = 0)
        {
            if (port != 0)
            {
                try
                {
                    var endPoint = new IPEndPoint(new IPAddress(new byte[] { 0, 0, 0, 0 }), port);
                    var newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    newSocket.NoDelay = true;
                    newSocket.Bind(endPoint);
                    newSocket.Listen(100);
                    ServicePort = port;
                    return newSocket;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("{0} Could not create Socket. The port is probably already occupied by another application, or you don't have rights to create the port (PORT:{1}, exception: {2})", TracerConstant, port, ex.Message));
                    return null;
                }
            }

            var maxPortsSize = Pear.Configuration.MaxPortNumber - Pear.Configuration.MinPortNumber;
            var ocupiedPorts = new List<int> {0}; // zero is definetely not allowed

            // Find vacant port number. Random access works fast with big range of ports
            // and small number of datapoints on one machine - most common situation.
            for (var i = 0; i < maxPortsSize; i++)
            {
                var newPortNumber = 0;
                while (newPortNumber == 0 || ocupiedPorts.Contains(newPortNumber))
                {
                    newPortNumber = _random.Next(Pear.Configuration.MinPortNumber, Pear.Configuration.MaxPortNumber);
                }

                Socket newSocket = null;

                try
                {
                    newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    newSocket.NoDelay = true;
                    var endPoint = new IPEndPoint(new IPAddress(new byte[] {0, 0, 0, 0}), newPortNumber);
                    newSocket.Bind(endPoint);
                    newSocket.Listen(100);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("{0} Could not create Socket. The port is probably already occupied by another application, or you don't have rights to create the port (PORT:{1}, exception: {2})", TracerConstant, newPortNumber, ex.Message));
                    ocupiedPorts.Add(newPortNumber);
                }

                if (newSocket != null)
                {
                    ServicePort = newPortNumber;
                    return newSocket;
                }
            }

            // we were not able to find free port, this shouldn't happen to often...
            return null;
        }

        /// <summary>
        /// Processing request of a client (GET, SET, etc.)
        /// </summary>
        /// <param name="clientSocketAsyncResult">asynchronous result from socket client connection</param>
        private void ProcessServiceClient(IAsyncResult clientSocketAsyncResult)
        {
            if (clientSocketAsyncResult.IsCompleted)
            {
                using (var clientSocket = _serviceSocket.EndAccept(clientSocketAsyncResult))
                {
                    if (clientSocket == null)
                    {
                        Trace.WriteLine(string.Format("{0} Client Processing Thread did not get Socket object as a parameter", TracerConstant));
                        return;
                    }

                    using (var networkStream = new NetworkStream(clientSocket))
                    using (var reader = new StreamReader(networkStream))
                    using (var writer = new StreamWriter(networkStream))
                    {
                        writer.AutoFlush = true;
                        var requestCommand = reader.ReadLine();

                        if (string.IsNullOrWhiteSpace(requestCommand))
                        {
                            Trace.WriteLine("Empty request message was received");
                            WriteErrorResponse(writer, "request command was empty");
                            return;
                        }

                        var splittedCommand = requestCommand.Split(';');
                        if (splittedCommand.Length < 2)
                        {
                            WriteErrorResponse(writer, "request did not have correct format");
                            Trace.WriteLine(string.Format("{0} Request did not have correct format (request:{1})", TracerConstant, requestCommand));
                            return;
                        }

                        switch (splittedCommand[0].Trim())
                        {
                            case GlobalConstants.Commands.Get:
                                ProcessGet(writer, splittedCommand[1]);
                                break;
                            case GlobalConstants.Commands.Update:
                                ProcessUpdate(writer, splittedCommand.Skip(1));
                                break;
                            default:
                                WriteUnknownCommandResponse(writer);
                                Trace.WriteLine(string.Format("{0} Unknown command was requested (Request: {1})", TracerConstant, requestCommand));
                                return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Writes an error message back to client
        /// </summary>
        /// <param name="writer">stream writer</param>
        /// <param name="message">error message</param>
        private void WriteErrorResponse(StreamWriter writer, string message)
        {
            writer.WriteLine("NOK;{0}", message);
        }

        /// <summary>
        /// Writes an unknown command error message to client
        /// </summary>
        /// <param name="writer">stream writer</param>
        private void WriteUnknownCommandResponse(StreamWriter writer)
        {
            writer.WriteLine("NOK;unknown command sent");
        }

        /// <summary>
        /// Writes response to Get request from client (requested dataitem or error message)
        /// </summary>
        /// <param name="writer">stream writer</param>
        /// <param name="name">data item name</param>
        private void ProcessGet(TextWriter writer, string name)
        {
            var item = _localDataPoint[name];
            if (item == null) // item was not found, and we return error
            {
                writer.WriteLine("NOK;Item does not exist");
                return;
            }

            writer.WriteLine("OK;{0};{1}", item.LastUpdateTime.ToString("O"), item.Value);
        }

        /// <summary>
        /// Sets data item to a specified value, or retrns error message
        /// </summary>
        /// <param name="writer">stream writer</param>
        /// <param name="parameters">parameters</param>
        private void ProcessUpdate(TextWriter writer, IEnumerable<string> parameters)
        {
            // enought parameters?
            var updateParams = parameters.ToArray();
            if (updateParams.Length < 2)
            {
                writer.WriteLine("NOK;wrong parameters");
            }

            // is name valid?
            var name = updateParams[0];
            if (!Utils.IsNameValid(name))
            {
                writer.WriteLine("NOK;Invalid name");
            }

            // does local dataitem exist
            var dataItem = _localDataPoint[name];
            if (dataItem == null)
            {
                writer.WriteLine("NOK;dataitem does not exist");
            }

            // get data to be set
            var data = string.Join(";", updateParams.Skip(1));
            dataItem.Set(data);

            // try to update value
            if (_localDataPoint.Update(dataItem))
            {
                writer.WriteLine("OK;item updated");
                return;
            }

            // could not update, return error
            writer.WriteLine("NOK;item update failed");
        }

        #endregion

    }
}
