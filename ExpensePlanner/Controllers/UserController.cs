using ExpensePlanner.Models;
using ExpensePlanner.Services;
using ExpensePlanner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePlanner.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly int userId = StaticService.userId;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Akcja usuwająca użytkownika
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            TempData["IsLogged"] = true;

            if (_userService.DeleteUser(id))
            {
                return RedirectToAction("Get");
            }

            return BadRequest();
        }

        /// <summary>
        /// Akcja wyświetlająca liste użytkowników
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            TempData["IsAdmin"] = _userService.IsAdmin(userId);
            TempData["IsLogged"] = true;

            var allUsers = _userService.GetAllUser().ToList();

            return View(allUsers);
        }

        /// <summary>
        /// Akcja wyświetlająca obiekty(wydatki) innego użytkownika
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserExpenses(int Id)
        {
            TempData["IsAdmin"] = _userService.IsAdmin(userId);
            TempData["IsLogged"] = true;

            var userExpenses = _userService.GetUserExpenses(Id);

            return View(userExpenses);
        }
    }
}
