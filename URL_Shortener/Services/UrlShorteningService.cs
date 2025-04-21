using URL_Shortener.Models;
using URL_Shortener.Repositories;

namespace URL_Shortener.Services;

public class UrlShorteningService : IUrlShorteningService
{
    private readonly IUrlShortenerRepository _urlShortenerRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UrlShorteningService(IUrlShortenerRepository urlShortenerRepository, IHttpContextAccessor httpContextAccessor)
    {
        _urlShortenerRepository = urlShortenerRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> ShortenUrl(string longUrl)
    {
        return await _urlShortenerRepository.CreateAsync(longUrl);
    }

    public async Task<string> GetLongUrl(string shortCode)
    {
        return await _urlShortenerRepository.GetByShortCodeAsync(shortCode);
    }

    public async Task<IEnumerable<LongUrlAndShortUrl>> GetMyUrls()
    {
        var res = await _urlShortenerRepository.GetMyUrls();
        return res;
    }
}
