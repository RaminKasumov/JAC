using JACService.Core.Contracts;

namespace JACService.Shared
{
    /// <summary>
    /// Represents a serializable message with essential properties
    /// </summary>
    /// <param name="ChannelName">The channelName of the message</param>
    /// <param name="Recipient">The recipient of the message</param>
    /// <param name="Sender">The sender of the message</param>
    /// <param name="Content">The content of the message</param>
    /// <param name="IsPrivate">The privacy of the message</param>
    public record SerializableMessage(string ChannelName, string Recipient, string Sender, string Content, bool IsPrivate);

    /// <summary>
    /// Provides extension methods for serializing and deserializing messages
    /// </summary>
    public static class MessageSerializationExtensions
    {
        #region methods
        /// <summary>
        /// Converts a message object to a serializable message object
        /// </summary>
        /// <param name="message">The message object to convert</param>
        /// <returns>A serializable message object</returns>
        private static SerializableMessage ToSerializableMessage(this IChatMessage message)
        {
            return new SerializableMessage(message.ChannelName, message.Recipient, message.Sender, message.Content, message.IsPrivate);
        }
    
        /// <summary>
        /// Converts a serializable message object to a message object
        /// </summary>
        /// <param name="messageSerializationExtensions">The serializable message object to convert</param>
        /// <returns>A message object</returns>
        private static IChatMessage ToMessage(this SerializableMessage messageSerializationExtensions)
        {
            var message = new ChatMessage(messageSerializationExtensions.ChannelName, messageSerializationExtensions.Recipient, messageSerializationExtensions.Sender, messageSerializationExtensions.Content, messageSerializationExtensions.IsPrivate);
            return message;
        }
    
        /// <summary>
        /// Converts a collection of message objects to a list of serializable message objects
        /// </summary>
        /// <param name="messages">The collection of message objects to convert</param>
        /// <returns>A list of serializable message objects</returns>
        public static List<SerializableMessage> ToSerializableMessages(this IEnumerable<IChatMessage> messages)
        {
            return messages.Select(message => message.ToSerializableMessage()).ToList();
        }
    
        /// <summary>
        /// Converts a collection of serializable message objects to a list of message objects
        /// </summary>
        /// <param name="serializableMessages">The collection of serializable message objects to convert</param>
        /// <returns>A list of message objects</returns>
        public static List<IChatMessage> ToMessages(this IEnumerable<SerializableMessage> serializableMessages)
        {
            return serializableMessages.Select(serializableMessage => serializableMessage.ToMessage()).ToList();
        }
        #endregion
    }   
}