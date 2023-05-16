using System;
using System.Collections.Generic;

namespace Api.Model;

public partial class Biome
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double PrecipitationMin { get; set; }

    public double PrecipitationMax { get; set; }

    public double TemperatureMin { get; set; }

    public double TemperatureMax { get; set; }

    public int FoodModifier { get; set; }

    public int OreModifier { get; set; }

    public int PlotAreaMax { get; set; }

    public int PlotAreaMin { get; set; }

    public int PlotsMax { get; set; }

    public int PlotsMin { get; set; }

    public int SolarModifier { get; set; }

    public int StoneModifier { get; set; }

    public int WaterModifier { get; set; }

    public int WindModifier { get; set; }

    public int WoodModifier { get; set; }

    public virtual ICollection<Tile> Tiles { get; set; } = new List<Tile>();
}
