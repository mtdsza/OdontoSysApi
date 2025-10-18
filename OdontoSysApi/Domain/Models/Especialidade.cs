using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class Especialidade
{
    public ulong IdEspecialidade { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<ProfissionalEspecialidade> ProfissionaisEspecialidades { get; set; } = new List<ProfissionalEspecialidade>();
}
