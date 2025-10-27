namespace sievefilteringinternational;

public interface IPostService
{
    Task<List<Post>> GetPosts();
}