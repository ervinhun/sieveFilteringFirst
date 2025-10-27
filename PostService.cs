namespace sievefilteringinternational;

public class PostService(MyDbContext ctx) : IPostService
{
    public async Task<List<Post>> GetPosts()
    {
        return ctx.Posts.ToList();

    }
}