using CashRegister.Models;
using Dapper;
using System.Data;

namespace CashRegister.Data
{
    public class FoodRepository : IFoodRepository
    {

        private readonly IDbConnection _conn;

        public FoodRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<Food> GetMenu()
        {
            var menu = _conn.Query<Food>("SELECT * FROM menuitems;");
            var categories = _conn.Query<Category>("SELECT * FROM categories;");

            foreach (var item in menu)
            {
                foreach (var category in categories)
                {
                    if (item.CategoryID == category.CategoryID)
                    {
                        item.CategoryID = category.CategoryName;
                    }
                }
            }

            return menu;
        }
        public Food GetItem(int id)
        {
            var item = _conn.QuerySingle<Food>("SELECT * FROM menuitems WHERE MenuItemID = @id", new { id });
            var categories = _conn.Query<Category>("SELECT * FROM categories;");

            foreach (var category in categories)
            {
                if (item.CategoryID == category.CategoryID)
                {
                    item.CategoryID = category.CategoryName;
                }
            }

            return item;
        }

        public void ModifyItem(Food food)
        {
            _conn.Execute("UPDATE menuitems SET Name = @name, Description = @description, Price = @price WHERE ProductID = @id",
                new { name = food.Name, description = food.Description, price = food.Price, id = food.MenuItemID });
        }
    }
}
