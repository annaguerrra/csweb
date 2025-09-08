using MockTestCs.Entities;

namespace MockTestCs.Features.AddHistory;

public class AddHistoryUseCase(
    MockTestCsDbContext ctx
)
{
    public async Task<Result<AddHistoryResponse>> Do(AddHistoryPayload payload)
    {
        var exist = await ctx.Histories.FindAsync(payload.Title);

        if (exist is not null)
            return Result<AddHistoryResponse>.Fail($"Already exists a history named '{payload.Title}'");

        var history = new History
        {
            Title = payload.Title,
            Content = payload.Content
        };

        ctx.Histories.Add(history);
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