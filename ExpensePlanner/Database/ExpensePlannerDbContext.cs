using ExpensePlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensePlanner.Database
{
    public class ExpensePlannerDbContext : DbContext
    {
        public ExpensePlannerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<ExpenseType> ExpenseTypes { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .HasOne(a => a.User)
                .WithMany(a => a.Expenses);

            modelBuilder.Entity<Expense>()
                .HasOne(a => a.ExpenseType)
                .WithMany(a => a.Expenses);

            modelBuilder.Entity<Role>()
                .HasMany(a => a.Users)
                .WithMany(a => a.Roles);


        }
    }
}
