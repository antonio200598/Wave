namespace Wave.API.Domain.Entities;

public class Post
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public User User { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<Comment> Comments { get; set; }

}
