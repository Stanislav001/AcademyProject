using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

using Models.Models;
using Services.Interfaces;
using Services.ViewModels;

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
            IEnumerable<PostViewModel> posts = this.postService.GetAll();

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
                userManager.GetUserName(this.User),
                userManager.GetUserId(this.User));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LeaveComment()
        {
            this.ViewData["Users"] = userService.GetAllUsernames(userManager.GetUserId(this.User));
            return this.View();
        }
      
        [HttpPost]
        public async Task<IActionResult> LeaveComment(Comment model)
        {
            await postService.LeaveComment(
                model.Context,
                model.Id,
                model.UserName,
                userManager.GetUserId(this.User));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetCommentsAsync(string id)
        {
            this.ViewData["Users"] = userService.GetAllUsernames(userManager.GetUserId(this.User));
            IEnumerable<Comment> comment = this.postService.GetAllCommentByPostId(id);

            return PartialView("PostPartials/_GetComments", comment);
        }
    }
}