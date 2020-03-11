using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataStore
{
    /// <summary>
    /// Concrete implementation of <see cref="IUserStore"/>
    /// </summary>
    public class UserStore : IUserStore
    {
        /// <summary>
        /// Connection to database
        /// </summary>
        private readonly DataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Connection to database</param>
        public UserStore(DataContext context) => _context = context;

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User matching Id or null</returns>
        public async Task<User> GetUserAsync(int userId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));

            return await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
        }

        /// <summary>
        /// Find user by email
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>User matching email or null</returns>
        public async Task<User> FindUserAsync(string email)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));

            return await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        /// <summary>
        /// Checks if user exists with same email
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>Boolean representing if user exists already</returns>
        public async Task<bool> UserExistsAsync(string email)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));

            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of users</returns>
        public async Task<List<User>> ListUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="user">User to be added</param>
        /// <returns>Added user</returns>
        public async Task<User> AddUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            await _context.AddAsync(user);

            await _context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">Update user values</param>
        /// <returns>Updated user record</returns>
        public async Task<User> UpdateUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userForUpdate = await _context.Users.FindAsync(user.Id);

            var props = user.GetType().GetProperties();

            foreach (var prop in props)
            {
                if (prop.Name == "Id" || prop.Name == "CreatedAt") continue;

                var updateValue = prop.GetValue(user);

                if (updateValue != null)
                {
                    prop.SetValue(userForUpdate, updateValue);
                }
            }

            await _context.SaveChangesAsync();

            return userForUpdate;
        }

        /// <summary>
        /// Delete user from data store
        /// </summary>
        /// <param name="userId">Id of user to delete</param>
        /// <returns>Boolean representing success of delete action</returns>
        public async Task<bool> DeleteUserAsync(int userId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));

            var user = await _context.Users.FindAsync(userId);

            if (user is null) return false;

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}