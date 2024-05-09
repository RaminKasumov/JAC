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
        public string Recipient { get; set; }
        
        /// <summary>
        /// Sender of the message
        /// </summary>
        public string Sender { get; set; }
        
        /// <summary>
        /// Content of the message
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// TimeStamp of the message
        /// </summary>
        public DateTime TimeStamp { get; set; }
        
        /// <summary>
        /// Is the message private or not
        /// </summary>
        public bool IsPrivate { get; set; }
        #endregion
        
        #region constructor
        /// <summary>
        /// Constructor for the Class ChatMessage
        /// </summary>
        /// <param name="recipient">Recipient of the message</param>
        /// <param name="sender">Sender of the message</param>
        /// <param name="content">Content of the message</param>
        /// <param name="isPrivate">Is the message private or not</param>
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