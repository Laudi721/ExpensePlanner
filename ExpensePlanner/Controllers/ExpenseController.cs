using ExpensePlanner.Models;
using ExpensePlanner.Models.Dtos;
using ExpensePlanner.Services;
using ExpensePlanner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpensePlanner.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly int userId = StaticService.userId;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Get()
        {
            TempData["IsLogged"] = true;
            var result = _expenseService.Get(userId);

            return View(result);
        }

        public IActionResult Post(ExpenseDto expense)
        {
            if (!ModelState.IsValid)
            {
                TempData["IsLogged"] = true;
                return View(expense);
            }

			_expenseService.Post(expense, userId);

            return RedirectToAction("Get");
        }

        public IActionResult Delete(int id)
        {
            TempData["IsLogged"] = true;
            _expenseService.Delete(id);

            return RedirectToAction("Get");
        }

        public IActionResult GetCompleted()
        {
            TempData["IsLogged"] = true;
            var result = _expenseService.GetCompleted();

            return View(result);
        }

        [Route("/MarkAsDone/{id}")]
        public IActionResult MarkAsDone(int id)
        {
            TempData["IsLogged"] = true;
            _expenseService.MarkAsDone(id);

            return RedirectToAction("Get");
        }
    }
}
