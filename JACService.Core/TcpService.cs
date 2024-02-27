using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace JAC.Service.Core
{
    public class TcpService
    {
        #region instancevariables
        /// <summary>
        /// Instance variable for Port of Server
        /// </summary>
        readonly int _port;
        
        /// <summary>
        /// Instance variable for Socket of Server
        /// </summary>
        Socket _serverSocket;

        /// <summary>
        /// Instance variable for Interface IServiceLogger
        /// </summary>
        readonly IServiceLogger _serviceLogger;
        #endregion
        
        #region properties
        /// <summary>
        /// Auto-property for EndPoint
        /// </summary>
        public IPEndPoint EndPoint
        {
            get; private set;
        }
        
        /// <summary>
        /// Auto-property for ClientHandler
        /// </summary>
        public ClientManager ClientManager
        {
            get; private set;
        }

        /// <summary>
        /// Auto-property to find out if the Server is online or not
        /// </summary>
        public bool Online
        {
            get; private set;
        }
        #endregion

        #region constructor
        /// <summary>
        /// Instance variables are being initialized
        /// </summary>
        /// <param name="port">Port of Server</param>
        /// <param name="serviceLogger">ServiceLogger</param>
        public TcpService(int port, IServiceLogger serviceLogger)
        {
            _port = port;
            _serviceLogger = serviceLogger;
        }
        #endregion

        #region methods
        /// <summary>
        /// Server is being started
        /// </summary>
        /// <param name="backLog">Maximum queue length for pending connections</param>
        public void Start(int backLog)
        {
            EndPoint = new IPEndPoint(IPAddress.Loopback, _port);
            
            try
            {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientManager = new ClientManager(_serverSocket, _serviceLogger);
                _serverSocket.Bind(EndPoint);
                _serverSocket.Listen(backLog);
                Online = true;
                _serviceLogger.LogServiceInfo($"[{DateTime.Now.ToShortTimeString()}] Server started on {EndPoint.Address}:{EndPoint.Port}.");

                ThreadStart threadStart = new ThreadStart(ClientManager.AcceptClients);
                Thread thread = new Thread(threadStart);
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception)
            {
                _serviceLogger.LogServiceInfo($"[{DateTime.Now.ToShortTimeString()}] Server failed to start on {EndPoint.Address}:{EndPoint.Port}.");
            }
        }

        /// <summary>
        /// Server is being terminated
        /// </summary>
        public void Stop()
        {
            _serverSocket.Close();
            Online = false;
            _serviceLogger.LogServiceInfo($"[{DateTime.Now.ToShortTimeString()}] Server stopped.");
        }
        #endregion
    }
}