namespace MockTestCs.Entities;
public class User
{
    public Guid ID { get; set; }
    public string Username { get; set; }
    public string Email  { get; set; }
    public string Password {get;set;}  
    public string Description {get;set;}
    public DateTime AccountCreatedDate {get;init;}
    public ICollection<History> Histories {get; set;} 
    public ICollection<ReadingList> ReadingLists {get; set;}
    public ICollection<ReadingListHistory> ReadingListHistories {get; set;}

}