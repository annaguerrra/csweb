using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MockTestCs.Features.AddToList;
using MockTestCs.Features.DeleteFromList;

namespace MockTestCs.Endpoints;

public static class ReadingListEndpoints
{
    public static void ConfigureReadingListEndpoints(this WebApplication app)
    {
        // add to list, delete from list
        app.MapPost("addToList/{id}", async (
            string listid,
            string history,
            HttpContext http,
            [FromServices] AddToListUseCase useCase
        ) =>
        {
            var claim = http.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim is null)
                return Results.BadRequest("User not found");

            var userid = Guid.Parse(claim.Value);
            var readingListID = Guid.Parse(listid);
            var historyid = Guid.Parse(history);
            

            var payload = new AddToListPayload(userid, historyid, readingListID);

            var result = await useCase.Do(payload);

            return (result.IsSuccess, result.Reason) switch
            {
                (false, "List or History not found") => Results.BadRequest(),
                (false, _) => Results.BadRequest(),
                (true, _) => Results.Ok()
            };
        });

        app.MapDelete("deleteFrom/{id}", async (
            string listid,
            string history,
            HttpContext http,
            [FromServices] DeleteFromListUseCase useCase
        ) =>
        {
            var claim = http.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim is null)
                return Results.BadRequest("User not found");

            var userid = Guid.Parse(claim.Value);
            var readinglistId = Guid.Parse(listid);
            var historyId = Guid.Parse(history);

            var payload = new DeleteFromListPayload(userid, readinglistId, historyId);
            var result = await useCase.Do(payload);

            return (result.IsSuccess, result.Reason) switch
            {
                (false, "List not found") => Results.BadRequest(),
                (false, _) => Results.BadRequest(),
                (true, _) => Results.Ok()
            };

        }).RequireAuthorization();
    }
}