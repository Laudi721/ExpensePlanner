using ExpensePlanner.Models;

namespace ExpensePlanner.Services.Interfaces
{
    public interface IAccountService
    {
        public bool CheckExistAccount(Register userRegisterData);
    }
}
