using ExpensePlanner.Models;
using ExpensePlanner.Models.Dtos;
using ExpensePlanner.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpensePlanner.Services
{
    public class AccountService : IAccountService
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

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Login = dto.Login,
                Password = dto.Password,
                RoleId = dto.RoleId,
            };

            newUser.Password = _passwordHasher.HashPassword(newUser, newUser.Password);

            _context.Users.Add(newUser);
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

        public bool ValidateData(LoginDto dto)
        {
            var query = _context.Set<User>()
                .FirstOrDefault(a => a.Login == dto.Login);

            var hashedPassword = _passwordHasher.VerifyHashedPassword(query, query.Password, dto.Password);

            if (query.Login == dto.Login && hashedPassword == PasswordVerificationResult.Success)
                return true;

            return false;

        }

		public int GetUserId()
        {
			var query = _context.Set<User>()
				.FirstOrDefault(a => a.Login == "admin");

            return query.Id;
		}

		//public User GetUser(LoginDto login)
		//{
		//    var user = _context.Set<User>()
		//        .FirstOrDefault(a => a.Login == login.Login);

		//    return user;
		//}
	}
}
