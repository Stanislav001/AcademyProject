using System.Collections.Generic;
using System.Threading.Tasks;

using Models.Models;

namespace Services.Interfaces
{
    public interface IPostService
    {
        public IEnumerable<Post> GetAll();
        public Task<bool> CreatePostAsync(string title, string context, string userId);

        public Task<bool> DeletePostAsync(string postId);

        public Post GetPostById(string postId);
    }
}