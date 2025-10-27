using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;

namespace sievefilteringinternational;

public class PostController(IPostService postService, ISieveProcessor processor) : ControllerBase
{
    [HttpGet(nameof(GetPosts))]
    public async Task<IQueryable<Post>> GetPosts(SieveModel sieveModel)
    {
        IQueryable<Post> result = await postService.GetPosts();

        return processor.Apply<Post>(sieveModel, result);
    }
}