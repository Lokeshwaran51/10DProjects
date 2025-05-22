using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace AmazonClone.API.Data.Entity
{
    public class ViewModelAPI
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string ImageUrl { get; set; }
            public int? CategoryId { get; set; }
            public int? SubCategoryId { get; set; }
            public string SubCategoryName { get; set; }

            public virtual Category Category { get; set; }
            public virtual SubCategory SubCategory { get; set; }
        }

        public class Category
        {
            public int CategoryId { get; set; }

            [Required(ErrorMessage = "Category name is required.")]
            public string Name { get; set; }

            public virtual ICollection<SubCategory> SubCategories { get; set; }
        }

        public class SubCategory
        {
            public int SubCategoryId { get; set; }

            [Required(ErrorMessage = "Subcategory name is required.")]
            public string SubCategoryName { get; set; }

            public int? CategoryId { get; set; }

            [Required(ErrorMessage = "Category name is required.")]
            public string CategoryName { get; set; }

            public virtual Category Category { get; set; }
        }

        public class User
        {
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "Username is required.")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Mobile number is required.")]
            [DataType(DataType.PhoneNumber)]
            [MaxLength(10)]
            [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid mobile number.")]
            public string Mobile { get; set; }

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email address.")]
            [MaxLength(30)]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
            public string Password { get; set; }
        }

        public class LoginModel
        {
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email address.")]
            [MaxLength(30)]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        /*public class Cart
        {
            [Key]
            public int CartId { get; set; }

            [Required]
            public int? UserId { get; set; }

            // Navigation property to the user who owns the cart
            public virtual User User { get; set; }

            // Collection of items in the cart
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
            public int? CartId { get; set; }

            [Required]
            public int? ProductId { get; set; }

            // Navigation property to the cart
            public virtual Cart Cart { get; set; }

            // Navigation property to the product
            public virtual Product Product { get; set; }

            public int Quantity { get; set; }

            [DataType(DataType.DateTime)]
            public DateTime AddedDate { get; set; }
        }*/
    }
}
