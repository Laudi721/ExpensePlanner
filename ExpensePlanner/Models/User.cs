using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensePlanner.Models
{
    public class User //: IdentityUser
    {
        public User()
        {
            Expenses = new List<Expense>();
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public virtual List<Expense> Expenses { get; set; }

        public Role Role { get; set; }

        public int RoleId { get; set; }

        public bool IsLogged { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedTime { get; set; }
    }
}
