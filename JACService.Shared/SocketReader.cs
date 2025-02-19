﻿using System.Net.Sockets;
using System.Text;

namespace JACService.Shared
{
    /// <summary>
    /// Reads data from a network socket
    /// </summary>
    public class SocketReader
    {
        #region properties
        /// <summary>
        /// The socket used to receive message
        /// </summary>
        private Socket ClientSocket
        {
            get; set;
        }

        /// <summary>
        /// The size of the buffer for receiving message
        /// </summary>
        private int BufferSize
        {
            get; set;
        }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SocketReader"/> class
        /// </summary>
        /// <param name="clientSocket">The socket used to receive message</param>
        /// <param name="bufferSize">The size of the buffer for receiving message</param>
        public SocketReader(Socket clientSocket, int bufferSize)
        {
            ClientSocket = clientSocket;
            BufferSize = bufferSize;
        }
        #endregion

        #region method
        /// <summary>
        /// Receives message from the socket
        /// </summary>
        /// <returns>The received message</returns>
        public string ReceiveText()
        {
            var message = new byte[BufferSize];
            ClientSocket.Receive(message);
            string text = Encoding.ASCII.GetString(message);
            text = text.Trim('\0');
            //text = text.Substring(0, text.IndexOf('\0'));
            return text;
        }
        #endregion
    }
}