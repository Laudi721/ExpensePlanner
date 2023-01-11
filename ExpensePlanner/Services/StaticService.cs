using ExpensePlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensePlanner.Services
{
    public class BaseService
    {
        //private readonly ExpensePlannerDbContext _context;

        //public BaseService(ExpensePlannerDbContext context)
        //{
        //    _context = context;
        //}

        public static int userId { get; set; }

        //    public User GetUser<T>(string login)
        //    {
        //        var user = _context.Set<User>()
        //            .FirstOrDefault(a => a.Login == login);

        //        if (user == null)
        //            return null;

        //        return user;
        //    }
    }
}
