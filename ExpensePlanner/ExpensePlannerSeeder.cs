using ExpensePlanner.Models;
using Microsoft.AspNetCore.Identity;

namespace ExpensePlanner
{
    public class ExpensePlannerSeeder
    {
        private readonly ExpensePlannerDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public ExpensePlannerSeeder(ExpensePlannerDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if(!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    _context.SaveChanges();
                }

                if(!_context.Users.Any())
                {
                    var user = GetUser();
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role
                {
                    Name = "Admin",
                },

                new Role
                {
                    Name = "User",
                },
            };

            return roles;
        }

        private User GetUser()
        {
            var user = new User
            {
                Login = "admin",
                Password = "admin1",
                RoleId = 1,
            };

            _passwordHasher.HashPassword(user, user.Password);

            return user;
        }
    }
}
