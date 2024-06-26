﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JACService.Core.Contracts
{
    public interface IServiceLogger
    {
        #region methods
        /// <summary>
        /// Shows information on the console
        /// </summary>
        /// <param name="message">Service Information</param>
        void LogServiceInfo(string message);

        /// <summary>
        /// Shows requests on the console
        /// </summary>
        /// <param name="message">Request Information</param>
        void LogRequestInfo(string message);
        
        /// <summary>
        /// Shows broadcast messages on the console
        /// </summary>
        /// <param name="message">Broadcast Message</param>
        void LogBroadcastMessage(string message);

        /// <summary>
        /// Shows whisper messages on the console
        /// </summary>
        /// <param name="message">Whisper Message</param>
        void LogWhisperMessage(string message);
        #endregion
    }
}