using CashRegister.Models;
using Dapper;
using System.Data;

namespace CashRegister.Data
{
    public class StatsRepo : IStats
    {

        private readonly IDbConnection _conn;

        public StatsRepo(IDbConnection conn)
        {
            _conn = conn;
        }

        public Stats GetAllSales()
        {
            var sales = _conn.Query<OrderItem>("Select * from orderitems");

            decimal total = 0;

            foreach (var item in sales)
            {
                total = total + item.Subtotal;
            }

            Stats NewPage = new Stats();

            NewPage.TotalSales = total;

            return NewPage;
        }

        public List<Stats> GetBestSoldItems(int month)
        {
            var orders = _conn.Query<Order>("SELECT * FROM orders");
            var orderItems = _conn.Query<OrderItem>("SELECT * FROM orderitems");
            var Menu = _conn.Query<Food>("SELECT * FROM menuitems");

            var top3 = new List<Stats>();

            int item1 = 0;
            int item2 = 0;
            int item3 = 0;
            int item4 = 0;
            int item5 = 0;
            int item6 = 0;
            int item7 = 0;
            int item8 = 0;
            int item9 = 0;
            int item10 = 0;
            int item11 = 0;
            int item12 = 0;
            int item13 = 0;
            int item14 = 0;
            int item15 = 0;
            int item16 = 0;
            int item17 = 0;
            int item18 = 0;

            foreach(var order in orders)
            {
                foreach(var item in orderItems)
                {
                    if (order.OrderID == item.OrderID)
                    {
                        if (order.OrderDate.Month == month || month == 0)
                        {
                            switch (item.MenuItemID)
                            {
                                case "1":
                                    item1 += item.Quantity;
                                    break;
                                case "2":
                                    item2 += item.Quantity;
                                    break;
                                case "3":
                                    item3 += item.Quantity;
                                    break;
                                case "4":
                                    item4 += item.Quantity;
                                    break;
                                case "5":
                                    item5 += item.Quantity;
                                    break;
                                case "6":
                                    item6 += item.Quantity;
                                    break;
                                case "7":
                                    item7 += item.Quantity;
                                    break;
                                case "8":
                                    item8 += item.Quantity;
                                    break;
                                case "9":
                                    item9 += item.Quantity;
                                    break;
                                case "10":
                                    item10 += item.Quantity;
                                    break;
                                case "11":
                                    item11 += item.Quantity;
                                    break;
                                case "12":
                                    item12 += item.Quantity;
                                    break;
                                case "13":
                                    item13 += item.Quantity;
                                    break;
                                case "14":
                                    item14 += item.Quantity;
                                    break;
                                case "15":
                                    item15 += item.Quantity;
                                    break;
                                case "16":
                                    item16 += item.Quantity;
                                    break;
                                case "17":
                                    item17 += item.Quantity;
                                    break;
                                case "18":
                                    item18 += item.Quantity;
                                    break;
                            }
                        }
                    }
                }
            }

            Dictionary<int, int> data = new Dictionary<int, int>
            {
                {1, item1}, {2, item2}, {3, item3}, {4, item4}, {5, item5}, {6, item6},
                {7, item7}, {8, item8}, {9, item9}, {10, item10}, {11, item11}, {12, item12},
                {13, item13}, {14, item14}, {15, item15}, {16, item16}, {17, item17}, {18, item18}
            };

            var largestThree = data.OrderByDescending(x => x.Value).Take(3);

            foreach (var food in Menu)
            {
                foreach (var item in largestThree)
                {
                    if (item.Key == food.MenuItemID)
                    {
                        Stats stats = new Stats();
                        stats.MenuName = food.Name;
                        stats.MenuQuantity = item.Value;
                        stats.MenuTotal = (item.Value * food.Price);

                        top3.Add(stats);
                    }
                }
            }

            return top3;
        }

        public IEnumerable<Stats> GetMonthSales(int month)
        {
            var results = new List<Stats>();

            var orders = _conn.Query<Order>("SELECT * FROM orders ORDER BY orderID");

            Stats NewPage = new Stats();

            var orderItems = _conn.Query<OrderItem>("SELECT * FROM orderitems");

            decimal newTotal = 0;

            foreach (var order in orders)
            {
                foreach (var item in orderItems)
                {
                    if (order.OrderID == item.OrderID)
                    {
                        if (month == order.OrderDate.Month || month == 0)
                        {
                            newTotal += item.Subtotal;
                        }
                    }
                }
            }

            switch (month)
            {
                case 0:
                    NewPage.Period = "2023";
                    break;
                case 1:
                    NewPage.Period = "January 2023";
                    break;
                case 2:
                    NewPage.Period = "February 2023";
                    break;
                case 3:
                    NewPage.Period = "March 2023";
                    break;
                case 4:
                    NewPage.Period = "April 2023";
                    break;
                case 5:
                    NewPage.Period = "May 2023";
                    break;
                case 6:
                    NewPage.Period = "June 2023";
                    break;
                case 7:
                    NewPage.Period = "July 2023";
                    break;
                case 8:
                    NewPage.Period = "August 2023";
                    break;
                case 9:
                    NewPage.Period = "September 2023";
                    break;
                case 10:
                    NewPage.Period = "Octuber 2023";
                    break;
                case 11:
                    NewPage.Period = "November 2023";
                    break;
                case 12:
                    NewPage.Period = "December 2023";
                    break;
            }

            NewPage.TotalOfMonth = newTotal;

            NewPage.Taxes = GetTaxes(NewPage);

            results.Add(NewPage);

            var top3 = GetBestSoldItems(month);

            return results.Concat(top3);
        }

        public decimal GetTaxes(Stats total)
        {
            //Asuming the tax is 2.25%

            var taxes = 2.25m / 100;

            return taxes * total.TotalOfMonth;
        }

        public Customer GetBestCustomer(int month)
        {
            var total = _conn.Query<Order>("SELECT * FROM orders;");
            var customers = _conn.Query<Customer>("SELECT * FROM customers");

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
                if (item.OrderDate.Month == month || month == 0)
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
            }

            Dictionary<int, decimal> data = new Dictionary<int, decimal>
            {
                {1, customer1}, {2, customer2}, {3, customer3}, {4, customer4}, {5, customer5}, {6, customer6},
                {7, customer7}, {8, customer8}, {9, customer9}, {10, customer10}
            };

            var bestClient = data.OrderByDescending(x => x.Value).Take(1);

            foreach (var client in bestClient)
            {
                foreach (var customer in customers)
                {
                    if (client.Key == customer.CustomerID)
                    {
                        answer = customer;
                        answer.AmountSpend = client.Value;
                    }
                }
            }

            return answer;
        }
    }
}
