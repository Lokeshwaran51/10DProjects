using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AmazonClone.MVC.Models
{
    public class ViewModel
    {
        public List<Category>? Categories { get;  set; }
        public int? SelectedCategoryId { get;  set; }
        public List<SubCategory>? SubCategories { get;  set; }

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string ImageUrl { get; set; }
            public int CategoryId { get; set; }
            public int SubCategoryId { get; set; }
            public string SubCategoryName { get; set; }

            public Category Category { get; set; }
            public virtual SubCategory SubCategory { get; set; }
        }
        public class Category
        {
            public int CategoryId { get; set; }
            [Required]
            public string Name { get; set; }
            public ICollection<SubCategory> SubCategories { get; set; }
            // Navigation property
            //public ICollection<Product> Products { get; set; }
        }

        public class SubCategory
        {
            public int SubCategoryId { get; set; }  

            [Required]
            public string SubCategoryName { get; set; }  

            public int CategoryId { get; set; }
            public string CategoryName { get; set; }

            public Category Category { get; set; }      
        }

        // Models/User.cs
        public class User
        {
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "UserName Field is Required..")]
            public String UserName { get; set; }

            [Required(ErrorMessage = "Mobile Field is Required.")]
            [DataType(DataType.PhoneNumber)]
            [MaxLength(10)]
            [RegularExpression(@"^[0-9]{10}$")]
            public String Mobile { get; set; }

            [Required(ErrorMessage = "Email Field is Required.")]
            [EmailAddress(ErrorMessage = "Invalid Email Address..")]
            [DataType(DataType.EmailAddress)]
            [MaxLength(30)]
            [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a correct email")]
            public String Email { get; set; }
            public string Password { get; set; }
        }
    }
}
