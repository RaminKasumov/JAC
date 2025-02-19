﻿using System.Net.Sockets;
using JACService.Shared;
using JACService.Core.Contracts;

namespace JACService.Core
{
    /// <summary>
    /// Handles a session with a client
    /// </summary>
    public class SessionHandler
    {
        #region instancevariables
        /// <summary>
        /// The SocketReader instance
        /// </summary>
        readonly SocketReader _reader;
        
        /// <summary>
        /// The SocketWriter instance
        /// </summary>
        readonly SocketWriter _writer;
        
        /// <summary>
        /// The logger for service events
        /// </summary>
        readonly IServiceLogger _serviceLogger;
        #endregion

        #region constant
        /// <summary>
        /// The size of the message buffer
        /// </summary>
        const int BufferSize = 1024;
        #endregion
        
        #region properties
        /// <summary>
        /// The socket for the client
        /// </summary>
        public Socket ClientSocket { get; }
        
        /// <summary>
        /// The user in the chat
        /// </summary>
        public IUser ChatUser { get; private set; }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionHandler"/> class
        /// </summary>
        /// <param name="clientSocket">Socket of the client</param>
        /// <param name="serviceLogger">Logger for service events</param>
        public SessionHandler(Socket clientSocket, IServiceLogger serviceLogger)
        {
            ClientSocket = clientSocket;
            _serviceLogger = serviceLogger;
            _reader = new SocketReader(ClientSocket, BufferSize);
            _writer = new SocketWriter(ClientSocket);
            ChatUser = new ChatUser();
        }
        #endregion
        
        #region eventHandler
        /// <summary>
        /// Event for a closed session
        /// </summary>
        public event EventHandler? SessionClosed;
        #endregion

        #region methods
        /// <summary>
        /// Handles communication with the client
        /// </summary>
        public void HandleCommunication()
        {
            try
            {
                Login();
                
                while (true)
                {
                    string receivedText = _reader.ReceiveText();
                    _serviceLogger.LogRequestInfo($"[{DateTime.Now}] REQUEST Received request from {ClientSocket.RemoteEndPoint}: \"{receivedText}\".");

                    ITextRequest request = RequestHandlerFactory.CreateTextRequest(receivedText, this);
                    string response = request.GetResponse(receivedText);
                    
                    if (!SendText(response))
                    {
                        SendText("Error occured!");
                    }
                    
                    _serviceLogger.LogRequestInfo($"[{DateTime.Now}] RESPONSE Sending response to {ClientSocket.RemoteEndPoint}: \"{response}\".");
                    
                    if (response.StartsWith("Exit-success"))
                    {
                        Close();
                    }
                }
            }
            catch (Exception)
            {
                Close();
            }
        }

        /// <summary>
        /// Terminates the connection between the client and server
        /// </summary>
        public void Close()
        {
            if (ClientSocket.Connected)
            {
                _serviceLogger.LogServiceInfo($"[{DateTime.Now}] NOTIFICATION Client {ClientSocket.RemoteEndPoint} has been disconnected.");
                
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
            }
            
            OnSessionClosed();
        }
        
        /// <summary>
        /// Sends a message to the client
        /// </summary>
        /// <param name="text">Message to send</param>
        /// <returns>Returns true if the message has been sent, otherwise false</returns>
        public bool SendText(string text)
        {
            if (_writer.SendText(text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Logs in a user
        /// </summary>
        private void Login()
        {
            LoginRequestHandler.UserLoggedIn += OnUserLoggedIn;
            
            while (!ChatUser.IsLoggedIn)
            {
                string receivedText = _reader.ReceiveText();
                
                ITextRequest request = RequestHandlerFactory.CreateTextRequest(receivedText, this);
                string response = request.GetResponse(receivedText);
                
                SendText(response);
            }
        }

        /// <summary>
        /// Raises the SessionClosed event by invoking it, notifying subscribers about the session closure
        /// </summary>
        private void OnSessionClosed()
        {
            SessionClosed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the UserLoggedIn event
        /// </summary>
        /// <param name="user">User</param>
        private void OnUserLoggedIn(IUser user)
        {
            ChatUser = user;
        }
        #endregion
    }
}