using ExpensePlanner.Models;
using ExpensePlanner.Models.Dtos;

namespace ExpensePlanner.Services.Interfaces
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto dto);
		public bool CheckExistAccount(RegisterUserDto dto);
		public bool ValidateData(LoginDto dto);

		public int GetUserId();
		//public User GetUser(LoginDto dto);

		//Task LogoutAsync();
	}
}
