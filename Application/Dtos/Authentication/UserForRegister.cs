namespace Application.Dtos.Authentication
{
    /// <summary>
    /// Dto submitted by user for registering
    /// </summary>
    public class UserForRegister
    {
        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User plain text password
        /// </summary>
        public string Password { get; set; }
    }
}