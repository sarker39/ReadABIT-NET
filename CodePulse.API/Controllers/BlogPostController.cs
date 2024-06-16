using CodePulse.API.Model.Domain;
using CodePulse.API.Model.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository,
            ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostDto request)
        {
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetCategoryByIdAsync(categoryGuid);

                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            var addBlogPost = blogPostRepository.CreateBlogPostAsync(blogPost);

            var response = new BlogPostResponseDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()

            };

            return Ok(response);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await blogPostRepository.GetAllBlogPostsAsync();
            var response = new List<BlogPostResponseDto>();

            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostResponseDto
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                    }).ToList()
                });
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var filteredBlogPost = await blogPostRepository.GetBlogPostByIdAsync(id);

            var response = new BlogPostResponseDto
            {
                Id = filteredBlogPost.Id,
                Author = filteredBlogPost.Author,
                Content = filteredBlogPost.Content,
                FeaturedImageUrl = filteredBlogPost.FeaturedImageUrl,
                IsVisible = filteredBlogPost.IsVisible,
                PublishedDate = filteredBlogPost.PublishedDate,
                ShortDescription = filteredBlogPost.ShortDescription,
                Title = filteredBlogPost.Title,
                UrlHandle = filteredBlogPost.UrlHandle,
                Categories = filteredBlogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };


            return Ok(response);
        }

        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
        {
            var filteredBlogPost = await blogPostRepository.GetBlogPostByUrlAsync(urlHandle);

            var response = new BlogPostResponseDto
            {
                Id = filteredBlogPost.Id,
                Author = filteredBlogPost.Author,
                Content = filteredBlogPost.Content,
                FeaturedImageUrl = filteredBlogPost.FeaturedImageUrl,
                IsVisible = filteredBlogPost.IsVisible,
                PublishedDate = filteredBlogPost.PublishedDate,
                ShortDescription = filteredBlogPost.ShortDescription,
                Title = filteredBlogPost.Title,
                UrlHandle = filteredBlogPost.UrlHandle,
                Categories = filteredBlogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };


            return Ok(response);
        }
    }
}