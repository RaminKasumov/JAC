using System;
using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class ChatMessage : IChatMessage
    {
        #region properties
        /// <summary>
        /// Recipient of the message
        /// </summary>
        public string Recipient { get; private set; }
        
        /// <summary>
        /// Sender of the message
        /// </summary>
        public string Sender { get; private set; }
        
        /// <summary>
        /// Content of the message
        /// </summary>
        public string Content { get; }
        
        /// <summary>
        /// TimeStamp of the message
        /// </summary>
        public DateTime TimeStamp { get; }
        
        /// <summary>
        /// Is the message private or not
        /// </summary>
        public bool IsPrivate { get; }
        #endregion
        
        #region constructor
        public ChatMessage(string recipient, string sender, string content, bool isPrivate)
        {
            Recipient = recipient;
            Sender = sender;
            Content = content;
            TimeStamp = DateTime.Now;
            IsPrivate = isPrivate;
        }
        #endregion
    }
}