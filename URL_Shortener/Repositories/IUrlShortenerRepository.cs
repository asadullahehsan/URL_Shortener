using URL_Shortener.Models;

namespace URL_Shortener.Repositories;

public interface IUrlShortenerRepository
{
    Task<string> CreateAsync(string longUrl);
    Task<string> GetByShortCodeAsync(string shortCode);
    Task<IEnumerable<LongUrlAndShortUrl>> GetMyUrls();
}
