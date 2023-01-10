using ExpensePlanner.Models;

namespace ExpensePlanner.Services.Interfaces
{
    public interface IAccountService
    {
        public bool CheckExistAccount(Register userRegisterData);
        public bool ValidateData(Login userRegisterData);
        public User GetUser(Login login);
    }
}
