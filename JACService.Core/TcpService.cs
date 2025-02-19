﻿using System.Net;
using System.Net.Sockets;
using JACService.Core.Contracts;

namespace JACService.Core
{
    /// <summary>
    /// Provides TCP server functionalities
    /// </summary>
    public class TcpService
    {
        #region instancevariables
        /// <summary>
        /// The socket for the server
        /// </summary>
        Socket _serverSocket = null!;

        /// <summary>
        /// The logger for service events
        /// </summary>
        readonly IServiceLogger _serviceLogger;
        #endregion
        
        #region properties
        /// <summary>
        /// The EndPoint for the server
        /// </summary>
        IPEndPoint EndPoint { get; set; } = null!;

        /// <summary>
        /// The manager for client connections
        /// </summary>
        ClientManager ClientManager { get; set; } = null!;

        /// <summary>
        /// Indicates whether the server is online
        /// </summary>
        public bool Online { get; private set; }

        /// <summary>
        /// The IP-address of the server
        /// </summary>
        public static string Ip => "127.0.0.1";

        /// <summary>
        /// The port number of the server
        /// </summary>
        public static int Port => 4711;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpService"/> class
        /// </summary>
        /// <param name="serviceLogger">The logger for service events</param>
        public TcpService(IServiceLogger serviceLogger)
        {
            _serviceLogger = serviceLogger;
        }
        #endregion

        #region methods
        /// <summary>
        /// Server is being started
        /// </summary>
        /// <param name="ip">IP-Address</param>
        /// <param name="port">Port</param>
        public void Start(string ip, int port)
        {
            try
            {
                if (!IPAddress.TryParse(ip, out IPAddress? ipAddress))
                {
                    ipAddress = IPAddress.Parse(Ip);
                }
                
                EndPoint = new IPEndPoint(ipAddress, port);
                
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientManager = new ClientManager(_serverSocket, _serviceLogger);
                
                _serverSocket.Bind(EndPoint);
                _serverSocket.Listen(20);
                
                _serviceLogger.LogServiceInfo($"[{DateTime.Now}] NOTIFICATION Server started on {_serverSocket.LocalEndPoint}.");
                
                Online = true;

                Thread thread = new Thread(ClientManager.AcceptClients)
                {
                    IsBackground = true
                };

                thread.Start();
            }
            catch (Exception)
            {
                _serviceLogger.LogServiceInfo($"[{DateTime.Now}] NOTIFICATION Server failed to start on {EndPoint?.Address}:{EndPoint?.Port}.");
            }
        }

        /// <summary>
        /// Server is being terminated
        /// </summary>
        public void Stop()
        {
            _serverSocket?.Close();
            Online = false;
            _serviceLogger.LogServiceInfo($"[{DateTime.Now}] NOTIFICATION Server stopped.");
        }
        #endregion
    }
}