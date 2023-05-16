using System;
using System.Collections.Generic;

namespace Api;

public partial class ProfileServerDatum
{
    public string ProfileId { get; set; } = null!;

    public string ServerId { get; set; } = null!;

    public string Id { get; set; } = null!;

    public virtual Profile Profile { get; set; } = null!;

    public virtual Server Server { get; set; } = null!;

    public virtual ICollection<Settlement> Settlements { get; set; } = new List<Settlement>();
}
