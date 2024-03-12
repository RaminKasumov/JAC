using System;
using System.Net;
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