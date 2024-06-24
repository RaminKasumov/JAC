using System.Drawing;

namespace JAC.Service.Core
{
    public class LogMessage
    {
        #region properties
        /// <summary>
        /// Auto-property for Message
        /// </summary>
        public string Message { get; }
        
        /// <summary>
        /// Auto-property for Color of Message
        /// </summary>
        public Color Color { get; }
        #endregion

        #region constructor
        /// <summary>
        /// Constructor for LogMessage
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="color">Color of Message</param>
        public LogMessage(string message, Color color)
        {
            Message = message;
            Color = color;
        }
        #endregion
        
        #region method
        /// <summary>
        /// Overrides the ToString method to return the Message
        /// </summary>
        /// <returns>Returns the Message</returns>
        public override string ToString()
        {
            return Message;
        }
        #endregion
    }
}