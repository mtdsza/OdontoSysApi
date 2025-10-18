using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class ProfissionalEspecialidade
{
    public ulong IdProfissionalEspecialidade { get; set; }

    public ulong IdProfissional { get; set; }

    public ulong IdEspecialidade { get; set; }

    public virtual Especialidade IdEspecialidadeNavigation { get; set; } = null!;

    public virtual Profissional IdProfissionalNavigation { get; set; } = null!;
}
