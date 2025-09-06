namespace MockTestCs.Services.Login;

public record LoginService
{
    public required Guid ID { get; set; } 
    public required string Username { get; set; }
    public required string Email { get; set; }
}