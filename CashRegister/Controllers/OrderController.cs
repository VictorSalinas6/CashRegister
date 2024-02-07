using CashRegister.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace CashRegister.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepo repo;

        public OrderController(IOrderRepo repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(string OrderDateMonth)
        {
            int selectedMonth = Convert.ToInt32(OrderDateMonth);
            var orders = repo.GetMonth(selectedMonth);
            orders.LastOrDefault().TotalSelected = repo.GetTotal(orders);
            return View(orders);
        }

        public IActionResult ViewItems(int id)
        {
            var items = repo.GetItemsOrder(id);
            items.LastOrDefault().TotalSelected = repo.GetTotal(items);
            items.LastOrDefault().AmountofItems = repo.GetAmountItems(items);
            return View(items);
        }
    }
}
