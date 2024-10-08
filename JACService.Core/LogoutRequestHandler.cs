﻿using JACService.Core.Contracts;

namespace JACService.Core
{
    public class LogoutRequestHandler : ITextRequest
    {
        #region instancevariable
        /// <summary>
        /// Instance variable for SessionHandler
        /// </summary>
        readonly SessionHandler _session;
        #endregion
        
        #region constructor
        /// <summary>
        /// Instance variable for Session is being initialized
        /// </summary>
        /// <param name="session">Session</param>
        public LogoutRequestHandler(SessionHandler session)
        {
            _session = session;
        }
        #endregion
        
        #region method
        /// <summary>
        /// Command of Client is being processed
        /// </summary>
        /// <param name="command">Command of Client</param>
        /// <returns>Returns a response to Client</returns>
        public string GetResponse(string command)
        {
            _session.ChatUser.Logout();
            
            return $"Logout-success: {_session.ChatUser}";
        }
        #endregion
    }
}