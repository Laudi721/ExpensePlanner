using ExpensePlanner.Models;
using ExpensePlanner.Models.Dtos;
using ExpensePlanner.Services;
using ExpensePlanner.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePlanner.Controllers
{
    public class Account1Controller : Controller
    {
        private readonly IAccountService _accountService;
		public int userId { get; set; }

        public Account1Controller(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult RegisterUser(RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
				return View();
			}

            _accountService.RegisterUser(dto);

            return RedirectToAction("Login");
        }

		[HttpGet]
		public IActionResult RegisterUser()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginDto dto)
		{
			if (!ModelState.IsValid)
				return View();

			if (!_accountService.ValidateData(dto))
			{
				ModelState.AddModelError(nameof(dto.Login), "Incorrect login or password");
				return View(dto);
			}

			userId = _accountService.GetUserId();

			return RedirectToAction("Get", "Expense");
		}


		[HttpGet]
		public async Task<IActionResult> Login()
		{
			return View();
		}

		public async Task<IActionResult> Logout()
		{
			
			return RedirectToAction("Login");
		}

		public bool CheckExistAccount(RegisterUserDto dto) => _accountService.CheckExistAccount(dto);
	}
}
