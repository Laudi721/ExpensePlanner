using ExpensePlanner.Models;

namespace ExpensePlanner.Services.Interfaces
{
    public interface GenericInterface
    {
        public User GetUser<T>(string login);
    }
}
