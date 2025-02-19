﻿using JACService.Core.Contracts;

namespace JACService.Shared
{
    /// <summary>
    /// Represents a user in the chat service
    /// </summary>
    public class ChatUser : IUser
    {
        #region properties
        /// <summary>
        /// The nickname of the user
        /// </summary>
        public string Nickname { get; set; } = null!;

        /// <summary>
        /// The password of the user
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// The current channel of the user
        /// </summary>
        public IChannel CurrentChannel { get; set; } = null!;

        /// <summary>
        /// Indicates whether the user is logged in or not
        /// </summary>
        public bool IsLoggedIn { get; set; }
        #endregion
        
        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatUser"/> class with the specified nickname and password
        /// </summary>
        /// <param name="nickname">The nickname of the user</param>
        /// <param name="password">The password of the user</param>
        public ChatUser(string nickname, string password)
        {
            Nickname = nickname;
            Password = password;
            
            CurrentChannel = new Channel();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatUser"/> class with default values
        /// </summary>
        public ChatUser()
        {
            IsLoggedIn = false;
        }
        #endregion
        
        #region methods
        /// <summary>
        /// Logs the user in
        /// </summary>
        public void Login()
        {
            IsLoggedIn = true;
        }
        
        /// <summary>
        /// Logs the user out
        /// </summary>
        public void Logout()
        {
            IsLoggedIn = false;
        }
        
        /// <summary>
        /// Converts a string to a ChatUser object
        /// </summary>
        /// <param name="user">The user string</param>
        /// <returns>The converted ChatUser object</returns>
        public static ChatUser FromString(string user)
        {
            string[] parts = user.Split('|');
            return new ChatUser(parts[0], parts[1]);
        }
        
        /// <summary>
        /// Converts a ChatUser object to a string representation
        /// </summary>
        /// <returns>A string that represents the current ChatUser object</returns>
        public override string ToString()
        {
            return $"{Nickname}|{Password}|{CurrentChannel}";
        }
        #endregion
    }
}