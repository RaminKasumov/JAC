using System;
using System.Collections.Generic;
using System.Net;

namespace JACService.Core.Contracts
{
    public interface IUser
    {
        #region properties
        /// <summary>
        /// Nickname of User
        /// </summary>
        string Nickname { get; }
        
        /// <summary>
        /// Current Channel of User
        /// </summary>
        string CurrentChannel { get; set; }
        
        /// <summary>
        /// Is User logged in or not
        /// </summary>
        bool IsLoggedIn { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// User is being logged in
        /// </summary>
        void Login();
        
        /// <summary>
        /// User is being logged out
        /// </summary>
        void Logout();
        #endregion
    }
}