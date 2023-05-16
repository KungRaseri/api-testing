using System.Text.Json;
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
    public async Task<IActionResult> Preview([FromForm] WorldPreviewSettings worldPreviewSettings)
    {
        var mapOptions = JsonSerializer.Deserialize<MapOptions>(worldPreviewSettings.MapOptions);
        var biomes = JsonSerializer.Deserialize<Biome[]>(worldPreviewSettings.Biomes);
        var elevationOptions = JsonSerializer.Deserialize<Options>(worldPreviewSettings.ElevationOptions);
        var precipitationOptions = JsonSerializer.Deserialize<Options>(worldPreviewSettings.PrecipitationOptions);
        var temperatureOptions = JsonSerializer.Deserialize<Options>(worldPreviewSettings.TemperatureOptions);

        return Ok(new
        {
            world = new World { },
            regions = new Region[] { },
            tiles = new Tile[] { },
            plots = new Plot[] { }
        });
    }

    [HttpPost]
    [Route("save")]
    public async Task<IActionResult> Save()
    {
        return Ok("save");
    }
}

public class WorldPreviewSettings
{
    public string MapOptions { get; set; }
    public string Biomes { get; set; }
    public string ElevationOptions { get; set; }
    public string PrecipitationOptions { get; set; }
    public string TemperatureOptions { get; set; }
}

public class MapOptions
{
    public string serverId { get; set; }
    public string worldName { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public long elevationSeed { get; set; }
    public long precipitationSeed { get; set; }
    public long temperatureSeed { get; set; }
}

public class Options
{
    public decimal amplitude;
    public decimal frequency;
    public int octaves;
    public decimal persistence;
    public int scale;
}
public class WorldPreviewResponse
{

}