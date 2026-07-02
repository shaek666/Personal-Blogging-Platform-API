using Microsoft.EntityFrameworkCore;
using PersonalBloggingPlatform.Models;

namespace PersonalBloggingPlatform.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<Post> Posts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Posts");
            entity.Property(p => p.Tags).HasColumnType("jsonb");
            entity.HasIndex(p => p.CreatedAt);
        });
    }
}