using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Interfaces
{
    public class UserInterface : IUserInterface
    {
        private readonly DataContext _context;

        public UserInterface(DataContext context) => _context = context;

        public async Task<User> GetUserAsync(int userId)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<List<User>> ListUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _context.AddAsync(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public Task<User> UpdateUserAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}