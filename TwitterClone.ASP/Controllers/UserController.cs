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
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly IPostService _postService;

        public UserController(
            IUserService userService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IPostService postService)
        {           
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _postService = postService;
        }

        [HttpGet]
        public IActionResult Index(string id = null)
        {
            var userId = (HttpContext.User.Claims.ToArray())[0].Value;
            var user = _userService.GetUser(id);
            if (userId == id)
            {
                ViewBag.My = true;                
            }
            else
            {
                ViewBag.My = false;               
            }
            return View(user);
        }
    }
}
