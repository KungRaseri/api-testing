namespace Api.Model;

public partial class Plot
{
    public string Id { get; set; } = null!;

    public string TileId { get; set; } = null!;

    public int Area { get; set; }

    public int Food { get; set; }

    public int Ore { get; set; }

    public int Solar { get; set; }

    public int Stone { get; set; }

    public int Water { get; set; }

    public int Wind { get; set; }

    public int Wood { get; set; }

    public virtual Settlement? Settlement { get; set; }

    public virtual Tile Tile { get; set; } = null!;
}
