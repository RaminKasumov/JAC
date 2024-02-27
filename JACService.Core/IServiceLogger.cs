using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAC.Service.Core
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
        #endregion
    }
}