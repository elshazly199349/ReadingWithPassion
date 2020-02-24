using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReadingWithPassion.Web.ViewModels.Account
{
    public class EditUserViewModel
    {
        public EditUserViewModel(){
            Roles = new List<string>();
            Claims = new List<string>();
        }
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Claims { get; set; }
    }
}
