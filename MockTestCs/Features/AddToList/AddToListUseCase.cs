using Microsoft.EntityFrameworkCore;
using MockTestCs.Entities;

namespace MockTestCs.Features.AddToList;

public class AddToListUseCase(
    MockTestCsDbContext ctx
)
{
    public async Task<Result<AddToListResponse>> Do(AddToListPayload payload)
    {
        var list = await ctx.ReadingLists.FindAsync(payload.ReadingListID);
        
         if (list is null)
            return Result<AddToListResponse>.Fail("Reading List not found");

        var history = await ctx.Histories.FindAsync(payload.HistoryID);

        if (history is null)
            return Result<AddToListResponse>.Fail("History not found");

        var exist = await ctx.ReadingListHistorys
            .AnyAsync(r => r.HistoryID == payload.HistoryID && r.ReadingListID == payload.ReadingListID);
        
        if(exist)
            return Result<AddToListResponse>.Fail("The list already contains this history");

        list.Histories.Add(history);
        await ctx.SaveChangesAsync();

        return Result<AddToListResponse>.Success(new());
    }
}