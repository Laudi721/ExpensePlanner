using ExpensePlanner.Models.Dtos;

namespace ExpensePlanner.Services.Interfaces
{
    public interface IUserService : IGenericService
    {
        public bool DeleteUser(int userId);

        public IQueryable<UserDto> GetAllUser();

        public IQueryable<ExpenseDto> GetUserExpenses(int userId);
    }
}
