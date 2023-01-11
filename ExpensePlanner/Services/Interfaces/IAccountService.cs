using ExpensePlanner.Models;
using ExpensePlanner.Models.Dtos;

namespace ExpensePlanner.Services.Interfaces
{
    public interface IAccountService : GenericInterface
    {
        public void RegisterUser(RegisterUserDto dto);
		public bool CheckExistAccount(RegisterUserDto dto);
		public bool ValidateData(LoginDto dto, int userId);

		public int GetUserId();
		//public User GetUser<T>(string login);

		public void LogoutAsync(int userId);
	}
}
