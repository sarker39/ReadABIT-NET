﻿using CodePulse.API.Model.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateBlogPostAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
    }
}