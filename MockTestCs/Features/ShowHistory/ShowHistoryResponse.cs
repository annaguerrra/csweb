namespace MockTestCs.Features.ShowHistory;

public record ShowHistoryResponse(
    Guid HistoryID,
    Guid CreatorID,
    string Title,
    string? Content,
    string CreatorName,
    List<ReadingListDto> ReadingLists
);

public record ReadingListDto(Guid ListID, string Title);

