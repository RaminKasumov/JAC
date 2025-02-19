﻿namespace JACService.Shared
{
    /// <summary>
    /// Provides data for the MessageReceived event
    /// </summary>
    public class MessageReceivedEventArgs : EventArgs
    {
        #region property
        /// <summary>
        /// The message that was received
        /// </summary>
        public string Message { get; }
        #endregion
        
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageReceivedEventArgs"/> class
        /// </summary>
        /// <param name="message">The message that was received</param>
        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }
        #endregion
    }
}