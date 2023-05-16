namespace Api.Model;

public partial class Region
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ElevationMap { get; set; } = null!;

    public string PrecipitationMap { get; set; } = null!;

    public string TemperatureMap { get; set; } = null!;

    public string WorldId { get; set; } = null!;

    public int XCoord { get; set; }

    public int YCoord { get; set; }

    public virtual ICollection<Tile> Tiles { get; set; } = new List<Tile>();

    public virtual World World { get; set; } = null!;
}
