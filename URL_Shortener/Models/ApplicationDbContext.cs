using Microsoft.EntityFrameworkCore;

namespace URL_Shortener.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortenedUrl>((builder) =>
        {
            builder
            .Property(shortenedUrl => shortenedUrl.Code)
            .HasMaxLength(ShortLinkSettings.Length);

            builder
            .HasIndex(shortenedUrl => shortenedUrl.Code)
            .IsUnique();
        });
    }
}
