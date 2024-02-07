using CashRegister.Data;
using CashRegister.Models;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.Controllers
{
    public class FoodController : Controller
    {

        private readonly IFoodRepository repo;

        public FoodController(IFoodRepository repo)
        {
            this.repo = repo;
        }


        public IActionResult Menu()
        {
            var foods = repo.GetMenu();
            return View(foods);
        }

        public IActionResult ViewItem(int id)
        {
            var item = repo.GetItem(id);
            return View(item);
        }

        public IActionResult ModifyItem(int id)
        {
            Food food = repo.GetItem(id);

            return View(food);
        }

        public IActionResult ModifyItemToDatabase(Food food)
        {
            repo.ModifyItem(food);
            return RedirectToAction("ViewItem", new { id = food.MenuItemID });
        }
    }
}
