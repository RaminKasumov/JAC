﻿using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core
{
    /// <summary>
    /// Handles requests for exiting the session
    /// </summary>
    public class ExitRequestHandler : ITextRequest
    {
        #region instancevariable
        /// <summary>
        /// The session handler instance
        /// </summary>
        readonly SessionHandler _session;
        #endregion
        
        #region constructor
        /// <summary>
        /// Handles requests for exiting the session
        /// </summary>
        /// <param name="session">The session handler</param>
        public ExitRequestHandler(SessionHandler session)
        {
            _session = session;
        }
        #endregion
    
        #region method
        /// <summary>
        /// Processes the client's command and returns a response
        /// </summary>
        /// <param name="command">The command from the client</param>
        /// <returns>A response to the client</returns>
        public string GetResponse(string command)
        {
            ChatDirectory directory = ChatDirectory.GetInstance();
            directory.SaveToFile();
            
            return $"Exit-success: {_session.ChatUser}";
        }
        #endregion
    }
}