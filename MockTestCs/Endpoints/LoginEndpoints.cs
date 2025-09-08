using Microsoft.AspNetCore.Mvc;
using MockTestCs.Features.Login;

namespace MockTestCs.Endpoints;

public static class LoginEndpoints
{
    public static void ConfigureLoginEnpoints(this WebApplication app)
    {
        app.MapPost("sign-in", async (
            [FromBody] LoginPayload payload,
            [FromServices] LoginUseCase useCase) =>
        {
            var result = await useCase.Do(payload);
            if(!result.IsSuccess)
                return Results.BadRequest("Invalid Login");

            return Results.Ok(result.Data);
        });
    }
}