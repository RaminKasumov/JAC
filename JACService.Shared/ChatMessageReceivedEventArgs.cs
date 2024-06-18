﻿using System;

namespace JAC.Shared
{
    public class ChatMessageReceivedEventArgs : EventArgs
    {
        #region property
        /// <summary>
        /// Property for Message
        /// </summary>
        public IChatMessage Message { get; }
        #endregion
        
        #region constructor
        /// <summary>
        /// Constructor for ChatMessageReceivedEventArgs
        /// </summary>
        /// <param name="message">Message</param>
        public ChatMessageReceivedEventArgs(IChatMessage message)
        {
            Message = message;
        }
        #endregion
    }
}