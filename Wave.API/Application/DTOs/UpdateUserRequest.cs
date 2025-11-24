using System.ComponentModel.DataAnnotations;

namespace Wave.API.Application.DTOs;

public record UpdateUserRequest
(
    [Required]
    long Id,

    string Email,

    string UserName,

    string PasswordHash,
    
    [Required]
    string UpdatedAt
);

