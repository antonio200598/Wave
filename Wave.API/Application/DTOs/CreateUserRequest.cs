using System.ComponentModel.DataAnnotations;

namespace Wave.API.Application.DTOs;

public record CreateUserRequest
(  
    [Required]
    string Name,
    [Required, EmailAddress, MaxLength(255)]
    string Email,
    [Required]
    string PasswordHash,
    [Required]
    string CreatedAt
);

