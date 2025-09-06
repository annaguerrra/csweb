namespace MockExamCs.Entities;

public class ReadingList
{
    public Guid ID { get; set; }
    public string? Title { get; set; }
    public DateTime ModificationDate { get; set; }

    public Guid UserID { get; set; }
    public User User { get; set; }

    public ICollection<Fanfic> Fanfics { get; set; } = new List<Fanfic>();
}