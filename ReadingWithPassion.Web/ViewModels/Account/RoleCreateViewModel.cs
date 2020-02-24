using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ReadingWithPassion.Web.ViewModels.Account
{
    public class RoleCreateViewModel
    {
        [Required]
        [Remote(action: "IsRoleNameUsed",controller: "Role",HttpMethod ="Get")]
        public string RoleName { get; set; }
    }
}
