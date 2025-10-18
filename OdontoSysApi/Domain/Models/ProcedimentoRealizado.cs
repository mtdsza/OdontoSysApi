using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class ProcedimentoRealizado
{
    public ulong IdProcedimentoRealizado { get; set; }

    public int Quantidade { get; set; }

    public decimal ValorCobrado { get; set; }

    public string? Descricao { get; set; }

    public string? Anexo { get; set; }

    public ulong IdConsulta { get; set; }

    public ulong IdProcedimento { get; set; }

    public ulong? IdOrcamentoItem { get; set; }

    public virtual Consulta IdConsultaNavigation { get; set; } = null!;

    public virtual OrcamentoItem? IdOrcamentoItemNavigation { get; set; }

    public virtual Procedimento IdProcedimentoNavigation { get; set; } = null!;

    public virtual ICollection<MovimentacaoFinanceira> MovimentacoesFinanceiras { get; set; } = new List<MovimentacaoFinanceira>();
}
