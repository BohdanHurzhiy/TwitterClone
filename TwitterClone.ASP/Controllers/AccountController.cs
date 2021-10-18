using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.ASP.ViewModels;
using TwitterClone.ASP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using WebMatrix.WebData;
using Microsoft.AspNetCore.Authentication;
using TwitterClone.ASP.Services.ServiceInterface;

namespace TwitterClone.ASP.Controllers
{
    [Authorize]   
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new() { Email = model.Email };

                string alias = model.Alias;
                if (alias == null)
                {
                    alias = GetAlias(user.Id);                   
                }                
                user.Alias = alias;
                user.Name = alias;
                user.UserName = alias;

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {                    
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        private static string GetAlias(string userId)
        {
            var index = userId.LastIndexOf('-');
            string alias = userId.Substring(index + 1);
            return alias;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel model = new()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByEmail(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Incorrect username and (or) password");
                    model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                    return View(model);
                }
                var result =
                    await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username and (or) password");
                    model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();                    
                }
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync();

            var siteCookies = HttpContext.Request.Cookies.Keys;
            foreach (var cookie in siteCookies)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        [AllowAnonymous]
        public  IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                new { returnUrl = returnUrl });
            var propeties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, propeties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string rempreError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            LoginViewModel loginViewModel = new()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (rempreError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {rempreError}");

                return View("Login", loginViewModel);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    var user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new User
                        {                           
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)                            
                        };
                        user.Alias = GetAlias(user.Id);
                        user.UserName = GetAlias(user.Id);
                        user.Name = GetAlias(user.Id);

                        await _userManager.CreateAsync(user);
                    }

                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.ErroTitle = $"Email claim not received from {info.LoginProvider}";
                ViewBag.ErrorrMessage = "Please contact support on some@gmail.com";

                return View("Error");
            }            
        }

    }
}
