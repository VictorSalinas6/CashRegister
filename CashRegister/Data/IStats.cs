using CashRegister.Models;

namespace CashRegister.Data
{
    public interface IStats
    {
        public IEnumerable<Stats> GetMonthSales(int month);
        public decimal GetTaxes(Stats total);
        public Stats GetAllSales();
        public List<Stats> GetBestSoldItems(int month);
        public Customer GetBestCustomer(int month);
    }
}
