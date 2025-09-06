using Microsoft.EntityFrameworkCore;
using MockExamCs.Entities;

namespace MockTest.Entities;

public class MockTestDbContext(DbContextOptions opt): DbContext(opt)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Fanfic> Fanfics => Set<Fanfic>();
    public DbSet<ReadingList> ReadingLists => Set<ReadingList>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<User>()
            .HasMany(u => u.Fanfics)
            .WithOne(f => f.Creator)
            .HasForeignKey(u => u.CreatorID)
            .OnDelete(DeleteBehavior.Cascade);

        model.Entity<User>()
            .HasMany(u => u.ReadingLists)
            .WithOne(r => r.User)
            .HasForeignKey(u => u.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        model.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        model.Entity<Fanfic>()
            .HasMany(f => f.ReadingLists)
            .WithMany(r => r.Fanfics)
            .UsingEntity(f => f.ToTable("ReadingListFanfics"));
    }
}