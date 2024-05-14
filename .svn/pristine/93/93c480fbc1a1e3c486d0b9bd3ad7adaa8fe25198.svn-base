using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class BroadcastRequestHandler : ITextRequest
    {
        #region instancevariables
        /// <summary>
        /// Instance variable for Channel
        /// </summary>
        string _channel = "";
        
        /// <summary>
        /// Instance variable for Message
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
                _channel = splitter[1].Trim();
            }
            else if (splitter.Length == 2)
            {
                _channel = "anonymous";
            }
            
            ChatServiceDirectory chatServiceDirectory = ChatServiceDirectory.GetInstance();

            if (chatServiceDirectory.FindChannel(_channel) != null)
            {
                _message = splitter[splitter.Length - 1];
            
                Publish(LoginRequestHandler.ChatUser);
                
                return "ok";
            }
            else
            {
                return "error";
            }
        }
        
        /// <summary>
        /// Checks if the command is request or message broadcast
        /// </summary>
        /// <param name="sender">Sender of the message</param>
        private void Publish(IUser sender)
        {
            IChatMessage chatMessage = new ChatMessage(_channel, sender.Nickname, _message, false);
            ChatMessageStorage chatMessageStorage = ChatMessageStorage.GetInstance();
            chatMessageStorage.AddMessage(chatMessage);
        }
        #endregion
    }
}