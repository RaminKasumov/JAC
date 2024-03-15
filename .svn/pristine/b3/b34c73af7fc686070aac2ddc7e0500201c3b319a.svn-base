using System;
using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public static class RequestHandlerFactory
    {
        #region method
        /// <summary>
        /// Creates a text request handler based on the given request
        /// </summary>
        /// <param name="request">Input request</param>
        /// <returns>ITextRequest instance</returns>
        public static ITextRequest CreateTextRequest(string request)
        {
            string[] splitRequest = request.Split(' ');
            
            if (splitRequest[0] == "/broadcast")
            {
                return new RequestHandler();
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}