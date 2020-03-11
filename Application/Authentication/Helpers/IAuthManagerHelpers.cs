using DAL.Entities;

namespace Application.Authentication.Helpers
{
    /// <summary>
    /// Helper methods for the authentication manager
    /// </summary>
    public interface IAuthManagerHelpers
    {
        /// <summary>
        /// Takes in user submitted password string and converts to hash and salt byte arrays
        /// </summary>
        /// <param name="password">User submitted password</param>
        /// <returns>Password Hash and Salt byte arrays as tuple</returns>
        (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password);

        /// <summary>
        /// Verifies if user submitted password matches the stored passwordHash
        /// </summary>
        /// <param name="password">User submitted password</param>
        /// <param name="passwordHash">encrypted password</param>
        /// <param name="passwordSalt">password salt</param>
        /// <returns>Boolean representing success of match</returns>
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

        /// <summary>
        /// Generates a JWT
        /// </summary>
        /// <param name="user">user for JWT claims</param>
        /// <param name="tokenSecret">secret from appsettings</param>
        /// <returns>token string</returns>
        string GenerateJwtToken(User user, string tokenSecret);
    }
}