using ExpensePlanner.Models;
using ExpensePlanner.Models.Dtos;
using ExpensePlanner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpensePlanner.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;

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
            var result = _expenseService.Get();

            return View(result);
        }

        public IActionResult Post(ExpenseDto expense)
        {
            if (!ModelState.IsValid)
            {
                return View(expense);
            }

            _expenseService.Post(expense);

            return RedirectToAction("Get");
        }

        public IActionResult Delete(int id)
        {
            _expenseService.Delete(id);

            return RedirectToAction("Get");
        }

        public IActionResult GetCompleted()
        {
            var result = _expenseService.GetCompleted();

            return View(result);
        }

        [Route("/MarkAsDone/{id}")]
        public IActionResult MarkAsDone(int id)
        {
            _expenseService.MarkAsDone(id);

            return RedirectToAction("Get");
        }
    }
}
