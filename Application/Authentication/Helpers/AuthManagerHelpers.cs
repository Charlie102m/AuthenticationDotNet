using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DAL.Entities;
using Microsoft.IdentityModel.Tokens;

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

        /// <summary>
        /// Generates a JWT
        /// </summary>
        /// <param name="user">user for JWT claims</param>
        /// <param name="tokenSecret">secret from appsettings</param>
        /// <returns>token string</returns>
        public string GenerateJwtToken(User user, string tokenSecret)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (tokenSecret == null) throw new ArgumentNullException(nameof(tokenSecret));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName), 
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}