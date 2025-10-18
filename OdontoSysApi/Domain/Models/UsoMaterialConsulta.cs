using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class UsoMaterialConsulta
{
    public ulong IdUsoMaterial { get; set; }

    public decimal Quantidade { get; set; }

    public DateTime DataUso { get; set; }

    public ulong IdConsulta { get; set; }

    public ulong IdItemEstoque { get; set; }

    public virtual Consulta IdConsultaNavigation { get; set; } = null!;

    public virtual Estoque IdItemEstoqueNavigation { get; set; } = null!;
}
