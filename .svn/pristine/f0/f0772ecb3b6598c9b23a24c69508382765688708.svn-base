using System;
using JACService.Core.Contracts;

namespace JAC.Service.Core
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
            string[] splitter = command.Split(' ');
            string channel = splitter[1];
            
            ChatServiceDirectory chatServiceDirectory = ChatServiceDirectory.GetInstance();

            if (string.IsNullOrEmpty(chatServiceDirectory.FindChannel(channel)))
            {
                chatServiceDirectory.AddChannel(channel);
            }
            
            return $"Channel {channel} joined.";
        }
        #endregion
    }
}