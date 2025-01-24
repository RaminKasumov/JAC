namespace JACService.Core.Contracts
{
    /// <summary>
    /// Represents a user in the chat service
    /// </summary>
    public interface IUser
    {
        #region properties
        /// <summary>
        /// The nickname of user
        /// </summary>
        string Nickname { get; set; }
        
        /// <summary>
        /// The password of user
        /// </summary>
        string Password { get; set; }
        
        /// <summary>
        /// THe current channel of user
        /// </summary>
        IChannel CurrentChannel { get; set; }
        
        /// <summary>
        /// Indicates whether the user is logged in or not
        /// </summary>
        bool IsLoggedIn { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// The user is being logged in
        /// </summary>
        void Login();
        
        /// <summary>
        /// The user is being logged out
        /// </summary>
        void Logout();
        #endregion
    }
}