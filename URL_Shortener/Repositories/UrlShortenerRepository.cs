using URL_Shortener.Models;
using URL_Shortener.Utilities;
using Microsoft.EntityFrameworkCore;

namespace URL_Shortener.Repositories;

public class UrlShortenerRepository : IUrlShortenerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UrlShortenerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> CreateAsync(string longUrl)
    {
        var res = await _dbContext.ShortenedUrls.SingleOrDefaultAsync(url => url.LongUrl == longUrl);

        if (res != null)
        {
            return res.ShortCode;
        }

        string salt = Guid.NewGuid().ToString("N").Substring(0, 5);
        string input = longUrl + salt;
        string shortCode = UrlEncoder.GenerateShortCode(input);

        ShortenedUrl shortenedUrl = new ShortenedUrl
        {
            Id = Guid.NewGuid(),
            CreatedOnUtc = DateTime.UtcNow,
            LongUrl = longUrl,
            ShortCode = shortCode,
        };

        await _dbContext.AddAsync(shortenedUrl);
        await _dbContext.SaveChangesAsync();
        return shortCode;
    }        

    public async Task<string> GetByShortCodeAsync(string shortCode)
    {
        var res = await _dbContext.ShortenedUrls.SingleOrDefaultAsync(url => url.ShortCode == shortCode);

        if (res == null)
        {
            return string.Empty;
        }

        return res.LongUrl;
    }
}
