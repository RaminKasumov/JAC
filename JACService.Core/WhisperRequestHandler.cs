using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class WhisperRequestHandler : ITextRequest
    {
        #region instancevariables
        /// <summary>
        /// Receiver of the message
        /// </summary>
        string _receiver = "";
        
        /// <summary>
        /// Message of the Client
        /// </summary>
        string _message = "";
        #endregion
        
        #region methods
        /// <summary>
        /// Command of Client is being processed
        /// </summary>
        /// <param name="command">Command of Client</param>
        /// <returns>Returns a response to Client</returns>
        public string GetResponse(string command)
        {
            string[] splitter = command.Split(' ');

            if (splitter.Length == 3)
            {
                _receiver = splitter[1].Trim();
                _message = splitter[splitter.Length - 1];
            }
            
            Publish(LoginRequestHandler.ChatUser);

            return "ok";
        }

        /// <summary>
        /// Checks if the command is request or message broadcast
        /// </summary>
        /// <param name="sender">Sender of the message</param>
        private void Publish(IUser sender)
        {
            IChatMessage chatMessage = new ChatMessage(_receiver, sender.Nickname, _message, true);
            ChatMessageStorage chatMessageStorage = ChatMessageStorage.GetInstance();
            chatMessageStorage.AddMessage(chatMessage);
        }
        #endregion
    }
}