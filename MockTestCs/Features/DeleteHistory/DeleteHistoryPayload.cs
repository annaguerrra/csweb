namespace MockTestCs.Features.DeleteHistory;

public record DeleteHistoryPayload(
    Guid HistoryID,
    Guid UserID
);