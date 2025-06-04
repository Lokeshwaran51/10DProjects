using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonClone.API.Data.Entity
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        [Column(TypeName = "int")]
        public int CartId { get; set; }

        [Column(TypeName = "int")]
        public int UserId { get; set; }

        [Column(TypeName = "int")]
        public int? ProductId { get; set; }
        [Column(TypeName = "Varchar(200)")]
        public string Email { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
