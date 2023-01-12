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

        public IActionResult Delete(int id)
        {
            TempData["IsLogged"] = true;

            if (_userService.DeleteUser(id))
            {
                return RedirectToAction("Get");
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            TempData["IsAdmin"] = _userService.IsAdmin(userId);
            TempData["IsLogged"] = true;

            var allUsers = _userService.GetAllUser().ToList();

            return View(allUsers);
        }

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
