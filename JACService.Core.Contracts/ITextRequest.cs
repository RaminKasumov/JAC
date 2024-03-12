using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace JACService.Core.Contracts
{
    public interface ITextRequest
    {
        #region method
        /// <summary>
        /// Command of Client is being processed
        /// </summary>
        /// <param name="command">Command of Client</param>
        /// <returns>Returns a response to Client</returns>
        string GetResponse(string command);
        #endregion
    }
}