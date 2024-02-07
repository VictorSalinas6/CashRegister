using CashRegister.Data;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.Controllers
{
    public class StatsController : Controller
    {
        private readonly IStats repo;

        public StatsController(IStats repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(string OrderDateMonth)
        {
            int selectedMonth = Convert.ToInt32(OrderDateMonth);
            var stats = repo.GetMonthSales(selectedMonth);
            stats.LastOrDefault().ClientName = repo.GetBestCustomer(selectedMonth).FirstName + " " + repo.GetBestCustomer(selectedMonth).LastName;
            stats.LastOrDefault().ClientTotal = repo.GetBestCustomer(selectedMonth).AmountSpend;
            return View(stats);
        }
    }
}
