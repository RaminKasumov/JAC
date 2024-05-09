using System;
using System.Collections.Generic;
using JAC.Shared;
using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class MessageDispatcher
    {
        #region instancevariables
        /// <summary>
        /// Instance variable for ChatMessage
        /// </summary>
        readonly IChatMessage _message;
        
        /// <summary>
        /// Instance variable for ChatServiceDirectory
        /// </summary>
        readonly ChatServiceDirectory _chatServiceDirectory;
        
        /// <summary>
        /// Instance variable for ChatMessageStorage
        /// </summary>
        readonly ChatMessageStorage _chatMessageStorage;
        #endregion
        
        #region constructor
        public MessageDispatcher(IChatMessage message)
        {
            _message = message;
            _chatServiceDirectory = ChatServiceDirectory.GetInstance();
            _chatMessageStorage = ChatMessageStorage.GetInstance();
        }
        #endregion
        
        #region eventHandler
        /// <summary>
        /// Event for ChatMessageReceived
        /// </summary>
        public event EventHandler ChatMessageReceived;
        #endregion
        
        #region methods
        /// <summary>
        /// Broadcasts a message to all users
        /// </summary>
        public void Broadcast()
        {
            List<string> channels = (List<string>)_chatServiceDirectory.GetChannelNames();

            foreach (string channel in channels)
            {
                List<string> users = (List<string>)_chatServiceDirectory.GetUsersByChannel(channel);
                foreach (string user in users)
                {
                    _chatMessageStorage.AddMessage(_message);
                }
            }

            OnChatMessageReceived();
        }
        
        /// <summary>
        /// Sends a message to a specific user
        /// </summary>
        public void Whisper()
        {
            IUser user = _chatServiceDirectory.FindUser(_message.Recipient);

            if (user != null)
            {
                _chatMessageStorage.AddMessage(_message);
            }

            OnChatMessageReceived();
        }
        
        /// <summary>
        /// Raises the ChatMessageReceived event
        /// </summary>
        private void OnChatMessageReceived()
        {
            ChatMessageReceived?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}