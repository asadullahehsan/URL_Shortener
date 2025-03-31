using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.Services;

namespace URL_Shortener.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UrlShortenerController(IUrlShorteningService urlShorteningService) : ControllerBase
{
    private readonly IUrlShorteningService urlShorteningService = urlShorteningService;

    [HttpPost("/createshorturl")]
    public IActionResult ShortenUrl([FromBody] string longUrl)
    {
        if (!Uri.TryCreate(longUrl, UriKind.Absolute, out _))
        {
            return BadRequest("This is not a valid URL");
        }

        return Ok(urlShorteningService.GetShortCode(longUrl));
    }

    [HttpGet("/{shortCode}")]
    public IActionResult GetLongUrl(string shortCode)
    {
        var longUrl = urlShorteningService.GetLongUrl(shortCode);

        if (longUrl is null)
        {
            return NotFound();
        }

        return Ok(longUrl);
    }

    [HttpGet("/samplepage")]
    public IActionResult GetSamplePage()
    {
        return Ok("This is a sample page to test our URL shortener.");
    }
}
