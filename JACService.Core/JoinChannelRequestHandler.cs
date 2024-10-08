﻿using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core
{
    public class JoinChannelRequestHandler : ITextRequest
    {
        #region instancevariable
        /// <summary>
        /// Instance variable for SessionHandler
        /// </summary>
        readonly SessionHandler _session;
        #endregion
        
        #region constructor
        /// <summary>
        /// Instance variable for Session is being initialized
        /// </summary>
        /// <param name="session">Session</param>
        public JoinChannelRequestHandler(SessionHandler session)
        {
            _session = session;
        }
        #endregion
        
        #region method
        /// <summary>
        /// Command of Client is being processed
        /// </summary>
        /// <param name="command">Command of Client</param>
        /// <returns>Returns a response to Client</returns>
        public string GetResponse(string command)
        {
            ChatDirectory directory = ChatDirectory.GetInstance();
            
            string[] splitter = command.Split(' ');
            IChannel channel = directory.FindChannel(splitter[2].Trim());
            _session.ChatUser.CurrentChannel = channel;
            
            return $"Channel {channel.Name} joined.";
        }
        #endregion
    }
}