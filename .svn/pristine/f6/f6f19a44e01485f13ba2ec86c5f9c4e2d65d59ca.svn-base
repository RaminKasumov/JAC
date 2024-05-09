using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class LoginRequestHandler : ITextRequest
    {
        #region property
        /// <summary>
        /// Auto-property for ChatUser
        /// </summary>
        public static IUser ChatUser { get; private set; }
        #endregion
        
        #region method
        /// <summary>
        /// Command of Client is being processed
        /// </summary>
        /// <param name="command">Command of Client</param>
        /// <returns>Returns a response to Client</returns>
        public string GetResponse(string command)
        {
            string[] splitter = command.Split(' ');
            string nickname = command.Substring(splitter[0].Length).Trim();
            ChatServiceDirectory directory = ChatServiceDirectory.GetInstance();
            
            if (directory.FindUser(nickname) != null)
            {
                return "Error: User already exists!";
            }
            else
            {
                ChatUser = new ChatUser(nickname);
                return "User logged in.";
            }
        }
        #endregion
    }
}