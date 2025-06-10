using AmazonClone.MVC.Models;
using FluentValidation;

namespace AmazonClone.MVC.Validators
{
    public class RegisterModelValidator:AbstractValidator<User>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName Field is Required.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$")
                .WithMessage("Password must be at least 8 characters and include uppercase, lowercase, digit, and special character.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email Field is Required.")
                .EmailAddress().WithMessage("Invalid Email Address.")
                .Matches(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}").WithMessage("Please enter a correct email");

            RuleFor(x=>x.Mobile)
                .NotEmpty().WithMessage("Mobile Field is Required.")
                .Length(10).WithMessage("Mobile Number must be 10 Digits.")
                .Matches(@"^[0-9]{10}$").WithMessage("Mobile number must contain digits only.");
        }
    }
}
