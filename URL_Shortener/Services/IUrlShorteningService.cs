namespace URL_Shortener.Services;

public interface IUrlShorteningService
{
    public string GetShortCode(string longUrl);
    public string? GetLongUrl(string shortCode);
}