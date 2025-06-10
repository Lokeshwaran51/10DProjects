namespace AmazonClone.MVC.Models
{
    public class PlaceOrderDTO
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string PaymentMode { get; set; }
    }
}
