﻿using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core
{
    public class JoinChannelRequestHandler : ITextRequest
    {
        #region method
        /// <summary>
        /// Command of Client is being processed
        /// </summary>
        /// <param name="command">Command of Client</param>
        /// <returns>Returns a response to Client</returns>
        public string GetResponse(string command)
        {
            string?[] splitter = command.Split(' ');
            IChannel channel = new Channel(splitter[1]);
            
            ChatDirectory chatDirectory = ChatDirectory.GetInstance();

            if (chatDirectory.GetChannelNames().All(c => c.Name != channel.Name))
            {
                chatDirectory.AddChannel(channel);
            }
            
            return $"Channel {channel} joined.";
        }
        #endregion
    }
}