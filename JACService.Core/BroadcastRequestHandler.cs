﻿using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core
{
    /// <summary>
    /// Handles broadcast requests for a specific channel
    /// </summary>
    public class BroadcastRequestHandler : ITextRequest
    {
        #region instancevariables
        /// <summary>
        /// The name of the group channel
        /// </summary>
        string _groupChannelName = null!;
        
        /// <summary>
        /// The message to be sent
        /// </summary>
        string _message = "";
        
        /// <summary>
        /// The session handler instance
        /// </summary>
        readonly SessionHandler _session;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BroadcastRequestHandler"/> class
        /// </summary>
        /// <param name="session">The session handler</param>
        public BroadcastRequestHandler(SessionHandler session)
        {
            _session = session;
        }
        #endregion
        
        #region methods
        /// <summary>
        /// Processes the client's command and returns a response
        /// </summary>
        /// <param name="command">The command from the client</param>
        /// <returns>A response to the client</returns>
        public string GetResponse(string command)
        {
            string[] splitter = command.Split(' ');
            _groupChannelName = splitter[1].Trim();
            
            string[] splitAtPipe = command.Split('|');
            _message = splitAtPipe[1].Trim();
            
            Publish(_session.ChatUser);
                
            return "ok";
        }
        
        /// <summary>
        /// Checks if the command is request or message broadcast
        /// </summary>
        /// <param name="sender">The sender of the message</param>
        private void Publish(IUser sender)
        {
            IChatMessage chatMessage = new ChatMessage(_groupChannelName, _groupChannelName, sender.Nickname, _message, false);
            ChatMessageStorage chatMessageStorage = ChatMessageStorage.GetInstance();
            chatMessageStorage.AddMessage(_groupChannelName, chatMessage);
        }
        #endregion
    }
}