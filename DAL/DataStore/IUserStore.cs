using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.DataStore
{
    /// <summary>
    /// Interface for user queries
    /// </summary>
    public interface IUserStore
    {
        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User matching Id or null</returns>
        Task<User> GetUserAsync(int userId);

        /// <summary>
        /// Find user by email
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>User matching email or null</returns>
        Task<User> FindUserAsync(string email);

        /// <summary>
        /// Checks if user exists with same email
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>Boolean representing if user exists already</returns>
        Task<bool> UserExistsAsync(string email);

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of users</returns>
        Task<List<User>> ListUsersAsync();

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="user">User to be added</param>
        /// <returns>Added user</returns>
        Task<User> AddUserAsync(User user);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">Update user values</param>
        /// <returns>Updated user record</returns>
        Task<User> UpdateUserAsync(User user);

        /// <summary>
        /// Delete user from data store
        /// </summary>
        /// <param name="userId">Id of user to delete</param>
        /// <returns>Boolean representing success of delete action</returns>
        Task<bool> DeleteUserAsync(int userId);
    }
}