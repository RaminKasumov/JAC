﻿using JACService.Core.Contracts;

namespace JACService.Shared
{
    /// <summary>
    /// Manages the storage of chat messages
    /// </summary>
    public class ChatMessageStorage
    {
        #region properties
        /// <summary>
        /// The list of chat messages
        /// </summary>
        public List<IChatMessage> Messages { get; set; } = new List<IChatMessage>();
        
        /// <summary>
        /// Singleton instance of ChatMessageStorage
        /// </summary>
        static ChatMessageStorage Instance { get; } = new ChatMessageStorage();
        #endregion
        
        #region constructor
        /// <summary>
        /// Prevents the creation of an instance of ChatMessageStorage
        /// </summary>
        private ChatMessageStorage()
        {
            
        }
        #endregion
        
        #region eventHandler
        /// <summary>
        /// Event for a received message
        /// </summary>
        public event EventHandler<ChatMessageReceivedEventArgs>? ChatMessageReceived;
        #endregion
        
        #region methods
        /// <summary>
        /// Gets the singleton instance of ChatMessageStorage
        /// </summary>
        /// <returns>The existing instance</returns>
        public static ChatMessageStorage GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// Adds a new message to the list of messages
        /// </summary>
        /// <param name="channelName">The name of the channel</param>
        /// <param name="message">The message to add</param>
        public void AddMessage(string channelName, IChatMessage message)
        {
            message.ChannelName = channelName;
            Messages.Add(message);
            
            OnChatMessageReceived(message);
        }
        
        /// <summary>
        /// Raises the ChatMessageReceived event by invoking it, notifying subscribers about the session closure
        /// </summary>
        /// <param name="chatMessage">The received message</param>
        private void OnChatMessageReceived(IChatMessage chatMessage)
        {
            ChatMessageReceived?.Invoke(this, new ChatMessageReceivedEventArgs(chatMessage));
        }
        #endregion
    }
}