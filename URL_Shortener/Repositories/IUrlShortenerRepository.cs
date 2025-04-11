namespace URL_Shortener.Repositories;

public interface IUrlShortenerRepository
{
    Task<string> CreateAsync(string longUrl);
    Task<string> GetByShortCodeAsync(string shortCode);
}
