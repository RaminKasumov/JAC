﻿using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core
{
    public class WhisperRequestHandler : ITextRequest
    {
        #region instancevariables
        /// <summary>
        /// Instance variable for Name of the Channel
        /// </summary>
        string _privateChannelName = null!;
        
        /// <summary>
        /// Instance variable for Name of the User
        /// </summary>
        string _userName = null!;
        
        /// <summary>
        /// Instance variable for the Channel
        /// </summary>
        IChannel _privateChannel = null!;
        
        /// <summary>
        /// Instance variable for the User
        /// </summary>
        IUser _user = null!;
        
        /// <summary>
        /// Message of the Client
        /// </summary>
        string _message = "";

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
        public WhisperRequestHandler(SessionHandler session)
        {
            _session = session;
        }
        #endregion
        
        #region methods
        /// <summary>
        /// Command of Client is being processed
        /// </summary>
        /// <param name="command">Command of Client</param>
        /// <returns>Returns a response to Client</returns>
        public string GetResponse(string command)
        {
            string[] splitter = command.Split(' ');
            _privateChannelName = splitter[1].Trim();
            string[] splitterName = _privateChannelName.Split("/");
            
            _userName = splitterName[0] == _session.ChatUser.Nickname ? splitterName[0].Trim() : splitterName[1].Trim();
            
            ChatDirectory directory = ChatDirectory.GetInstance();
            
            if (directory.FindUser(_userName) != null)
            {
                _user = directory.FindUser(_userName);
                _privateChannel = directory.FindChannel(_privateChannelName);
                    
                string[] splitAtPipe = command.Split('|');
                _message = splitAtPipe[1].Trim();
            
                Publish(_session.ChatUser);
                
                return "ok";
            }
            
            return "Error: Channel does not exist!";
        }

        /// <summary>
        /// Checks if the command is request or message broadcast
        /// </summary>
        /// <param name="sender">Sender of the message</param>
        private void Publish(IUser sender)
        {
            IChatMessage chatMessage = new ChatMessage(_privateChannel.Name, _user.Nickname, sender.Nickname, _message, true);
            ChatMessageStorage chatMessageStorage = ChatMessageStorage.GetInstance();
            chatMessageStorage.AddMessage(_privateChannel.Name, chatMessage);
        }
        #endregion
    }
}