using FluentResults;
using GeradorTestes.Dominio.ModuloDisciplina;
using System;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloDisciplina
{
    public partial class TelaCadastroDisciplinasForm : Form
    {
        private Disciplina disciplina;

        public TelaCadastroDisciplinasForm()
        {
            InitializeComponent();
            this.ConfigurarTela();
        }

        public Func<Disciplina, Result<Disciplina>> GravarRegistro { get; set; }

        public Disciplina Disciplina
        {
            get => disciplina;

            set
            {
                disciplina = value;

                txtNome.Text = disciplina.Nome;
            }
        }

        private void btnGravar_Click(object sender, System.EventArgs e)
        {
            disciplina.Nome = txtNome.Text;

            var resultadoValidacao = GravarRegistro(Disciplina);

            if (resultadoValidacao.IsFailed)
            {
                string erro = resultadoValidacao.Errors[0].Message;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }
    }
}
