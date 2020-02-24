using Microsoft.AspNetCore.Mvc;
using ReadingWithPassion.Web.Utilities.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace ReadingWithPassion.Web.ViewModels.Account
{
    public enum Gender
    {
        Male = 1,
        Female = 0
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailUse", controller:"Account",HttpMethod ="Get")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare("Password",
            ErrorMessage ="Password and confirmation don't match.")]
        public string ConfirmPassword { get; set; }
        
        [Required]
        [MinLength(11)]
        [MaxLength(14)]
        [ValidPhoneNumber(ErrorMessage ="Invalid phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        public Gender  Gender { get; set; }
    }
}
