using GeradorTestes.Dominio;
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeradorTesteDbContext).Assembly);
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
