﻿namespace JACService.Shared
{
    public class ChatUser : IUser
    {
        #region properties
        /// <summary>
        /// Auto-property for Nickname of User
        /// </summary>
        public string Nickname { get; set; } = null!;

        /// <summary>
        /// Auto-property for Password of User
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Auto-property for CurrentChannel of User
        /// </summary>
        public IChannel CurrentChannel { get; set; } = null!;

        /// <summary>
        /// Auto-property to find out if the User is logged in or not
        /// </summary>
        public bool IsLoggedIn { get; set; }
        #endregion
        
        #region constructors

        /// <summary>
        /// Constructor if a Nickname is given
        /// </summary>
        /// <param name="nickname">Nickname of User</param>
        /// <param name="password">Password of User</param>
        public ChatUser(string nickname, string password)
        {
            Nickname = nickname;
            Password = password;
            
            ChatDirectory directory = ChatDirectory.GetInstance();
            CurrentChannel = directory.FindChannel("Anonymous");
        }
        
        /// <summary>
        /// Constructor if no Nickname is given
        /// </summary>
        public ChatUser()
        {
            IsLoggedIn = false;
        }
        #endregion
        
        #region methods
        /// <summary>
        /// User is being logged in
        /// </summary>
        public void Login()
        {
            IsLoggedIn = true;
        }
        
        /// <summary>
        /// User is being logged out
        /// </summary>
        public void Logout()
        {
            IsLoggedIn = false;
        }
        
        /// <summary>
        /// Converts a string to a ChatUser object
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>User with the current Channel</returns>
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
            return $"{Nickname}|{Password}|{CurrentChannel.Name}";
        }
        #endregion
    }
}