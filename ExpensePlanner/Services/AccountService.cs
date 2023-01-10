using ExpensePlanner.Models;
using ExpensePlanner.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpensePlanner.Services
{
    public class AccountService //: IAccountService
    {
        private readonly ExpensePlannerDbContext _context;

        public AccountService(ExpensePlannerDbContext context)
        {
            _context = context;
        }

        public static void LoginHistory(string userName)
        {
            StreamWriter streamWriter;
            var path = @"C:\LoginHistory\LoginHistory.txt";
            var directoryPath = @"C:\LoginHistory";

            if (!File.Exists(path))
            {
                Directory.CreateDirectory(directoryPath);
                using (streamWriter = File.CreateText(path))
                {
                    streamWriter.WriteLine($"userName; dateTime login");
                    streamWriter.WriteLine($"{userName}; {DateTime.Now}");
                    streamWriter.Close();
                }
            }
            else
            {
                using(streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine($"{userName}; {DateTime.Now}");
                    streamWriter.Close();
                }
            }
        }

        //public bool CheckExistAccount(Register userRegisterData)
        //{
        //    var query = _context.Set<User>()
        //        .Where(a => a.UserName == userRegisterData.UserName || a.Email == userRegisterData.Email);

        //    if (query.Any(a => userRegisterData.UserName.Contains(a.UserName) || userRegisterData.Email.Contains(a.Email)))
        //        return true;

        //    return false;
        //}

        //public bool ValidateData(Login userLoginData)
        //{
        //    var query = _context.Set<User>()
        //        .Select(a => a.UserName)
        //        .ToList();

        //    if (query.Contains(userLoginData.UserName))
        //        return true;

        //    return false;
                
        //}

        //public User GetUser(Login login)
        //{
        //    var result = _context.Set<User>()
        //        .FirstOrDefault(a => a.UserName == login.UserName);
       
        //    return result;
        //}
    }
}
