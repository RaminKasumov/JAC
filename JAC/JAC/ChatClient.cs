﻿using System.Net.Sockets;
using JACService.Shared;

namespace JAC
{
    /// <summary>
    /// Represents a client for handling chat operations
    /// </summary>
    public class ChatClient
    {
        #region properties
        /// <summary>
        /// The singleton instance of the <see cref="ChatClient"/> class
        /// </summary>
        static ChatClient Instance { get; } = new ChatClient();
        
        /// <summary>
        /// The instance of the <see cref="Socket"/> used for communication
        /// </summary>
        public Socket ClientSocket { get; private set; } = null!;

        /// <summary>
        /// The instance of the <see cref="SocketWriter"/> used for writing data to the socket
        /// </summary>
        public SocketWriter Writer { get; private set; } = null!;
        #endregion

        #region constructor
        /// <summary>
        /// The constructor is private to prevent the creation of an instance of <see cref="ChatClient"/>
        /// </summary>
        private ChatClient()
        {
            
        }
        #endregion
        
        #region methods
        /// <summary>
        /// Returns the singleton instance of the <see cref="ChatClient"/> class
        /// </summary>
        /// <returns>Returns the existing instance of <see cref="ChatClient"/></returns>
        public static ChatClient GetInstance()
        {
            return Instance;
        }
        
        /// <summary>
        /// Reinitializes the <see cref="Socket"/> and the <see cref="SocketWriter"/>
        /// </summary>
        public void Reinitialize()
        {
            if (ClientSocket is not { Connected: true })
            {
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Writer = new SocketWriter(ClientSocket);
            }
        }
        #endregion
    }
}