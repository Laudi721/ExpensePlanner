using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensePlanner.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        [Column(TypeName ="datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        public ExpenseType ExpenseType { get; set; }

        public int ExpenseTypeId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
