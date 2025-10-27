namespace sievefilteringinternational;

public class Post
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int LikeCount { get; set; }

    public int CommentCount { get; set; }

    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

}