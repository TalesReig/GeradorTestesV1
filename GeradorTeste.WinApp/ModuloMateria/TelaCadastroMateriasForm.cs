using FluentResults;
using FluentValidation.Results;
using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloMateria
{
    public partial class TelaCadastroMateriasForm : Form
    {
        private Materia materia;

        public TelaCadastroMateriasForm(List<Disciplina> disciplinas)
        {
            InitializeComponent();
            this.ConfigurarTela();
            CarregarDisciplinas(disciplinas);
            CarregarSeries();
            
        }

        public Materia Materia
        {
            get { return materia; }

            set
            {
                materia = value;
                txtNome.Text = materia.Nome;
                cmbDisciplinas.SelectedItem = materia.Disciplina;
                cmbSeries.SelectedValue = materia.Serie;
            }
        }

        public Func<Materia, Result<Materia>> GravarRegistro { get; set; }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            materia.Nome = txtNome.Text;
            materia.Serie = (SerieMateriaEnum)cmbSeries.SelectedValue;
            materia.ConfigurarDisciplina((Disciplina)cmbDisciplinas.SelectedItem);

            var resultadoValidacao = GravarRegistro(materia);

            if (resultadoValidacao.IsFailed)
            {
                string erro = resultadoValidacao.Errors[0].Message;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }

        private void CarregarSeries()
        {
            var series = Enum.GetValues(typeof(SerieMateriaEnum));

            ArrayList items = new ArrayList();

            foreach (Enum serie in series)
            {
                var item = KeyValuePair.Create(serie, serie.GetDescription());
                items.Add(item);
            }

            cmbSeries.DataSource = items;
            cmbSeries.DisplayMember = "Value";
            cmbSeries.ValueMember = "Key";
        }

        private void CarregarDisciplinas(List<Disciplina> disciplinas)
        {
            cmbDisciplinas.Items.Clear();

            foreach (var item in disciplinas)
            {
                cmbDisciplinas.Items.Add(item);
            }
        }

    }
}
