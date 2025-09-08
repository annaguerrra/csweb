namespace MockTestCs.Features.AddHistory;

public record AddHistoryResponse(
    Guid HistoryID,
    string Title,
    string? Content
);