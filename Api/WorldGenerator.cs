using System.Text.Json;
using Api.Model;
using NCuid;

namespace Api;

public partial class WorldGenerator
{
    public static Maps GenerateMaps(MapOptions mapOptions, Options elevationOptions, Options precipitationOptions, Options temperatureOptions)
    {
        var elevationNoise = new FastNoiseLite((int)mapOptions.ElevationSeed);
        var precipitationNoise = new FastNoiseLite((int)mapOptions.PrecipitationSeed);
        var temperatureNoise = new FastNoiseLite((int)mapOptions.TemperatureSeed);

        elevationNoise.SetFractalOctaves(elevationOptions.octaves);
        elevationNoise.SetFractalGain(elevationOptions.persistence);
        elevationNoise.SetFractalWeightedStrength(elevationOptions.scale);
        elevationNoise.SetFractalType(FastNoiseLite.FractalType.Ridged);

        precipitationNoise.SetFractalOctaves(precipitationOptions.octaves);
        precipitationNoise.SetFractalGain(precipitationOptions.persistence);
        precipitationNoise.SetFractalWeightedStrength(precipitationOptions.scale);
        precipitationNoise.SetFractalType(FastNoiseLite.FractalType.Ridged);

        temperatureNoise.SetFractalOctaves(temperatureOptions.octaves);
        temperatureNoise.SetFractalGain(temperatureOptions.persistence);
        temperatureNoise.SetFractalWeightedStrength(temperatureOptions.scale);
        temperatureNoise.SetFractalType(FastNoiseLite.FractalType.Ridged);

        var elevationNoiseData = new double[mapOptions.Width * mapOptions.Height];
        var precipitationNoiseData = new double[mapOptions.Width * mapOptions.Height];
        var temperatureNoiseData = new double[mapOptions.Width * mapOptions.Height];

        var index = 0;
        for (int x = 0; x < mapOptions.Width; x++)
        {
            for (int y = 0; y < mapOptions.Height; y++)
            {
                elevationNoiseData[index] = (double)elevationNoise.GetNoise(x, y);
                precipitationNoiseData[index] = (double)precipitationNoise.GetNoise(x, y);
                temperatureNoiseData[index] = (double)temperatureNoise.GetNoise(x, y);

                index++;
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
            Id = Cuid.Generate(),
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
        if (maps.ElevationNoiseData == null || maps.PrecipitationNoiseData == null || maps.TemperatureNoiseData == null)
        {
            throw new Exception("NoiseData failed to be generated");
        }

        var regions = new List<Region>();

        var elevationChunks = maps.ElevationNoiseData.ToSquare2D(100);
        var precipitationChunks = maps.PrecipitationNoiseData.ToSquare2D(100);
        var temperatureChunks = maps.TemperatureNoiseData.ToSquare2D(100);

        for (int row = 0; row < elevationChunks.GetLength(0); row++)
        {
            var region = new Region
            {
                Id = Cuid.Generate(),
                WorldId = world.Id,
                XCoord = row,
                YCoord = row + row / 10,
                Name = $"{row}:{row + row / 10}"
            };


            var elevationMap = new List<double>();
            var precipitationMap = new List<double>();
            var temperatureMap = new List<double>();

            for (int column = 0; column < elevationChunks.GetLength(1); column++)
            {
                elevationMap.Add(elevationChunks[row, column]);
                precipitationMap.Add(precipitationChunks[row, column]);
                temperatureMap.Add(temperatureChunks[row, column]);
            }

            region.ElevationMap = JsonSerializer.Serialize(elevationMap);
            region.PrecipitationMap = JsonSerializer.Serialize(precipitationMap);
            region.TemperatureMap = JsonSerializer.Serialize(temperatureMap);

            regions.Add(region);
        }

        return regions.ToArray();
    }

    public static Tile[] InitializeTiles(Region[] regions, Biome[] biomes)
    {
        var tiles = new List<Tile>();

        foreach (var region in regions)
        {
            var elevationMap = JsonSerializer.Deserialize<double[]>(region.ElevationMap);
            var precipitationMap = JsonSerializer.Deserialize<double[]>(region.PrecipitationMap);
            var temperatureMap = JsonSerializer.Deserialize<double[]>(region.TemperatureMap);


            for (int index = 0; index < elevationMap.Length; index++)
            {
                var type = elevationMap[index] < 0 ? TileType.OCEAN : TileType.LAND;
                var biome = DetermineBiome(biomes, normalizeValue(precipitationMap[index], 0, 450), normalizeValue(temperatureMap[index], -10, 32));

                tiles.Add(new Tile
                {
                    Id = Cuid.Generate(),
                    RegionId = region.Id,
                    BiomeId = biome.Id,
                    Type = type,
                    Elevation = elevationMap[index],
                    Precipitation = precipitationMap[index],
                    Temperature = temperatureMap[index]
                });
            }
        }

        return tiles.ToArray();
    }

    public static Biome DetermineBiome(Biome[] biomes, double precipitation, double temperature)
    {
        var filteredBiomes = biomes
                                .ToList()
                                .FindAll(
                                    biome =>
                                        Math.Round(precipitation) >= biome.PrecipitationMin
                                        && Math.Round(precipitation) <= biome.PrecipitationMax
                                        && Math.Round(temperature) >= biome.TemperatureMin
                                        && Math.Round(temperature) <= biome.TemperatureMax);

        if (!filteredBiomes.Any())
        {
            filteredBiomes = biomes
                                .ToList()
                                .FindAll(
                                    biome =>
                                        Math.Round(precipitation) >= biome.PrecipitationMin
                                        && Math.Round(precipitation) <= biome.PrecipitationMax);
        }

        return filteredBiomes[new Random().Next(filteredBiomes.Count)];
    }

    public static double normalizeValue(double value, double min, double max)
    {
        return value * (max - min) / 2 + (max + min) / 2;
    }

    public static Plot[] InitializePlots(Tile[] tiles, Biome[] biomes)
    {
        var plots = new List<Plot>();

        foreach (var tile in tiles)
        {
            var biome = biomes.FirstOrDefault<Biome>(biome => biome.Id == tile.BiomeId);

            var plotsTotal = Math.Floor(new Random().NextDouble() * (biome.PlotsMax - biome.PlotsMin + 1)) + biome.PlotsMin;

            for (int i = 0; i < plotsTotal; i++)
            {
                plots.Add(new Plot
                {
                    Id = Cuid.Generate(),
                    TileId = tile.Id,
                    Area = (int)(50 + Math.Floor(tile.Elevation + 1 * 5)),
                    Solar = (int)(3 + Math.Floor(tile.Elevation + 1) - Math.Floor(tile.Precipitation + 1 * 2) + Math.Floor(tile.Temperature + 1 * 3)),
                    Wind = (int)(3 + Math.Floor(tile.Elevation + 1 * 2) + Math.Floor(tile.Precipitation + 1 * 2) - Math.Floor(tile.Temperature + 1 * 2)),
                    Water = (int)(3 + Math.Floor(tile.Precipitation + 1 * 3) - Math.Floor(tile.Temperature + 1)),
                    Food = (int)(1 + Math.Floor(tile.Precipitation + 1 * 2) + Math.Floor(tile.Temperature + 1)),
                    Wood = (int)(1 + Math.Floor(tile.Precipitation + 1 * 3) + new Random().NextInt64(3)),
                    Stone = (int)(1 + Math.Floor(tile.Elevation + 1 * 3) + new Random().NextInt64(2)),
                    Ore = (int)(1 + Math.Floor(tile.Elevation + 1 * 3) + new Random().NextInt64(3))
                });
            }
        }

        return plots.ToArray();
    }
}