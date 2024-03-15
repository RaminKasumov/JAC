using System;
using System.Net.Sockets;
using JAC.Shared;

namespace JAC.Service.Core
{
    public class ChatClient
    {
        #region instancevariable
        /// <summary>
        /// Buffer size for the Socket
        /// </summary>
        readonly int _bufferSize = 1024;
        #endregion
        
        #region properties
        /// <summary>
        /// Property for the instance of ChatClient
        /// </summary>
        static ChatClient Instance { get; } = new ChatClient();
        
        /// <summary>
        /// Auto-property for the instance of Socket
        /// </summary>
        public Socket ClientSocket { get; }
        
        /// <summary>
        /// Auto-property for the instance of SocketReader
        /// </summary>
        public SocketReader Reader { get; }

        /// <summary>
        /// Auto-property for the instance of SocketWriter
        /// </summary>
        public SocketWriter Writer { get; }
        #endregion

        #region constructor
        /// <summary>
        /// Constructor is private to prevent the creation of an instance of ChatClient
        /// </summary>
        private ChatClient()
        {
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Reader = new SocketReader(ClientSocket, _bufferSize);
            Writer = new SocketWriter(ClientSocket);
        }
        #endregion
        
        #region method
        /// <summary>
        /// Returns the instance of ChatClient
        /// </summary>
        /// <returns>Returns the existing instance</returns>
        public static ChatClient GetInstance()
        {
            return Instance;
        }
        #endregion
    }
}