using ExpensePlanner.Models;
using ExpensePlanner.Models.Dtos;

namespace ExpensePlanner.Services.Interfaces
{
    public interface IAccountService : IGenericService
    {
        public bool RegisterUser(RegisterUserDto dto);

		public bool CheckExistAccount(RegisterUserDto dto);

		public bool ValidateData(LoginDto dto, int userId);

		public int GetUserId();

		public void LogoutAsync(int userId);
	}
}
