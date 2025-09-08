using Microsoft.EntityFrameworkCore;
using MockTestCs.Entities;

namespace MockTestCs.Features.AddToList;

public class AddToListUseCase(
    MockTestCsDbContext ctx
)
{
    public async Task<Result<AddToListResponse>> Do(AddToListPayload payload)
    {
        var list = await ctx.ReadingLists.FirstOrDefaultAsync(r => r.ID == payload.ReadingListID 
                && r.UserID == payload.UserID);
        
         if (list is null)
            return Result<AddToListResponse>.Fail("Reading List not found");

        var history = await ctx.Histories.FindAsync(payload.HistoryID);

        if (history is null)
            return Result<AddToListResponse>.Fail("History not found");

        if(list.Histories.Any(h => h.Id == payload.HistoryID))
            return Result<AddToListResponse>.Fail("The list already contains this history");

        list.Histories.Add(history);
        await ctx.SaveChangesAsync();

        return Result<AddToListResponse>.Success(new());
    }
}