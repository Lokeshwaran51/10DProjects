using System.ComponentModel.DataAnnotations;

namespace AmazonClone.MVC.Models
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }

        [Required]
        public string SubCategoryName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public Category Category { get; set; }
        public int? SelectedCategoryId { get; set; }
        public List<SubCategory>? SubCategories { get; set; }
    }
}
