using ExpensePlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensePlanner.Services
{
    public class StaticService
    {
        /// <summary>
        /// Pole statyczne do przetrzymywania id użytkownika
        /// </summary>
        public static int userId { get; set; }
    }
}
