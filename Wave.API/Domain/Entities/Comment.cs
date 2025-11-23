namespace Wave.API.Domain.Entities;

public class Comment
{
    public long Id { get; set; }

    public User User { get; set; }

    public Post Post { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}
