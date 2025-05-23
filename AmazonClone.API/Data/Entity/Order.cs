using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonClone.API.Data.Entity
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; } // Primary key

        [Required]
        public int Id { get; set; } // Consider renaming to ProductId if that's its purpose

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string ProductName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        /*[Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? PaymentMode { get; set; }*/

        [Column(TypeName = "int")]
        public int? UserId { get; set; }

        // Optional navigation property
        // [ForeignKey("UserId")]
        // public User User { get; set; }
    }
}
