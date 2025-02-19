﻿using JACService.Core.Contracts;

namespace JACService.Shared
{
    /// <summary>
    /// Represents a channel in the chat service
    /// </summary>
    public class Channel : IChannel
    {
        #region properties
        /// <summary>
        /// The name of the channel
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The users in the channel
        /// </summary>
        public List<IUser> Users { get; set; }

        /// <summary>
        /// The amount of users in the channel
        /// </summary>
        public int UsersCount => Users.Count;
        
        /// <summary>
        /// Indicates whether the channel is private or not
        /// </summary>
        public bool IsPrivate { get; set; }
        #endregion
        
        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Channel"/> class with the specified name
        /// </summary>
        /// <param name="name">The name of the channel</param>
        public Channel(string name)
        {
            Name = name;
            Users = new List<IUser>();
            IsPrivate = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Channel"/> class with the default name "Anonymous"
        /// </summary>
        public Channel() : this("Anonymous")
        {
            
        }
        #endregion
        
        #region methods
        /// <summary>
        /// Adds a new user to the channel
        /// </summary>
        /// <param name="user">The user to add</param>
        public void AddUser(IUser user)
        {
            if (!Users.Contains(user))
            {
                Users.Add(user);
            }
        }
        
        /// <summary>
        /// Removes a user from the channel
        /// </summary>
        /// <param name="user">The user to remove</param>
        public void RemoveUser(IUser user)
        {
            Users.Remove(user);
        }
        
        /// <summary>
        /// Returns a string representation of the channel
        /// </summary>
        /// <returns>A string that represents the channel</returns>
        public override string ToString()
        {
            return $"{Name}|{UsersCount}|{IsPrivate}";
        }
        #endregion
    }
}