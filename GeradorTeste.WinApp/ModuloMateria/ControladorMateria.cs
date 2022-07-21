using GeradorTestes.Aplicacao.ModuloDisciplina;
using GeradorTestes.Aplicacao.ModuloMateria;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloMateria
{
    public class ControladorMateria : ControladorBase
    {
        private ServicoDisciplina servicoDisciplina;
        private ServicoMateria servicoMateria;

        private TabelaMateriasControl tabelaMaterias;

        public ControladorMateria(ServicoMateria servicoMateria,
            ServicoDisciplina servicoDisciplina)
        {
            this.servicoMateria = servicoMateria;
            this.servicoDisciplina = servicoDisciplina;
        }

        public override void Inserir()
        {
            List<Disciplina> disciplinas = servicoDisciplina.SelecionarTodos().Value;

            var tela = new TelaCadastroMateriasForm(disciplinas);

            tela.Materia = new Materia();

            tela.GravarRegistro = servicoMateria.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarMaterias();
            }
        }

        public override void Editar()
        {
            var numero = tabelaMaterias.ObtemNumeroMateriaSelecionado();

            Materia materiaSelecionada = servicoMateria.SelecionarPorId(numero).Value;

            if (materiaSelecionada == null)
            {
                MessageBox.Show("Selecione uma materia primeiro",
                "Edição de Compromissos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<Disciplina> materias = servicoDisciplina.SelecionarTodos().Value;

            var tela = new TelaCadastroMateriasForm(materias);

            tela.Materia = materiaSelecionada.Clone();

            tela.GravarRegistro = servicoMateria.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarMaterias();
            }

        }

        public override void Excluir()
        {
            var numero = tabelaMaterias.ObtemNumeroMateriaSelecionado();

            Materia materiaSelecionada = servicoMateria.SelecionarPorId(numero).Value;

            if (materiaSelecionada == null)
            {
                MessageBox.Show("Selecione uma materia primeiro",
                "Exclusão de Materias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a materia?",
               "Exclusão de Materias", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                servicoMateria.Excluir(materiaSelecionada);
                CarregarMaterias();
            }
        }



        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxMateria();
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaMaterias == null)
                tabelaMaterias = new TabelaMateriasControl();

            CarregarMaterias();

            return tabelaMaterias;
        }

        private void CarregarMaterias()
        {
            List<Materia> materias = servicoMateria.SelecionarTodos().Value;

            tabelaMaterias.AtualizarRegistros(materias);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {materias.Count} materias(s)");
        }
    }
}
