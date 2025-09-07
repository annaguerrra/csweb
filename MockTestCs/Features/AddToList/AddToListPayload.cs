using MockExamCs.Entities;

namespace MockTestCs.Features.AddToList;

public record AddToListPayload(
    Guid ListID,
    Guid FanficID
);