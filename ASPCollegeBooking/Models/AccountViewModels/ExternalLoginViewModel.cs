using System.ComponentModel.DataAnnotations;

namespace ASPCollegeBooking.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
