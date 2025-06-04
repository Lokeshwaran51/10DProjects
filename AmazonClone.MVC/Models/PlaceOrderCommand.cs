namespace AmazonClone.MVC.Models
{
    public class PlaceOrderCommand
    {
        public OrderDetail Order { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; } 
        public int? UserId { get; set; }   
    }
        
    public class OrderDetail
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string PaymentMode { get; set; }
    }
}
