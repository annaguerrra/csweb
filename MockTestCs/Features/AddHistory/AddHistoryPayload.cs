namespace MockTestCs.Features.AddHistory;

public record AddHistoryPayload(
    Guid UserID,
    Guid HistoryID,
    string Title,
    string? Content
);