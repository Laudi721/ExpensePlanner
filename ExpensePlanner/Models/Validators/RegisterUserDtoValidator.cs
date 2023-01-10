using ExpensePlanner.Models.Dtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ExpensePlanner.Models.Validators
{
	public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
	{
		private readonly ExpensePlannerDbContext _context;
		public RegisterUserDtoValidator(ExpensePlannerDbContext dbContext)
		{
			RuleFor(a => a.Login)
				.Custom((value, context) =>
				{
					var loginInUse = dbContext.Users.Any(b => b.Login == value);
					if(loginInUse)
					{
						context.AddFailure("Login", "That login i taken");
					}
				});
			RuleFor(a => a.Password).MinimumLength(6);
		}
	}
}
