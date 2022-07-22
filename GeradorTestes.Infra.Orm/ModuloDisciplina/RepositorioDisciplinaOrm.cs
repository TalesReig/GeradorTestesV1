using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloDisciplina;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Infra.Orm.ModuloDisciplina
{
    public class RepositorioDisciplinaOrm : IRepositorioDisciplina
    {
        protected readonly GeradorTesteDbContext db;
        protected readonly DbSet<Disciplina> Disciplinas;

        public RepositorioDisciplinaOrm(IContextoDados dbContext)
        {
            db = dbContext as GeradorTesteDbContext;
            Disciplinas = db.Set<Disciplina>();
        }

        public void Inserir(Disciplina disciplina)
        {
            Disciplinas.Add(disciplina);
        }

        public void Editar(Disciplina disciplina)
        {
            Disciplinas.Update(disciplina);
        }

        public void Excluir(Disciplina disciplina)
        {
            Disciplinas.Remove(disciplina);
        }

        public List<Disciplina> SelecionarTodos()
        {
            return Disciplinas.ToList();
        }

        public Disciplina SelecionarPorId(Guid id)
        {
            return Disciplinas.Find(id);
        }

        public Disciplina SelecionarDisciplinaPorNome(string nome)
        {
            return Disciplinas.FirstOrDefault(x => x.Nome == nome);
        }

        public List<Disciplina> SelecionarTodos(bool incluirMateriasEhQuestoes)
        {
            List<Disciplina> disciplinas = null;

            if (incluirMateriasEhQuestoes)
                disciplinas = Disciplinas
                    .Include(x => x.Materias)
                    .ThenInclude(x => x.Questoes).ToList();
            else
                disciplinas = Disciplinas.ToList();

            return disciplinas;
        }
    }
}
