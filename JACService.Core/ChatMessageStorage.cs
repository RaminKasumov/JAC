﻿using System.Collections.Generic;
using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class ChatMessageStorage
    {
        #region properties
        /// <summary>
        /// Auto-property for Messages
        /// </summary>
        public List<IChatMessage> Messages { get; set; } = new List<IChatMessage>();
        
        /// <summary>
        /// Auto-property for the instance of ChatMessageStorage
        /// </summary>
        static ChatMessageStorage Instance { get; } = new ChatMessageStorage();
        #endregion
        
        #region constructor
        /// <summary>
        /// Constructor is private to prevent the creation of an instance of ChatMessageStorage
        /// </summary>
        private ChatMessageStorage()
        {
            
        }
        #endregion
        
        #region methods
        /// <summary>
        /// Returns the instance of ChatMessageStorage
        /// </summary>
        /// <returns>Returns the existing instance</returns>
        public static ChatMessageStorage GetInstance()
        {
            return Instance;
        }
        
        /// <summary>
        /// Adds a new message to the list of Messages
        /// </summary>
        /// <param name="message">Message</param>
        public void AddMessage(IChatMessage message)
        {
            Messages.Add(message);
        }
        #endregion
    }
}