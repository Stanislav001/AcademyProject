using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

using Date;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;

namespace Services.Implementation
{
    public class PostService : BaseService, IPostService
    {
        private readonly IUserService userService;
        public PostService(ApplicationDbContext dbContext, IMapper mapper, IUserService userService)
            : base(dbContext, mapper)
        {
            this.userService = userService;
        }

        public IEnumerable<Post> GetAll()
        {
            IEnumerable<Post> posts = this.DbContext.Posts
                .Select(posts => new Post
                {
                    Title = posts.Title,
                    UserId = posts.UserId,
                    UserName = posts.UserName,
                    Context = posts.Context
                }).ToList();

            return posts;
        }

        public async Task<bool> CreatePostAsync(string userName,string title, string context, string userId)
        {
            Post model = new Post
            {
                Title = title,
                Context = context,
                UserId = userId,
                UserName = userName,
            };

            await this.DbContext.Posts.AddAsync(model);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePostAsync(string postId)
        {
            var post = await this.DbContext.Posts
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                return false;
            }

            this.DbContext.Posts.Remove(post);

            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public Post GetPostById(string postId)
        {
            var post = this.DbContext.Posts.FirstOrDefault(p => p.Id == postId);

            return post;
        }


        // Comment 
        public async Task<bool> LeaveComment(string context, string userId, string postId)
        {
            Comment comment = new Comment
            {
                Context = context,
                UserId = userId,
                PostId = postId
            };

            await this.DbContext.Comments.AddAsync(comment);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public IEnumerable<Comment> GetAllComment()
        {
            IEnumerable<Comment> comments = this.DbContext.Comments
                .Select(comments => new Comment
                {
                    PostId = comments.PostId,
                    UserId = comments.UserId,
                    Context = comments.Context,
                }).ToList();

            return comments;
        }
    }
}