using System;
using System.Collections.Generic;
using JAC.Shared;
using JACService.Core.Contracts;

namespace JAC.Service.Core
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
            
            ChatDirectory instance = ChatDirectory.GetInstance();
            List<IChannel> channels = (List<IChannel>)instance.GetChannelNames();

            foreach (IChannel channel in channels)
            {
                if (channel != channels[channels.Count - 1])
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