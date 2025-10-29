using AmazonClone.API.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartServices.Data.Entity
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string ImageUrl { get; set; } = string.Empty;

        [Column(TypeName = "int")]
        public int? CategoryId { get; set; }

        [Column(TypeName = "int")]
        public int? SubCategoryId { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SubCategoryName { get; set; } = string.Empty;

        public virtual User? User { get; set; }
        public virtual Category? Category { get; set; }
        public virtual SubCategory? SubCategory { get; set; }
    }
}
