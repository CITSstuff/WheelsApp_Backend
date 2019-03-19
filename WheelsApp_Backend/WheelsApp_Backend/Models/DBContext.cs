using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WheelsApp_Backend.Models
{
    public class DBContext :IdentityDbContext<IdentityUser>
    {
        public DBContext(DbContextOptions<DBContext>options):base(options) {

        }
        public DbSet<Client> clients { get; set; }
        public DbSet<Car> cars { get; set; }
    }
}