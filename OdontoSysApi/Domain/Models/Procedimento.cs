using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class Procedimento
{
    public ulong IdProcedimento { get; set; }

    public string Nome { get; set; } = null!;

    public decimal ValorPadrao { get; set; }

    public virtual ICollection<OrcamentoItem> OrcamentoItens { get; set; } = new List<OrcamentoItem>();

    public virtual ICollection<ProcedimentoRealizado> ProcedimentosRealizados { get; set; } = new List<ProcedimentoRealizado>();
}
