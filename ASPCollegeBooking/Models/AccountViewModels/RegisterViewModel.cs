using System.ComponentModel.DataAnnotations;

namespace ASPCollegeBooking.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        // [EmailAddress]
        [Display(Name = "First name and a space and Last Name")]
        public string Email { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
