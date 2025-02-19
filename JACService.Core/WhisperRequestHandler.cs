﻿using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core
{
    /// <summary>
    /// Handles whisper requests
    /// </summary>
    public class WhisperRequestHandler : ITextRequest
    {
        #region instancevariables
        /// <summary>
        /// The name of the private channel
        /// </summary>
        string _privateChannelName = null!;
        
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
        /// Initializes a new instance of the <see cref="WhisperRequestHandler"/> class
        /// </summary>
        /// <param name="session">The session handler</param>
        public WhisperRequestHandler(SessionHandler session)
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
            _privateChannelName = splitter[1].Trim();
            
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
            IChatMessage chatMessage = new ChatMessage(_privateChannelName, _privateChannelName, sender.Nickname, _message, true);
            ChatMessageStorage chatMessageStorage = ChatMessageStorage.GetInstance();
            chatMessageStorage.AddMessage(_privateChannelName, chatMessage);
        }
        #endregion
    }
}