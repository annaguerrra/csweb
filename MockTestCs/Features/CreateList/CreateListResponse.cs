namespace MockTestCs.Features.CreateList;

public record CreateListResponse(
    Guid ID,
    string Title,
    DateTime LastModificationDate
);