﻿using System.Net.Sockets;
using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core
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
        readonly MessageDispatcher _dispatcher;
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
            _dispatcher = new MessageDispatcher(_sessions, _serviceLogger);
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
                    
                    //Registers OnChatMessageReceived method for ChatMessageReceived event
                    ChatMessageStorage chatMessageStorage = ChatMessageStorage.GetInstance();
                    chatMessageStorage.ChatMessageReceived += OnChatMessageReceived;
                    
                    //Registers OnSessionClosed method for SessionClosed event
                    session.SessionClosed += OnSessionClosed;
                    
                    _sessions.Add(session);
                    
                    _serviceLogger.LogServiceInfo($"[{DateTime.Now}] NOTIFICATION A new client connected from: {session.ClientSocket.RemoteEndPoint}");
                
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
                
                foreach (SessionHandler session in sessionsCopy)
                {
                    session.Close();
                }
            }
        }

        /// <summary>
        /// Handles the SessionClosed event by removing the closed session, updating client count and logging the information on the console
        /// </summary>
        /// <param name="sender">Triggering Session</param>
        /// <param name="e">Event arguments</param>
        private void OnSessionClosed(object? sender, EventArgs e)
        {
            SessionHandler session = (SessionHandler)sender!;
            _sessions.Remove(session);
            
            _serviceLogger.LogServiceInfo($"[{DateTime.Now}] NOTIFICATION Amount of the clients: {_sessions.Count}");
        }
        
        /// <summary>
        /// Handles the ChatMessageReceived event
        /// </summary>
        /// <param name="sender">Triggering Event</param>
        /// <param name="e">Event arguments</param>
        private void OnChatMessageReceived(object? sender, ChatMessageReceivedEventArgs e)
        {
            if (!e.Message.IsPrivate)
            {
                _dispatcher.Broadcast(e.Message);
            }
            else
            {
                _dispatcher.Whisper(e.Message);
            }
        }
        #endregion
    }
}