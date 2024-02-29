using System;
using System.Net;

namespace JAC.Service.Core
{
    public class RequestHandler : ITextRequest
    {
        #region methods
        /// <summary>
        /// Command of Client is being processed
        /// </summary>
        /// <param name="command">Command of Client</param>
        /// <returns>Returns a response to Client</returns>
        public string GetResponse(string command)
        {
            if (command != "exit")
            {
                string output = $"<<{command}>>";
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