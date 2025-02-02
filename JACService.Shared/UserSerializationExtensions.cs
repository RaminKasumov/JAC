using JACService.Core.Contracts;

namespace JACService.Shared
{
    /// <summary>
    /// Represents a serializable user with essential properties
    /// </summary>
    /// <param name="Nickname">The nickname of the user</param>
    /// <param name="Password">The password of the user</param>
    /// <param name="CurrentChannel">The current channel of the user</param>
    public record SerializableUser(string Nickname, string Password, IChannel CurrentChannel);

    /// <summary>
    /// Provides extension methods for serializing and deserializing users
    /// </summary>
    public static class UserSerializationExtensions
    {
        #region methods
        /// <summary>
        /// Converts a user object to a serializable user object
        /// </summary>
        /// <param name="user">The user object to convert</param>
        /// <returns>A serializable user object</returns>
        private static SerializableUser ToSerializableUser(this IUser user)
        {
            return new SerializableUser(user.Nickname, user.Password, user.CurrentChannel);
        }
    
        /// <summary>
        /// Converts a serializable user object to a user object
        /// </summary>
        /// <param name="userSerializationExtensions">The serializable user object to convert</param>
        /// <returns>A user object</returns>
        private static IUser ToUser(this SerializableUser userSerializationExtensions)
        {
            var user = new ChatUser(userSerializationExtensions.Nickname, userSerializationExtensions.Password)
            {
                CurrentChannel = userSerializationExtensions.CurrentChannel
            };
            return user;
        }
    
        /// <summary>
        /// Converts a collection of user objects to a list of serializable user objects
        /// </summary>
        /// <param name="users">The collection of user objects to convert</param>
        /// <returns>A list of serializable user objects</returns>
        public static List<SerializableUser> ToSerializableUsers(this IEnumerable<IUser> users)
        {
            return users.Select(user => user.ToSerializableUser()).ToList();
        }
    
        /// <summary>
        /// Converts a collection of serializable user objects to a list of user objects
        /// </summary>
        /// <param name="serializableUsers">The collection of serializable user objects to convert</param>
        /// <returns>A list of user objects</returns>
        public static List<IUser> ToUsers(this IEnumerable<SerializableUser> serializableUsers)
        {
            return serializableUsers.Select(serializableUser => serializableUser.ToUser()).ToList();
        }
        #endregion
    }
}