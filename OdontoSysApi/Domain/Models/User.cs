using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class User
{
    public ulong Id { get; set; }

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public ulong? IdProfissional { get; set; }

    public DateTime? EmailVerifiedAt { get; set; }

    public string? RememberToken { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Profissional? IdProfissionalNavigation { get; set; }
}
