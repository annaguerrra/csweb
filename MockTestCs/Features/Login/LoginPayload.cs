namespace MockTestCs.Features.Login;

public record LoginPayload(
    Guid UserID,
    string Login,
    string Password
);

