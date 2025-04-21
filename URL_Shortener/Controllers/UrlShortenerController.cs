using URL_Shortener.Services;
using Microsoft.AspNetCore.Mvc;

namespace URL_Shortener.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UrlShortenerController(IUrlShorteningService urlShorteningService) : ControllerBase
{
    private readonly IUrlShorteningService _urlShorteningService = urlShorteningService;

    [HttpPost("/shorten-url")]
    public async Task<IActionResult> ShortenUrl([FromBody] string longUrl)
    {
        if (!Uri.TryCreate(longUrl, UriKind.Absolute, out var uriResult) || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
        {
            return BadRequest("This is not a valid URL. It should start with http:// or https://");
        }

        try
        {
            return Ok(await _urlShorteningService.ShortenUrl(longUrl));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("/{shortCode}")]
    public async Task<IActionResult> GetLongUrl(string shortCode)
    {
        var longUrl = await _urlShorteningService.GetLongUrl(shortCode);

        if (longUrl == string.Empty)
        {
            return NotFound("No such URL exists.");
        }

        return Ok(longUrl);
    }

    [HttpGet("/my-urls")]
    public async Task<IActionResult> GetAllUrls()
    {
        var res = await _urlShorteningService.GetMyUrls();
        return Ok(res);
    }
}
