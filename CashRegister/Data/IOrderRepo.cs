using CashRegister.Models;

namespace CashRegister.Data
{
    public interface IOrderRepo
    {
        public IEnumerable<Order> GetAllOrders();
        public IEnumerable<Order> GetMonth(int month);
        public IEnumerable<OrderItem> GetItemsOrder(int id);
        public decimal GetTotal(IEnumerable<Order> total);
        public decimal GetTotal(IEnumerable<OrderItem> total);
        public int GetAmountItems(IEnumerable<OrderItem> orders);
        public Customer GetBestCustomer(IEnumerable<Order> total);

    }
}
