﻿using JACService.Shared;
using JACService.Core.Contracts;

namespace JACService.Core
{
    /// <summary>
    /// Handles requests for logging in a user
    /// </summary>
    public class LoginRequestHandler : ITextRequest
    {
        #region instancevariable
        /// <summary>
        /// The user who is logged in
        /// </summary>
        IUser? _user;
        #endregion
        
        #region eventHandler
        /// <summary>
        /// EventHandler for ChatUser who is logged in
        /// </summary>
        public static event Action<IUser>? UserLoggedIn;
        #endregion
        
        #region methods
        /// <summary>
        /// Processes the client's command and returns a response
        /// </summary>
        /// <param name="command">The command from the client</param>
        /// <returns>A response to the client</returns>
        public string GetResponse(string command)
        {
            string[] splitter = command.Split(' ');
            string nickname = command.Substring(splitter[0].Length, command.LastIndexOf(' ') - splitter[0].Length).Trim();
            string password = command.Substring(command.LastIndexOf(' ') + 1).Trim();

            ChatDirectory directory = ChatDirectory.GetInstance();
            ChatUserStorage userStorage = ChatUserStorage.GetInstance();

            if (File.Exists("ChatDirectory.json"))
            {
                directory.LoadFromFile();
            }
            
            if (userStorage.FindUser(nickname) != null)
            {
                _user = userStorage.FindUser(nickname);
                
                if (_user.Password != password)
                {
                    return "Error: Wrong password!!!";
                }
            }
            else
            {
                _user = new ChatUser(nickname, password);
                
                userStorage.AddUser(_user);
                
                foreach (IChannel channel in directory.Channels)
                {
                    if (!channel.IsPrivate)
                    {
                        channel.AddUser(_user);
                    }
                }
            }
            
            _user.Login();
            OnUserLoggedIn(_user);
            return $"Login-success: {_user}";
        }

        /// <summary>
        /// Raises the UserLoggedIn event by invoking it, notifying subscribers about the user login
        /// </summary>
        /// <param name="user">The user who logged in</param>
        private void OnUserLoggedIn(IUser user)
        {
            UserLoggedIn?.Invoke(user);
        }
        #endregion
    }
}