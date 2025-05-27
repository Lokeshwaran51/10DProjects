namespace AmazonClone.API.Data.Entity
{
    public class AddToCartRequest
    {
        public string Email { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
