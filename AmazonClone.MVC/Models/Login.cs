using System.ComponentModel.DataAnnotations;

namespace AmazonClone.MVC.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email Field is Required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address..")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(30)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a correct email")]
        public String? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters and include an uppercase letter, a lowercase letter, a digit, and a special character.")]
        public string? Password { get; set; }
        public int UserId { get; internal set; }
    }

}
