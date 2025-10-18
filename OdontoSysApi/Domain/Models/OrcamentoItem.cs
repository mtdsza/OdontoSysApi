using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class OrcamentoItem
{
    public ulong IdOrcamentoItem { get; set; }

    public decimal ValorUnitario { get; set; }

    public int Quantidade { get; set; }

    public ulong IdOrcamento { get; set; }

    public ulong IdProcedimento { get; set; }

    public virtual Orcamento IdOrcamentoNavigation { get; set; } = null!;

    public virtual Procedimento IdProcedimentoNavigation { get; set; } = null!;

    public virtual ICollection<ProcedimentoRealizado> ProcedimentosRealizados { get; set; } = new List<ProcedimentoRealizado>();
}
