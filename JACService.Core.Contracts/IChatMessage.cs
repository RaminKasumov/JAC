namespace JACService.Core.Contracts
{
    /// <summary>
    /// Represents a chat message in the chat service
    /// </summary>
    public interface IChatMessage
    {
        #region properties
        /// <summary>
        /// The name of the channel
        /// </summary>
        string ChannelName { get; set; }

        /// <summary>
        /// The recipient of the message
        /// </summary>
        string Recipient { get; set; }
        
        /// <summary>
        /// The sender of the message
        /// </summary>
        string Sender { get; set; }
        
        /// <summary>
        /// The content of the message
        /// </summary>
        string Content { get; set; }
        
        /// <summary>
        /// The timeStamp of the message
        /// </summary>
        string TimeStamp { get; set; }
        
        /// <summary>
        /// Indicates whether the message is private or not
        /// </summary>
        bool IsPrivate { get; set; }
        #endregion
    }
}