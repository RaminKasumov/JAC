using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class RequestHandler : ITextRequest
    {
        #region method
        /// <summary>
        /// Command of Client is being processed
        /// </summary>
        /// <param name="command">Command of Client</param>
        /// <returns>Returns a response to Client</returns>
        public string GetResponse(string command)
        {
            string[] splitter = command.Split(' ');
            string result = command.Substring(splitter[0].Length + 1);
            
            if (result != "exit")
            {
                string output = $"<<{result}>>";
                return output;
            }
            else
            {
                return "Connection terminated.";
            }
        }
        #endregion
    }
}