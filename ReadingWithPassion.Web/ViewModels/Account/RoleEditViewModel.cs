using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReadingWithPassion.Web.ViewModels.Account
{
    public class RoleEditViewModel
    {
        public RoleEditViewModel() {
            Users = new List<string>();
        }
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<string> Users { get; set; }
    }
}
