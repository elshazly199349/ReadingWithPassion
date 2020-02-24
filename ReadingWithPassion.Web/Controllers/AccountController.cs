using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReadingWithPassion.Web.Models.DataAccess;
using ReadingWithPassion.Web.Security;
using ReadingWithPassion.Web.ViewModels.Account;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReadingWithPassion.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IEmailSender emailSender) {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        
        [HttpGet]
        public IActionResult Users()
        {
            return View(_userManager.Users);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Gender = Convert.ToBoolean(model.Gender),
                PhoneNumber = model.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(true);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            await SendConfirmationEmailAsync(user.Id).ConfigureAwait(true);

            if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
            {
                return RedirectToAction("Users", new { Controller = "Account" });
            }
            ViewBag.userId = user.Id;
            ViewBag.message = "Confirmation message has been sent, please validate your account";
            return View("ConfirmEmail");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync().ConfigureAwait(true)).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(true);
                if (user != null && !user.EmailConfirmed && (await _userManager.CheckPasswordAsync(user, model.Password).ConfigureAwait(true)))
                {
                    ViewBag.message = "Please confirm your email by clicking the link you recieved in your email";
                    ViewBag.userId = user.Id;
                    return View("ConfirmEmail");
                }
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", new { Controller = "Home" });
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync().ConfigureAwait(true)).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendConfirmationEmail(string userId) {
            if (await SendConfirmationEmailAsync(userId).ConfigureAwait(true))
            {
                ViewBag.message = "Confirmation message has been sent, please validate your account";
            }
            else
            {
                ViewBag.message = "Error happend, please try again";
            }
            ViewBag.userId = userId;
            return View("ConfirmEmail");
        }

        public async Task<Boolean> SendConfirmationEmailAsync(string userId) {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(true);
            if (user == null)
                return false;

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(true);
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new
            {
                userId = user.Id,
                token
            }, Request.Scheme);
            await _emailSender.SendEmailAsync(user.Email, "Confirm Email",$"click the link to confirm your email" +
                $"this link is valid just for 24 hours" +
                $" {confirmationLink}").ConfigureAwait(true);
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token) {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            var user =await _userManager.FindByIdAsync(userId).ConfigureAwait(true);
            if(user==null)
                return RedirectToAction("Index", "Home");

           var result= await _userManager.ConfirmEmailAsync(user, token).ConfigureAwait(true);
            if (result.Succeeded)
                ViewBag.message = "Thank you for confirming your email";
            else
            {
                ViewBag.message = "Email cann't be confirmed";
                ViewBag.userId = user.Id;
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl) {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider,redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl, string remoteError)
        {

            returnUrl = returnUrl ?? Url.Content("~/");
            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync().ConfigureAwait(true)).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider:{remoteError}");
                return View("Login", loginViewModel);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync().ConfigureAwait(true);
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return View("Login", loginViewModel);
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (email!=null)
            {
                var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(true);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                    await _userManager.CreateAsync(user).ConfigureAwait(true);
                    await SendConfirmationEmailAsync(user.Id).ConfigureAwait(true);
                    ViewBag.message = "Please confirm your email by clicking the link you recieved in your email";
                    ViewBag.userId = user.Id;
                    return View("ConfirmEmail");
                }
                if (!user.EmailConfirmed)
                {
                    ViewBag.message = "Please confirm your email by clicking the link you recieved in your email";
                    ViewBag.userId = user.Id;
                    return View("ConfirmEmail");
                }

                await _userManager.AddLoginAsync(user, info).ConfigureAwait(true);
                await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(true);
            }

            var signinResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true).ConfigureAwait(true);
            if (signinResult.Succeeded)
                return LocalRedirect(returnUrl);
            else
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View("Login", loginViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", new { Controller = "Home" });
        }

        [HttpGet]
        public async Task<IActionResult> IsEmailUse(string email) {
            var user =await _userManager.FindByEmailAsync(email);
            if (user==null)
            {
                return Json(true);
            }
            return Json($"The email {email} is already token");
        }
        
        [HttpGet]
        public IActionResult AccessDenied(string reurnUrl) {
            return RedirectToAction("Login", new { Controller = "Account", returnUrl = reurnUrl });
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.errorMessage = $"user with Id={userId} cann't be found";
                return View("NotFound");
            }
            if (!(User.IsInRole("Admin") || User.FindFirst(ClaimTypes.NameIdentifier).Value == userId))
            {
                return RedirectToAction("Login", new { Controller = "Account", returnUrl = $"/Account/EditUser/{user.Id}" });
            }
            var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
            var userClaims = (await _userManager.GetClaimsAsync(user));
            var userModel = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Gender = (Gender)(Convert.ToInt32(user.Gender)),
                Roles = userRoles,
                Claims = userClaims.Select(e => e.Value).ToList()
            };
            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model) {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user==null)
                {
                    ViewBag.errorMessage = $"User with Id={model.Id} cann't be found";
                    return View("NotFound");
                }
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Gender = Convert.ToBoolean(model.Gender);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Users", new { Controller = "Account" });
                    }
                    else
                    {
                        return RedirectToAction("Index", new { Controller = "Home" });
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", $"{error.Description}");
                    }
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteUser(string userId) {
            var user =await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.errorMessage = $"User with Id={userId} cann't be found";
                return View("NotFound");
            }
            if (!(User.IsInRole("Admin") || User.FindFirst(ClaimTypes.NameIdentifier).Value == userId))
            {
                return RedirectToAction("Login", new { Controller = "Account", returnUrl = $"/Account/EditUser/{user.Id}" });
            }
            var result= await _userManager.DeleteAsync(user);
            if (result.Succeeded==false)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", $"{error.Description}");
                }
            }
            return RedirectToAction("Users", new { Controller = "Account" });
        }
        
        public IActionResult ForgotPassword() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPassordViewModel model) {
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(true);
                if (user==null)
                {
                    ViewBag.errorMessage = $"user with specified email: {model.Email} cann't be found";
                    return View("NotFound");
                }
                if (await _userManager.IsEmailConfirmedAsync(user).ConfigureAwait(true))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(true);
                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token }, Request.Scheme);
                    await _emailSender.SendEmailAsync(model.Email, "Genral topics \n Reset password", $"clcik the link to reset passord {passwordResetLink}").ConfigureAwait(true);
                    return View("ForgotPasswordConfirmation");
                }
                else
                {
                    ViewBag.message = "Please confirm your email first by clciking the link we sent to you";
                    ViewBag.userId = user.Id;
                    return View("ForgotPasswordConfirmation");
                }
            }
            return View(model);
        }

        [HttpGet]
        public  IActionResult ResetPassword(string email,string token) {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                ViewBag.errorMessage = "Invalid Paramters";
                return View("NotFound");
            }
            var model = new ResetPasswordViewModel
            {
                Token = token,
                Email = email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(true);
                if (user!=null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password).ConfigureAwait(true);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                ViewBag.errorMessage = $"User with Email:{user.Email} cann't be found";
                return View("NotFound");
            }
            return View(model);
        }
    }
}