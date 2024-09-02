using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core;

public class RemoveChannelRequestHandler : ITextRequest
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
    public RemoveChannelRequestHandler(SessionHandler session)
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
        
        string[] splitter = command.Split(' ');
        string channelName = splitter[2].Trim();
        IChannel channel = directory.FindChannel(channelName);

        if (_session.ChatUser.CurrentChannel == channel)
        {
            _session.ChatUser.CurrentChannel = directory.FindChannel("Anonymous");
        }
        
        directory.RemoveChannel(channel);
        
        return $"Channel {channel.Name} deleted.";
    }
    #endregion
}