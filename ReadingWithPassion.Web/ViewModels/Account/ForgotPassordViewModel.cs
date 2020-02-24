using System.ComponentModel.DataAnnotations;

namespace ReadingWithPassion.Web.ViewModels.Account
{
    public class ForgotPassordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
