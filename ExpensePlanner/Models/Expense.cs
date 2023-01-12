using ExpensePlanner.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensePlanner.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public ExpenseType ExpenseType{ get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        [Column(TypeName="datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName="datetime2")]
        public DateTime? RealizedDate { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName="datetime2")]
        public DateTime? DeletedTime { get; set; }
    }
}
