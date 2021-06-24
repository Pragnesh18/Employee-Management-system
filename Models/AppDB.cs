using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Emp.Models
{
    public class AppDB : IdentityDbContext<ApplicationUser>
    {
        public AppDB(DbContextOptions<AppDB> options) 
            : base(options)
        {
        }

        public DbSet <Employee> Employees{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
