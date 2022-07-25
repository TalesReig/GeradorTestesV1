using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GeradorTestes.Infra.Orm.Compartilhado
{
    public class GeradorTesteDbContextFactory : IDesignTimeDbContextFactory<GeradorTesteDbContext>
    {
        public GeradorTesteDbContext CreateDbContext(string[] args)
        {
            var configuracao = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("ConfiguracaoAplicacao.json")
             .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            return new GeradorTesteDbContext(connectionString);
        }
    }
}
