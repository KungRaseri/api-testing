namespace Api.Model;

public partial class Settlement
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string PlayerProfileId { get; set; } = null!;

    public string PlotId { get; set; } = null!;

    public virtual ProfileServerDatum PlayerProfile { get; set; } = null!;

    public virtual Plot Plot { get; set; } = null!;

    public virtual ICollection<SettlementStructure> SettlementStructures { get; set; } = new List<SettlementStructure>();
}
