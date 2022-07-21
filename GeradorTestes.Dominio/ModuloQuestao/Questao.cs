using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Dominio.ModuloQuestao
{
    public class Questao : EntidadeBase<Questao>
    {

        public Questao()
        {
        }

        public Questao(string enunciado) : this()
        {
            Enunciado = enunciado;
        }

        public virtual List<Alternativa> Alternativas
        {
            get; set;
        }

        public string Enunciado { get; set; }

        public Materia Materia { get; set; }

        public Guid MateriaId { get; set; }

        public List<Teste> Testes { get; set; }

        public bool AdicionarAlternativa(Alternativa alternativa)
        {
            if (Alternativas.Contains(alternativa))
                return false;

            alternativa.Questao = this;
            Alternativas.Add(alternativa);

            return true;
        }

        public Alternativa ObtemAlternativaCorreta()
        {
            if (Alternativas.Any())
                return Alternativas.FirstOrDefault(x => x.Correta);

            return null;
        }

        public override void Atualizar(Questao questao)
        {
            Enunciado = questao.Enunciado;
            Materia = questao.Materia;
        }

        public Questao Clone()
        {
            return MemberwiseClone() as Questao;
        }

        private void RedefinirLetras()
        {
            char letra = 'A';

            foreach (var item in Alternativas)
            {
                item.Letra = letra;
                letra = letra.Next();
            }
        }

        public char GerarLetraAlternativa()
        {
            if (Alternativas == null)
                Alternativas = new List<Alternativa>();

            return Alternativas.Count == 0 ? 'A' :
                Alternativas.Select(x => x.Letra).Last().Next();
        }

        public void AtualizarAlternativaCorreta(Alternativa alternativaCorreta)
        {
            foreach (var a in Alternativas)
            {
                if (a.Equals(alternativaCorreta))
                {
                    a.Correta = true;
                }
                else
                {
                    a.Correta = false;
                }
            }
        }

        public void RemoverAlternativa(Alternativa alternativa)
        {
            Alternativas.Remove(alternativa);
            RedefinirLetras();
        }

        public void ConfigurarMateria(Materia materia)
        {
            if (materia == null)
                return;

            Materia = materia;
            MateriaId = materia.Id;
            Materia.AdicionaQuestao(this);
        }

        public override string ToString()
        {
            return Enunciado;
        }
    }
}