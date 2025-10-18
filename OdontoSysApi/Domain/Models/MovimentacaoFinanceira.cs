using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class MovimentacaoFinanceira
{
    public ulong IdMovimentacaoFinanceira { get; set; }

    public string Descricao { get; set; } = null!;

    public decimal Valor { get; set; }

    public string Tipo { get; set; } = null!;

    public DateTime DataMovimentacao { get; set; }

    public ulong? IdConsulta { get; set; }

    public ulong? IdOrcamento { get; set; }

    public ulong? IdProcedimentoRealizado { get; set; }

    public ulong? IdParcelaPaga { get; set; }

    public virtual Consulta? IdConsultaNavigation { get; set; }

    public virtual Orcamento? IdOrcamentoNavigation { get; set; }

    public virtual ParcelaAReceber? IdParcelaPagaNavigation { get; set; }

    public virtual ProcedimentoRealizado? IdProcedimentoRealizadoNavigation { get; set; }
}
