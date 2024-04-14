using System;
using System.Collections.Generic;
using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class ChatServiceDirectory
    {
        #region instancevariables
        /// <summary>
        /// List of Users
        /// </summary>
        readonly List<IUser> _users = new List<IUser>();
        
        /// <summary>
        /// List of Channels
        /// </summary>
        readonly List<string> _channels = new List<string>();
        #endregion
        
        #region property
        /// <summary>
        /// Property for the instance of ChatServiceDirectory
        /// </summary>
        static ChatServiceDirectory Instance { get; } = new ChatServiceDirectory();
        #endregion

        #region constructor
        /// <summary>
        /// Constructor is private to prevent the creation of an instance of ChatServiceDirectory
        /// </summary>
        private ChatServiceDirectory()
        {
            
        }
        #endregion
        
        #region methods
        /// <summary>
        /// Returns the instance of ChatServiceDirectory
        /// </summary>
        /// <returns>Returns the existing instance</returns>
        public static ChatServiceDirectory GetInstance()
        {
            return Instance;
        }
        
        /// <summary>
        /// Adds a User to the list of Users
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Returns whether a new user was added</returns>
        public bool AddUser(IUser user)
        {
            if (!_users.Contains(user))
            {
                _users.Add(user);
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Removes a User from the list of Users
        /// </summary>
        /// <param name="user">User</param>
        public void RemoveUser(IUser user)
        {
            _users.Remove(user);
        }

        /// <summary>
        /// Finds a User by Nickname
        /// </summary>
        /// <param name="nickname">Nickname of User</param>
        /// <returns>Returns the User with the given Nickname</returns>
        public IUser FindUser(string nickname)
        {
            foreach (IUser user in _users)
            {
                if (user.Nickname == nickname)
                {
                    return user;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Adds a Channel to the list of Channels
        /// </summary>
        /// <param name="channel">Channel</param>
        /// <returns>Returns whether a new channel was added</returns>
        public bool AddChannel(string channel)
        {
            if (!_channels.Contains(channel))
            {
                _channels.Add(channel);
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Removes a Channel from the list of Channels
        /// </summary>
        /// <param name="channelName">Name of channel</param>
        public void RemoveChannel(string channelName)
        {
            _channels.Remove(channelName);
        }
        
        /// <summary>
        /// Finds a Channel by Name
        /// </summary>
        /// <param name="channelName">Name of channel</param>
        /// <returns>Returns the Channel with the given Name</returns>
        public string FindChannel(string channelName)
        {
            foreach (string channel in _channels)
            {
                if (channel == channelName)
                {
                    return channel;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Users in the Channel are being returned
        /// </summary>
        /// <param name="channel">Channel</param>
        /// <returns>Returns all Users in the Channel</returns>
        public IEnumerable<string> GetUsersByChannel(string channel)
        {
            if (string.IsNullOrEmpty(channel))
            {
                channel = "anonymous";
            }

            List<string> users = new List<string>();
            foreach (IUser user in _users)
            {
                if (user.CurrentChannel == channel)
                {
                    users.Add(user.Nickname);
                }
            }
            return users;
        }

        /// <summary>
        /// All existing Channels are being returned
        /// </summary>
        /// <returns>Returns all Channels</returns>
        public IEnumerable<string> GetChannelNames()
        {
            return _channels;
        }
        #endregion
    }
}