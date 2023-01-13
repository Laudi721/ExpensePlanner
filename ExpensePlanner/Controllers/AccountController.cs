using ExpensePlanner.Models;
using ExpensePlanner.Models.Dtos;
using ExpensePlanner.Services;
using ExpensePlanner.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePlanner.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
		private readonly int userId = StaticService.userId;


        public AccountController(IAccountService accountService) 
		{ 
            _accountService = accountService;
        }

        /// <summary>
        /// Akcja rejestrujaca użytkownika
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
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

            if (!_accountService.RegisterUser(dto))
            {
                ModelState.AddModelError(nameof(dto.Login), "the passwords do not match. Try again");
                return View(dto);
            }

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Akcja zwracajaca pusty widok
        /// </summary>
        /// <returns></returns>
		[HttpGet]
		public IActionResult RegisterUser()
		{
            TempData["IsLogged"] = (bool)false;
            return View();
		}

        /// <summary>
        /// Akcja logujaca użytkownika do aplikacji
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Akcja zwracajaca pusty widok
        /// </summary>
        /// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Login()
		{
            TempData["IsLogged"] = false;

            return View();
		}

        /// <summary>
        /// Akcja wylogowujaca
        /// </summary>
        /// <returns></returns>
		public async Task<IActionResult> Logout()
		{
			_accountService.LogoutAsync(userId);

			return RedirectToAction("Login");
		}

        /// <summary>
        /// Metoda sprawdzająca czy uzytkownik o takim loginie istnieje w bazie
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
		public bool CheckExistAccount(RegisterUserDto dto) => _accountService.CheckExistAccount(dto);

        /// <summary>
        /// Metoda zwracajaca dane uzytkownika
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="login"></param>
        /// <returns></returns>
        public User GetUser<T>(string login) => _accountService.GetUser<T>(login);
    }
}
