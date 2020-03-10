using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    /// <summary>
    /// EF Core data context
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Inherited options</param>
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        /// <summary>
        /// User entity
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
