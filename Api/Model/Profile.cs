using System;
using System.Collections.Generic;

namespace Api;

public partial class Profile
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Picture { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual ProfileServerDatum? ProfileServerDatum { get; set; }
}
