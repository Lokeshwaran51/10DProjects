namespace AmazonClone.MVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }  // Product name
        public decimal Price { get; set; }       // Unit price
        public int Quantity { get; set; }        // Quantity ordered
        public decimal Total { get; set; }       // Total price (Price * Quantity)
        public string? PaymentMode { get; set; }
    }
}
