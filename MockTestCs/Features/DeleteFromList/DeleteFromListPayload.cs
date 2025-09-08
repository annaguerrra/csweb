namespace MockTestCs.Features.DeleteFromList;

public record DeleteFromListPayload(
    Guid ReadingListID,
    Guid HistoryID
);