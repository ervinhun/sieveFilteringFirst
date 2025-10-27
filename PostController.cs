using Microsoft.AspNetCore.Mvc;

namespace sievefilteringinternational;

public class PostController(IPostService postService) : ControllerBase
{
    [HttpGet(nameof(GetPosts))]
    public async Task<object> GetPosts()
    {
        return await postService.GetPosts();
    }
}