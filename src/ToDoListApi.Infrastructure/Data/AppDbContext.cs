using Microsoft.EntityFrameworkCore;
using ToDoListApi.Domain.Entities;

namespace ToDoListApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(40);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.MaxCompletionDate).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });
    }
}
