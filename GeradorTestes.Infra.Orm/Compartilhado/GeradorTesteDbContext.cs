using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GeradorTestes.Infra.Orm
{
    public class GeradorTesteDbContext : DbContext, IContextoDados
    {
        private ILoggerFactory serilogLoggerFactory;
        private string connectionString;

        public GeradorTesteDbContext(string connectionString)
        {
            this.connectionString = connectionString;

            ConfigurarLogEntityFramework();            
        }       

        public void GravarDados()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLoggerFactory(serilogLoggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Materia>(entidade =>
            {
               
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

        private void ConfigurarLogEntityFramework()
        {
            serilogLoggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, level) =>
                {
                    return category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Debug;
                })
                .AddDebug()
                .AddSerilog(Log.Logger, dispose: true);
            });
        }
    }
}
