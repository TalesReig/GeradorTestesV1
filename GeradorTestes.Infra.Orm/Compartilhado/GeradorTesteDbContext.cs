using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using GeradorTestes.Infra.Orm.ModuloDisciplina;
using GeradorTestes.Infra.Orm.ModuloMateria;
using GeradorTestes.Infra.Orm.ModuloQuestao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace GeradorTestes.Infra.Orm
{
    public class GeradorTesteDbContext : DbContext, IContextoPersistencia
    {
        private string enderecoConexaoComBanco;

        public DbSet<Questao> Questoes { get; set; }
        public DbSet<Teste> Testes { get; set; }

        public GeradorTesteDbContext()
        {
            enderecoConexaoComBanco = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DbGeradorTestesOrm;Integrated Security=True";
        }

        public GeradorTesteDbContext(string enderecoBanco)
        {
            enderecoConexaoComBanco = enderecoBanco;
        }

        public void GravarDados()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(enderecoConexaoComBanco);

            ILoggerFactory loggerFactory = LoggerFactory.Create((x) =>
            {
                //instalar o pacote Serilog.Extensions.Logging
                x.AddSerilog(Log.Logger);
            });

            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapeadorDisciplinaOrm());

            modelBuilder.ApplyConfiguration(new MapeadorMateriaOrm());

            modelBuilder.ApplyConfiguration(new MapeadorQuestaoOrm());

            modelBuilder.ApplyConfiguration(new MapeadorAlternativaOrm());

            modelBuilder.Entity<Teste>(entidade =>
            {
                entidade.ToTable("TBTeste");
                entidade.Property(x => x.Id).ValueGeneratedNever();
                entidade.Property(x => x.Titulo).IsRequired().HasColumnType("varchar(250)");
                entidade.Property(x => x.QuantidadeQuestoes).IsRequired();
                entidade.Property(x => x.DataGeracao).IsRequired();
                entidade.Property(x => x.Provao).IsRequired();
                entidade.HasMany(x => x.Questoes);

                entidade.HasOne(x => x.Disciplina)
                    .WithMany().OnDelete(DeleteBehavior.NoAction);

                entidade.HasOne(x => x.Materia)
                    .WithMany().OnDelete(DeleteBehavior.NoAction);
            });

        }


    }
}
