using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using WheelsApp_Backend.Models;

namespace WheelsApp_Backend.Models
{
    public class WheelsContext : DbContext {
        public WheelsContext() {}
        public WheelsContext(DbContextOptions<WheelsContext> options) : base(options) {}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("WheelsDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<NextOfKin> NextOfs { get; set; }
        public DbSet<Passwords> Passwords { get; set; }
    }
}