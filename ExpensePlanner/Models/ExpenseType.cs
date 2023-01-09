using System.ComponentModel.DataAnnotations;

namespace ExpensePlanner.Models
{
    public class ExpenseType
    {
        public ExpenseType()
        {
            Expenses = new List<Expense>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Expense> Expenses { get; set; }
    }
}
