﻿using System.Text.Json;
using System.Text.Json.Serialization;
using JACService.Core.Contracts;

namespace JACService.Shared
{
    /// <summary>
    /// Manages the directory of users, channels and messages in the chat service
    /// </summary>
    public class ChatDirectory
    {
        #region instancevariable
        /// <summary>
        /// The file path for the chat directory JSON file
        /// </summary>
        string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "ChatDirectory.json");
        #endregion
        
        #region properties
        /// <summary>
        /// The list of users in the chat service
        /// </summary>
        public List<IUser> Users = ChatUserStorage.GetInstance().Users;
        
        /// <summary>
        /// The list of channels in the chat service
        /// </summary>
        public List<IChannel> Channels = ChatChannelStorage.GetInstance().Channels;

        /// <summary>
        /// The list of messages in the chat service
        /// </summary>
        public List<IChatMessage> Messages = ChatMessageStorage.GetInstance().Messages;
        
        /// <summary>
        /// Singleton instance of the ChatDirectory
        /// </summary>
        static ChatDirectory Instance { get; set; } = new ChatDirectory();
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatDirectory"/> class
        /// </summary>
        [JsonConstructor]
        private ChatDirectory()
        {
            
        }
        #endregion
        
        #region methods
        /// <summary>
        /// Gets the singleton instance of the ChatDirectory
        /// </summary>
        /// <returns>The singleton instance</returns>
        public static ChatDirectory GetInstance()
        {
            return Instance;
        }
        
        /// <summary>
        /// Saves the directory to a JSON file
        /// </summary>
        public void SaveToFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            
            var data = new
            {
                Users = Users.ToSerializableUsers(),
                Channels = Channels.ToSerializableChannels(),
                Messages = Messages.ToSerializableMessages()
            };

            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(_filePath, json);
        }
        
        /// <summary>
        /// Loads the directory from a JSON file
        /// </summary>
        public void LoadFromFile()
        {
            string json = File.ReadAllText(_filePath);
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            ChatDirectory data = JsonSerializer.Deserialize<ChatDirectory>(json, options) ?? new ChatDirectory();
            
            ChatUserStorage.GetInstance().Users = data.Users.ToSerializableUsers().ToUsers();
            ChatChannelStorage.GetInstance().Channels = data.Channels.ToSerializableChannels().ToChannels();
            ChatMessageStorage.GetInstance().Messages = data.Messages.ToSerializableMessages().ToMessages();

            Instance = data;
        }
        #endregion
    }
}