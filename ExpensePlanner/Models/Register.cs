using System.ComponentModel.DataAnnotations;

namespace ExpensePlanner.Models
{
	public class Register
	{
		[Required]
		public string UserName { get; set; }

		[EmailAddress, Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
