namespace Api.Model;

public class MapOptions
{
    public string ServerId { get; set; }
    public string WorldName { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public long ElevationSeed { get; set; }
    public long PrecipitationSeed { get; set; }
    public long TemperatureSeed { get; set; }
}
