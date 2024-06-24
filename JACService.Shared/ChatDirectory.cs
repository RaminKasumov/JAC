﻿using System.Collections.Generic;

namespace JAC.Shared
{
    public class ChatDirectory
    {
        #region instancevariables
        /// <summary>
        /// List of Users
        /// </summary>
        readonly List<IUser> _users = new List<IUser>();
        
        /// <summary>
        /// List of Channels
        /// </summary>
        readonly List<IChannel> _channels = new List<IChannel>();
        #endregion
        
        #region property
        /// <summary>
        /// Property for the instance of ChatServiceDirectory
        /// </summary>
        static ChatDirectory Instance { get; } = new ChatDirectory();
        #endregion

        #region constructor
        /// <summary>
        /// Constructor is private to prevent the creation of an instance of ChatServiceDirectory
        /// </summary>
        private ChatDirectory()
        {
            AddChannel(new Channel());
        }
        #endregion
        
        #region methods
        /// <summary>
        /// Returns the instance of ChatServiceDirectory
        /// </summary>
        /// <returns>Returns the existing instance</returns>
        public static ChatDirectory GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// Adds a User to the list of Users
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Returns whether a new user was added</returns>
        public void AddUser(IUser user)
        {
            if (!_users.Contains(user))
            {
                _users.Add(user);
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
        public void AddChannel(IChannel channel)
        {
            if (!_channels.Contains(channel))
            {
                _channels.Add(channel);
            }
        }
        
        /// <summary>
        /// Removes a Channel from the list of Channels
        /// </summary>
        /// <param name="channelName">Name of channel</param>
        public void RemoveChannel(IChannel channelName)
        {
            _channels.Remove(channelName);
        }
        
        /// <summary>
        /// Finds a Channel by Name
        /// </summary>
        /// <param name="channelName">Name of channel</param>
        /// <returns>Returns the Channel with the given Name</returns>
        public IChannel FindChannel(IChannel channelName)
        {
            foreach (IChannel channel in _channels)
            {
                if (channel.Name == channelName.Name)
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
        public IEnumerable<string> GetUsersByChannel(IChannel channel)
        {
            if (channel == null)
            {
                channel = new Channel("Anonymous");
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
        public IEnumerable<IChannel> GetChannelNames()
        {
            return _channels;
        }
        #endregion
    }
}