using AutoMapper;
using System.Threading.Tasks;

using Date;
using Services.ViewModels;
using Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Services.Implementation
{
    public class PostService : BaseService
    {
        public PostService(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<bool> CreatePostAsync(string title, string context, string userId)
        {
            Post model = new Post
            {
                Title = title,
                Context = context,
                UserId = userId
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
    }
}