using System.ComponentModel.DataAnnotations;

namespace Wave.API.Application.DTOs;

public record CreateCommentRequest
(
    [Required] 
    long UserId,
    [Required]
    long PostId,
    [Required]
    string Content
);
