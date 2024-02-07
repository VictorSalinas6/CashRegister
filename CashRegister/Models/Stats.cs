namespace CashRegister.Models
{
    public class Stats
    {
        public decimal TotalOfMonth { get; set; }
        public decimal Taxes { get; set; }
        public decimal TotalSales { get; set; }
        public string Period { get; set; }
        public string MenuName { get; set; }
        public int MenuQuantity { get; set; }
        public decimal MenuTotal { get; set; }
        public string ClientName { get; set; }
        public decimal ClientTotal { get; set; }

    }
}
