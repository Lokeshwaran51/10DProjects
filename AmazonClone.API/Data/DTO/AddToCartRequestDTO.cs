namespace AmazonClone.API.Data.DTO
{
    public class AddToCartRequestDto
    {
        public string? Email { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}   
