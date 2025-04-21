using URL_Shortener.Models;

namespace URL_Shortener.Services;

public interface IUrlShorteningService
{
    public Task<string> ShortenUrl(string longUrl);
    public Task<string> GetLongUrl(string shortCode);
    public Task<IEnumerable<LongUrlAndShortUrl>> GetMyUrls();
}