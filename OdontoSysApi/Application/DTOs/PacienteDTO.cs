using System.ComponentModel.DataAnnotations;

namespace OdontoSysApi.Application.DTOs;

public class PacienteDTO
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Nome obrigatório.")]
    [MaxLength(255, ErrorMessage = "Limite excedido.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "CPF obrigatório.")]
    [MaxLength(11, ErrorMessage = "Limite excedido.")]
    public string CPF { get; set; }

    [MaxLength(13, ErrorMessage = "Limite excedido.")]
    public string Telefone { get; set; }

    [MaxLength(255, ErrorMessage = "Limite excedido.")]
    public string Email { get; set; }

    [MaxLength(255, ErrorMessage = "Limite excedido.")]
    public string Endereco { get; set; }

    public DateTime Nascimento { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}