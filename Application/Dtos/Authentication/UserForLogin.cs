namespace Application.Dtos.Authentication
{
    /// <summary>
    /// Dto submitted by user for registering
    /// </summary>
    public class UserForLogin
    {
        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }
    }
}