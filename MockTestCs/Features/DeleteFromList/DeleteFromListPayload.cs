namespace MockTestCs.Features.DeleteFromList;

public record DeleteFromListPayload(
    Guid ListID,
    Guid FanficID,
    string FTitle
);