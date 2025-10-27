using Microsoft.EntityFrameworkCore;

namespace sievefilteringinternational;

public class PostService(MyDbContext ctx) : IPostService
{
    public async Task<IQueryable<Post>> GetPosts()
    {
        //return ctx.Posts.ToList();
        return ctx.Posts.AsNoTracking();
    }
}