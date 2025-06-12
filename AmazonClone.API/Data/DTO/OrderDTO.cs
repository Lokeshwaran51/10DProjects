namespace AmazonClone.API.Data.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; } 
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public string? PaymentMode { get; set; }
        public int? UserId { get; set; }
    }
}
