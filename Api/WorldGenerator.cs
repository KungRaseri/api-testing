using System.Text.Json;
using Api.Model;
using Visus.Cuid;

namespace Api;

public static class WorldGenerator
{
    public static Maps GenerateMaps(MapOptions mapOptions, Options elevationOptions, Options precipitationOptions, Options temperatureOptions)
    {

        var elevationNoise = new FastNoiseLite((int)mapOptions.ElevationSeed);
        var precipitationNoise = new FastNoiseLite((int)mapOptions.PrecipitationSeed);
        var temperatureNoise = new FastNoiseLite((int)mapOptions.TemperatureSeed);

        var elevationNoiseData = new float[mapOptions.Width, mapOptions.Height];
        var precipitationNoiseData = new float[mapOptions.Width, mapOptions.Height];
        var temperatureNoiseData = new float[mapOptions.Width, mapOptions.Height];

        for (int y = 0; y < mapOptions.Height; y++)
        {
            for (int x = 0; x < mapOptions.Width; x++)
            {
                elevationNoiseData[x, y] = elevationNoise.GetNoise(x, y);
                precipitationNoiseData[x, y] = precipitationNoise.GetNoise(x, y);
                temperatureNoiseData[x, y] = temperatureNoise.GetNoise(x, y);
            }
        }

        return new Maps()
        {
            ElevationNoiseData = elevationNoiseData,
            PrecipitationNoiseData = precipitationNoiseData,
            TemperatureNoiseData = temperatureNoiseData
        };
    }

    public static World InitializeWorld(MapOptions mapOptions, Options elevationOptions, Options precipitationOptions, Options temperatureOptions)
    {
        return new World
        {
            Id = Cuid.NewCuid().ToString(),
            Name = mapOptions.WorldName,
            ServerId = mapOptions.ServerId,
            ElevationSettings = JsonSerializer.Serialize(elevationOptions),
            PrecipitationSettings = JsonSerializer.Serialize(precipitationOptions),
            TemperatureSettings = JsonSerializer.Serialize(temperatureOptions),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public static Region[] InitializeRegions(World world, Maps maps)
    {
        return new Region[] {

        };
    }
}