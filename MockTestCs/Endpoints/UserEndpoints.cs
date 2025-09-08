using Microsoft.AspNetCore.Mvc;
using MockTestCs.Features.CreateUser;

namespace MockTestCs.Endpoints;

public static class UserEndpoints
{
    public static void ConfigureUserEndpoints(this WebApplication app)
    {
        app.MapPost("create", async (
            [FromBody] CreateUserPayload payload,
            [FromServices] CreateUserUseCase useCase
        ) =>
        {
            var result = await useCase.Do(payload);

            if (result.IsSuccess)
                return Results.Created();

            return Results.BadRequest(result.Reason);
        });
    }
}