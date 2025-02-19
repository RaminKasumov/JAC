﻿using JACService.Shared;
using JACService.Core.Contracts;

namespace JACService.Core
{
    /// <summary>
    /// Handles user requests
    /// </summary>
    public class UsersRequestHandler : ITextRequest
    {
        #region method
        /// <summary>
        /// Processes the client's command
        /// </summary>
        /// <param name="command">The client's command</param>
        /// <returns>A response to the client</returns>
        public string GetResponse(string command)
        {
            string result = "";
            
            string[] splitter = command.Split(' ');
            Channel channel = new Channel(splitter[1].Trim());
            
            ChatChannelStorage channelStorage = ChatChannelStorage.GetInstance();
            List<IUser> users = channelStorage.GetUsersByChannel(channel);
            
            foreach (IUser user in users)
            {
                if (user != users[^1])
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