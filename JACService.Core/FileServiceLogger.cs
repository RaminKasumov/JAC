﻿using System.Drawing;
using JACService.Core.Contracts;

namespace JACService.Core
{
    public class FileServiceLogger : IServiceLogger
    {
        #region instancevariables
        /// <summary>
        /// Instance variable for original color of console
        /// </summary>
        readonly ConsoleColor _originalColor = Console.ForegroundColor;
        
        /// <summary>
        /// Instance variable for Messages
        /// </summary>
        readonly List<LogMessage> _logMessages = new List<LogMessage>();
        #endregion
        
        #region constant
        /// <summary>
        /// Constant for filepath
        /// </summary>
        const string Filepath = @"ServiceInformation.txt";
        #endregion
        
        #region eventHandler
        /// <summary>
        /// Event to log the Messages
        /// </summary>
        public event EventHandler? LogUpdated;
        #endregion

        #region methods
        /// <summary>
        /// Shows information on the console
        /// </summary>
        /// <param name="message">Service Information</param>
        public void LogServiceInfo(string message)
        {
            LogMessage msg = new LogMessage(message, Color.Red);
            _logMessages.Add(msg);
            OnLogUpdated(EventArgs.Empty);
            
            LogMessageWithColor(msg);
        }

        /// <summary>
        /// Shows requests on the console
        /// </summary>
        /// <param name="message">Service Information</param>
        public void LogRequestInfo(string message)
        {
            LogMessage msg = new LogMessage(message, Color.Gray);
            _logMessages.Add(msg);
            OnLogUpdated(EventArgs.Empty);
            
            LogMessageWithColor(msg);
        }
        
        /// <summary>
        /// Shows broadcast messages on the console
        /// </summary>
        /// <param name="message">Broadcast Message</param>
        public void LogBroadcastMessage(string message)
        {
            LogMessage msg = new LogMessage(message, Color.Green);
            _logMessages.Add(msg);
            OnLogUpdated(EventArgs.Empty);
            
            LogMessageWithColor(msg);
        }
        
        /// <summary>
        /// Shows whisper messages on the console
        /// </summary>
        /// <param name="message">Whisper Message</param>
        public void LogWhisperMessage(string message)
        {
            LogMessage msg = new LogMessage(message, Color.Yellow);
            _logMessages.Add(msg);
            OnLogUpdated(EventArgs.Empty);
            
            LogMessageWithColor(msg);
        }

        /// <summary>
        /// Shows information or requests on the console and writes it to a file
        /// </summary>
        /// <param name="message">Service Information</param>
        private void LogMessageWithColor(LogMessage message)
        {
            Console.ForegroundColor = ConvertColorToConsoleColor(message.Color);
            Console.WriteLine(message);
            Console.ForegroundColor = _originalColor;

            try
            {
                File.AppendAllText(Filepath, message.Message + Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured: {e.Message}");
            }
        }
        
        /// <summary>
        /// Converts a color to a ConsoleColor
        /// </summary>
        /// <param name="color">Color</param>
        /// <returns>Returns a color converted into a ConsoleColor</returns>
        private ConsoleColor ConvertColorToConsoleColor(Color color)
        {
            int index = (color.R > 128 | color.G > 128 | color.B > 128) ? 8 : 0;
            index |= (color.R > 64) ? 4 : 0;
            index |= (color.G > 64) ? 2 : 0;
            index |= (color.B > 64) ? 1 : 0;
            return (ConsoleColor)index;
        }
        
        /// <summary>
        /// OnLogUpdated is a method that triggers the LogUpdated event
        /// </summary>
        /// <param name="e">Event arguments</param>
        private void OnLogUpdated(EventArgs e)
        {
            LogUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// GetLogMessages is a method that returns the list of log messages
        /// </summary>
        /// <returns>List of LogMessage objects</returns>
        public List<LogMessage> GetLogMessages()
        {
            return _logMessages;
        }
        #endregion
    }
}