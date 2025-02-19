﻿using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core
{
    /// <summary>
    /// Handles requests for creating a new channel
    /// </summary>
    public class CreateChannelRequestHandler : ITextRequest
    {
        #region instancevariable
        /// <summary>
        /// The session handler instance
        /// </summary>
        readonly SessionHandler _session;
        #endregion
        
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateChannelRequestHandler"/> class
        /// </summary>
        /// <param name="session">The session handler</param>
        public CreateChannelRequestHandler(SessionHandler session)
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
            ChatUserStorage userStorage = ChatUserStorage.GetInstance();
            ChatChannelStorage channelStorage = ChatChannelStorage.GetInstance();
        
            string[] splitter = command.Split(' ');
            string channelType = splitter[2].Trim();
            string channelName = splitter[4].Trim();
            IChannel newChannel;

            if (channelType == "private")
            {
                string user = splitter[7].Trim();
                IUser chatUser = userStorage.FindUser(user);
                newChannel = new Channel(channelName) { IsPrivate = true };
                
                newChannel.AddUser(chatUser);
            }
            else
            {
                newChannel = new Channel(channelName);
            }
        
            newChannel.AddUser(_session.ChatUser);

            if (channelStorage.Channels.All(channel => !channel.Name.Contains(newChannel.Name)))
            {
                channelStorage.AddChannel(newChannel);

                if (channelType == "private")
                {
                    return $"Channel {newChannel.Name} created with User {splitter[7].Trim()}.";
                }
                
                return $"Channel {newChannel.Name} created.";
            }

            return "Channel already exists!!!";
        }
        #endregion
    }
}