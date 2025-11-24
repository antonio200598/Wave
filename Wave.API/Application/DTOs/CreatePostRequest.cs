using System.ComponentModel.DataAnnotations;

namespace Wave.API.Application.DTOs;

public record CreatePostRequest
(
    [Required]
    long UserId,
    [Required]
    string Title,
    [Required]
    string Content
);
