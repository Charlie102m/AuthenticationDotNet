using System;
using System.Threading.Tasks;
using Application.Authentication.Helpers;
using Application.Dtos.Authentication;
using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Authentication.Manager
{
    /// <summary>
    /// Concrete implementation of <see cref="IAuthManager"/>
    /// </summary>
    public class AuthManager : IAuthManager
    {
        /// <summary>
        /// For application logging
        /// </summary>
        private readonly IAuthManagerHelpers _helpers;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly ILogger<AuthManager> _logger;

        /// <summary>
        /// Helper methods for authentication
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Data access layer
        /// </summary>
        private readonly IUserInterface _dal;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">For application logging</param>
        /// <param name="mapper">AutoMapper</param>
        /// <param name="helpers">Helper methods for authentication</param>
        public AuthManager(ILogger<AuthManager> logger, IMapper mapper, IAuthManagerHelpers helpers, IUserInterface dal) =>
            (_logger, _mapper, _helpers, _dal) = (logger, mapper, helpers, dal);

        /// <summary>
        /// Validates submitted user details for creating new user
        /// </summary>
        /// <param name="userForRegister">User submitted details</param>
        /// <returns>Created user record</returns>
        public async Task<User> RegisterUserAsync(UserForRegister userForRegister)
        {
            if (userForRegister == null) throw new ArgumentNullException(nameof(userForRegister));

            var (passwordHash, passwordSalt) = _helpers.CreatePasswordHash(userForRegister.Password);

            var userToAdd = _mapper.Map<User>(userForRegister);

            userToAdd.CreatedAt = DateTime.Now;
            userToAdd.PasswordHash = passwordHash;
            userToAdd.PasswordSalt = passwordSalt;

            var user = await _dal.AddUserAsync(userToAdd);

            return user;
        }

        /// <summary>
        /// Validates user submitted details for login
        /// </summary>
        /// <param name="userForLogin"></param>
        /// <returns></returns>
        public async Task<User> LoginAsync(UserForLogin userForLogin)
        {
            if (userForLogin == null) throw new ArgumentNullException(nameof(userForLogin));

            var user = _mapper.Map<User>(userForLogin);

            return user;
        }
    }
}