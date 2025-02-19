﻿using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core
{
    /// <summary>
    /// Handles requests for joining a channel
    /// </summary>
    public class JoinChannelRequestHandler : ITextRequest
    {
        #region instancevariable
        /// <summary>
        /// The session handler instance
        /// </summary>
        readonly SessionHandler _session;
        #endregion
        
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinChannelRequestHandler"/> class
        /// </summary>
        /// <param name="session">The session handler instance</param>
        public JoinChannelRequestHandler(SessionHandler session)
        {
            _session = session;
        }
        #endregion
        
        #region method
        /// <summary>
        /// Processes the client's command and returns a response
        /// </summary>
        /// <param name="command">The command from the client</param>
        /// <returns>A response to the client</returns>
        public string GetResponse(string command)
        {
            ChatChannelStorage storage = ChatChannelStorage.GetInstance();
            
            string[] splitter = command.Split(' ');
            IChannel channel = storage.FindChannel(splitter[2].Trim());
            _session.ChatUser.CurrentChannel.Name = channel.Name;
            
            return $"Channel {channel.Name} joined.";
        }
        #endregion
    }
}