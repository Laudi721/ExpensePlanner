using ExpensePlanner.Models;
using Microsoft.EntityFrameworkCore;
using ExpensePlanner.Models.Dtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ExpensePlanner
{
    public class ExpensePlannerDbContext : IdentityDbContext<User>
    {
        public ExpensePlannerDbContext(DbContextOptions<ExpensePlannerDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(a => a.Expenses)
                .WithOne(a => a.User);

            modelBuilder.Entity<User>()
                .HasMany(a => a.Roles)
                .WithMany(a => a.Users);
        }
    }
}
