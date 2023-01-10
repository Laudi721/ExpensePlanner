﻿using ExpensePlanner.Models;
using ExpensePlanner.Services;
using ExpensePlanner.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePlanner.Controllers
{
    public class AccountController : Controller, IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountService _accountService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IAccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Login(Login userLoginData)
		{
			if (!ModelState.IsValid)
				return View(userLoginData);

            var user = GetUser(userLoginData);

            if (!ValidateData(userLoginData) || !await _userManager.CheckPasswordAsync(user, userLoginData.Password))
            {

                ModelState.AddModelError(nameof(userLoginData.UserName), "Incorrect login or password");
                return View(userLoginData);
            }

            await _signInManager.PasswordSignInAsync(userLoginData.UserName, userLoginData.Password, true, false);

            AccountService.LoginHistory(userLoginData.UserName);

			return RedirectToAction("Get", "Expense");
		}

		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register userRegisterData)
        {
            if (!ModelState.IsValid)
                return View(userRegisterData);

            if (CheckExistAccount(userRegisterData))
            {
                ModelState.AddModelError(nameof(userRegisterData.UserName), "Account with this username or email already exists");
                return View(userRegisterData);
            }

            var newUser = new User
            {
                UserName = userRegisterData.UserName,
                Email = userRegisterData.Email
            };

            await _userManager.CreateAsync(newUser, userRegisterData.Password);

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        public bool CheckExistAccount(Register userRegisterData) => _accountService.CheckExistAccount(userRegisterData);
        //{
        //    return _accountService.CheckExistAccount(userRegisterData);
        //}

        public bool ValidateData(Login userLoginData) => _accountService.ValidateData(userLoginData);
        

        public User GetUser(Login login) => _accountService.GetUser(login);
        //{
        //    var user = _accountService.User(login);

        //    return user;
        //}
    }
}