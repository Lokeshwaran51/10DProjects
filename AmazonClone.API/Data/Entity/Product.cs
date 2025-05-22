using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonClone.API.Data.Entity
{
    [Table("Products", Schema = "AmazonClone")] 
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
