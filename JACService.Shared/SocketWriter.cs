using System;
using System.Net.Sockets;
using System.Text;

namespace JAC.Shared
{
    public class SocketWriter
    {
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
        /// Property for Socket of Client is being initialized
        /// </summary>
        /// <param name="clientSocket">Socket of Client</param>
        public SocketWriter(Socket clientSocket)
        {
            ClientSocket = clientSocket;
        }
        #endregion

        #region method
        /// <summary>
        /// Message is being sent to the Socket of Client
        /// </summary>
        /// <param name="text">Message</param>
        /// <returns>Returns true if the message was sent successfully, otherwise false</returns>
        public bool SendText(string text)
        {
            try
            {
                byte[] message = Encoding.ASCII.GetBytes(text);
                ClientSocket.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}