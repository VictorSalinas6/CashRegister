using CashRegister.Models;

namespace CashRegister.Data
{
    public interface IOrderItemRepo
    {
        public IEnumerable<OrderItem> GetOrderedItems();
    }
}
