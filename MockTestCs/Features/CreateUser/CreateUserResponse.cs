namespace MockTestCs.Features.CreateUser;

public record CreateUserResponse(
    Guid UserID,
    string Username,
    string Email,
    string Description
);