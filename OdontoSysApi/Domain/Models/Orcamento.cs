using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class Orcamento
{
    public ulong IdOrcamento { get; set; }

    public DateOnly DataEmissao { get; set; }

    public DateOnly? DataValidade { get; set; }

    public decimal ValorTotal { get; set; }

    public ulong IdPaciente { get; set; }

    public ulong IdProfissional { get; set; }

    public ulong? IdConsulta { get; set; }

    public virtual Consulta? IdConsultaNavigation { get; set; }

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;

    public virtual Profissional IdProfissionalNavigation { get; set; } = null!;

    public virtual ICollection<MovimentacaoFinanceira> MovimentacoesFinanceiras { get; set; } = new List<MovimentacaoFinanceira>();

    public virtual ICollection<OrcamentoItem> OrcamentoItens { get; set; } = new List<OrcamentoItem>();

    public virtual ICollection<ParcelaAReceber> ParcelasARecebers { get; set; } = new List<ParcelaAReceber>();
}
