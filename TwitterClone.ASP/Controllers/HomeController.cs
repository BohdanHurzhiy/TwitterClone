using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.ASP.Models;
using TwitterClone.ASP.Services.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TwitterClone.ASP.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly IPostService _postService;

        public HomeController(
            ILogger<HomeController> logger,
            IUserService userService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IPostService postService)
        {          
            _logger = logger;
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _postService = postService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var idUserClaims = (HttpContext.User.Claims.ToArray())[0].Value;
            try
            {
                var user = _userService.GetUser(idUserClaims);

                ViewBag.IdUser = idUserClaims;
                ViewBag.nameUser = user.Name;
               
                return View("Home", user);
            }
            catch (NullReferenceException e)
            {
                _signInManager.SignOutAsync();
            }
            return View("Home");
            // var posts = _userService.GetPostsForUser(idUserClaims, numPost);

        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
