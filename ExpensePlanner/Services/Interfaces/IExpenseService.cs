using ExpensePlanner.Models.Dtos;
using ExpensePlanner.Models;

namespace ExpensePlanner.Services.Interfaces
{
    public interface IExpenseService
    {
        public void Post(ExpenseDto expenseint, int userId);

        public IEnumerable<Expense> Get(int userId);

        public IEnumerable<ExpenseDto> GetCompleted();

        public bool MarkAsDone(int id);

        public bool Delete(int id);
    }
}
