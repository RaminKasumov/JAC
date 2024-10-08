﻿namespace JACService.Shared
{
    public class MessageReceivedEventArgs : EventArgs
    {
        #region property
        /// <summary>
        /// Property for Message
        /// </summary>
        public string Message { get; }
        #endregion
        
        #region constructor
        /// <summary>
        /// Instance variables are being initialized
        /// </summary>
        /// <param name="message">Message</param>
        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }
        #endregion
    }
}