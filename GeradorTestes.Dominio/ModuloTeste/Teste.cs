using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Dominio.ModuloTeste
{

    public class Teste : EntidadeBase<Teste>
    {
        public Teste()
        {
            DataGeracao = DateTime.Now;
            QuantidadeQuestoes = 5;
        }

        public string Titulo { get; set; }

        public List<Questao> Questoes { get; set; }

        public bool Provao { get; set; }

        public DateTime DataGeracao { get; set; }

        public Disciplina Disciplina { get; set; }

        public Guid DisciplinaId { get; set; }

        public Materia Materia { get; set; }

        /// <summary>
        /// A matéria é opcional para cada teste
        /// Nullable
        /// </summary>
        public Guid? MateriaId { get; set; }

        public int QuantidadeQuestoes { get; set; }


        public Gabarito ObterGabarito()
        {
            Gabarito gabarito = new Gabarito();

            gabarito.QuestoesCorretas = new List<AlternativaCorreta>(QuantidadeQuestoes);

            foreach (var questao in Questoes)
            {
                Alternativa alternativa = questao.ObtemAlternativaCorreta();

                gabarito.AdicionaQuestaoCorreta(questao.Id, alternativa.Letra);
            }

            return gabarito;
        }

        public void SortearQuestoes()
        {
            List<Questao> questoesSelecionadas = Provao ? Disciplina.ObterTodasQuestoes() : Materia.Questoes;

            if (questoesSelecionadas == null)
            {
                Questoes = new List<Questao>();
                return;
            }

            if (questoesSelecionadas.Count >= QuantidadeQuestoes)
                Questoes = questoesSelecionadas.Randomize(QuantidadeQuestoes).ToList();
            else
                Questoes = questoesSelecionadas.Randomize().ToList();
        }

        public override void Atualizar(Teste teste)
        {
            Titulo = teste.Titulo;
            Provao = teste.Provao;
            DataGeracao = teste.DataGeracao;
            Disciplina = teste.Disciplina;
            Materia = teste.Materia;
            QuantidadeQuestoes = teste.QuantidadeQuestoes;
            Questoes = teste.Questoes;
        }

        public Teste Clone()
        {
            return MemberwiseClone() as Teste;
        }

        public void ConfigurarMateria(Materia materia)
        {
            if (materia == null)
                return;

            Materia = materia;
        }

        public void ConfigurarDisciplina(Disciplina disciplina)
        {
            if (disciplina == null)
                return;

            Disciplina = disciplina;
        }
    }
}