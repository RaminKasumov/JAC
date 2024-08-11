using JACService.Core.Contracts;
using JACService.Shared;

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
        /// Instance variables are being initialized
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
            ChatDirectory directory = ChatDirectory.GetInstance();
            directory.RemoveUser(_session.ChatUser);
            _session.Close();
            
            return $"Logout-success: {_session.ChatUser}";
        }
        #endregion
    }
}