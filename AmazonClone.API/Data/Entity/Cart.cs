using AmazonClone.API.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Cart")]
public class Cart
{
    [Key]
    [Column(TypeName = "int")]
    public int CartId { get; set; }

    [Column(TypeName = "int")]
    public int UserId { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
