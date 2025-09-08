namespace MockTestCs.Entities;

public class ReadingList
{
    public Guid ID { get; set; }
    public string Title { get; set; }
    public DateTime LastModificationDate { get; set; }

    public Guid UserID {get; set;}
    public User User {get; set;}
    public ICollection<History> Histories {get; set;} = new List<History>();
    public ICollection<ReadingListHistory> ReadingListHistories {get; set;}
}