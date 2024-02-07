namespace CashRegister.Models
{
    public class Food
    {
        public int MenuItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryID { get; set; }
    }
}
