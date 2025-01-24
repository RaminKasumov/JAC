namespace JACService.Core.Contracts
{
    /// <summary>
    /// Represents a channel in the chat service
    /// </summary>
    public interface IChannel
    {
        #region properties
        /// <summary>
        /// The name of the channel
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// The users in the channel
        /// </summary>
        List<IUser> Users { get; set; }
        
        /// <summary>
        /// The amount of users in the channel
        /// </summary>
        int UsersCount { get; }
        
        /// <summary>
        /// The messages in the channel
        /// </summary>
        List<IChatMessage> Messages { get; set; }
        
        /// <summary>
        /// Indicates whether the channel is private or not
        /// </summary>
        bool IsPrivate { get; set; }
        #endregion
        
        #region methods
        /// <summary>
        /// Adds a new user to the channel
        /// </summary>
        /// <param name="user">User</param>
        void AddUser(IUser user);
        
        /// <summary>
        /// Removes a user from the channel
        /// </summary>
        /// <param name="user">User</param>
        void RemoveUser(IUser user);
        
        /// <summary>
        /// Adds a new message to the channel
        /// </summary>
        /// <param name="message"></param>
        void AddMessage(IChatMessage message);
        
        /// <summary>
        /// Removes a message from the channel
        /// </summary>
        /// <param name="message">Message</param>
        void RemoveMessage(IChatMessage message);
        
        /// <summary>
        /// Loads all messages in the channel
        /// </summary>
        void LoadMessages();
        #endregion
    }
}