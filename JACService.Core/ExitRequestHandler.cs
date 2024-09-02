using JACService.Core.Contracts;

namespace JACService.Core;

public class ExitRequestHandler : ITextRequest
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
    public ExitRequestHandler(SessionHandler session)
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
        return $"Exit-success: {_session.ChatUser}";
    }
    #endregion
}