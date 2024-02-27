using System;
using System.IO;

namespace JAC.Service.Core
{
    public class FileServiceLogger : IServiceLogger
    {
        #region instancevariable
        readonly ConsoleColor _originalColor = Console.ForegroundColor;
        #endregion
        
        #region constant
        const string FILEPATH = @"..\ServiceInformation.txt";
        #endregion

        #region methods
        public void LogServiceInfo(string message)
        {
            LogMessageWithColor(message, ConsoleColor.Red);
        }

        public void LogRequestInfo(string message)
        {
            LogMessageWithColor(message, ConsoleColor.Green);
        }

        private void LogMessageWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = _originalColor;

            try
            {
                File.AppendAllText(FILEPATH, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
        }
        #endregion
    }
}