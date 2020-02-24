using System.ComponentModel.DataAnnotations;

namespace ReadingWithPassion.Web.ViewModels.Account
{
    public class UserRoleViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public bool Isselected { get; set; }
    }
}
