using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class Profissional
{
    public ulong IdProfissional { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefone { get; set; }

    public string Cro { get; set; } = null!;

    public bool? Ativo { get; set; }

    public DateOnly? DataContratacao { get; set; }

    public decimal? SalarioBase { get; set; }

    public virtual ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();

    public virtual ICollection<Orcamento> Orcamentos { get; set; } = new List<Orcamento>();

    public virtual ICollection<ProfissionalEspecialidade> ProfissionaisEspecialidades { get; set; } = new List<ProfissionalEspecialidade>();

    public virtual ICollection<Prontuario> Prontuarios { get; set; } = new List<Prontuario>();

    public virtual User? User { get; set; }
}
