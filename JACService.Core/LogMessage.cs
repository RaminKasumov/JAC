﻿using System.Drawing;

namespace JACService.Core
{
    /// <summary>
    /// Represents a log message with a specific color
    /// </summary>
    public class LogMessage
    {
        #region properties
        /// <summary>
        /// The log message
        /// </summary>
        public string Message { get; }
        
        /// <summary>
        /// The color of the log message
        /// </summary>
        public Color Color { get; }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LogMessage"/> class
        /// </summary>
        /// <param name="message">The log message</param>
        /// <param name="color">The color of the log message</param>
        public LogMessage(string message, Color color)
        {
            Message = message;
            Color = color;
        }
        #endregion
        
        #region method
        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return Message;
        }
        #endregion
    }
}