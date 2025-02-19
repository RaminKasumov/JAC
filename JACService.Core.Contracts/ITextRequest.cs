﻿namespace JACService.Core.Contracts
{
    /// <summary>
    /// Represents a text request in the chat service
    /// </summary>
    public interface ITextRequest
    {
        #region method
        /// <summary>
        /// Processes the client's command
        /// </summary>
        /// <param name="command">The client's command</param>
        /// <returns>A response for the client</returns>
        string GetResponse(string command);
        #endregion
    }
}