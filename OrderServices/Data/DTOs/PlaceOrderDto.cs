namespace OrderServices.Data.DTOs
{
    public class PlaceOrderDto
    {
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
