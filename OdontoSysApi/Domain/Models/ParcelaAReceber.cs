using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class ParcelaAReceber
{
    public ulong IdParcela { get; set; }

    public string? Descricao { get; set; }

    public decimal Valor { get; set; }

    public DateOnly DataVencimento { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? DataPagamento { get; set; }

    public ulong IdOrcamento { get; set; }

    public virtual Orcamento IdOrcamentoNavigation { get; set; } = null!;

    public virtual ICollection<MovimentacaoFinanceira> MovimentacoesFinanceiras { get; set; } = new List<MovimentacaoFinanceira>();
}
