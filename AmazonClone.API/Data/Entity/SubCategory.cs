using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonClone.API.Data.Entity
{
    [Table("SubCategories", Schema = "AmazonClone")]
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
}
