using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;

namespace URL_Shortener.Services;

public class UrlShorteningService : IUrlShorteningService
{
    private readonly ApplicationDbContext _context;
    public UrlShorteningService(ApplicationDbContext context)
    {
        _context = context;
    }

    private static readonly char[] _chars = ShortLinkSettings.Characters.ToCharArray();

    private Dictionary<string, string> UrlDict { get; set; } = [];

    private static string GenerateShortCode()
    {
        return string.Create(ShortLinkSettings.Length, _chars, (shortCode, charsState)
            => Random.Shared.GetItems(charsState, shortCode));
    }

    public string GetShortCode(string longUrl)
    {
        var shortenedUrl = _context.ShortenedUrls.SingleOrDefault(url => url.LongUrl == longUrl);

        if (shortenedUrl != null)
        {
            return shortenedUrl.Code;
        }

        string shortCode = string.Empty;
        while (true)
        {
            shortCode = GenerateShortCode();

            var sUrl = _context.ShortenedUrls.SingleOrDefault(url => url.Code == shortCode);
            if (shortenedUrl != null)
            {
                continue;
            }

            _context.Add(new ShortenedUrl
            {
                Id = Guid.NewGuid(),
                CreatedOnUtc = DateTime.Now,
                Code = shortCode,
                LongUrl = longUrl,
                ShortUrl = "/" + shortCode
            });

            var res = _context.SaveChanges();

            if (res > 0)
            {
                break;
            }
        }
        var resUrl = _context.ShortenedUrls.SingleOrDefault(url => url.LongUrl == longUrl)!;
        return resUrl.Code;
    }

    public string? GetLongUrl(string shortCode)
    {
        var shortenedUrl = _context.ShortenedUrls.SingleOrDefault(url => url.Code == shortCode);
        if (shortenedUrl != null)
        {
            return shortenedUrl.LongUrl;
        }

        return default;
    }
}
