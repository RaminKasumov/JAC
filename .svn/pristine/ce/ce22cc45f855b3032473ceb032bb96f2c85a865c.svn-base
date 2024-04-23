using System.Runtime.Remoting.Channels;
using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class BroadcastRequestHandler : ITextRequest
    {
        #region methods
        /// <summary>
        /// Command of Client is being processed
        /// </summary>
        /// <param name="command">Command of Client</param>
        /// <returns>Returns a response to Client</returns>
        public string GetResponse(string command)
        {
            string[] splitter = command.Split(' ');
            string channel = "";

            if (splitter.Length == 2)
            {
                channel = splitter[1].Trim();
            }
            else
            {
                channel = "anonymous";
            }
            
            string message = splitter[splitter.Length - 1];
            
            LoginRequestHandler loginRequestHandler = new LoginRequestHandler();
            string userNickname = loginRequestHandler.ChatUser.Nickname;
            
            IChatMessage chatMessage = new ChatMessage(channel, userNickname, message, false);
            ChatMessageStorage chatMessageStorage = ChatMessageStorage.GetInstance();
            chatMessageStorage.AddMessage(chatMessage);

            return "ok";
        }
        
        /// <summary>
        /// Checks if the command is request or message broadcast
        /// </summary>
        /// <param name="sender">Sender of the message</param>
        public void Publish(IUser sender)
        {
            
        }
        #endregion
    }
}