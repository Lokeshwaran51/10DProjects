using System.ComponentModel.DataAnnotations;
using static AmazonClone.MVC.Models.ViewModel;

namespace AmazonClone.MVC.Models
{
    public class CartModel
    {
        public class Cart
        {
            [Key]
            public int CartId { get; set; }

            [Required]
            public int UserId { get; set; }

            public virtual User User { get; set; }

            public virtual ICollection<CartItem> CartItems { get; set; }

            [DataType(DataType.DateTime)]
            public DateTime CreatedDate { get; set; }

            [DataType(DataType.DateTime)]
            public DateTime? UpdatedDate { get; set; }
        }

        public class CartItem
        {
            [Key]
            public int CartItemId { get; set; }

            [Required]
            public int CartId { get; set; }

            [Required]
            public int ProductId { get; set; }

            public virtual Cart Cart { get; set; }

            public virtual Product Product { get; set; }

            public int Quantity { get; set; }

            [DataType(DataType.DateTime)]
            public DateTime AddedDate { get; set; }
        }
    }
}
