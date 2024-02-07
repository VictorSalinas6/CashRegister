using CashRegister.Models;
using Dapper;
using System.Data;

namespace CashRegister.Data
{
    public class OrderRepo : IOrderRepo
    {

        private readonly IDbConnection _conn;

        public OrderRepo(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var orders = _conn.Query<Order>("SELECT * FROM orders");

            return orders;
        }

        public IEnumerable<Order> GetMonth(int month)
        {
            var orders = _conn.Query<Order>("SELECT * FROM orders ORDER BY orderID");

            var customers = _conn.Query<Customer>("SELECT * FROM customers;");

            var ordersPerMonth = new List<Order>();
            var orderItems = _conn.Query<OrderItem>("SELECT * FROM orderitems");

            decimal newTotal = 0;

            //Modify the values on Orders
            foreach (var order in orders)
            {
                foreach (var item in orderItems)
                {
                    if (order.OrderID == item.OrderID)
                    {
                        newTotal += item.Subtotal;
                    }
                }

                order.TotalAmount = newTotal;
                newTotal = 0;
            }

            foreach (var order in orders)
            {

                if (month == order.OrderDate.Month)
                {
                    ordersPerMonth.Add(order);
                }
                else if (month == 0)
                {
                    ordersPerMonth.Add(order);
                }
            }

            foreach (var order in ordersPerMonth)
            {
                foreach (var customer in customers)
                {
                    if (order.CustomerID == customer.CustomerID.ToString())
                    {
                        order.CustomerName = customer.FirstName + " " + customer.LastName;
                    }
                }
            }

            return ordersPerMonth;
        }

        public IEnumerable<OrderItem> GetItemsOrder(int id)
        {

            var orderItems = _conn.Query<OrderItem>("SELECT * FROM orderitems WHERE OrderID = @id", new { id });

            var menu = _conn.Query<Food>("SELECT * FROM menuitems;");

            foreach (var item in menu)
            {
                foreach (var food in orderItems)
                {
                    if (item.MenuItemID.ToString() == food.MenuItemID)
                    {
                        food.MenuItemID = item.Name;
                    }
                }
            }

            return orderItems;
        }

        public decimal GetTotal(IEnumerable<Order> total)
        {
            decimal totalTotal = 0;

            foreach (var item in total)
            {
                totalTotal += item.TotalAmount;
            }

            return totalTotal;
        }

        public decimal GetTotal(IEnumerable<OrderItem> total)
        {
            decimal totalTotal = 0;

            foreach (var item in total)
            {
                totalTotal += item.Subtotal;
            }

            return totalTotal;
        }

        public int GetAmountItems(IEnumerable<OrderItem> orders)
        {
            int totalTotal = 0;

            foreach (var item in orders)
            {
                totalTotal += item.Quantity;
            }

            return totalTotal;
        }

        public Customer GetBestCustomer(IEnumerable<Order> total)
        {
            decimal customer1 = 0;
            decimal customer2 = 0;
            decimal customer3 = 0;
            decimal customer4 = 0;
            decimal customer5 = 0;
            decimal customer6 = 0;
            decimal customer7 = 0;
            decimal customer8 = 0;
            decimal customer9 = 0;
            decimal customer10 = 0;

            var answer = new Customer();

            foreach (var item in total)
            {
                switch (item.CustomerID)
                {
                    case "1":
                        customer1 += item.TotalAmount;
                        break;
                    case "2":
                        customer2 += item.TotalAmount;
                        break;
                    case "3":
                        customer3 += item.TotalAmount;
                        break;
                    case "4":
                        customer4 += item.TotalAmount;
                        break;
                    case "5":
                        customer5 += item.TotalAmount;
                        break;
                    case "6":
                        customer6 += item.TotalAmount;
                        break;
                    case "7":
                        customer7 += item.TotalAmount;
                        break;
                    case "8":
                        customer8 += item.TotalAmount;
                        break;
                    case "9":
                        customer9 += item.TotalAmount;
                        break;
                    case "10":
                        customer10 += item.TotalAmount;
                        break;
                }
            }

            Dictionary<int, decimal> data = new Dictionary<int, decimal>
            {
                {1, customer1}, {2, customer2}, {3, customer3}, {4, customer4}, {5, customer5}, {6, customer6},
                {7, customer7}, {8, customer8}, {9, customer9}, {10, customer10}
            };

            var bestClient = data.OrderByDescending(x => x.Value).Take(1);

            var customers = _conn.Query<Customer>("SELECT * FROM customers;");

            foreach (var client in bestClient)
            {
                foreach (var customer in customers)
                {
                    if (client.Key == customer.CustomerID)
                    {
                        answer = customer;
                    }
                }
            }

            return answer;

        }
    }
}
