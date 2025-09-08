namespace MockTestCs.Entities;
public class User
{
    public Guid ID { get; set; }
    public required string Username { get; set; }
    public required string Email  { get; set; }
    public required string Password {get;set;}  
    public string? Description {get;set;}
    public DateTime AccountCreatedDate {get;init;}
    public ICollection<History>? Histories {get; set;} 
    public ICollection<ReadingList>? ReadingLists {get; set;}
    public ICollection<ReadingListHistory>? ReadingListHistories {get; set;}

}