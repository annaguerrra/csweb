using Microsoft.EntityFrameworkCore;
using MockTestCs.Entities;

namespace MockTestCs.Features.AddHistory;

public class AddHistoryUseCase(
    MockTestCsDbContext ctx
)
{
    public async Task<Result<AddHistoryResponse>> Do(AddHistoryPayload payload)
    {
        var user = await ctx.Users
            .FindAsync(payload.UserID);
        
        if(user is null)
            return Result<AddHistoryResponse>.Fail("User not found");

        var history = new History
        {
            Title = payload.Title,
            Content = payload.Content
        };

        user.Histories.Add(history);
        await ctx.SaveChangesAsync();
        
        return Result<AddHistoryResponse>.Success(
            new AddHistoryResponse(
                history.Id,
                history.Title,
                history.Content
            )
        );
    }
}