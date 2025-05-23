using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonClone.API.Data.Entity
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid mobile number.")]
        [Column(TypeName = "nvarchar(10)")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [MaxLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [Column(TypeName = "nvarchar(255)")]
        public string Password { get; set; }
    }
}
