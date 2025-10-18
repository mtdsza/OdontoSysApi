using OdontoSysApi.Application;
using OdontoSysApi.Application.DTOs;
using OdontoSysApi.Domain.Services.Interfaces;
using OdontoSysApi.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OdontoSysApi.Controllers;

public class PacienteController : ApiControllerBase
{

    private readonly IPacienteServiceDomain
                             _pacienteService;

    public PacienteController(IPacienteServiceDomain service)
    {
        _pacienteService = service;
    }

    [HttpPost("inserir")]
    public async Task<IActionResult>
            Inserir([FromBody] PacienteDTO paciente)
    {
        ApplicationResult<long> result =
               await _pacienteService.Inserir(paciente);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("buscartodos")]
    public async Task<IActionResult>
            BuscarTodos()
    {
        var result = await _pacienteService.BuscarTodos();
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult>
            Atualizar([FromBody] PacienteDTO paciente)
    {
        var result = await _pacienteService.
                             Atualizar(paciente);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult>
                         Excluir(int id)
    {
        var result = await _pacienteService.Excluir(id);
        return StatusCode(result.StatusCode, result);
    }
}
