using System;
using System.Collections.Generic;
using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class MessageDispatcher
    {
        #region instancevariables
        /// <summary>
        /// Instance variable for List of Sessions
        /// </summary>
        readonly List<SessionHandler> _sessions;
        
        /// <summary>
        /// Instance variable for Interface IServiceLogger
        /// </summary>
        readonly IServiceLogger _serviceLogger;
        #endregion
        
        #region constructor
        /// <summary>
        /// Instance variables are being initialized
        /// </summary>
        /// <param name="sessions">List of sessions</param>
        /// <param name="serviceLogger">ServiceLogger</param>
        public MessageDispatcher(List<SessionHandler> sessions, IServiceLogger serviceLogger)
        {
            _sessions = sessions;
            _serviceLogger = serviceLogger;
            
            //Register OnChatMessageReceived method for ChatMessageReceived event
            ChatMessageStorage chatMessageStorage = ChatMessageStorage.GetInstance();
            chatMessageStorage.ChatMessageReceived += OnChatMessageReceived;
        }
        #endregion
        
        #region methods
        /// <summary>
        /// Broadcasts a message to all users
        /// </summary>
        private void Broadcast(IChatMessage chatMessage)
        {
            _sessions.FindAll(session => session.ChatUser.CurrentChannel == chatMessage.Recipient)
                .ForEach(session => session.SendText(chatMessage.ToString()));
        }
        
        /// <summary>
        /// Sends a message to a specific user
        /// </summary>
        private void Whisper(IChatMessage chatMessage)
        {
            _sessions.FindAll(session => session.ChatUser.Nickname == chatMessage.Recipient)
                .ForEach(session => session.SendText(chatMessage.ToString()));
        }
        
        /// <summary>
        /// Handles the ChatMessageReceived event by logging the information on the console
        /// </summary>
        /// <param name="sender">Triggering Event</param>
        /// <param name="e">Event arguments</param>
        private void OnChatMessageReceived(object sender, IChatMessage e)
        {
            if (e.IsPrivate)
            {
                Whisper(e);
            }
            else
            {
                Broadcast(e);
            }
            
            _serviceLogger.LogServiceInfo($"[{DateTime.Now.ToShortTimeString()}] A message was received");
        }
        #endregion
    }
}