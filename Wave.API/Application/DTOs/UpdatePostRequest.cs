using System.ComponentModel.DataAnnotations;

namespace Wave.API.Application.DTOs;

public record UpdatePostRequest
(
    [Required]
    long Id,
    string Title,
    string Content
);
