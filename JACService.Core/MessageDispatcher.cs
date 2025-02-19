﻿using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core
{
    /// <summary>
    /// Dispatches messages to users
    /// </summary>
    public class MessageDispatcher
    {
        #region instancevariables
        /// <summary>
        /// The list of sessions
        /// </summary>
        readonly List<SessionHandler> _sessions;
        
        /// <summary>
        /// The logger for service events
        /// </summary>
        readonly IServiceLogger _serviceLogger;

        /// <summary>
        /// The ChatMessageReceivedEventArgs instance
        /// </summary>
        ChatMessageReceivedEventArgs? _chatMessageReceived;
        #endregion
        
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDispatcher"/> class
        /// </summary>
        /// <param name="sessions">The list of sessions</param>
        /// <param name="serviceLogger">The logger for service events</param>
        public MessageDispatcher(List<SessionHandler> sessions, IServiceLogger serviceLogger)
        {
            _sessions = sessions;
            _serviceLogger = serviceLogger;
        }
        #endregion
        
        #region methods
        /// <summary>
        /// Broadcasts a message to all users
        /// </summary>
        /// <param name="chatMessage">The chat message to send</param>
        public void Broadcast(IChatMessage chatMessage)
        {
            _chatMessageReceived = new ChatMessageReceivedEventArgs(chatMessage);
            
            _sessions.Find(session => session.ChatUser.CurrentChannel.Name == chatMessage.Recipient)?.SendText($"MessageReceived: {_chatMessageReceived.Message}");
            
            _serviceLogger.LogBroadcastMessage($"[{DateTime.Now}] BROADCAST Broadcast message was received.");
        }
        
        /// <summary>
        /// Sends a message to a specific user
        /// </summary>
        /// <param name="chatMessage">The chat message to send</param>
        public void Whisper(IChatMessage chatMessage)
        {
            _chatMessageReceived = new ChatMessageReceivedEventArgs(chatMessage);
            
            _sessions.Find(session => session.ChatUser.CurrentChannel.Name == chatMessage.Recipient)?.SendText($"MessageReceived: {_chatMessageReceived.Message}");
            
            _serviceLogger.LogWhisperMessage($"[{DateTime.Now}] WHISPER Whisper message was received.");
        }
        #endregion
    }
}