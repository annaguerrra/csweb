using Microsoft.EntityFrameworkCore;
using MockTestCs.Entities;

namespace MockTestCs.Features.ShowHistory;

public class ShowHistoryUseCase( 
    MockTestCsDbContext ctx
)
{
    public async Task<Result<ShowHistoryResponse>> Do(ShowHistoryPayload payload)
    {
        var history = await ctx.Histories
            .Include(h => h.User)
            .Include(u => u.ReadingListHistories)
                .ThenInclude(rlh => rlh.ReadingList)
            .FirstOrDefaultAsync(r => r.Id == payload.HistoryID);

        if (history is null)
            return Result<ShowHistoryResponse>.Fail("History not found");

        var response = new ShowHistoryResponse
        (
            history.Id,
            history.UserID,
            history.Title,
            history.Content,
            history.User.Username,
            history.ReadingListHistories
                    .Select(r => new ReadingListDto(r.ID, r.ReadingList.Title))
                    .ToList()
        );

        return Result<ShowHistoryResponse>.Success(response);
    }
}