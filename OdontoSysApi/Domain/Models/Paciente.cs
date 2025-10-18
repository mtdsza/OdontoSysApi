using System;
using System.Collections.Generic;

namespace OdontoSysApi.Models;

public partial class Paciente
{
    public ulong IdPaciente { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public DateOnly Nascimento { get; set; }

    public string? Telefone { get; set; }

    public string? Email { get; set; }

    public string? Endereco { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();

    public virtual ICollection<Orcamento> Orcamentos { get; set; } = new List<Orcamento>();

    public virtual ICollection<Prontuario> Prontuarios { get; set; } = new List<Prontuario>();
}
