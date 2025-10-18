using OdontoSysApi.Application;
using OdontoSysApi.Application.DTOs;
using OdontoSysApi.Models;

namespace OdontoSysApi.Domain.Services.Interfaces;

public interface IPacienteServiceDomain
{
    Task<ApplicationResult<long>> Inserir(PacienteDTO pacienteDTO);

    Task<ApplicationResult<PacienteDTO>> Atualizar(PacienteDTO pacienteDTO);

    Task<ApplicationResult<PacienteDTO>> Excluir(int id);

    Task<ApplicationResult<List<PacienteDTO>>>
                 BuscarTodos(); 

}    