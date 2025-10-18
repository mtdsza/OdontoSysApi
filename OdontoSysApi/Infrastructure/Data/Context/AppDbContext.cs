using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using OdontoSysApi.Models;

namespace OdontoSysApi.Infra;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cache> Caches { get; set; }

    public virtual DbSet<CacheLock> CacheLocks { get; set; }

    public virtual DbSet<Consulta> Consultas { get; set; }

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<Estoque> Estoques { get; set; }

    public virtual DbSet<FailedJob> FailedJobs { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobBatch> JobBatches { get; set; }

    public virtual DbSet<Migration> Migrations { get; set; }

    public virtual DbSet<MovimentacaoFinanceira> MovimentacoesFinanceiras { get; set; }

    public virtual DbSet<MovimentacaoGeralEstoque> MovimentacoesGeraisEstoque { get; set; }

    public virtual DbSet<Orcamento> Orcamentos { get; set; }

    public virtual DbSet<OrcamentoItem> OrcamentoItens { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<ParcelaAReceber> ParcelasARecebers { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public virtual DbSet<Procedimento> Procedimentos { get; set; }

    public virtual DbSet<ProcedimentoRealizado> ProcedimentosRealizados { get; set; }

    public virtual DbSet<Profissional> Profissionais { get; set; }

    public virtual DbSet<ProfissionalEspecialidade> ProfissionalEspecialidades { get; set; }

    public virtual DbSet<Prontuario> Prontuarios { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsoMaterialConsulta> UsoMateriaisConsulta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=odontosys;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.11-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cache>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("PRIMARY");

            entity.ToTable("cache");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Expiration)
                .HasColumnType("int(11)")
                .HasColumnName("expiration");
            entity.Property(e => e.Value)
                .HasColumnType("mediumtext")
                .HasColumnName("value");
        });

        modelBuilder.Entity<CacheLock>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("PRIMARY");

            entity.ToTable("cache_locks");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Expiration)
                .HasColumnType("int(11)")
                .HasColumnName("expiration");
            entity.Property(e => e.Owner)
                .HasMaxLength(255)
                .HasColumnName("owner");
        });

        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.HasKey(e => e.IdConsulta).HasName("PRIMARY");

            entity.ToTable("consultas");

            entity.HasIndex(e => e.IdPaciente, "consultas_id_paciente_foreign");

            entity.HasIndex(e => e.IdProfissional, "consultas_id_profissional_foreign");

            entity.Property(e => e.IdConsulta)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_consulta");
            entity.Property(e => e.DataFim)
                .HasColumnType("datetime")
                .HasColumnName("data_fim");
            entity.Property(e => e.DataInicio)
                .HasColumnType("datetime")
                .HasColumnName("data_inicio");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.IdPaciente)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_paciente");
            entity.Property(e => e.IdProfissional)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_profissional");
            entity.Property(e => e.Situacao)
                .HasDefaultValueSql("'Agendada'")
                .HasColumnType("enum('Agendada','Realizada','Cancelada')")
                .HasColumnName("situacao");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Consultas)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consultas_id_paciente_foreign");

            entity.HasOne(d => d.IdProfissionalNavigation).WithMany(p => p.Consultas)
                .HasForeignKey(d => d.IdProfissional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consultas_id_profissional_foreign");
        });

        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidade).HasName("PRIMARY");

            entity.ToTable("especialidades");

            entity.HasIndex(e => e.Nome, "especialidades_nome_unique").IsUnique();

            entity.Property(e => e.IdEspecialidade)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_especialidade");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Estoque>(entity =>
        {
            entity.HasKey(e => e.IdItemEstoque).HasName("PRIMARY");

            entity.ToTable("estoque");

            entity.Property(e => e.IdItemEstoque)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_item_estoque");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
            entity.Property(e => e.EstoqueMin)
                .HasPrecision(10, 3)
                .HasColumnName("estoque_min");
            entity.Property(e => e.Quantidade)
                .HasPrecision(10, 3)
                .HasColumnName("quantidade");
        });

        modelBuilder.Entity<FailedJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("failed_jobs");

            entity.HasIndex(e => e.Uuid, "failed_jobs_uuid_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Connection)
                .HasColumnType("text")
                .HasColumnName("connection");
            entity.Property(e => e.Exception).HasColumnName("exception");
            entity.Property(e => e.FailedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("failed_at");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.Queue)
                .HasColumnType("text")
                .HasColumnName("queue");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("jobs");

            entity.HasIndex(e => e.Queue, "jobs_queue_index");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Attempts)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("attempts");
            entity.Property(e => e.AvailableAt)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("available_at");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("created_at");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.Queue).HasColumnName("queue");
            entity.Property(e => e.ReservedAt)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("reserved_at");
        });

        modelBuilder.Entity<JobBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("job_batches");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CancelledAt)
                .HasColumnType("int(11)")
                .HasColumnName("cancelled_at");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("int(11)")
                .HasColumnName("created_at");
            entity.Property(e => e.FailedJobIds).HasColumnName("failed_job_ids");
            entity.Property(e => e.FailedJobs)
                .HasColumnType("int(11)")
                .HasColumnName("failed_jobs");
            entity.Property(e => e.FinishedAt)
                .HasColumnType("int(11)")
                .HasColumnName("finished_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Options)
                .HasColumnType("mediumtext")
                .HasColumnName("options");
            entity.Property(e => e.PendingJobs)
                .HasColumnType("int(11)")
                .HasColumnName("pending_jobs");
            entity.Property(e => e.TotalJobs)
                .HasColumnType("int(11)")
                .HasColumnName("total_jobs");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("migrations");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Batch)
                .HasColumnType("int(11)")
                .HasColumnName("batch");
            entity.Property(e => e.Migration1)
                .HasMaxLength(255)
                .HasColumnName("migration");
        });

        modelBuilder.Entity<MovimentacaoFinanceira>(entity =>
        {
            entity.HasKey(e => e.IdMovimentacaoFinanceira).HasName("PRIMARY");

            entity.ToTable("movimentacoes_financeiras");

            entity.HasIndex(e => e.IdConsulta, "movimentacoes_financeiras_id_consulta_foreign");

            entity.HasIndex(e => e.IdOrcamento, "movimentacoes_financeiras_id_orcamento_foreign");

            entity.HasIndex(e => e.IdParcelaPaga, "movimentacoes_financeiras_id_parcela_paga_foreign");

            entity.HasIndex(e => e.IdProcedimentoRealizado, "movimentacoes_financeiras_id_procedimento_realizado_foreign");

            entity.Property(e => e.IdMovimentacaoFinanceira)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_movimentacao_financeira");
            entity.Property(e => e.DataMovimentacao)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("data_movimentacao");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
            entity.Property(e => e.IdConsulta)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_consulta");
            entity.Property(e => e.IdOrcamento)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_orcamento");
            entity.Property(e => e.IdParcelaPaga)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_parcela_paga");
            entity.Property(e => e.IdProcedimentoRealizado)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_procedimento_realizado");
            entity.Property(e => e.Tipo)
                .HasColumnType("enum('Entrada','Saida')")
                .HasColumnName("tipo");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.IdConsultaNavigation).WithMany(p => p.MovimentacoesFinanceiras)
                .HasForeignKey(d => d.IdConsulta)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("movimentacoes_financeiras_id_consulta_foreign");

            entity.HasOne(d => d.IdOrcamentoNavigation).WithMany(p => p.MovimentacoesFinanceiras)
                .HasForeignKey(d => d.IdOrcamento)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("movimentacoes_financeiras_id_orcamento_foreign");

            entity.HasOne(d => d.IdParcelaPagaNavigation).WithMany(p => p.MovimentacoesFinanceiras)
                .HasForeignKey(d => d.IdParcelaPaga)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("movimentacoes_financeiras_id_parcela_paga_foreign");

            entity.HasOne(d => d.IdProcedimentoRealizadoNavigation).WithMany(p => p.MovimentacoesFinanceiras)
                .HasForeignKey(d => d.IdProcedimentoRealizado)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("movimentacoes_financeiras_id_procedimento_realizado_foreign");
        });

        modelBuilder.Entity<MovimentacaoGeralEstoque>(entity =>
        {
            entity.HasKey(e => e.IdMovimentacaoGeral).HasName("PRIMARY");

            entity.ToTable("movimentacoes_gerais_estoque");

            entity.HasIndex(e => e.IdItemEstoque, "movimentacoes_gerais_estoque_id_item_estoque_foreign");

            entity.Property(e => e.IdMovimentacaoGeral)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_movimentacao_geral");
            entity.Property(e => e.DataMovimentacao)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("data_movimentacao");
            entity.Property(e => e.IdItemEstoque)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_item_estoque");
            entity.Property(e => e.Justificativa)
                .HasMaxLength(255)
                .HasColumnName("justificativa");
            entity.Property(e => e.Quantidade)
                .HasPrecision(10, 3)
                .HasColumnName("quantidade");
            entity.Property(e => e.Tipo)
                .HasColumnType("enum('Entrada','Perda','Ajuste')")
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdItemEstoqueNavigation).WithMany(p => p.MovimentacoesGeraisEstoque)
                .HasForeignKey(d => d.IdItemEstoque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("movimentacoes_gerais_estoque_id_item_estoque_foreign");
        });

        modelBuilder.Entity<Orcamento>(entity =>
        {
            entity.HasKey(e => e.IdOrcamento).HasName("PRIMARY");

            entity.ToTable("orcamentos");

            entity.HasIndex(e => e.IdConsulta, "orcamentos_id_consulta_foreign");

            entity.HasIndex(e => e.IdPaciente, "orcamentos_id_paciente_foreign");

            entity.HasIndex(e => e.IdProfissional, "orcamentos_id_profissional_foreign");

            entity.Property(e => e.IdOrcamento)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_orcamento");
            entity.Property(e => e.DataEmissao).HasColumnName("data_emissao");
            entity.Property(e => e.DataValidade).HasColumnName("data_validade");
            entity.Property(e => e.IdConsulta)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_consulta");
            entity.Property(e => e.IdPaciente)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_paciente");
            entity.Property(e => e.IdProfissional)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_profissional");
            entity.Property(e => e.ValorTotal)
                .HasPrecision(10, 2)
                .HasColumnName("valor_total");

            entity.HasOne(d => d.IdConsultaNavigation).WithMany(p => p.Orcamentos)
                .HasForeignKey(d => d.IdConsulta)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("orcamentos_id_consulta_foreign");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Orcamentos)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("orcamentos_id_paciente_foreign");

            entity.HasOne(d => d.IdProfissionalNavigation).WithMany(p => p.Orcamentos)
                .HasForeignKey(d => d.IdProfissional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orcamentos_id_profissional_foreign");
        });

        modelBuilder.Entity<OrcamentoItem>(entity =>
        {
            entity.HasKey(e => e.IdOrcamentoItem).HasName("PRIMARY");

            entity.ToTable("orcamento_itens");

            entity.HasIndex(e => e.IdOrcamento, "orcamento_itens_id_orcamento_foreign");

            entity.HasIndex(e => e.IdProcedimento, "orcamento_itens_id_procedimento_foreign");

            entity.Property(e => e.IdOrcamentoItem)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_orcamento_item");
            entity.Property(e => e.IdOrcamento)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_orcamento");
            entity.Property(e => e.IdProcedimento)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_procedimento");
            entity.Property(e => e.Quantidade)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("quantidade");
            entity.Property(e => e.ValorUnitario)
                .HasPrecision(10, 2)
                .HasColumnName("valor_unitario");

            entity.HasOne(d => d.IdOrcamentoNavigation).WithMany(p => p.OrcamentoItens)
                .HasForeignKey(d => d.IdOrcamento)
                .HasConstraintName("orcamento_itens_id_orcamento_foreign");

            entity.HasOne(d => d.IdProcedimentoNavigation).WithMany(p => p.OrcamentoItens)
                .HasForeignKey(d => d.IdProcedimento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orcamento_itens_id_procedimento_foreign");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PRIMARY");

            entity.ToTable("pacientes");

            entity.HasIndex(e => e.Cpf, "pacientes_cpf_unique").IsUnique();

            entity.Property(e => e.IdPaciente)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_paciente");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("cpf");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Endereco)
                .HasMaxLength(255)
                .HasColumnName("endereco");
            entity.Property(e => e.Nascimento).HasColumnName("nascimento");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(13)
                .HasColumnName("telefone");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ParcelaAReceber>(entity =>
        {
            entity.HasKey(e => e.IdParcela).HasName("PRIMARY");

            entity.ToTable("parcelas_a_receber");

            entity.HasIndex(e => e.IdOrcamento, "parcelas_a_receber_id_orcamento_foreign");

            entity.Property(e => e.IdParcela)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_parcela");
            entity.Property(e => e.DataPagamento)
                .HasColumnType("datetime")
                .HasColumnName("data_pagamento");
            entity.Property(e => e.DataVencimento).HasColumnName("data_vencimento");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
            entity.Property(e => e.IdOrcamento)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_orcamento");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Pendente'")
                .HasColumnType("enum('Pendente','Paga','Vencida','Cancelada')")
                .HasColumnName("status");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.IdOrcamentoNavigation).WithMany(p => p.ParcelasARecebers)
                .HasForeignKey(d => d.IdOrcamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("parcelas_a_receber_id_orcamento_foreign");
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PRIMARY");

            entity.ToTable("password_reset_tokens");

            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("token");
        });

        modelBuilder.Entity<Procedimento>(entity =>
        {
            entity.HasKey(e => e.IdProcedimento).HasName("PRIMARY");

            entity.ToTable("procedimentos");

            entity.Property(e => e.IdProcedimento)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_procedimento");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.ValorPadrao)
                .HasPrecision(10, 2)
                .HasColumnName("valor_padrao");
        });

        modelBuilder.Entity<ProcedimentoRealizado>(entity =>
        {
            entity.HasKey(e => e.IdProcedimentoRealizado).HasName("PRIMARY");

            entity.ToTable("procedimentos_realizados");

            entity.HasIndex(e => e.IdConsulta, "procedimentos_realizados_id_consulta_foreign");

            entity.HasIndex(e => e.IdOrcamentoItem, "procedimentos_realizados_id_orcamento_item_foreign");

            entity.HasIndex(e => e.IdProcedimento, "procedimentos_realizados_id_procedimento_foreign");

            entity.Property(e => e.IdProcedimentoRealizado)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_procedimento_realizado");
            entity.Property(e => e.Anexo)
                .HasMaxLength(512)
                .HasColumnName("anexo");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.IdConsulta)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_consulta");
            entity.Property(e => e.IdOrcamentoItem)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_orcamento_item");
            entity.Property(e => e.IdProcedimento)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_procedimento");
            entity.Property(e => e.Quantidade)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("quantidade");
            entity.Property(e => e.ValorCobrado)
                .HasPrecision(10, 2)
                .HasColumnName("valor_cobrado");

            entity.HasOne(d => d.IdConsultaNavigation).WithMany(p => p.ProcedimentosRealizados)
                .HasForeignKey(d => d.IdConsulta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("procedimentos_realizados_id_consulta_foreign");

            entity.HasOne(d => d.IdOrcamentoItemNavigation).WithMany(p => p.ProcedimentosRealizados)
                .HasForeignKey(d => d.IdOrcamentoItem)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("procedimentos_realizados_id_orcamento_item_foreign");

            entity.HasOne(d => d.IdProcedimentoNavigation).WithMany(p => p.ProcedimentosRealizados)
                .HasForeignKey(d => d.IdProcedimento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("procedimentos_realizados_id_procedimento_foreign");
        });

        modelBuilder.Entity<Profissional>(entity =>
        {
            entity.HasKey(e => e.IdProfissional).HasName("PRIMARY");

            entity.ToTable("profissionais");

            entity.HasIndex(e => e.Email, "profissionais_email_unique").IsUnique();

            entity.Property(e => e.IdProfissional)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_profissional");
            entity.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("ativo");
            entity.Property(e => e.Cro)
                .HasMaxLength(15)
                .HasColumnName("cro");
            entity.Property(e => e.DataContratacao).HasColumnName("data_contratacao");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.SalarioBase)
                .HasPrecision(10, 2)
                .HasColumnName("salario_base");
            entity.Property(e => e.Telefone)
                .HasMaxLength(13)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<ProfissionalEspecialidade>(entity =>
        {
            entity.HasKey(e => e.IdProfissionalEspecialidade).HasName("PRIMARY");

            entity.ToTable("profissional_especialidades");

            entity.HasIndex(e => new { e.IdProfissional, e.IdEspecialidade }, "prof_espec_unique").IsUnique();

            entity.HasIndex(e => e.IdEspecialidade, "profissional_especialidades_id_especialidade_foreign");

            entity.Property(e => e.IdProfissionalEspecialidade)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_profissional_especialidade");
            entity.Property(e => e.IdEspecialidade)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_especialidade");
            entity.Property(e => e.IdProfissional)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_profissional");

            entity.HasOne(d => d.IdEspecialidadeNavigation).WithMany(p => p.ProfissionaisEspecialidades)
                .HasForeignKey(d => d.IdEspecialidade)
                .HasConstraintName("profissional_especialidades_id_especialidade_foreign");

            entity.HasOne(d => d.IdProfissionalNavigation).WithMany(p => p.ProfissionaisEspecialidades)
                .HasForeignKey(d => d.IdProfissional)
                .HasConstraintName("profissional_especialidades_id_profissional_foreign");
        });

        modelBuilder.Entity<Prontuario>(entity =>
        {
            entity.HasKey(e => e.IdProntuario).HasName("PRIMARY");

            entity.ToTable("prontuarios");

            entity.HasIndex(e => e.IdConsulta, "prontuarios_id_consulta_foreign");

            entity.HasIndex(e => e.IdPaciente, "prontuarios_id_paciente_foreign");

            entity.HasIndex(e => e.IdProfissional, "prontuarios_id_profissional_foreign");

            entity.Property(e => e.IdProntuario)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_prontuario");
            entity.Property(e => e.DataRegistro)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("data_registro");
            entity.Property(e => e.Diagnostico)
                .HasColumnType("text")
                .HasColumnName("diagnostico");
            entity.Property(e => e.HistoricoOdontologico)
                .HasColumnType("text")
                .HasColumnName("historico_odontologico");
            entity.Property(e => e.IdConsulta)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_consulta");
            entity.Property(e => e.IdPaciente)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_paciente");
            entity.Property(e => e.IdProfissional)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_profissional");
            entity.Property(e => e.Observacoes)
                .HasColumnType("text")
                .HasColumnName("observacoes");
            entity.Property(e => e.Prescricoes)
                .HasColumnType("text")
                .HasColumnName("prescricoes");
            entity.Property(e => e.TratamentosAnteriores)
                .HasColumnType("text")
                .HasColumnName("tratamentos_anteriores");

            entity.HasOne(d => d.IdConsultaNavigation).WithMany(p => p.Prontuarios)
                .HasForeignKey(d => d.IdConsulta)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("prontuarios_id_consulta_foreign");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Prontuarios)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("prontuarios_id_paciente_foreign");

            entity.HasOne(d => d.IdProfissionalNavigation).WithMany(p => p.Prontuarios)
                .HasForeignKey(d => d.IdProfissional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prontuarios_id_profissional_foreign");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sessions");

            entity.HasIndex(e => e.LastActivity, "sessions_last_activity_index");

            entity.HasIndex(e => e.UserId, "sessions_user_id_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.LastActivity)
                .HasColumnType("int(11)")
                .HasColumnName("last_activity");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.UserAgent)
                .HasColumnType("text")
                .HasColumnName("user_agent");
            entity.Property(e => e.UserId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_unique").IsUnique();

            entity.HasIndex(e => e.IdProfissional, "users_id_profissional_unique").IsUnique();

            entity.HasIndex(e => e.Login, "users_login_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerifiedAt)
                .HasColumnType("timestamp")
                .HasColumnName("email_verified_at");
            entity.Property(e => e.IdProfissional)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_profissional");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RememberToken)
                .HasMaxLength(100)
                .HasColumnName("remember_token");
            entity.Property(e => e.Tipo)
                .HasColumnType("enum('Admin','Secretaria','Profissional')")
                .HasColumnName("tipo");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.IdProfissionalNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.IdProfissional)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_id_profissional_foreign");
        });

        modelBuilder.Entity<UsoMaterialConsulta>(entity =>
        {
            entity.HasKey(e => e.IdUsoMaterial).HasName("PRIMARY");

            entity.ToTable("uso_materiais_consulta");

            entity.HasIndex(e => e.IdConsulta, "uso_materiais_consulta_id_consulta_foreign");

            entity.HasIndex(e => e.IdItemEstoque, "uso_materiais_consulta_id_item_estoque_foreign");

            entity.Property(e => e.IdUsoMaterial)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_uso_material");
            entity.Property(e => e.DataUso)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("data_uso");
            entity.Property(e => e.IdConsulta)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_consulta");
            entity.Property(e => e.IdItemEstoque)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_item_estoque");
            entity.Property(e => e.Quantidade)
                .HasPrecision(10, 3)
                .HasColumnName("quantidade");

            entity.HasOne(d => d.IdConsultaNavigation).WithMany(p => p.UsosMateriaisConsultas)
                .HasForeignKey(d => d.IdConsulta)
                .HasConstraintName("uso_materiais_consulta_id_consulta_foreign");

            entity.HasOne(d => d.IdItemEstoqueNavigation).WithMany(p => p.UsosMateriaisConsultas)
                .HasForeignKey(d => d.IdItemEstoque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("uso_materiais_consulta_id_item_estoque_foreign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
