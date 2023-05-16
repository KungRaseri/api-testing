using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WorldController : ControllerBase
{
    private readonly ILogger<WorldController> _logger;
    private readonly BrowserGameContext _context;

    public WorldController(ILogger<WorldController> logger, BrowserGameContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    [Route("preview")]
    public async Task<IActionResult> Preview()
    {
        var accounts = _context.Set<Account>();

        return Ok(accounts);
    }

    [HttpPost]
    [Route("save")]
    public async Task<IActionResult> Save()
    {
        return Ok("save");
    }
}

public class WorldPreviewResponse
{

}