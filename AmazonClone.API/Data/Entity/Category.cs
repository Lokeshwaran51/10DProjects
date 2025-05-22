using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonClone.API.Data.Entity
{
    [Table("Categories", Schema = "AmazonClone")]
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string Name { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
