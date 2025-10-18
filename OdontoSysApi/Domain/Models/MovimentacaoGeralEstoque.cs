using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class MovimentacaoGeralEstoque
{
    public ulong IdMovimentacaoGeral { get; set; }

    public decimal Quantidade { get; set; }

    public string Tipo { get; set; } = null!;

    public string? Justificativa { get; set; }

    public DateTime DataMovimentacao { get; set; }

    public ulong IdItemEstoque { get; set; }

    public virtual Estoque IdItemEstoqueNavigation { get; set; } = null!;
}
