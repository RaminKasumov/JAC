﻿namespace JACService.Shared
{
    public interface IChannel
    {
        #region properties
        /// <summary>
        /// Name of the Channel
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// Users in the Channel
        /// </summary>
        List<IUser> Users { get; set; }
        
        /// <summary>
        /// Amount of Users in the Channel
        /// </summary>
        int UsersCount { get; }
        
        /// <summary>
        /// Messages in the Channel
        /// </summary>
        List<IChatMessage> Messages { get; set; }
        
        /// <summary>
        /// Is the Channel private or not
        /// </summary>
        bool IsPrivate { get; set; }
        #endregion
        
        #region methods
        /// <summary>
        /// Adds a new User to the Channel
        /// </summary>
        /// <param name="user">User</param>
        void AddUser(IUser user);
        
        /// <summary>
        /// Removes a User from the Channel
        /// </summary>
        /// <param name="user">User</param>
        void RemoveUser(IUser user);
        
        /// <summary>
        /// Adds a new Message to the Channel
        /// </summary>
        /// <param name="message"></param>
        void AddMessage(IChatMessage message);
        
        /// <summary>
        /// Removes a Message from the Channel
        /// </summary>
        /// <param name="message">Message</param>
        void RemoveMessage(IChatMessage message);
        
        /// <summary>
        /// Loads all messages in the Channel
        /// </summary>
        void LoadMessages();
        #endregion
    }
}