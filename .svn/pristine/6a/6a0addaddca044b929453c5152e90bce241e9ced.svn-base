using System;
using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class ChatUser : IUser
    {
        #region properties
        /// <summary>
        /// Auto-property for Nickname of User
        /// </summary>
        public string Nickname { get; set; }
        
        /// <summary>
        /// Auto-property for CurrentChannel of User
        /// </summary>
        public string CurrentChannel { get; set; }
        
        /// <summary>
        /// Auto-property to find out if the User is logged in or not
        /// </summary>
        public bool IsLoggedIn { get; set; }
        #endregion
        
        #region constructors
        /// <summary>
        /// Constructor if no Nickname is given
        /// </summary>
        public ChatUser()
        {
            IsLoggedIn = false;
        }
        
        /// <summary>
        /// Constructor if a Nickname is given
        /// </summary>
        /// <param name="nickname">Nickname of User</param>
        public ChatUser(string nickname)
        {
            Nickname = nickname;
            CurrentChannel = "anonymous";
            IsLoggedIn = true;
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
        #endregion
    }
}