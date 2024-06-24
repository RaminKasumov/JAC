﻿using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public static class RequestHandlerFactory
    {
        #region method
        /// <summary>
        /// Creates a text request handler based on the given request
        /// </summary>
        /// <param name="request">Input request</param>
        /// <param name="session">SessionHandler</param>
        /// <returns>ITextRequest instance</returns>
        public static ITextRequest CreateTextRequest(string request, SessionHandler session)
        {
            string[] splitRequest = request.Split(' ');
            
            if (splitRequest[0] == "/login")
            {
                return new LoginRequestHandler();
            }
            else if (splitRequest[0] == "/join")
            {
                return new JoinChannelRequestHandler();
            }
            else if (splitRequest[0] == "/broadcast")
            {
                return new BroadcastRequestHandler(session);
            }
            else if (splitRequest[0] == "/whisper")
            {
                return new WhisperRequestHandler(session);
            }
            else if (splitRequest[0] == "/getusers")
            {
                return new UsersRequestHandler();
            }
            else if (splitRequest[0] == "/getchannels")
            {
                return new ChannelsRequestHandler();
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}