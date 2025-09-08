namespace MockTestCs.Entities;


public class ReadingListHistory
{
    public Guid ID {get;set;}

    public Guid UserID {get;set;}
    public User User {get;set;}

    public Guid ReadingListID {get; set;}
    public ReadingList ReadingList {get; set;}

    public Guid HistoryID {get; set;}
    public History History {get; set;}
    
}