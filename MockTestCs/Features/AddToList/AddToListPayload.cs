namespace MockTestCs.Features.AddToList;

public record AddToListPayload(
    Guid UserID,
    Guid HistoryID,
    Guid ReadingListID
);