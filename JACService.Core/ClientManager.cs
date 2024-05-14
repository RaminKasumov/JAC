using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class ClientManager
    {
        #region instancevariables
        /// <summary>
        /// Instance variable for Socket of Server
        /// </summary>
        readonly Socket _serverSocket;
        
        /// <summary>
        /// List of connections with Client(s)
        /// </summary>
        readonly List<SessionHandler> _sessions;

        /// <summary>
        /// Instance variable for Interface IServiceLogger
        /// </summary>
        readonly IServiceLogger _serviceLogger;
        
        /// <summary>
        /// Instance variable for MessageDispatcher
        /// </summary>
        readonly MessageDispatcher _messageDispatcher;
        #endregion

        #region constructor
        /// <summary>
        /// Instance variables are being initialized
        /// </summary>
        /// <param name="serverSocket">Socket of Server</param>
        /// <param name="serviceLogger">ServiceLogger</param>
        public ClientManager(Socket serverSocket, IServiceLogger serviceLogger)
        {
            _serverSocket = serverSocket;
            _serviceLogger = serviceLogger;
            _sessions = new List<SessionHandler>();

            _messageDispatcher = new MessageDispatcher(_sessions, _serviceLogger);
        }
        #endregion

        #region methods
        /// <summary>
        /// Creates a new Socket for a created connection
        /// </summary>
        public void AcceptClients()
        {
            try
            {
                while (true)
                {
                    Socket clientSocket = _serverSocket.Accept();
                    SessionHandler session = new SessionHandler(clientSocket, _serviceLogger);
                    
                    //Register OnSessionClosed method for SessionClosed event
                    session.SessionClosed += OnSessionClosed;
                    
                    _sessions.Add(session);
                    
                    _serviceLogger.LogServiceInfo($"[{DateTime.Now.ToShortTimeString()}] A new client connected from: {session.ClientSocket.RemoteEndPoint}");
                
                    Thread thread = new Thread(session.HandleCommunication)
                    {
                        IsBackground = true
                    };
                    thread.Start();
                }
            }
            catch (Exception)
            {
                List<SessionHandler> sessionsCopy = _sessions.ToList();

                for (int i = 0; i < sessionsCopy.Count; i++)
                {
                    sessionsCopy[i].Close();
                }
            }
        }

        /// <summary>
        /// Handles the SessionClosed event by removing the closed session, updating client count and logging the information on the console
        /// </summary>
        /// <param name="sender">Triggering Session</param>
        /// <param name="e">Event arguments</param>
        private void OnSessionClosed(object sender, EventArgs e)
        {
            SessionHandler session = (SessionHandler)sender;
            _sessions.Remove(session);
            
            _serviceLogger.LogServiceInfo($"[{DateTime.Now.ToShortTimeString()}] Amount of the clients: {_sessions.Count}");
        }
        #endregion
    }
}