using Microsoft.EntityFrameworkCore;
using MockTest.Entities;

namespace MockTestCs.Features.AddToList;

public class AddToListUseCase(
    MockTestDbContext ctx
)
{
    public async Task<Result<AddToListResponse>> Do(AddToListPayload payload)
    {
        var list = await ctx.ReadingLists.FirstOrDefaultAsync(r => r.ID == payload.ListID);
        var fanfic = await ctx.Fanfics.FirstOrDefaultAsync(f => f.ID == payload.FanficID);

        if (list is null)
            return Result<AddToListResponse>.Fail("The list doesn't exists");

        if (fanfic is null)
            return Result<AddToListResponse>.Fail("The fanfic doesn't exists");

        if (list.Fanfics.Contains(fanfic))
            return Result<AddToListResponse>.Fail("The list alredy contains this fanfic");

        list.Fanfics.Add(fanfic);
        fanfic.ReadingLists.Add(list);
        await ctx.SaveChangesAsync();

        return Result<AddToListResponse>.Success(new ());
    }
}