namespace MockTestCs.Features.AddToList;

public record AddToListPayload(
    Guid HistoryID,
    Guid ReadingListID
);