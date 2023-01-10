using System.ComponentModel.DataAnnotations;

namespace ExpensePlanner.Models
{
    public class User
    {
        public User()
        {
            Expenses = new List<Expense>();
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public virtual List<Expense> Expenses { get; set; }

        public Role Role { get; set; }

        public int RoleId { get; set; }
    }
}
