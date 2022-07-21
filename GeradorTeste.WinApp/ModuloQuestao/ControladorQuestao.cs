using GeradorTestes.Aplicacao.ModuloDisciplina;
using GeradorTestes.Aplicacao.ModuloQuestao;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloQuestao;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloQuestao
{
    public class ControladorQuestao : ControladorBase
    {
        private ServicoDisciplina servicoDisciplina;
        private ServicoQuestao servicoQuestao;
        private TabelaQuestoesControl tabelaQuestoes;

        public ControladorQuestao(ServicoQuestao servicoQuestao, ServicoDisciplina servicoDisciplina)
        {
            this.servicoQuestao = servicoQuestao;
            this.servicoDisciplina = servicoDisciplina;
        }

        public override void Inserir()
        {
            var disciplinas = servicoDisciplina.SelecionarTodos().Value;

            var tela = new TelaCadastroQuestoesForm(disciplinas);

            tela.Questao = new Questao();

            tela.GravarRegistro = servicoQuestao.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarQuestoes();
            }
        }

        public override void Editar()
        {
            var numero = tabelaQuestoes.ObtemNumeroQuestaoSelecionado();

            Questao questaoSelecionada = servicoQuestao.SelecionarPorId(numero).Value;

            if (questaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma questão primeiro",
                "Edição de Questões", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var disciplinas = servicoDisciplina.SelecionarTodos().Value;

            var tela = new TelaCadastroQuestoesForm(disciplinas);

            tela.Questao = questaoSelecionada.Clone();

            tela.GravarRegistro = servicoQuestao.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarQuestoes();
            }
        }

        public override void Excluir()
        {
            var numero = tabelaQuestoes.ObtemNumeroQuestaoSelecionado();

            Questao questaoSelecionada = servicoQuestao.SelecionarPorId(numero).Value;

            if (questaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma questão primeiro",
                "Edição de Questões", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a questão?",
               "Exclusão de Questões", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                servicoQuestao.Excluir(questaoSelecionada);
                CarregarQuestoes();
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxQuestao();
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaQuestoes == null)
                tabelaQuestoes = new TabelaQuestoesControl();

            CarregarQuestoes();

            return tabelaQuestoes;
        }

        private void CarregarQuestoes()
        {
            List<Questao> questoes = servicoQuestao.SelecionarTodos().Value;

            tabelaQuestoes.AtualizarRegistros(questoes);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {questoes.Count} questão(ões)");
        }
    }
}
