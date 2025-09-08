namespace MockTestCs.Entities;

public class History
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }

    public Guid UserID { get; set; }
    public User User { get; set; }

    public ICollection<ReadingList>? ReadingLists {get; set;}
    public ICollection<ReadingListHistory>? ReadingListHistories {get; set;}
  }