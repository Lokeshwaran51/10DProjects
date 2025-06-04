//using System.ComponentModel.DataAnnotations;

//namespace AmazonClone.MVC.Models
//{
//    public class Category
//    {
//        public int CategoryId { get; set; }
//        [Required]
//        public string Name { get; set; }
//        //public ICollection<SubCategory> SubCategories { get; set; }

//        //public List<Category>? Categories { get; set; }
//        // Navigation property
//        //public ICollection<Product> Products { get; set; }
//    }
//}


using System.ComponentModel.DataAnnotations;

namespace AmazonClone.MVC.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
