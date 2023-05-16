using System;
using System.Collections.Generic;

namespace Api;

public partial class Structure
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<SettlementStructure> SettlementStructures { get; set; } = new List<SettlementStructure>();
}
