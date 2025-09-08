using Microsoft.EntityFrameworkCore;
using MockTestCs.Entities;

namespace MockTestCs.Features.DeleteFromList;

public class DeleteFromListUseCase(
    MockTestCsDbContext ctx
)
{
    public async Task<Result<DeleteFromListResponse>> Do(DeleteFromListPayload payload)
    {
        var list = await ctx.ReadingLists.FindAsync(payload.ReadingListID);

        if (list is null)
            return Result<DeleteFromListResponse>.Fail("Reading List not found");

        var history = await ctx.Histories.FindAsync(payload.HistoryID);

        if (history is null)
            return Result<DeleteFromListResponse>.Fail("History not found");

        var exist = await ctx.ReadingListHistorys
            .AnyAsync(r => r.HistoryID == payload.HistoryID && r.ReadingListID == payload.ReadingListID);

        if (!exist)
            return Result<DeleteFromListResponse>.Fail("The list does not contain this history");

        list.Histories.Remove(history);
        await ctx.SaveChangesAsync();
        return Result<DeleteFromListResponse>.Success(new());
    }
}