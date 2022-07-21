using FluentResults;
using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloTeste
{
    public partial class TelaCriacaoTesteForm : Form
    {
        private Teste teste;

        public TelaCriacaoTesteForm(List<Disciplina> disciplinas)
        {
            InitializeComponent();
            this.ConfigurarTela();
            CarregarDisciplinas(disciplinas);
        }

        public Func<Teste, Result<Teste>> GravarRegistro { get; set; }

        public Teste Teste
        {
            get => teste;
            set
            {
                teste = value;

                txtTitulo.Text = teste.Titulo;
                cmbDisciplinas.SelectedItem = teste.Disciplina;
                cmbMaterias.SelectedItem = teste.Materia;
                chkProvao.Checked = teste.Provao;
                txtQtdQuestoes.Value = teste.QuantidadeQuestoes;

                if (teste.Questoes != null)
                {
                    foreach (var item in teste.Questoes)
                    {
                        listQuestoes.Items.Add(item);
                    }
                }
            }
        }

        private void btnSortear_Click(object sender, EventArgs e)
        {
            teste.Titulo = txtTitulo.Text;
            teste.Disciplina = cmbDisciplinas.SelectedItem as Disciplina;
            teste.Materia = cmbMaterias.SelectedItem as Materia;
            teste.Provao = chkProvao.Checked;
            teste.QuantidadeQuestoes = (int)txtQtdQuestoes.Value;

            ValidadorTeste validadorTeste = new ValidadorTeste();

            var resultadoValidacao = validadorTeste.Validate(teste);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                return;
            }

            teste.SortearQuestoes();

            listQuestoes.Items.Clear();

            foreach (var item in teste.Questoes)
            {
                listQuestoes.Items.Add(item);
            }

            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            var resultadoValidacao = GravarRegistro(teste);

            if (resultadoValidacao.IsFailed)
            {
                string erro = resultadoValidacao.Errors[0].Message;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }        

        private void cmbDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var disciplina = (Disciplina)cmbDisciplinas.SelectedItem;

            CarregarMaterias(disciplina.Materias);
        }

        private void CarregarDisciplinas(List<Disciplina> disciplinas)
        {
            cmbDisciplinas.Items.Clear();

            foreach (var disciplina in disciplinas)
            {
                cmbDisciplinas.Items.Add(disciplina);
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

        private void chkProvao_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProvao.Checked)
            {
                cmbMaterias.Enabled = false;
                cmbMaterias.Items.Clear();
            }
            else
            {
                cmbMaterias.Enabled = true;
                var disciplina = cmbDisciplinas.SelectedItem as Disciplina;

                if (disciplina != null)
                    CarregarMaterias(disciplina.Materias);
            }
        }
    }
}
