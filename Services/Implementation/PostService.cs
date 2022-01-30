using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

using Date;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using System;

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

        public IEnumerable<PostViewModel> GetAll()
        {
            IEnumerable<PostViewModel> posts = this.DbContext.Posts
                .Select(posts => new PostViewModel
                {
                    Title = posts.Title,
                    UserId = posts.UserId,
                    UserName = posts.UserName,
                    Context = posts.Context,
                    CommentContext = posts.CommentContext,
                    PostId = posts.Id,
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
        public PostViewModel GetDetailsById(string id)
        {
            PostViewModel post = this.DbContext.Posts
                .Select(post => new PostViewModel
                {
                    Title = post.Title,
                    PostId = post.Id,
                    Context = post.Context,
                    CommentContext = post.CommentContext,
                    UserId = post.UserId,
                    UserName = post.UserName
                }).SingleOrDefault(post => post.PostId == id);

            return post;
        }

        public Post GetPostById(string postId)
        {
            var post = this.DbContext.Posts.FirstOrDefault(p => p.Id == postId);

            return post;
        }

        public async Task<bool> LeaveComment(string context, string postId,string userName,string userId)
        {
            Comment comment = new Comment
            {
                PostId = postId,
                Context = context,
                UserId = userId,
                UserName = userName,
            };

            await this.DbContext.Comments.AddAsync(comment);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public IEnumerable<Comment> GetAllCommentByPostId(string id)
        {
            IEnumerable<Comment> comemnts = this.DbContext.Comments
                .Where(x=> x.PostId == id)
                .Select(comemnt => new Comment
                {
                    PostId = comemnt.PostId,
                    Context = comemnt.Context,
                    UserId = comemnt.UserId,
                    Id = comemnt.Id,
                    UserName = comemnt.UserName
                }).ToList();

            return comemnts;
        }

        public User GetUserNameById(string id)
        {
            User user = this.DbContext.Users
                .Where(x => x.Id == id)
                .Select(user => new User
                {
                    Id = user.Id,
                    UserName = user.UserName
                }).FirstOrDefault(x => x.Id == id);

            return user;
        }
    }
}