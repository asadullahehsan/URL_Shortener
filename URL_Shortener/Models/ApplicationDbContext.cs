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
            .Property(shortenedUrl => shortenedUrl.ShortCode)
            .HasMaxLength(ShortLinkSettings.Length);

            builder
            .HasIndex(shortenedUrl => shortenedUrl.ShortCode)
            .IsUnique();
        });
    }
}
