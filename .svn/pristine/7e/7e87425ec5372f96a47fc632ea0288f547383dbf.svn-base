using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class WhisperRequestHandler : ITextRequest
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
            string recipient = splitter[1].Trim();
            string message = splitter[splitter.Length - 1];
            
            LoginRequestHandler loginRequestHandler = new LoginRequestHandler();
            string userNickname = loginRequestHandler.ChatUser.Nickname;
            
            IChatMessage chatMessage = new ChatMessage(recipient, userNickname, message, true);
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