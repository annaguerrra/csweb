namespace MockTestCs.Features.AddHistory;

public record AddHistoryPayload(
    Guid HistoryID,
    string Title,
    string? Content
);