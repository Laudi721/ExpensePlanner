using ExpensePlanner.Models;

namespace ExpensePlanner.Services.Interfaces
{
    public interface IGenericService
    {
        public User GetUser<T>(string login);

        public bool IsAdmin(int userId);
    }
}
