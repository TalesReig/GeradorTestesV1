using GeradorTestes.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GeradorTestes.Infra.Orm
{
    public class GeradorTesteDbContext : DbContext, IContextoDados
    {
        private string connectionString;

        public GeradorTesteDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void GravarDados()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(Log.Logger, dispose: true);
            }));
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeradorTesteDbContext).Assembly);
        }
       
    }
}
