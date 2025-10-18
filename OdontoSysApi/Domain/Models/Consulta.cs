using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class Consulta
{
    public ulong IdConsulta { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public string? Descricao { get; set; }

    public string Situacao { get; set; } = null!;

    public ulong IdPaciente { get; set; }

    public ulong IdProfissional { get; set; }

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;

    public virtual Profissional IdProfissionalNavigation { get; set; } = null!;

    public virtual ICollection<MovimentacaoFinanceira> MovimentacoesFinanceiras { get; set; } = new List<MovimentacaoFinanceira>();

    public virtual ICollection<Orcamento> Orcamentos { get; set; } = new List<Orcamento>();

    public virtual ICollection<ProcedimentoRealizado> ProcedimentosRealizados { get; set; } = new List<ProcedimentoRealizado>();

    public virtual ICollection<Prontuario> Prontuarios { get; set; } = new List<Prontuario>();

    public virtual ICollection<UsoMaterialConsulta> UsosMateriaisConsultas { get; set; } = new List<UsoMaterialConsulta>();
}
