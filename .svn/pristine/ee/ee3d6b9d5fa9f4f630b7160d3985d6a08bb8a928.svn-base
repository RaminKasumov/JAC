using System;
using System.IO;
using JACService.Core.Contracts;

namespace JAC.Service.Core
{
    public class FileServiceLogger : IServiceLogger
    {
        #region instancevariable
        /// <summary>
        /// Instance variable for original color of console
        /// </summary>
        readonly ConsoleColor _originalColor = Console.ForegroundColor;
        #endregion
        
        #region constant
        /// <summary>
        /// Constant for filepath
        /// </summary>
        const string Filepath = @"ServiceInformation.txt";
        #endregion

        #region methods
        /// <summary>
        /// Shows information on the console
        /// </summary>
        /// <param name="message">Service Information</param>
        public void LogServiceInfo(string message)
        {
            LogMessageWithColor(message, ConsoleColor.Red);
        }

        /// <summary>
        /// Shows requests on the console
        /// </summary>
        /// <param name="message">Service Information</param>
        public void LogRequestInfo(string message)
        {
            LogMessageWithColor(message, ConsoleColor.Green);
        }

        /// <summary>
        /// Shows information or requests on the console and writes it to a file
        /// </summary>
        /// <param name="message">Service Information</param>
        /// <param name="color">Color</param>
        private void LogMessageWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = _originalColor;

            try
            {
                File.AppendAllText(Filepath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
        }
        #endregion
    }
}