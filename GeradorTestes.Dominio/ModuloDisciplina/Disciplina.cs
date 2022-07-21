using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Dominio.ModuloDisciplina
{
    public class Disciplina : EntidadeBase<Disciplina>
    {
        public Disciplina()
        {
        }

        public List<Questao> ObterTodasQuestoes()
        {
            var todasQuestoes = new List<Questao>();

            if (Materias.Any())
                foreach (var m in Materias)
                {
                    if (m.Questoes != null)
                        todasQuestoes.AddRange(m.Questoes);
                }
            return todasQuestoes;
        }

        public List<Materia> Materias { get; set; } 

        public string Nome { get; set; }

        public bool AdicionarMateria(Materia materia)
        {
            if (Materias == null)
                Materias = new List<Materia>();

            if (Materias.Contains(materia))
                return false;

            Materias.Add(materia);

            return true;
        }

        public override string ToString()
        {
            return Nome;
        }

        public override void Atualizar(Disciplina disciplina)
        {
            Nome = disciplina.Nome;
        }

        public override bool Equals(object obj)
        {
            return obj is Disciplina disciplina &&
                   Id == disciplina.Id &&
                   Nome == disciplina.Nome;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Materias, Nome);
        }

        public Disciplina Clone()
        {
            return MemberwiseClone() as Disciplina;
        }
    }
}