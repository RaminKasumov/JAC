﻿using JACService.Shared;
using JACService.Core.Contracts;

namespace JACService.Core
{
    public class ChannelsRequestHandler : ITextRequest
    {
        #region method
        /// <summary>
        /// Command of Client is being processed
        /// </summary>
        /// <param name="command">Command of Client</param>
        /// <returns>Returns a response to Client</returns>
        public string GetResponse(string command)
        {
            string result = "";
            
            ChatDirectory directory = ChatDirectory.GetInstance();
            List<IChannel> channels = directory.GetChannels();

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