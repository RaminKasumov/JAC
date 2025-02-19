﻿using System.Net.Sockets;
using System.Text;

namespace JACService.Shared
{
    /// <summary>
    /// Writes data to a network socket
    /// </summary>
    public class SocketWriter
    {
        #region property
        /// <summary>
        /// The socket used to send message
        /// </summary>
        private Socket ClientSocket
        {
            get; set;
        }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SocketWriter"/> class
        /// </summary>
        /// <param name="clientSocket">The socket used to send message</param>
        public SocketWriter(Socket clientSocket)
        {
            ClientSocket = clientSocket;
        }
        #endregion

        #region method
        /// <summary>
        /// Sends the message to the socket
        /// </summary>
        /// <param name="text">The message to send</param>
        /// <returns>Returns true if the message was sent successfully, otherwise false</returns>
        public bool SendText(string text)
        {
            try
            {
                byte[] message = Encoding.ASCII.GetBytes(text);
                ClientSocket.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }
        #endregion
    }
}