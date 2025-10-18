using System.Diagnostics.Eventing.Reader;
using OdontoSysApi.Application;
using OdontoSysApi.Application.DTOs;
using OdontoSysApi.Models;
using OdontoSysApi.Domain.Services.Interfaces;
using OdontoSysApi.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using YamlDotNet.Core;

namespace OdontoSysApi.Domain.Services;

public class PacienteServiceDomain : IPacienteServiceDomain
{
    private readonly AppDbContext _context;

    public PacienteServiceDomain(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationResult<long>>
                    Inserir(PacienteDTO pacienteDTO)
    {
        if (pacienteDTO == null)
        {
            return ApplicationResult<long>
                .Failure("Dados inválidos", 400);
        }

        Paciente paciente = new Paciente();
        paciente.Nome = pacienteDTO.Nome;

        await _context.Set<OdontoSysApi.Models.Paciente>().AddAsync(paciente);
        await _context.SaveChangesAsync();

        var result = ApplicationResult<long>
            .Success((long)paciente.IdPaciente, 200,
            "Registro Salvo com Sucesso");

        return result;
    }

    public async Task<ApplicationResult<List<PacienteDTO>>>
            BuscarTodos()
    {
        var pacientes = _context.Pacientes;
        //Realizando consulta customizada
        var resultPacientes = await pacientes.Select(
            e => new PacienteDTO
            {
                Id = (long)e.IdPaciente,
                Nome = e.Nome
            }
        ).ToListAsync();

        var result = ApplicationResult<List<PacienteDTO>>
                .Success(resultPacientes);

        return result;
    }

    public async Task<ApplicationResult<PacienteDTO>>
            Atualizar(PacienteDTO pacienteDTO)
    {
        var info = new
        {
            Message = "Parâmetros Inválidos"
        };

        if (pacienteDTO == null || pacienteDTO.Id <= 0)
        {
            return ApplicationResult<PacienteDTO>
                .Failure(info.Message, 400);
        }

        var result = await _context.Pacientes
                      .Where(e => (long)e.IdPaciente == pacienteDTO.Id)
                      .FirstOrDefaultAsync();

        if (result == null)
        {
            info = new
            {
                Message = "Registro não encontrado."
            };

            return ApplicationResult<PacienteDTO>
                .Failure(info.Message, 400);
        }

        result.Nome = pacienteDTO.Nome;

        _context.Entry<Paciente>(result)
                        .State = EntityState.Modified;

        await _context.SaveChangesAsync();

        info = new { Message = "Dados Alterados" };

        return ApplicationResult<PacienteDTO>
                        .Success(pacienteDTO, 
                        message: info.Message);
    }

    public async Task<ApplicationResult<PacienteDTO>>
                         Excluir(int id)
    {
        var info = new
        {
            Message = "Parâmetros Inválidos"
        };

        if (id <= 0)
        {
            return ApplicationResult<PacienteDTO>
                .Failure(info.Message, 400);
        }

        var result = await _context.Pacientes                    
                      .Where(e => e.IdPaciente == (ulong)id)
                      .FirstOrDefaultAsync();

        if (result == null)
        {
            info = new
            {
                Message = "Registro não encontrado."
            };

            return ApplicationResult<PacienteDTO>
                .Failure(info.Message, 400);
        }

        _context.Entry<Paciente>(result)
                        .State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        info = new { Message = "Registro Excluído" };

        var pacienteDTO = new PacienteDTO
        {
            Id = (long)result.IdPaciente,
            Nome = result.Nome
        };

        return ApplicationResult<PacienteDTO>
                .Success(pacienteDTO,
                 message: info.Message);
    }
}
