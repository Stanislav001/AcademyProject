﻿using AutoMapper;
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
        public PostService(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
        public IEnumerable<Post> GetAll()
        {
            IEnumerable<Post> posts = this.DbContext.Posts
                .Select(posts => new Post
                {
                    Title = posts.Title,
                    Context = posts.Context,
                    UserId = posts.UserId,
                }).ToList();

            return posts;
        }

        public async Task<bool> CreatePostAsync(string title, string context, string userId)
        {
            Post model = new Post
            {
                Title = title,
                Context = context,
                UserId = userId,
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

        // TODO: need this?
        public Post GetPostById(string postId)
        {
            var post = this.DbContext.Posts.FirstOrDefault(p => p.Id == postId);

            return post;
        }
    }
}