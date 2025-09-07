using Microsoft.EntityFrameworkCore;
using MockTest.Entities;

namespace MockTestCs.Features.DeleteFanfic;

public class DeleteFanficUseCase(
    MockTestDbContext ctx
)
{
    public async Task<Result<DeleteFanficResponse>> Do(DeleteFanficPayload payload)
    {
        var fanfic = await ctx.Fanfics
            .Include(f => f.ReadingLists)
            .FirstOrDefaultAsync(r => r.ID == payload.FanficID);

        if (fanfic is null)
            return Result<DeleteFanficResponse>.Fail("The fanfic doesn't exist.");

        fanfic.ReadingLists.Clear();
        ctx.Fanfics.Remove(fanfic);

        await ctx.SaveChangesAsync();

        return Result<DeleteFanficResponse>.Success(new ());
    
    }
}