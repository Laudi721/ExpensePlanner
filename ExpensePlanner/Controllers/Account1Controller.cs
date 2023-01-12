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
		private readonly int userId = StaticService.userId;


        public Account1Controller(IAccountService accountService) 
		{ 
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult RegisterUser(RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["IsLogged"] = (bool)false;
                return View();
			}

            if (CheckExistAccount(dto))
            {
                ModelState.AddModelError(nameof(dto.Login), "Account with this login already exist");
                return View(dto);
            }

            var user = GetUser<RegisterUserDto>(dto.Login);
			if (user == null)
				TempData["IsLogged"] = (bool)false;

            _accountService.RegisterUser(dto);

            return RedirectToAction("Login");
        }

		[HttpGet]
		public IActionResult RegisterUser()
		{
            TempData["IsLogged"] = (bool)false;
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginDto dto)
		{
			if (!ModelState.IsValid)
				return View();

            var user = GetUser<LoginDto>(dto.Login);

            if (!_accountService.ValidateData(dto, userId))
			{
				ModelState.AddModelError(nameof(dto.Login), "Incorrect login or password");
                TempData["IsLogged"] = (bool)false;
                return View(dto);
			}
            TempData["IsAdmin"] = _accountService.IsAdmin(user.Id);

            AccountService.LoginHistory(dto.Login);

            TempData["IsLogged"] = user.IsLogged;

			return RedirectToAction("Get", "Expense");
		}

		[HttpGet]
		public async Task<IActionResult> Login()
		{
            TempData["IsLogged"] = false;

            return View();
		}

		public async Task<IActionResult> Logout()
		{
			_accountService.LogoutAsync(userId);

			return RedirectToAction("Login");
		}

		public bool CheckExistAccount(RegisterUserDto dto) => _accountService.CheckExistAccount(dto);

        public User GetUser<T>(string login) => _accountService.GetUser<T>(login);
    }
}
