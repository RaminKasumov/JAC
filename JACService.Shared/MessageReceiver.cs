﻿using System;
using System.Net.Sockets;
using System.Threading;

namespace JAC.Shared
{
    public class MessageReceiver
    {
        #region instancevariables
        /// <summary>
        /// Instance variable for Socket
        /// </summary>
        readonly Socket _socket;

        /// <summary>
        /// Instance variable for SocketReader
        /// </summary>
        readonly SocketReader _reader;
        #endregion
        
        #region property
        /// <summary>
        /// Auto-property to find out if the recipient is receiving messages
        /// </summary>
        public bool IsReceiving { get; private set; }
        #endregion
        
        #region constant
        /// <summary>
        /// Constant for size of message
        /// </summary>
        const int BufferSize = 1024;
        #endregion

        #region eventHandler
        /// <summary>
        /// EventHandler for MessageReceived
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        #endregion

        #region constructor
        /// <summary>
        /// Instance variables are being initialized
        /// </summary>
        /// <param name="socket">Socket</param>
        public MessageReceiver(Socket socket)
        {
            _socket = socket;
            _reader = new SocketReader(_socket, BufferSize);
        }
        #endregion

        #region methods
        /// <summary>
        /// Starts the receiving of messages
        /// </summary>
        public void Start()
        {
            IsReceiving = true;
            
            ThreadStart threadStart = new ThreadStart(ReceiveMessages);
            Thread thread = new Thread(threadStart)
            {
                IsBackground = true
            };
            thread.Start();
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
            try
            {
                string receivedMessage = _reader.ReceiveText();
            
                OnMessageReceived(receivedMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Raises the MessageReceived event by invoking it, notifying subscribers about the received message
        /// </summary>
        /// <param name="message">Message</param>
        protected virtual void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
        }
        #endregion
    }
}