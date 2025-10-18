using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class Prontuario
{
    public ulong IdProntuario { get; set; }

    public string? HistoricoOdontologico { get; set; }

    public string? TratamentosAnteriores { get; set; }

    public string? Diagnostico { get; set; }

    public string? Prescricoes { get; set; }

    public string? Observacoes { get; set; }

    public DateTime DataRegistro { get; set; }

    public ulong IdPaciente { get; set; }

    public ulong IdProfissional { get; set; }

    public ulong? IdConsulta { get; set; }

    public virtual Consulta? IdConsultaNavigation { get; set; }

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;

    public virtual Profissional IdProfissionalNavigation { get; set; } = null!;
}
