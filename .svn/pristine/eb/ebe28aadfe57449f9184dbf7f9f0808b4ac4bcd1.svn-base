using JACService.Core.Contracts;

namespace JACService.Shared
{
    /// <summary>
    /// Manages the storage of chat users
    /// </summary>
    public class ChatUserStorage
    {
        #region properties
        /// <summary>
        /// The list of chat users
        /// </summary>
        public List<IUser> Users { get; set; } = new List<IUser>();

        /// <summary>
        /// Singleton instance of ChatUserStorage
        /// </summary>
        static ChatUserStorage Instance { get; } = new ChatUserStorage();
        #endregion

        #region constructor
        /// <summary>
        /// Prevents the creation of an instance of ChatUserStorage
        /// </summary>
        private ChatUserStorage()
        {

        }
        #endregion

        #region methods
        /// <summary>
        /// Gets the singleton instance of ChatUserStorage
        /// </summary>
        /// <returns>The existing instance</returns>
        public static ChatUserStorage GetInstance()
        {
            return Instance;
        }
        
        /// <summary>
        /// Adds a user to the list of users
        /// </summary>
        /// <param name="user">The user to add</param>
        public void AddUser(IUser user)
        {
            if (!Users.Contains(user))
            {
                Users.Add(user);
            }
        }
        
        /// <summary>
        /// Removes a user from the list of users
        /// </summary>
        /// <param name="user">The user to remove</param>
        public void RemoveUser(IUser user)
        {
            Users.Remove(user);
        }

        /// <summary>
        /// Finds a user by nickname
        /// </summary>
        /// <param name="nickname">The nickname of the user</param>
        /// <returns>The user with the given nickname</returns>
        public IUser FindUser(string nickname)
        {
            foreach (IUser user in Users)
            {
                if (user.Nickname == nickname)
                {
                    return user;
                }
            }
            return null!;
        }
        #endregion
    }
}