﻿namespace JACService.Core.Contracts
{
    /// <summary>
    /// Provides logging functionalities for the service
    /// </summary>
    public interface IServiceLogger
    {
        #region methods
        /// <summary>
        /// Shows information on the console
        /// </summary>
        /// <param name="message">The service information message</param>
        void LogServiceInfo(string message);

        /// <summary>
        /// Shows requests on the console
        /// </summary>
        /// <param name="message">The request information message</param>
        void LogRequestInfo(string message);
        
        /// <summary>
        /// Shows broadcast messages on the console
        /// </summary>
        /// <param name="message">The broadcast message</param>
        void LogBroadcastMessage(string message);

        /// <summary>
        /// Shows whisper messages on the console
        /// </summary>
        /// <param name="message">The whisper message</param>
        void LogWhisperMessage(string message);
        #endregion
    }
}