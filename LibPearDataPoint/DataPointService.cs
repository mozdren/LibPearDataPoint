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
    /// DataPointService is a service that should run over every DataPoint and provide data to client (Pear) DataPoints
    /// </summary>
    internal class DataPointService
    {
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
        private LocalDataPoint _localDataPoint;

        /// <summary>
        /// Property showing which port is being used to run the service
        /// </summary>
        internal int ServicePort { get; private set; }

        /// <summary>
        /// A constructor that expects the datapoint instance as a parameter
        /// </summary>
        /// <param name="localDataPoint">local datapoint</param>
        internal DataPointService(LocalDataPoint localDataPoint)
        {
            _localDataPoint = localDataPoint;
        }

        /// <summary>
        /// Starting service with a specific port, or selects port according to configuration
        /// </summary>
        /// <param name="port">port number to be used for service, or 0 if range should be used</param>
        internal void StartService(int port = 0)
        {
            _serviceSocket = GetServiceSocket(port);

            if (_serviceSocket == null)
            {
                Trace.WriteLine("Could not create service socket. Aborting!");
                return;
            }

            _serviceThread = new Thread(threadCode =>
            {
                while (true)
                {
                    try
                    {
                        var clientSocket = _serviceSocket.Accept();
                        new Thread(ProcessServiceClient).Start(clientSocket);
                    }
                    catch (Exception ex)
                    {
                        // We log the exception, but we continue accept connection no matter what
                        Trace.WriteLine(string.Format("Exception when listening to clients (ex: {0})", ex.Message));
                    }
                }
                // ReSharper disable once FunctionNeverReturns
            });

            _serviceThread.Start();
        }

        /// <summary>
        /// Stops the service
        /// </summary>
        internal void StopService()
        {
            try
            {
                _serviceSocket.Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Closing service socket exception (ex: {0})", ex.Message));
            }
            
            try
            {
                _serviceThread.Abort();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Could not abort service thread (ex: {0})", ex.Message));
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
                    var newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    newSocket.NoDelay = true;
                    var endPoint = new IPEndPoint(new IPAddress(new byte[] { 0, 0, 0, 0 }), port);
                    newSocket.Bind(endPoint);
                    newSocket.Listen(100);
                    ServicePort = port;
                    return newSocket;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("Could not create Socket. The port is probably already occupied by another application, or you don't have rights to create the port (PORT:{0}, exception: {1})", port, ex.Message));
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
                    Trace.WriteLine(string.Format("Could not create Socket. The port is probably already occupied by another application, or you don't have rights to create the port (PORT:{0}, exception: {1})", newPortNumber, ex.Message));
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
        /// Processing request of a client (GET, SET)
        /// </summary>
        /// <param name="clientSocketObject"></param>
        private void ProcessServiceClient(object clientSocketObject)
        {
            var clientSocket = clientSocketObject as Socket;
            if (clientSocket == null)
            {
                Trace.WriteLine("Client Processing Thread did get Socket object as a parameter");
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
                    Trace.WriteLine(string.Format("Request did not have correct format (request:{0})", requestCommand));
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
                        Trace.WriteLine(string.Format("Unknown command was requested (Request: {0})", requestCommand));
                        return;
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
        private void ProcessGet(StreamWriter writer, string name)
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
        private void ProcessUpdate(StreamWriter writer, IEnumerable<string> parameters)
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
    }
}
