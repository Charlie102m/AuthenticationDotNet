using System;
using System.Threading.Tasks;
using Application.Authentication.Helpers;
using Application.Dtos.Authentication;
using Application.Utils;
using DAL.Entities;
using AutoMapper;
using DAL.DataStore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Authentication.Manager
{
    /// <summary>
    /// Concrete implementation of <see cref="IAuthManager"/>
    /// </summary>
    public class AuthManager : IAuthManager
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly ILogger<AuthManager> _logger;

        /// <summary>
        /// For application logging
        /// </summary>
        private readonly IAuthManagerHelpers _helpers;

        /// <summary>
        /// Helper methods for authentication
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Data access layer
        /// </summary>
        private readonly IUserStore _store;

        /// <summary>
        /// Access to appsettings.json
        /// </summary>
        private readonly IOptions<AppSettings> _config;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">For application logging</param>
        /// <param name="mapper">AutoMapper</param>
        /// <param name="helpers">Helper methods for authentication</param>
        /// <param name="store">Data access layer</param>
        /// <param name="config">Access to appsettings.json</param>
        public AuthManager(ILogger<AuthManager> logger, 
                            IMapper mapper, 
                            IAuthManagerHelpers helpers, 
                            IUserStore store,
                            IOptions<AppSettings> config)
        {
            _logger = logger;
            _helpers = helpers;
            _mapper = mapper;
            _store = store;
            _config = config;
        }

        /// <summary>
        /// Validates submitted user details for creating new user
        /// </summary>
        /// <param name="userForRegister">User submitted details</param>
        /// <returns>Created user record</returns>
        public async Task<User> RegisterUserAsync(UserForRegister userForRegister)
        {
            if (userForRegister == null) throw new ArgumentNullException(nameof(userForRegister));

            if (await _store.UserExistsAsync(userForRegister.Email)) throw new ArgumentException("User already exists by that email address");

            var (passwordHash, passwordSalt) = _helpers.CreatePasswordHash(userForRegister.Password);

            var userToAdd = _mapper.Map<User>(userForRegister);

            userToAdd.CreatedAt = DateTime.Now;
            userToAdd.PasswordHash = passwordHash;
            userToAdd.PasswordSalt = passwordSalt;

            var user = await _store.AddUserAsync(userToAdd);

            return user;
        }

        /// <summary>
        /// Validates user submitted details for login
        /// </summary>
        /// <param name="userForLogin"></param>
        /// <returns></returns>
        public async Task<string> LoginAsync(UserForLogin userForLogin)
        {
            if (userForLogin == null) throw new ArgumentNullException(nameof(userForLogin));

            var user = await _store.FindUserAsync(userForLogin.Email);

            if (!_helpers.VerifyPasswordHash(userForLogin.Password, user.PasswordHash, user.PasswordSalt)) throw new ArgumentException("Incorrect login details");

            user.LastLogin = DateTime.Now;

            var userForReturn = await _store.UpdateUserAsync(user);

            return _helpers.GenerateJwtToken(userForReturn, _config.Value.Token);
        }
    }
}