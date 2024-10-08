﻿using JACService.Shared;
using JACService.Core.Contracts;

namespace JACService.Core
{
    public class UsersRequestHandler : ITextRequest
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
            
            string[] splitter = command.Split(' ');
            Channel channel = new Channel(splitter[1].Trim());
            
            ChatDirectory directory = ChatDirectory.GetInstance();
            List<IUser> users = directory.GetUsersByChannel(channel);
            
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