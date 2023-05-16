using System;
using System.Collections.Generic;

namespace Api;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string UserAuthToken { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Profile? Profile { get; set; }
}
