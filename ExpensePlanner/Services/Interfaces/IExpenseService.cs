using ExpensePlanner.Models.Dtos;
using ExpensePlanner.Models;

namespace ExpensePlanner.Services.Interfaces
{
    public interface IExpenseService : IGenericService
    {
        public void Post(ExpenseDto expenseint, int userId);

        public IEnumerable<Expense> Get(int userId);

        public IQueryable<ExpenseDto> GetCompleted(int userId);

        public bool MarkAsDone(int id);

        public bool Delete(int id);
    }
}
