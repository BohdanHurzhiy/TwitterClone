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
using TwitterClone.ASP.ViewModels;

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
        public IActionResult Index(string alias = null)
        {
            var userId = (HttpContext.User.Claims.ToArray())[0].Value;

            var user = alias == null ? _userService.GetUser(userId) : _userService.GetUserByAlias(alias);

            ViewBag.UserName = user.Name;
            ViewBag.Id = user.Id;

            if (alias == null || userId == user.Id)
            {
                ViewBag.My = true;                
            }
            else
            {
                if (_userService.SubscriptionCheck(userId, user.Id))
                {
                    ViewBag.Subscribe = "Unsubscribe";
                }
                else
                {
                    ViewBag.Subscribe = "Subscribe";                   
                }
                ViewBag.IdUser = userId;
                ViewBag.My = false;               
            }
            var numberPosts = 10;
            return View(user);
        }

        [HttpPost]
        public JsonResult MessageHandler(string folowerId, string folowedId)
        {
            string result = "";
            if (_userService.SubscriptionCheck(folowerId, folowedId))
            {
                _userService.UnFollow(folowerId, folowedId);
                result = "Subscribe";
            }
            else
            {
                _userService.Follow(folowerId, folowedId);
                result = "Unsubscribe";
            }          
            return Json(result); 
        }

        public async Task<IActionResult> Edit(string alias)
        {
            User userInBase = _userService.GetUserByAlias(alias);
            User user = await _userManager.FindByIdAsync(userInBase.Id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new () {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                SecondName = user.SecondName,
                Alias = user.Alias,
                NumberPhone = user.NumberPhone
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email ?? user.Email;
                    user.UserName = model.Alias ?? user.Alias;
                    user.Name = model.Name ?? user.Name;                    
                    user.SecondName = model.SecondName ?? user.SecondName;
                    user.Alias = model.Alias ?? user.Alias;                    
                    user.NumberPhone = model.NumberPhone ?? user.NumberPhone;                   

                    var result = await _userManager.UpdateAsync(user);                    

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
    }
}
