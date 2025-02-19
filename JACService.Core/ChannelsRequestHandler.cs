﻿using JACService.Shared;
using JACService.Core.Contracts;

namespace JACService.Core
{
    /// <summary>
    /// Handles requests for retrieving the list of channels
    /// </summary>
    public class ChannelsRequestHandler : ITextRequest
    {
        #region method
        /// <summary>
        /// Processes the client's command and returns a response
        /// </summary>
        /// <param name="command">The command from the client</param>
        /// <returns>A response to the client</returns>
        public string GetResponse(string command)
        {
            string result = "";
            
            ChatDirectory directory = ChatDirectory.GetInstance();
            List<IChannel> channels = directory.Channels;

            foreach (IChannel channel in channels)
            {
                if (channel != channels[^1])
                {
                    result += channel + Environment.NewLine;
                }
                else
                {
                    result += channel;
                }
            }

            return result;
        }
        #endregion
    }
}