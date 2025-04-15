using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using TaskManager.API.Entities;

namespace TaskManagerApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    userName = "admin",
                    PasswordHash = "admin123",
                    Role = "Admin",
                },
                new User
                {
                    id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    userName = "user1",
                    PasswordHash = "user123",
                    Role = "User",
                },
                 new User
                 {
                     id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                     userName = "user2",
                     PasswordHash = "user123",
                     Role = "User",
                 }
            );
        }
    }
}
