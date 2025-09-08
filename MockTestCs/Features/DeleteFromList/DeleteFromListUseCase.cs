using Microsoft.EntityFrameworkCore;
using MockTestCs.Entities;

namespace MockTestCs.Features.DeleteFromList;

public class DeleteFromListUseCase(
    MockTestCsDbContext ctx
)
{
    public async Task<Result<DeleteFromListResponse>> Do(DeleteFromListPayload payload)
    {
        var list = await ctx.ReadingLists.FirstOrDefaultAsync(r => r.ID == payload.ReadingListID
            && r.UserID == payload.UserID);

        if (list is null)
            return Result<DeleteFromListResponse>.Fail("Reading List not found");

        var history = await ctx.Histories.FindAsync(payload.HistoryID);

        if (history is null)
            return Result<DeleteFromListResponse>.Fail("History not found");

        if(!list.Histories.Any(h => h.Id == payload.HistoryID))
            return Result<DeleteFromListResponse>.Fail("The list does not contain this history");

        list.Histories.Remove(history);
        await ctx.SaveChangesAsync();
        return Result<DeleteFromListResponse>.Success(new());
    }
}