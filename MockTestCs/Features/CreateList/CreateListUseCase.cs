using Microsoft.EntityFrameworkCore;
using MockTestCs.Entities;

namespace MockTestCs.Features.CreateList;

public class CreateListUseCase(
    MockTestCsDbContext ctx
)
{
    public async Task<Result<CreateListResponse>> Do(CreateListPayload payload)
    {
        var exist = ctx.ReadingLists
            .FirstOrDefaultAsync(l => l.UserID == payload.UserID && l.Title == payload.Title);

        if (exist is not null)
            return Result<CreateListResponse>.Fail("You already have a list with that name");
            
        var user = await ctx.Users
            .Include(u => u.ReadingLists)
            .FirstOrDefaultAsync(u => u.ID == payload.UserID);

        if (user is null)
            return Result<CreateListResponse>.Fail("User not found");

        var list = new ReadingList
        {
            Title = payload.Title,
            UserID = user.ID,
            LastModificationDate = DateTime.UtcNow
        };

        ctx.ReadingLists.Add(list);
        
        await ctx.SaveChangesAsync();

        return Result<CreateListResponse>.Success(new CreateListResponse(
            list.ID,
            list.Title,
            list.LastModificationDate.ToLocalTime()
        ));
    }
}