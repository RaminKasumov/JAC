using JACService.Core.Contracts;

namespace JACService.Shared
{
    /// <summary>
    /// Represents a serializable channel with essential properties
    /// </summary>
    /// <param name="Name">The name of the channel</param>
    /// <param name="Users">The list of users in the channel</param>
    public record SerializableChannel(string Name, List<IUser> Users);

    /// <summary>
    /// Provides extension methods for serializing and deserializing channels
    /// </summary>
    public static class ChannelSerializationExtensions
    {
        #region methods
        /// <summary>
        /// Converts a channel object to a serializable channel object
        /// </summary>
        /// <param name="channel">The channel object to convert</param>
        /// <returns>A serializable channel object</returns>
        private static SerializableChannel ToSerializableChannel(this IChannel channel)
        {
            return new SerializableChannel(channel.Name, channel.Users);
        }
    
        /// <summary>
        /// Converts a serializable channel object to a channel object
        /// </summary>
        /// <param name="channelSerializationExtensions">The serializable channel object to convert</param>
        /// <returns>A channel object</returns>
        private static IChannel ToChannel(this SerializableChannel channelSerializationExtensions)
        {
            var channel = new Channel(channelSerializationExtensions.Name)
            {
                Users = channelSerializationExtensions.Users,
            };
            return channel;
        }
    
        /// <summary>
        /// Converts a collection of channel objects to a list of serializable channel objects
        /// </summary>
        /// <param name="channels">The collection of channel objects to convert</param>
        /// <returns>A list of serializable channel objects</returns>
        public static List<SerializableChannel> ToSerializableChannels(this IEnumerable<IChannel> channels)
        {
            return channels.Select(channel => channel.ToSerializableChannel()).ToList();
        }
    
        /// <summary>
        /// Converts a collection of serializable channel objects to a list of channel objects
        /// </summary>
        /// <param name="serializableChannels">The collection of serializable channel objects to convert</param>
        /// <returns>A list of channel objects</returns>
        public static List<IChannel> ToChannels(this IEnumerable<SerializableChannel> serializableChannels)
        {
            return serializableChannels.Select(serializableChannel => serializableChannel.ToChannel()).ToList();
        }
        #endregion
    }
}