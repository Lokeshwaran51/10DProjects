namespace AmazonClone.MVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string? SubCategoryName { get; set; }

        /*public User User { get; set; }*/
        public Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
