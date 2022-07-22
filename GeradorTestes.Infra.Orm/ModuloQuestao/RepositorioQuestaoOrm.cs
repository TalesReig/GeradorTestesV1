using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloQuestao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Infra.Orm.ModuloQuestao
{
    public class RepositorioQuestaoOrm : IRepositorioQuestao
    {
        protected readonly GeradorTesteDbContext db;
        protected readonly DbSet<Questao> Questoes;

        public RepositorioQuestaoOrm(IContextoDados dbContext)
        {
            db = dbContext as GeradorTesteDbContext;
            Questoes = db.Set<Questao>();
        }

        public void Inserir(Questao Questao)
        {
            Questoes.Add(Questao);
        }

        public void Editar(Questao Questao)
        {
            Questoes.Update(Questao);
        }

        public void Excluir(Questao Questao)
        {
            Questoes.Remove(Questao);
        }

        public List<Questao> SelecionarTodos()
        {
            return Questoes.ToList();
        }

        public Questao SelecionarPorId(Guid id)
        {
            return Questoes.Find(id);
        }
        
        public Questao SelecionarPorId(Guid id, 
            bool incluirMateria = false, bool incluirAlternativas = false)
        {
            Questao questao = null;

            if (incluirMateria && incluirAlternativas)
                questao = Questoes
                    .Include(x => x.Materia)
                    .Include(x => x.Alternativas)
                    .SingleOrDefault(x => x.Id == id);

            return questao;

            //var questao = Questoes
            //        .Single(x => x.Id == id);

            //if (incluirAlternativas)
            //    db.Entry(questao)
            //        .Collection(x => x.Alternativas)
            //        .Load();

            //if (incluirMateria)
            //    db.Entry(questao)
            //        .Reference(x => x.Materia)
            //        .Load();

            //return questao;
        }
    }
}