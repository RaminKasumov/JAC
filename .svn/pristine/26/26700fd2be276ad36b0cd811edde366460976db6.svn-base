using System;
using System.Collections.Generic;
using JAC.Shared;
using JACService.Core.Contracts;

namespace JAC.Service.Core
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
            Channel channel = new Channel(command.Substring(splitter[0].Length).Trim());
            
            ChatDirectory instance = ChatDirectory.GetInstance();
            List<string> users = (List<string>)instance.GetUsersByChannel(channel);

            foreach (string user in users)
            {
                if (user != users[users.Count - 1])
                {
                    result += user + Environment.NewLine;
                }
                else
                {
                    result += user;
                }
            }

            return result;
        }
        #endregion
    }
}