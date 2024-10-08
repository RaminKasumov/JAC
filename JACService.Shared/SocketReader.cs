﻿using System.Net.Sockets;
using System.Text;

namespace JACService.Shared
{
    public class SocketReader
    {
        #region properties
        /// <summary>
        /// Auto-property for Socket of Client
        /// </summary>
        private Socket ClientSocket
        {
            get; set;
        }

        /// <summary>
        /// Auto-property for size of message
        /// </summary>
        private int BufferSize
        {
            get; set;
        }
        #endregion

        #region constructor
        /// <summary>
        /// Properties are being initialized
        /// </summary>
        /// <param name="clientSocket">Socket of Client</param>
        /// <param name="bufferSize">Size of message</param>
        public SocketReader(Socket clientSocket, int bufferSize)
        {
            ClientSocket = clientSocket;
            BufferSize = bufferSize;
        }
        #endregion

        #region method
        /// <summary>
        /// Message is being received from the Socket of Client
        /// </summary>
        /// <returns>Returns the received message</returns>
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