using CashRegister.Models;

namespace CashRegister.Data
{
    public interface IFoodRepository
    {
        public IEnumerable<Food> GetMenu();
        public Food GetItem(int id);
        public void ModifyItem(Food food);
    }
}
