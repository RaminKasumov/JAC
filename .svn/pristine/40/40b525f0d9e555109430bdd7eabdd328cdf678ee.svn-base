using System.Net.Sockets;
using System.Text;

namespace JAC.Shared
{
    public class SocketReader
    {
        #region properties
        /// <summary>
        /// Auto-property for Socket of Client
        /// </summary>
        public Socket ClientSocket
        {
            get; private set;
        }

        /// <summary>
        /// Auto-property for size of message
        /// </summary>
        public int BufferSize
        {
            get; private set;
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