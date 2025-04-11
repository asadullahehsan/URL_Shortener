namespace URL_Shortener.Services;

public interface IUrlShorteningService
{
    public Task<string> ShortenUrl(string longUrl);
    public Task<string> GetLongUrl(string shortCode);
}