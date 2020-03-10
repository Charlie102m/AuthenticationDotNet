using System;
using System.Linq;

namespace Application.Authentication.Helpers
{
    /// <summary>
    /// Concrete implementation of <see cref="IAuthManagerHelpers"/>
    /// </summary>
    public class AuthManagerHelpers : IAuthManagerHelpers
    {
        /// <summary>
        /// Takes in user submitted password string and converts to hash and salt byte arrays
        /// </summary>
        /// <param name="password">User submitted password</param>
        /// <returns>Password Hash and Salt byte arrays as tuple</returns>
        public (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var salt = hmac.Key;

                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return (hash, salt);
            }
        }

        /// <summary>
        /// Verifies if user submitted password matches the stored passwordHash
        /// </summary>
        /// <param name="password">User submitted password</param>
        /// <param name="passwordHash">encrypted password</param>
        /// <param name="passwordSalt">password salt</param>
        /// <returns>Boolean representing success of match</returns>
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));
            if (passwordSalt == null) throw new ArgumentNullException(nameof(passwordSalt));

            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}