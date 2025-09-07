using Microsoft.EntityFrameworkCore;
using MockTest.Entities;
namespace MockTestCs.Features.DeleteFromList;

public class DeleteFromListUseCase(
    MockTestDbContext ctx
)
{
    public async Task<Result<DeleteFromListResponse>> Do(DeleteFromListPayload payload)
    {
        var fanfic = await ctx.ReadingLists
            .Include(r => r.Fanfics.Select(f => f.ID == payload.FanficID))
            .Where(f => f.ID == payload.ListID)
            .FirstOrDefaultAsync();

        if (fanfic is null)
            return Result<DeleteFromListResponse>.Fail("Item not found");

        if (!fanfic.Fanfics.Any())
            return Result<DeleteFromListResponse>.Fail("Fanfic is not in the list");

        var item = fanfic.Fanfics.FirstOrDefault();
        fanfic.Fanfics.Remove(item);

        await ctx.SaveChangesAsync();

        return Result<DeleteFromListResponse>.Success(new ());        
    }
}