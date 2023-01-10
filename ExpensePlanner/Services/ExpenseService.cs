using ExpensePlanner.Models;
using ExpensePlanner.Models.Dtos;
using ExpensePlanner.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExpensePlanner.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ExpensePlannerDbContext _context;

        public ExpenseService(ExpensePlannerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Expense> Get()
        {
            var result = _context.Set<Expense>()
                .Include(a => a.User)
                .Where(a => !a.IsCompleted && !a.IsDeleted)
                .ToList();

            return result;
        }

        public void Post(ExpenseDto expense)
        {
            var model = new Expense();

            CustomGetMapping(model, expense);
            try
            {
                _context.Set<Expense>().Add(model);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception("Error", e);
            }
        }

        public IEnumerable<ExpenseDto> GetCompleted()
        {
            var query = _context.Set<Expense>()
                .Include(a => a.User)
                .Where(a => a.IsCompleted);

            var data = query.Select(a => new
            {
                a.Id,
                a.Name,
                a.Amount,
                a.Description,
                a.ExpenseType,
                a.IsCompleted,
                a.CreateDate,
                a.RealizedDate,
            }).ToList();

            var result = data.Select(a => new ExpenseDto
            {
                Id = a.Id,
                Name = a.Name,
                Amount = a.Amount,
                Description = a.Description,
                ExpenseType = a.ExpenseType,
                IsCompleted = a.IsCompleted,
                CreateDate = a.CreateDate,
                RealizedDate = a.RealizedDate,
            });

            return result;
        }        

        private void CustomGetMapping(Expense model, ExpenseDto dto)
        {
            model.Name = dto.Name;
            model.Description = dto.Description;
            model.Amount = dto.Amount;
            model.ExpenseType = dto.ExpenseType;
            model.CreateDate = DateTime.Now;
            model.UserId = 1;
        }

        public bool MarkAsDone(int id)
        {
            var model = _context.Set<Expense>()
                .FirstOrDefault(a => a.Id == id);

            if (!model.IsCompleted)
            {
                model.IsCompleted = true;
                model.RealizedDate = DateTime.Now;
            }
            else
            {
                return false;
            }

            try
            {                
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

            return true;
        }

        public bool Delete(int id)
        {
            var model = _context.Set<Expense>()
                .FirstOrDefault(a => a.Id == id);

            try
            {
                model.IsDeleted = true;
                model.DeletedTime = DateTime.Now;

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }

            return true;
        }
    }
}
