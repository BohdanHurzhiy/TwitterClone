using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.ASP.Models;
using TwitterClone.ASP.Services.ServiceInterface;

namespace TwitterClone.ASP.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SearchByTag(string tag)
        {
            ICollection<Post> posts = null;
            try
            {
                posts = _tagService.GetPostsByTag(tag);
            }
            catch (NullReferenceException e)
            {
                return PartialView("GetPostsPartial", posts);
            }
            return PartialView("GetPostsPartial", posts);
        }
    }
}
