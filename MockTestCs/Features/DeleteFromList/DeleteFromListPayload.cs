namespace MockTestCs.Features.DeleteFromList;

public record DeleteFromListPayload(
    Guid UserID,
    Guid ReadingListID,
    Guid HistoryID
);