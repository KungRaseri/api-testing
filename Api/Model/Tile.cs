using System;
using System.Collections.Generic;

namespace Api;

public partial class Tile
{
    public string Id { get; set; } = null!;

    public double Elevation { get; set; }

    public double Temperature { get; set; }

    public double Precipitation { get; set; }

    public string RegionId { get; set; } = null!;

    public string BiomeId { get; set; } = null!;

    public virtual Biome Biome { get; set; } = null!;

    public virtual ICollection<Plot> Plots { get; set; } = new List<Plot>();

    public virtual Region Region { get; set; } = null!;
}
