using Microsoft.EntityFrameworkCore;
using WheelsApp_Backend.Models;

namespace WheelsApp_Backend.Models
{
    public class WheelsContext : DbContext {
        public WheelsContext(DbContextOptions<WheelsContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }
}