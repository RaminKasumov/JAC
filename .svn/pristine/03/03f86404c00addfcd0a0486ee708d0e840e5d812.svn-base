using JACService.Core.Contracts;

namespace JACService.Shared
{
    /// <summary>
    /// Manages the storage of chat channels
    /// </summary>
    public class ChatChannelStorage
    {
        #region properties
        /// <summary>
        /// The list of chat channels
        /// </summary>
        public List<IChannel> Channels { get; set; } = new List<IChannel>();
        
        /// <summary>
        /// Singleton instance of ChatChannelStorage
        /// </summary>
        static ChatChannelStorage Instance { get; } = new ChatChannelStorage();
        #endregion
        
        #region constructor
        /// <summary>
        /// Prevents the creation of an instance of ChatChannelStorage
        /// </summary>
        private ChatChannelStorage()
        {
            Channels.Add(new Channel());
        }
        #endregion
        
        #region methods
        /// <summary>
        /// Gets the singleton instance of ChatChannelStorage
        /// </summary>
        /// <returns>The existing instance</returns>
        public static ChatChannelStorage GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// Adds a channel to the list of channels
        /// </summary>
        /// <param name="channel">The channel to add</param>
        public void AddChannel(IChannel channel)
        {
            if (!Channels.Contains(channel))
            {
                Channels.Add(channel);
            }
        }
        
        /// <summary>
        /// Removes a channel from the list of channels
        /// </summary>
        /// <param name="channelName">The name of the channel</param>
        public void RemoveChannel(IChannel channelName)
        {
            Channels.Remove(channelName);
        }
        
        /// <summary>
        /// Finds a channel by name
        /// </summary>
        /// <param name="channelName">The name of the channel</param>
        /// <returns>The channel with the given name</returns>
        public IChannel FindChannel(string channelName)
        {
            foreach (IChannel channel in Channels)
            {
                if (channel.Name == channelName)
                {
                    return channel;
                }
            }
            return null!;
        }
        
        /// <summary>
        /// Gets all users in a specific channel
        /// </summary>
        /// <param name="channel">The channel</param>
        /// <returns>A list of users in the channel</returns>
        public List<IUser> GetUsersByChannel(IChannel channel)
        {
            ChatUserStorage storage = ChatUserStorage.GetInstance();
            List<IUser> users = storage.Users;
            return users
                .Where(user => channel.Users.Contains(user))
                .ToList();
        }
        #endregion
    }
}