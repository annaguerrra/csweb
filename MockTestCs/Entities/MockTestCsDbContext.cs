using Microsoft.EntityFrameworkCore;

namespace MockTestCs.Entities;

public class MockTestCsDbContext(DbContextOptions opt) : DbContext(opt)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<ReadingList> ReadingLists => Set<ReadingList>();
    public DbSet<ReadingListHistory> ReadingListHistorys => Set<ReadingListHistory>();
    public DbSet<History> Histories => Set<History>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<History>()
            .HasOne(h => h.User)
            .WithMany(u => u.Histories)
            .HasForeignKey(h => h.UserID)
            .OnDelete(DeleteBehavior.NoAction); 

        model.Entity<ReadingListHistory>()
            .HasOne(r => r.User)
            .WithMany(u => u.ReadingListHistories)
            .HasForeignKey(r => r.UserID)
            .OnDelete(DeleteBehavior.NoAction); 

        model.Entity<ReadingListHistory>()
            .HasOne(r => r.ReadingList)
            .WithMany(u => u.ReadingListHistories)
            .HasForeignKey(r => r.ReadingListID)
            .OnDelete(DeleteBehavior.NoAction);

        model.Entity<ReadingListHistory>()
            .HasOne(r => r.History)
            .WithMany(u => u.ReadingListHistories)
            .HasForeignKey(r => r.HistoryID)
            .OnDelete(DeleteBehavior.NoAction);   
    }
}