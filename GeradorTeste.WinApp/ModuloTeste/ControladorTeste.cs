using GeradorTestes.Aplicacao.ModuloDisciplina;
using GeradorTestes.Aplicacao.ModuloMateria;
using GeradorTestes.Aplicacao.ModuloQuestao;
using GeradorTestes.Aplicacao.ModuloTeste;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloTeste
{
    public class ControladorTeste : ControladorBase
    {
        private ServicoDisciplina servicoDisciplina;
        private ServicoMateria servicoMateria;
        private ServicoQuestao servicoQuestao;
        private ServicoTeste servicoTeste;

        private TabelaTestesControl tabelaTestes;

        public ControladorTeste(ServicoTeste servicoTeste, ServicoQuestao servicoQuestao,
            ServicoDisciplina servicoDisciplina, ServicoMateria servicoMateria)
        {
            this.servicoQuestao = servicoQuestao;
            this.servicoDisciplina = servicoDisciplina;
            this.servicoMateria = servicoMateria;
            this.servicoTeste = servicoTeste;
        }

        public override void Inserir()
        {
            var disciplinas = servicoDisciplina.SelecionarTodos().Value;

            var tela = new TelaCriacaoTesteForm(disciplinas);

            tela.Teste = new Teste();

            tela.GravarRegistro = servicoTeste.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
        }

        public override void Duplicar()
        {
            var numero = tabelaTestes.ObtemNumeroTesteSelecionado();

            Teste testeSelecionado = servicoTeste.SelecionarPorId(numero).Value;

            if (testeSelecionado == null)
            {
                MessageBox.Show("Selecione um Teste primeiro",
                "Duplicação de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var disciplinas = servicoDisciplina.SelecionarTodos().Value;

            var tela = new TelaCriacaoTesteForm(disciplinas);

            tela.Teste = testeSelecionado.Clone();

            tela.GravarRegistro = servicoTeste.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
        }

        public override void Excluir()
        {
            var numero = tabelaTestes.ObtemNumeroTesteSelecionado();

            Teste testeSelecionado = servicoTeste.SelecionarPorId(numero).Value;

            if (testeSelecionado == null)
            {
                MessageBox.Show("Selecione um Teste primeiro",
                "Exclusão de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir o Teste?",
               "Exclusão de Testes", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                servicoTeste.Excluir(testeSelecionado);
                CarregarTestes();
            }
        }

        public override void Visualizar()
        {
            var numero = tabelaTestes.ObtemNumeroTesteSelecionado();

            Teste testeSelecionado = servicoTeste.SelecionarPorId(numero).Value;

            if (testeSelecionado == null)
            {
                MessageBox.Show("Selecione um Teste primeiro",
                "Exclusão de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaVisualizacaoTesteForm tela = new TelaVisualizacaoTesteForm(testeSelecionado);
            tela.ShowDialog();
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxTeste();
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaTestes == null)
                tabelaTestes = new TabelaTestesControl();

            CarregarTestes();

            return tabelaTestes;
        }

        private void CarregarTestes()
        {
            List<Teste> testes = servicoTeste.SelecionarTodos().Value;

            tabelaTestes.AtualizarRegistros(testes);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {testes.Count} teste(s)");
        }

        #region métodos não implementados
        public override void Editar()
        {
        }
        #endregion
    }
}
