namespace MockExamCs.Entities;

public class Fanfic
{
    public Guid ID { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }

    public Guid CreatorID { get; set; }
    public User Creator { get; set; }
    public ICollection<ReadingList> ReadingLists {get;set;}
}