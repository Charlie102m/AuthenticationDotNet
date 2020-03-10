using System.Threading.Tasks;
using Application.Dtos.Authentication;
using DAL.Entities;

namespace Application.Authentication.Manager
{
    /// <summary>
    /// Interface for authentication manager
    /// </summary>
    public interface IAuthManager
    {
        /// <summary>
        /// Validates submitted user details for creating new user
        /// </summary>
        /// <param name="userForRegister">User submitted details</param>
        /// <returns>Created user record</returns>
        Task<User> RegisterUserAsync(UserForRegister userForRegister);

        /// <summary>
        /// Validates user submitted details for login
        /// </summary>
        /// <param name="userForLogin"></param>
        /// <returns></returns>
        Task<User> LoginAsync(UserForLogin userForLogin);
    }
}