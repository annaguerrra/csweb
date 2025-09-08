using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MockTestCs.Features.AddHistory;
using MockTestCs.Features.DeleteHistory;

namespace MockTestCs.Endpoints;

public static class HistoryEndPoints
{
    public static void ConfigureHistory(this WebApplication app)
    {
        app.MapPost("addHistory", async (
            [FromBody] AddHistoryPayload payload,
            [FromServices] AddHistoryUseCase useCase
        ) =>
        {
            var result = await useCase.Do(payload);

            return (result.IsSuccess, result.Reason) switch
            {
                (false, "Error") => Results.NotFound(),
                (false, _) => Results.BadRequest(),
                (true, _) => Results.Ok(result.Data)
            };
        });

        app.MapDelete("delete/{id}", async (
            string id,
            HttpContext http,
            [FromServices] DeleteHistoryUseCase useCase) =>
            {
                var claim = http.User.FindFirst(ClaimTypes.NameIdentifier);
                
                if (claim is null)
                    return Results.BadRequest("User not found");
                    
                var userID = Guid.Parse(claim.Value);
                var historyID = Guid.Parse(id);

                var payload = new DeleteHistoryPayload(historyID, userID);
                var result = await useCase.Do(payload);

                return (result.IsSuccess, result.Reason) switch
                {
                    (false, "History not found") => Results.NotFound(),
                    (false, _) => Results.BadRequest(),
                    (true, _) => Results.Ok()
                };
            }).RequireAuthorization();
    }
}