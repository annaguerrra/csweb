using System.ComponentModel.DataAnnotations;

namespace MockTestCs.Features.CreateUser;

public record CreateUserPayload
{
    [Required]
    [MaxLength(8)]
    public string Username { get; init; }

    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required]
    [MinLength(6)]
    [MaxLength(12)]
    public string Password { get; init; }

    [Required]
    [Compare("Password")]
    public string RepeatPassword { get; init; }

    [Required]
    [MaxLength(200)]
    [MinLength(1)]
    public string Description { get; init; }
    
    
}