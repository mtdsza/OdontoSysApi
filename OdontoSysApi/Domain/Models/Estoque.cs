using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class Estoque
{
    public ulong IdItemEstoque { get; set; }

    public string Descricao { get; set; } = null!;

    public decimal Quantidade { get; set; }

    public decimal EstoqueMin { get; set; }

    public virtual ICollection<MovimentacaoGeralEstoque> MovimentacoesGeraisEstoque { get; set; } = new List<MovimentacaoGeralEstoque>();

    public virtual ICollection<UsoMaterialConsulta> UsosMateriaisConsultas { get; set; } = new List<UsoMaterialConsulta>();
}
