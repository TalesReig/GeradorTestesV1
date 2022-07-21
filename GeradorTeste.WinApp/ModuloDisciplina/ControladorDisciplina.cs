using GeradorTestes.Aplicacao.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloDisciplina;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloDisciplina
{
    public class ControladorDisciplina : ControladorBase
    {
        private ServicoDisciplina servicoDisciplina;
        
        private TabelaDisciplinasControl tabelaDisciplinas;


        public ControladorDisciplina(ServicoDisciplina servicoDisciplina)
        {
            this.servicoDisciplina = servicoDisciplina;
        }

        public override void Inserir()
        {
            var tela = new TelaCadastroDisciplinasForm();

            tela.Disciplina = new Disciplina();

            tela.GravarRegistro = servicoDisciplina.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarDisciplinas();
            }
        }

        public override void Editar()
        {
            var numero = tabelaDisciplinas.ObtemNumeroDisciplinaSelecionado();

            Disciplina disciplinaSelecionada = servicoDisciplina.SelecionarPorId(numero).Value;

            if (disciplinaSelecionada == null)
            {
                MessageBox.Show("Selecione uma disciplina primeiro",
                "Edição de Compromissos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var tela = new TelaCadastroDisciplinasForm();

            tela.Disciplina = disciplinaSelecionada.Clone();

            tela.GravarRegistro = servicoDisciplina.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarDisciplinas();
            }

        }

        public override void Excluir()
        {
            var numero = tabelaDisciplinas.ObtemNumeroDisciplinaSelecionado();

            Disciplina disciplinaSelecionada = servicoDisciplina.SelecionarPorId(numero).Value;

            if (disciplinaSelecionada == null)
            {
                MessageBox.Show("Selecione uma disciplina primeiro",
                "Exclusão de Disciplinas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a disciplina?",
               "Exclusão de Disciplinas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                servicoDisciplina.Excluir(disciplinaSelecionada);
                CarregarDisciplinas();
            }
        }


        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxDisciplina();
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaDisciplinas == null)
                tabelaDisciplinas = new TabelaDisciplinasControl();

            CarregarDisciplinas();

            return tabelaDisciplinas;
        }

        private void CarregarDisciplinas()
        {
            List<Disciplina> disciplinas = servicoDisciplina.SelecionarTodos().Value;

            tabelaDisciplinas.AtualizarRegistros(disciplinas);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {disciplinas.Count} disciplina(s)");
        }
    }
}
