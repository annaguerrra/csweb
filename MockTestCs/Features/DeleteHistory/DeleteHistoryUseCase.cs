using Microsoft.EntityFrameworkCore;
using MockTestCs.Entities;

namespace MockTestCs.Features.DeleteHistory;

public class DeleteHistoryUseCase(
    MockTestCsDbContext ctx
)
{
    public async Task<Result<DeleteHistoryResponse>> Do(DeleteHistoryPayload payload)
    {
        var user = await ctx.Users
            .Include(u => u.Histories)
            .FirstOrDefaultAsync(u => u.ID == payload.UserID);

        if (user is null)
            return Result<DeleteHistoryResponse>.Fail("Sign in to continue");

        var history = user.Histories.FirstOrDefault( u=> u.Id == payload.HistoryID);

        if(history is null)
            return Result<DeleteHistoryResponse>.Fail("The history doesn't exist");

        user.Histories.Remove(history);
        await ctx.SaveChangesAsync();

        return Result<DeleteHistoryResponse>.Success(new());
    }
}