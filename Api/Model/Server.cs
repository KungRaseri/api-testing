namespace Api.Model;

public partial class Server
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Hostname { get; set; } = null!;

    public int Port { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ProfileServerDatum? ProfileServerDatum { get; set; }

    public virtual ICollection<World> Worlds { get; set; } = new List<World>();
}
