using ExpensePlanner.Models;
using ExpensePlanner.Models.Dtos;
using ExpensePlanner.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ExpensePlanner.Services
{
    public class AccountService : StaticService, IAccountService
    {
        private readonly ExpensePlannerDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(ExpensePlannerDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
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

        public bool RegisterUser(RegisterUserDto dto)
        { 
            if (dto.Password == dto.ConfirmPassword)
            {
                var newUser = new User()
                {
                    Login = dto.Login,
                    Password = dto.Password,
                    RoleId = dto.RoleId,
                };

                newUser.Password = _passwordHasher.HashPassword(newUser, newUser.Password);

                try
                {
                    _context.Users.Add(newUser);
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

        public void LogoutAsync(int userId)
        {
            var query = _context.Set<User>()
                .FirstOrDefault(a => a.Id == userId);

            query.IsLogged = false;
            _context.SaveChanges();
        }

        public bool CheckExistAccount(RegisterUserDto dto)
        {
            var query = _context.Set<User>()
                .Where(a => a.Login == dto.Login);

            if (query.Any(a => a.Login.Contains(dto.Login)))
                return true;

            return false;
        }

        public bool ValidateData(LoginDto dto, int userId)
        {
            var query = _context.Set<User>()
                .FirstOrDefault(a => a.Login == dto.Login && !a.IsDeleted);

            if (query != null)
            {
                var hashedPassword = _passwordHasher.VerifyHashedPassword(query, query.Password, dto.Password);

                if (query.Login == dto.Login && hashedPassword == PasswordVerificationResult.Success)
                {
                    StaticService.userId = query.Id;
                    query.IsLogged = true;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                return false;
            }
            

            return false;             
        }

        public int GetUserId()
        {
            var query = _context.Set<User>()
                .FirstOrDefault(a => a.Login == "admin");

            return query.Id;
        }

        public User GetUser<T>(string login)
        {
            var user = _context.Set<User>()
                .FirstOrDefault(a => a.Login == login);

            if (user == null)
                return null;

            return user;
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
