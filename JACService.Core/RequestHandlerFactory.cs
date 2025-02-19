﻿using JACService.Core.Contracts;

namespace JACService.Core
{
    /// <summary>
    /// Factory class for creating text request handlers
    /// </summary>
    public static class RequestHandlerFactory
    {
        #region method
        /// <summary>
        /// Creates a text request handler based on the given request
        /// </summary>
        /// <param name="request">The input request</param>
        /// <param name="session">The session handler</param>
        /// <returns>An instance of ITextRequest</returns>
        public static ITextRequest CreateTextRequest(string request, SessionHandler session)
        {
            string[] splitRequest = request.Split(' ');
            string command = splitRequest[0];

            return command switch
            {
                "/login" => new LoginRequestHandler(),
                "/logout" => new LogoutRequestHandler(session),
                "/join" => new JoinChannelRequestHandler(session),
                "/create" => new CreateChannelRequestHandler(session),
                "/delete" => new RemoveChannelRequestHandler(session),
                "/getUsers" => new UsersRequestHandler(),
                "/getChannels" => new ChannelsRequestHandler(),
                "/broadcast" => new BroadcastRequestHandler(session),
                "/whisper" => new WhisperRequestHandler(session),
                "/exit" => new ExitRequestHandler(session),
                _ => null!
            };
        }
        #endregion
    }
}