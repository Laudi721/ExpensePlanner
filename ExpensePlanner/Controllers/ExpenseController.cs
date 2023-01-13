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
            TempData["IsAdmin"] = _expenseService.IsAdmin(userId);
            TempData["IsLogged"] = true;

            var result = _expenseService.Get(userId);

            return View(result);
        }

        public IActionResult Post(ExpenseDto expense)
        {
            TempData["IsAdmin"] = _expenseService.IsAdmin(userId);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(expense.Amount), "Wrong data type in Amount");
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
            TempData["IsAdmin"] = _expenseService.IsAdmin(userId);
            TempData["IsLogged"] = true;

            var result = _expenseService.GetCompleted(userId);

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
