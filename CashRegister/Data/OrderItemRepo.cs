using CashRegister.Models;
using Dapper;
using System.Data;

namespace CashRegister.Data
{
    public class OrderItemRepo : IOrderItemRepo
    {

        private readonly IDbConnection _conn;

        public OrderItemRepo(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<OrderItem> GetOrderedItems()
        {
            return _conn.Query<OrderItem>("SELECT * FROM orderitems");
        }
    }
}
