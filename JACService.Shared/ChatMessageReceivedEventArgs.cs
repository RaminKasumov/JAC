﻿using JACService.Core.Contracts;

namespace JACService.Shared
{
    /// <summary>
    /// Provides data for the ChatMessageReceived event
    /// </summary>
    public class ChatMessageReceivedEventArgs : EventArgs
    {
        #region property
        /// <summary>
        /// The chat message that was received
        /// </summary>
        public IChatMessage Message { get; }
        #endregion
        
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageReceivedEventArgs"/> class
        /// </summary>
        /// <param name="message">The chat message that was received</param>
        public ChatMessageReceivedEventArgs(IChatMessage message)
        {
            Message = message;
        }
        #endregion
    }
}