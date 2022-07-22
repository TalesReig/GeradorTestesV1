using Microsoft.Extensions.Configuration;
using Serilog;
using System.IO;

namespace GeradorTeste.Infra.Logging
{
    public class ConfiguracaoLogsGeradorTeste
    {
        public static void ConfigurarEscritaLogs()
        {
            var configuracao = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("ConfiguracaoAplicacao.json")
                .Build();

            var diretorioSaida = configuracao
                .GetSection("ConfiguracaoLogs")
                .GetSection("DiretorioSaida")
                .Value;

            Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .WriteTo.Debug()
                   .WriteTo.Seq("http://localhost:5341")
                   .WriteTo.File(diretorioSaida + "/log.txt", rollingInterval: RollingInterval.Day,
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
               .CreateLogger();
        }
    }
}
