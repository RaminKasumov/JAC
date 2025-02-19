﻿using System.Drawing;
using JACService.Core.Contracts;

namespace JACService.Core
{
    /// <summary>
    /// Logs service information, requests, and messages to the console and a file
    /// </summary>
    public class FileServiceLogger : IServiceLogger
    {
        #region instancevariables
        /// <summary>
        /// The original color of the console
        /// </summary>
        readonly ConsoleColor _originalColor = Console.ForegroundColor;
        
        /// <summary>
        /// The list of log messages
        /// </summary>
        readonly List<LogMessage> _logMessages = new List<LogMessage>();
        #endregion
        
        #region constant
        /// <summary>
        /// The file path for the logging messages
        /// </summary>
        const string Filepath = @"ServiceInformation.txt";
        #endregion
        
        #region eventHandler
        /// <summary>
        /// Event triggered when a log message is updated
        /// </summary>
        public event EventHandler? LogUpdated;
        #endregion

        #region methods
        /// <summary>
        /// Logs service information to the console and file
        /// </summary>
        /// <param name="message">The service information message</param>
        public void LogServiceInfo(string message)
        {
            LogMessage msg = new LogMessage(message, Color.Red);
            _logMessages.Add(msg);
            OnLogUpdated(EventArgs.Empty);
            
            LogMessageWithColor(msg);
        }

        /// <summary>
        /// Logs request information to the console and file
        /// </summary>
        /// <param name="message">The request information message</param>
        public void LogRequestInfo(string message)
        {
            LogMessage msg = new LogMessage(message, Color.Gray);
            _logMessages.Add(msg);
            OnLogUpdated(EventArgs.Empty);
            
            LogMessageWithColor(msg);
        }
        
        /// <summary>
        /// Logs broadcast messages to the console and file
        /// </summary>
        /// <param name="message">The broadcast message</param>
        public void LogBroadcastMessage(string message)
        {
            LogMessage msg = new LogMessage(message, Color.Green);
            _logMessages.Add(msg);
            OnLogUpdated(EventArgs.Empty);
            
            LogMessageWithColor(msg);
        }
        
        /// <summary>
        /// Logs whisper messages to the console and file
        /// </summary>
        /// <param name="message">The whisper message</param>
        public void LogWhisperMessage(string message)
        {
            LogMessage msg = new LogMessage(message, Color.Yellow);
            _logMessages.Add(msg);
            OnLogUpdated(EventArgs.Empty);
            
            LogMessageWithColor(msg);
        }

        /// <summary>
        /// Logs a message with color to the console and writes it to a file
        /// </summary>
        /// <param name="message">The log message</param>
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
        /// <param name="color">The color to convert</param>
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
        /// Triggers the LogUpdated event
        /// </summary>
        /// <param name="e">The event arguments</param>
        private void OnLogUpdated(EventArgs e)
        {
            LogUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Returns the list of log messages
        /// </summary>
        /// <returns>The list of log messages</returns>
        public List<LogMessage> GetLogMessages()
        {
            return _logMessages;
        }
        #endregion
    }
}