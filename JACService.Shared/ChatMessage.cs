﻿using JACService.Core.Contracts;

namespace JACService.Shared
{
    /// <summary>
    /// Represents a chat message in the chat service
    /// </summary>
    public class ChatMessage : IChatMessage
    {
        #region properties
        /// <summary>
        /// The name of the channel
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// The recipient of the message
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// The sender of the message
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// The content of the message
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The timestamp of the message
        /// </summary>
        public string TimeStamp { get; set; }
        
        /// <summary>
        /// Indicates whether the message is private or not
        /// </summary>
        public bool IsPrivate { get; set; }
        #endregion
        
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessage"/> class with the specified parameters
        /// </summary>
        /// <param name="channelName">The name of the channel</param>
        /// <param name="recipient">The recipient of the message</param>
        /// <param name="sender">The sender of the message</param>
        /// <param name="content">The content of the message</param>
        /// <param name="isPrivate">Indicates whether the message is private or not</param>
        public ChatMessage(string channelName, string recipient, string sender, string content, bool isPrivate)
        {
            ChannelName = channelName;
            Recipient = recipient;
            Sender = sender;
            Content = content;
            TimeStamp = DateTime.Now.ToShortTimeString();
            IsPrivate = isPrivate;
        }
        #endregion

        #region methods
        /// <summary>
        /// Converts a string to a ChatMessage object
        /// </summary>
        /// <param name="message">The message string</param>
        /// <returns>The converted ChatMessage object</returns>
        public static ChatMessage FromString(string message)
        {
            string[] parts = message.Split('|');
            ChatMessage msg = new ChatMessage(parts[1], parts[2], parts[3], parts[4],bool.Parse(parts[5]))
            {
                TimeStamp = parts[0]
            };
            return msg;
        }
        
        /// <summary>
        /// Converts a ChatMessage object to a string representation
        /// </summary>
        /// <returns>A string that represents the current ChatMessage object</returns>
        public override string ToString()
        {
            return $"{TimeStamp}|{ChannelName}|{Recipient}|{Sender}|{Content}|{IsPrivate}";
        }
        #endregion
    }
}