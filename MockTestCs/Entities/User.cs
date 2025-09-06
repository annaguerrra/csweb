namespace MockExamCs.Entities;

public class User
{
    public Guid ID { get; set; }
    public required string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime AccountCreated { get; set; }

    public ICollection<Fanfic> Fanfics { get; set; }
    public ICollection<ReadingList> ReadingLists { get; set; }

}
