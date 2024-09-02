using JACService.Core.Contracts;
using JACService.Shared;

namespace JACService.Core;

public class CreateChannelRequestHandler : ITextRequest
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
    public CreateChannelRequestHandler(SessionHandler session)
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
        string channelType = splitter[2].Trim();
        string user = splitter[4].Trim();
        IChannel newChannel;

        if (channelType == "private")
        {
            if (directory.FindUser(user) != null)
            {
                IUser chatUser = directory.FindUser(user);
                string channelName = $"{_session.ChatUser.Nickname}/{chatUser.Nickname}";
                newChannel = new Channel(channelName) { IsPrivate = true };
                
                newChannel.AddUser(chatUser);
            }
            else
            {
                return "Error: User does not exist!!!";
            }
        }
        else
        {
            newChannel = new Channel(splitter[4].Trim());
        }
        
        newChannel.AddUser(_session.ChatUser);

        if (directory.GetChannels().All(channel => !channel.Name.Contains(newChannel.Name)))
        {
            directory.AddChannel(newChannel);
            return $"Channel {newChannel.Name} created.";
        }

        return "Channel already exists!!!";
    }
    #endregion
}