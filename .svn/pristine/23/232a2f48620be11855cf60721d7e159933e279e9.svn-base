using System;
using System.Net.Sockets;
using JAC.Shared;
using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class SessionHandler
    {
        #region instancevariables
        /// <summary>
        /// Instance variable for SocketReader
        /// </summary>
        readonly SocketReader _reader;
        
        /// <summary>
        /// Instance variable for SocketWriter
        /// </summary>
        readonly SocketWriter _writer;
        
        /// <summary>
        /// Instance variable for Interface IServiceLogger
        /// </summary>
        readonly IServiceLogger _serviceLogger;
        
        /// <summary>
        /// Instance variable for ChatServiceDirectory
        /// </summary>
        readonly ChatDirectory _directory = ChatDirectory.GetInstance();
        #endregion

        #region constant
        /// <summary>
        /// Constant for size of message
        /// </summary>
        const int BufferSize = 1024;
        #endregion
        
        #region properties
        /// <summary>
        /// Auto-property for Socket of Client
        /// </summary>
        public Socket ClientSocket { get; }
        
        /// <summary>
        /// Auto-property for ChatUser
        /// </summary>
        public IUser ChatUser { get; private set; }
        #endregion

        #region constructor
        /// <summary>
        /// Instance variables are being initialized
        /// </summary>
        /// <param name="clientSocket">Socket of Client</param>
        /// <param name="serviceLogger">ServiceLogger</param>
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
        /// Event for a closed Session
        /// </summary>
        public event EventHandler SessionClosed;
        #endregion

        #region methods
        /// <summary>
        /// Server handles communication with Client
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

                    if (response.StartsWith("Channel"))
                    {
                        string[] splitter = response.Split(' ');
                        ChatUser.CurrentChannel = new Channel(splitter[1]);
                    }
                    
                    _serviceLogger.LogRequestInfo($"[{DateTime.Now}] RESPONSE Sending response to {ClientSocket.RemoteEndPoint}: \"{response}\".");
                    
                    if (receivedText == "exit")
                    {
                        _directory.RemoveUser(ChatUser);
                        Close();
                    }
                }
            }
            catch (Exception)
            {
                _directory.RemoveUser(ChatUser);
                Close();
            }
        }

        /// <summary>
        /// Connection between Client and Server is being terminated
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
        /// Server sends a message to Client
        /// </summary>
        /// <param name="text">Message</param>
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
        /// Server logs in a User
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