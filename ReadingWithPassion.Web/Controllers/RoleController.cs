using ReadingWithPassion.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReadingWithPassion.Web.Models.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingWithPassion.Web
{
    [Authorize(Roles =("Admin"))]
    public class RoleController:Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager) {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Roles() {
            var roles =_roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result= await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = model.RoleName
                });
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles", new { Controller = "Role" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> IsRoleNameUsed(string roleName) {
            var isFound=await _roleManager.RoleExistsAsync(roleName);
            if (isFound)
            {
                return Json($"The role {roleName} is already taken");
            }
            return Json(true);
        }
        
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role==null)
            {
                ViewBag.errorMessage = $"Role with Id={id} cann't be found";
                return View("NotFound");
            }
            var model = new RoleEditViewModel
            {
                Id = role.Id,
                Name = role.Name,
            };
            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role == null)
                {
                    ViewBag.errorMessage = $"Role with Id={model.Id} cann't be found";
                    return View("NotFound");
                }
                role.Name = model.Name;
                var result= await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles", new { Controller = "Role" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UsersInRole(string roleId) {
            var userRoleViewModels = new List<UserRoleViewModel>();
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.errorMessage = $"Role with Id={roleId} cann't be found";
                return View("NotFound");
            }
            TempData["roleId"] = roleId;
            foreach (var user in _userManager.Users.ToList())
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserName = user.UserName,
                    UserId = user.Id,
                    Isselected =await _userManager.IsInRoleAsync(user, role.Name)
                };
                userRoleViewModels.Add(userRoleViewModel);
            }
            return View(userRoleViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> UsersInRole(List<UserRoleViewModel> model)
        {
            var roleId = TempData["roleId"].ToString();
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.errorMessage = $"Role with Id={roleId} cann't be found";
                return View("NotFound");
            }
            foreach (var userModel in model)
            {
                var user = await _userManager.FindByIdAsync(userModel.UserId);
                IdentityResult result= null;
                if (userModel.Isselected && !(await _userManager.IsInRoleAsync(user,role.Name)))
                {
                    result =await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if((!userModel.Isselected) && (await _userManager.IsInRoleAsync(user,role.Name)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
            }
            return RedirectToAction("Roles", new { Controller = "Role" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId) {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role==null)
            {
                ViewBag.errorMessage = $"role with Id={roleId} cann't be found";
                return View("NotFound");
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded==false)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", $"{error.Description}");
                }
            }
            return RedirectToAction("Roles",new { Controller="Role"});
        }
    }
}
