using HomeApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeApp.API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { Database.Migrate(); }

        //public DbSet<User> Users { get; set; }  //do not need with identity

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole => userRole.HasKey(ur => new { ur.UserId, ur.RoleId }));
        }
    }
}