using ExpensePlanner.Models.Dtos;

namespace ExpensePlanner.Services.Interfaces
{
    public interface IUserService : IGenericService
    {
        public bool DeleteUser(int userId);

        public IEnumerable<UserDto> GetAllUser();

        public IQueryable<ExpenseDto> GetUserExpenses(int userId);
    }
}
