﻿using System.Net;
using System.Net.Sockets;
using JACService.Core.Contracts;

namespace JACService.Core
{
    public class TcpService
    {
        #region instancevariables
        /// <summary>
        /// Instance variable for Socket of Server
        /// </summary>
        Socket _serverSocket = null!;

        /// <summary>
        /// Instance variable for Interface IServiceLogger
        /// </summary>
        readonly IServiceLogger _serviceLogger;
        #endregion
        
        #region properties
        /// <summary>
        /// Auto-property for EndPoint
        /// </summary>
        IPEndPoint EndPoint { get; set; } = null!;

        /// <summary>
        /// Auto-property for ClientHandler
        /// </summary>
        ClientManager ClientManager { get; set; } = null!;

        /// <summary>
        /// Auto-property to find out if the Server is online or not
        /// </summary>
        public bool Online { get; private set; }

        /// <summary>
        /// Auto-property for IP-Address
        /// </summary>
        public static string Ip => "127.0.0.1";

        /// <summary>
        /// Auto-property for Port
        /// </summary>
        public static int Port => 4711;
        #endregion

        #region constructor
        /// <summary>
        /// Instance variables are being initialized
        /// </summary>
        /// <param name="serviceLogger">ServiceLogger</param>
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
                _serviceLogger.LogServiceInfo($"[{DateTime.Now}] NOTIFICATION Server failed to start on {EndPoint.Address}:{EndPoint.Port}.");
            }
        }

        /// <summary>
        /// Server is being terminated
        /// </summary>
        public void Stop()
        {
            _serverSocket.Close();
            Online = false;
            _serviceLogger.LogServiceInfo($"[{DateTime.Now}] NOTIFICATION Server stopped.");
        }
        #endregion
    }
}