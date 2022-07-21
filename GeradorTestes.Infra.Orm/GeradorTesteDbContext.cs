using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using System;

namespace GeradorTestes.Infra.Orm
{
    public class GeradorTesteDbContext : DbContext
    {
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Questao> Questoes { get; set; }
        public DbSet<Teste> Testes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var enderecoConexaoComBanco = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DbGeradorTestesOrm;Integrated Security=True";
            optionsBuilder.UseSqlServer(enderecoConexaoComBanco);
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Disciplina>(entidade =>
            {
                entidade.ToTable("TBDisciplina");
                entidade.Property(x => x.Id).ValueGeneratedNever();
                entidade.Property(x => x.Nome).HasColumnType("varchar(100)").IsRequired();
            });

            modelBuilder.Entity<Materia>(entidade =>
            {
                entidade.ToTable("TBMateria");
                entidade.Property(x => x.Id).ValueGeneratedNever();
                entidade.Property(x => x.Nome).IsRequired().HasColumnType("varchar(100)");
                entidade.Property(x => x.Serie).HasConversion<int>();
                entidade.HasOne(x => x.Disciplina);
            });

            modelBuilder.Entity<Questao>(entidade =>
            {
                entidade.ToTable("TBQuestao");
                entidade.Property(x => x.Id).ValueGeneratedNever();
                entidade.Property(x => x.Enunciado).IsRequired().HasColumnType("varchar(500)");                
                entidade.HasOne(x => x.Materia);
                entidade.HasMany(x => x.Testes);
            });

            modelBuilder.Entity<Alternativa>(entidade =>
            {
                entidade.ToTable("TBAlternativa");
                entidade.Property(x => x.Id).ValueGeneratedNever();
                entidade.Property(x => x.Resposta).IsRequired().HasColumnType("varchar(500)");
                entidade.Property(x => x.Letra).IsRequired().HasColumnType("char(1)");
                entidade.Property(x => x.Correta).IsRequired();

                entidade.HasOne(x => x.Questao)
                    .WithMany(x => x.Alternativas)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TBAlternativa_TBQuestao");
            });

            modelBuilder.Entity<Teste>(entidade => {

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
