using FluentResults;
using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloQuestao
{
    public partial class TelaCadastroQuestoesForm : Form
    {
        private Questao questao;

        public TelaCadastroQuestoesForm(List<Disciplina> disciplinas)
        {
            InitializeComponent();
            this.ConfigurarTela();
            CarregarDisciplinas(disciplinas);
        }

        public Func<Questao, Result<Questao>> GravarRegistro { get; set; }

        public Questao Questao
        {
            get => questao;
            set
            {
                questao = value;
                txtEnunciado.Text = Questao.Enunciado;
                cmbDisciplinas.SelectedItem = Questao.Materia?.Disciplina;
                cmbMaterias.SelectedItem = Questao.Materia;

                if (questao.Alternativas != null)
                {
                    int i = 0;
                    foreach (var item in questao.Alternativas)
                    {
                        listAlternativas.Items.Add(item);

                        if (item.Correta)
                            listAlternativas.SetItemChecked(i, true);

                        i++;
                    }
                }
            }
        }

        private void CarregarMaterias(List<Materia> materias)
        {
            cmbMaterias.Items.Clear();

            foreach (var item in materias)
            {
                cmbMaterias.Items.Add(item);
            }
        }

        private void CarregarDisciplinas(List<Disciplina> disciplinas)
        {
            cmbDisciplinas.Items.Clear();

            foreach (var item in disciplinas)
            {
                cmbDisciplinas.Items.Add(item);
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Questao.Enunciado = txtEnunciado.Text;
            Questao.ConfigurarMateria((Materia)cmbMaterias.SelectedItem);

            int i = 0;
            foreach (Alternativa item in listAlternativas.Items)
            {
                if (listAlternativas.GetItemChecked(i))
                    item.Correta = true;
                else
                    item.Correta = false;

                i++;
            }

            var resultadoValidacao = GravarRegistro(questao);

            if (resultadoValidacao.IsFailed)
            {
                string erro = resultadoValidacao.Errors[0].Message;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            Alternativa alternativa = new Alternativa();

            alternativa.Letra = questao.GerarLetraAlternativa();
            alternativa.Resposta = txtResposta.Text;

            Questao.AdicionarAlternativa(alternativa);

            RecarregarAlternativas();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            var alternativa = listAlternativas.SelectedItem as Alternativa;

            if (alternativa != null)
            {
                Questao.RemoverAlternativa(alternativa);

                listAlternativas.Items.Remove(alternativa);

                RecarregarAlternativas();
            }
        }

        private void RecarregarAlternativas()
        {
            listAlternativas.Items.Clear();

            int i = 0;
            foreach (var item in questao.Alternativas)
            {
                listAlternativas.Items.Add(item);

                if (item.Correta)
                    listAlternativas.SetItemChecked(i, true);

                i++;
            }
        }

        private void cmbDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var disciplina = cmbDisciplinas.SelectedItem as Disciplina;

            if (disciplina != null)
                CarregarMaterias(disciplina.Materias);
        }

    }
}