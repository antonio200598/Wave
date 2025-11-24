using Microsoft.AspNetCore.SignalR;

namespace Wave.API.Domain.Entities;

public class Comment
{
    public long Id { get; set; }

    public User User { get; set; }

    public long UserId { get; set; }

    public Post Post { get; set; }

    public long PostId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }  = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; }

}
