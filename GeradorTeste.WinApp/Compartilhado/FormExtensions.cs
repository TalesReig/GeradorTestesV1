using System.Windows.Forms;

namespace GeradorTeste.WinApp
{
    public static class FormExtensions
    {
        public static void ConfigurarTela(this Form tela)
        {
            tela.FormBorderStyle = FormBorderStyle.FixedDialog;
            tela.StartPosition = FormStartPosition.CenterScreen;
            tela.MaximizeBox = false;
            tela.MinimizeBox = false;
            tela.ShowIcon = false;
            tela.ShowInTaskbar = false;
        }


    }
}
