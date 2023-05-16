using System;
using System.Collections.Generic;

namespace Api;

public partial class SettlementStructure
{
    public string Id { get; set; } = null!;

    public string SettlementId { get; set; } = null!;

    public int Level { get; set; }

    public string StructureId { get; set; } = null!;

    public virtual Settlement Settlement { get; set; } = null!;

    public virtual Structure Structure { get; set; } = null!;
}
