namespace sievefilteringinternational;

public interface IPostService
{
    Task<IQueryable<Post>> GetPosts();
}