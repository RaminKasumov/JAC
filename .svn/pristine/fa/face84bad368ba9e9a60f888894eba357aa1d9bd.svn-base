using System;
using System.Net.Sockets;
using JAC.Shared;

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
        #endregion

        #region constant
        /// <summary>
        /// Constant for size of message
        /// </summary>
        const int BufferSize = 1024;
        #endregion
        
        #region property
        /// <summary>
        /// Auto-property for Socket of Client
        /// </summary>
        public Socket ClientSocket
        {
            get; private set;
        }
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
                while (true)
                {
                    string receivedText = _reader.ReceiveText();

                    _serviceLogger.LogRequestInfo($"[{DateTime.Now.ToShortTimeString()}] Received request from {ClientSocket.RemoteEndPoint}: \"{receivedText}\".");

                    RequestHandler requestHandler = new RequestHandler();
                    string response = requestHandler.GetResponse(receivedText);
                    
                    if (!_writer.SendText(response))
                    {
                        _writer.SendText("Error occured!");
                    }
                    
                    _serviceLogger.LogServiceInfo($"[{DateTime.Now.ToShortTimeString()}] Sending response to {ClientSocket.RemoteEndPoint}: \"{response}\".");
                    
                    if (receivedText == "exit")
                    {
                        Close();
                    }
                    else
                    {
                        HandleCommunication();
                    }
                }
            }
            catch (Exception)
            {
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
                _serviceLogger.LogServiceInfo($"[{DateTime.Now.ToShortTimeString()}] Client {ClientSocket.RemoteEndPoint} has been disconnected.");
                
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
            }
            
            OnSessionClosed();
        }
        
        /// <summary>
        /// Raises the SessionClosed event by invoking it, notifying subscribers about the session closure
        /// </summary>
        protected virtual void OnSessionClosed()
        {
            SessionClosed?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}