using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    /// <summary>
    /// Base user entity
    /// </summary>
    public class User
    {
        /// <summary>
        /// User id, auto incremented on DB
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hash of password
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Salt for decryption of password
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Last time the user logged in/was active
        /// </summary>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Date of registration
        /// </summary>
        public DateTime? CreatedAt { get; set; }
    }
}