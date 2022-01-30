using System.Collections.Generic;
using System.Threading.Tasks;

using Models.Models;
using Services.ViewModels;

namespace Services.Interfaces
{
    public interface IPostService
    {
        public IEnumerable<PostViewModel> GetAll();
        public Task<bool> CreatePostAsync(string userName, string title, string context, string userId);
        public Task<bool> DeletePostAsync(string postId);
        public Post GetPostById(string postId);
        PostViewModel GetDetailsById(string id);
        public IEnumerable<Comment> GetAllCommentByPostId(string postId);
        public Task<bool> LeaveComment(string context, string userId, string postId, string userName);
    }
}