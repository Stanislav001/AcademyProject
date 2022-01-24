using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

using Models.Models;
using Services.Interfaces;

namespace AcademyProject.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public PostController(IPostService postService, IUserService userService, UserManager<User> userManager)
        {
            this.postService = postService;
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Post> posts = this.postService.GetAll();

            return this.View(posts);
        }

        public IActionResult Create()
        {
            this.ViewData["Users"] = userService.GetAllUsernames(userManager.GetUserId(this.User));
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post model)
        {
            
            await postService.CreatePostAsync(
                model.Title,
                model.Context,
                userManager.GetUserId(this.User));

            return RedirectToAction("Index");
        }
    }
}
