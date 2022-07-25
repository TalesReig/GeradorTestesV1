using GeradorTeste.Infra.Logging;
using GeradorTeste.WinApp.Compartilhado.Ioc;
using GeradorTestes.Infra.Orm.Compartilhado;
using System;
using System.Windows.Forms;

namespace GeradorTeste.WinApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MigradorBancoDadosGeradorTeste.AtualizarBancoDados();
            ConfiguracaoLogsGeradorTeste.ConfigurarEscritaLogs();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TelaPrincipalForm(new ServiceLocatorManual()));
        }
    }
}
