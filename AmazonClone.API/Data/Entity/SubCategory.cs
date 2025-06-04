using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonClone.API.Data.Entity
{
    [Table("SubCategories")]
    public class SubCategory
    {
        [Key]
        [Column(TypeName = "int")]
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "Subcategory name is required.")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SubCategoryName { get; set; } = string.Empty;

        [Column(TypeName = "int")]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string CategoryName { get; set; } = string.Empty;

        public virtual Category? Category { get; set; }
        //public int Id { get; internal set; }
    }
}
