using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserInterface
    {
        Task<User> GetUserAsync(int userId);

        Task<List<User>> ListUsersAsync();

        Task<User> AddUserAsync(User user);

        Task<User> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(int userId);
    }
}