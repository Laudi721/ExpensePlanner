namespace ExpensePlanner.Models.Dtos
{
	public class UserDto
	{
		public int Id { get; set; }

		public string Login { get; set; }

		public string RoleName { get; set; }

		public List<ExpenseDto> UserExpsenses { get; set; }
	}
}
