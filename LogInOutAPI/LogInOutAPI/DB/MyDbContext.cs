using LogInOutAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LogInOutAPI.DB
{
    public class MyDbContext :DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet <Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            
            modelBuilder.Entity<Users>().ToTable("users");
        }
        public DbSet <UserCompany> UserCompany { get; set; }
    }
}
