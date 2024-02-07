namespace CashRegister.Models
{
    public class OrderItem
    {
        public string OrderItemID { get; set; }
        public string OrderID { get; set; }
        public string MenuItemID { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TotalSelected { get; set; }
        public int AmountofItems { get; set; }
    }
}
