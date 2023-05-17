using System.Text.Json;
using Api.Model;
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
        var serialOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        var mapOptions = JsonSerializer.Deserialize<MapOptions>(worldPreviewSettings.MapOptions, serialOptions);
        var biomes = JsonSerializer.Deserialize<Biome[]>(worldPreviewSettings.Biomes, serialOptions);
        var elevationOptions = JsonSerializer.Deserialize<Options>(worldPreviewSettings.ElevationOptions, serialOptions);
        var precipitationOptions = JsonSerializer.Deserialize<Options>(worldPreviewSettings.PrecipitationOptions, serialOptions);
        var temperatureOptions = JsonSerializer.Deserialize<Options>(worldPreviewSettings.TemperatureOptions, serialOptions);

        if (mapOptions == null || biomes == null || elevationOptions == null || precipitationOptions == null || temperatureOptions == null) return BadRequest("Failed to deserialize parameters");

        var maps = WorldGenerator.GenerateMaps(mapOptions, elevationOptions, precipitationOptions, temperatureOptions);
        var world = WorldGenerator.InitializeWorld(mapOptions, elevationOptions, precipitationOptions, temperatureOptions);
        var regions = WorldGenerator.InitializeRegions(world, maps);
        var tiles = WorldGenerator.InitializeTiles(regions, biomes);
        var plots = WorldGenerator.InitializePlots(tiles, biomes);

        return Ok(new
        {
            world = world,
            regions = regions,
            tiles = tiles,
            plots = plots
        });
    }

    [HttpPost]
    [Route("save")]
    public async Task<IActionResult> Save()
    {
        return Ok("save");
    }

}
