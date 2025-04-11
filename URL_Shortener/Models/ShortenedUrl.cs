namespace URL_Shortener.Models;

public class ShortenedUrl
{
    public Guid Id { get; set; }
    public string LongUrl { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public DateTime CreatedOnUtc { get; set; }
}
