using ExpensePlanner.Models;
using ExpensePlanner.Models.Dtos;
using ExpensePlanner.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpensePlanner.Services
{
    public class UserService : StaticService, IUserService
    {
        private readonly ExpensePlannerDbContext _context;
        
        public UserService(ExpensePlannerDbContext context)
        {
            _context = context;
        }

        public void CustomDeleteData(User user)
        {
            user.IsDeleted = true;
            user.DeletedTime= DateTime.Now;

            foreach(var item in user.Expenses)
            {
                item.IsDeleted = true;
                item.DeletedTime = DateTime.Now;
            }
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Set<User>()
                .Include(a => a.Expenses)
                .FirstOrDefault(a => a.Id == userId);

            if (user != null)
            {
                try
                {
                    CustomDeleteData(user);

                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("Error", e);
                }

                return true;
            }

            return false;
        }

        public IEnumerable<UserDto> GetAllUser()
        {
            var query = _context.Set<User>()
                .Include(a => a.Expenses)
                .Include(a => a.Role)
                .Where(a => !a.IsDeleted)
                .ToList();

            var data = query.Select(a => new
            {
                Id = a.Id,
                Login = a.Login,
                RoleId = a.RoleId,
                RoleName = a.Role.Name,
            }).ToList();

            var users = data.Select(a => new UserDto
            {
                Id= a.Id,
                Login = a.Login,
                RoleName = a.RoleName,                
            }).ToList();

            return users;
        }

        public User GetUser<T>(string login)
        {
            var user = _context.Set<User>()
                .FirstOrDefault(a => a.Login == login);

            if (user == null)
                return null;

            return user;
        }

        public IQueryable<ExpenseDto> GetUserExpenses(int userId)
        {
            var query = _context.Set<Expense>()
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .ToList();

            var data = query.Select(a => new
            {
                Id = a.Id,
                Name = a.Name,
                Amount = a.Amount,
                ExpenseType = a.ExpenseType,
                Description = a.Description,
                IsCompleted = a.IsCompleted,
                CreateDate = a.CreateDate,
                RealizedDate = a.RealizedDate,
                IsDeleted = a.IsDeleted,
                DeleteTime = a.DeletedTime,
            }).ToList();

            var expenses = data.Select(a => new ExpenseDto
            {
                Id = a.Id,
                Name = a.Name,
                Amount = a.Amount,
                ExpenseType = a.ExpenseType,
                Description = a.Description,
                IsCompleted = a.IsCompleted,
                CreateDate = a.CreateDate,
                RealizedDate = a.RealizedDate,
                IsDeleted = a.IsDeleted,
                DeletedTime = a.DeleteTime
            }).AsQueryable()
            .OrderBy(a => a.Id);

            return expenses;
        }

        public bool IsAdmin(int userId)
        {
            var query = _context.Set<User>()
                .FirstOrDefault(a => a.Id == userId);

            if (query.RoleId == 1)
            {
                return true;
            }

            return false;
        }
    }
}
