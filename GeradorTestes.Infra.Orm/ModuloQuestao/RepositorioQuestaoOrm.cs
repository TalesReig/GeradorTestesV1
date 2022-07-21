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

    }
}
