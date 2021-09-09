using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.ASP.Models;
using TwitterClone.ASP.Services.ServiceInterface;

namespace TwitterClone.ASP.Controllers
{
    public class PostController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly IPostService _postService;

        public PostController(IUserService userService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IPostService postService)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _postService = postService;
        }


        public IActionResult Index(int id)
        {
            var post = _postService.GetPostToId(id);
            return View(post);
        }

        public IActionResult GetPostForUser(string id, int numberPosts)
        {
            var posts = _userService.GetPostsForUser(id, numberPosts);
            return PartialView("GetPostsPartial", posts);
        }

        public IActionResult GetUserPosts(string id, int numberPosts)
        {
            var posts = _userService.GetUserPosts(id, numberPosts);
            return PartialView("GetPostsPartial", posts);
        }
    }
}
