using System.ComponentModel.DataAnnotations;

namespace Wave.API.Application.DTOs;

public record UpdateCommentRequest
(
    [Required]
    long UserId,

    [Required]
    long CommentId,

    [Required]
    string Content,
    
    [Required]
    string UpdatedAt
);

