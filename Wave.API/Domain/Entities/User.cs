using Wave.API.Domain.Enums;

namespace Wave.API.Domain.Entities;

public class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public UserType Type { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<Post> Posts { get; set; }

    public ICollection<Comment> Comments { get; set; }
}
