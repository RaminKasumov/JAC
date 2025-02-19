﻿using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core
{
    /// <summary>
    /// Handles requests for logging out a user
    /// </summary>
    public class LogoutRequestHandler : ITextRequest
    {
        #region instancevariable
        /// <summary>
        /// The session handler instance
        /// </summary>
        readonly SessionHandler _session;
        #endregion
        
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LogoutRequestHandler"/> class
        /// </summary>
        /// <param name="session">The session handler instance</param>
        public LogoutRequestHandler(SessionHandler session)
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
            _session.ChatUser.Logout();
            
            ChatDirectory directory = ChatDirectory.GetInstance();
            directory.SaveToFile();
            
            return $"Logout-success: {_session.ChatUser}";
        }
        #endregion
    }
}