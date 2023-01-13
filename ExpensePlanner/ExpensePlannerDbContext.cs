using ExpensePlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensePlanner
{
    public class ExpensePlannerDbContext : DbContext
    {
        public ExpensePlannerDbContext(DbContextOptions<ExpensePlannerDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasMany(a => a.Expenses)
                .WithOne(a => a.User);

            modelBuilder.Entity<User>()
                .HasOne(a => a.Role)
                .WithMany(a => a.Users);
        }
    }
}
