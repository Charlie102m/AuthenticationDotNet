using System.IO;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

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

    /// <summary>
    /// Class added to allow ef migrations to execute without reference to a startup project
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../API/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new DataContext(builder.Options);
        }
    }
}
