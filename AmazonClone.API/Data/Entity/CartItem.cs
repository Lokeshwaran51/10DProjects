using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonClone.API.Data.Entity
{
    [Table("CartItem")]
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; } // primary key

        [Column(TypeName = "int")]
        public int CartId { get; set; } // foreign key to Cart

        [ForeignKey("CartId")]
        [Required]  // Ensures every CartItem must have a Cart
        public virtual Cart Cart { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string ProductName { get; set; } = string.Empty;

        [Column(TypeName = "int")]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName ="int")]
        public User userId { get; set; }    
    }
}
