using System.ComponentModel.DataAnnotations;

namespace ASPCollegeBooking.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
