namespace Api.Model;

public partial class World
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ElevationSettings { get; set; } = null!;

    public string PrecipitationSettings { get; set; } = null!;

    public string TemperatureSettings { get; set; } = null!;

    public string ServerId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

    public virtual Server Server { get; set; } = null!;
}
