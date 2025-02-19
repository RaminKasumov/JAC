﻿using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core
{
    /// <summary>
    /// Handles requests for removing a channel
    /// </summary>
    public class RemoveChannelRequestHandler : ITextRequest
    {
        #region instancevariable
        /// <summary>
        /// The session handler instance
        /// </summary>
        readonly SessionHandler _session;
        #endregion
        
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveChannelRequestHandler"/> class
        /// </summary>
        /// <param name="session">The session handler instance</param>
        public RemoveChannelRequestHandler(SessionHandler session)
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
            ChatChannelStorage channelStorage = ChatChannelStorage.GetInstance();
        
            string[] splitter = command.Split(' ');
            string channelName = splitter[2].Trim();
            IChannel channel = channelStorage.FindChannel(channelName);

            if (_session.ChatUser.CurrentChannel.Name == channel.Name)
            {
                _session.ChatUser.CurrentChannel = new Channel();
            }
        
            channelStorage.RemoveChannel(channel);
        
            return $"Channel {channel.Name} deleted.";
        }
        #endregion
    }
}