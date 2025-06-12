namespace AmazonClone.MVC.Models
{
    public class AddToCartDTO
    {
        public string Email { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
