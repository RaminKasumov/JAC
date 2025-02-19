﻿using System.Net.Sockets;

namespace JACService.Shared
{
    /// <summary>
    /// Handles the reception of messages over a network socket
    /// </summary>
    public class MessageReceiver
    {
        #region instancevariable
        /// <summary>
        /// The SocketReader instance
        /// </summary>
        readonly SocketReader _reader;
        #endregion
        
        #region property
        /// <summary>
        /// Indicates whether the receiver is currently receiving messages or not
        /// </summary>
        private bool IsReceiving { get; set; }
        #endregion
        
        #region constant
        /// <summary>
        /// Size of the buffer for receiving messages
        /// </summary>
        const int BufferSize = 1024;
        #endregion

        #region eventHandler
        /// <summary>
        /// Event triggered when a message is received
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs>? MessageReceived;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageReceiver"/> class
        /// </summary>
        /// <param name="socket">The socket to receive messages from</param>
        public MessageReceiver(Socket socket)
        {
            _reader = new SocketReader(socket, BufferSize);
        }
        #endregion

        #region methods
        /// <summary>
        /// Starts the receiving of messages
        /// </summary>
        public void Start()
        {
            if (!IsReceiving)
            {
                IsReceiving = true;

                Thread thread = new Thread(ReceiveMessages)
                {
                    IsBackground = true
                };
                thread.Start();
            }
        }

        /// <summary>
        /// Stops the receiving of messages
        /// </summary>
        public void Stop()
        {
            IsReceiving = false;
        }

        /// <summary>
        /// Receives messages and raises the MessageReceived event
        /// </summary>
        private void ReceiveMessages()
        {
            while (IsReceiving)
            {
                try
                {
                    string receivedMessage = _reader.ReceiveText();
                    OnMessageReceived(receivedMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Raises the MessageReceived event by invoking it, notifying subscribers about the received message
        /// </summary>
        /// <param name="message">The received message</param>
        private void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
        }
        #endregion
    }
}