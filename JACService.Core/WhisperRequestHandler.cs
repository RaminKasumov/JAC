using JAC.Shared;
using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class WhisperRequestHandler : ITextRequest
    {
        #region instancevariables
        /// <summary>
        /// Receiver of the message
        /// </summary>
        IChannel _receiverChannel;
        
        /// <summary>
        /// Message of the Client
        /// </summary>
        string _message = "";

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
        public WhisperRequestHandler(SessionHandler session)
        {
            _session = session;
        }
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

            if (splitter.Length >= 3)
            {
                _receiverChannel = new Channel(splitter[1].Trim());
            }
            
            ChatDirectory chatDirectory = ChatDirectory.GetInstance();

            if (chatDirectory.FindChannel(_receiverChannel) != null)
            {
                string[] splitAtPipe = command.Split('|');
                _message = command.Substring(splitAtPipe[0].Length).Trim();
            
                Publish(_session.ChatUser);
                
                return "ok";
            }
            else
            {
                return "Error: Channel does not exist!";
            }
        }

        /// <summary>
        /// Checks if the command is request or message broadcast
        /// </summary>
        /// <param name="sender">Sender of the message</param>
        private void Publish(IUser sender)
        {
            IChatMessage chatMessage = new ChatMessage(_receiverChannel.Name, sender.Nickname, _message, true);
            ChatMessageStorage chatMessageStorage = ChatMessageStorage.GetInstance();
            chatMessageStorage.AddMessage(chatMessage);
        }
        #endregion
    }
}