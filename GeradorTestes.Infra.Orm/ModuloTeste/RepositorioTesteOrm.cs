using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Infra.Orm.ModuloTeste
{
    public class RepositorioTesteOrm : IRepositorioTeste
    {
        protected readonly GeradorTesteDbContext db;
        protected readonly DbSet<Teste> Testes;

        public RepositorioTesteOrm(IContextoDados dbContext)
        {
            db = dbContext as GeradorTesteDbContext;
            Testes = db.Set<Teste>();
        }

        public void Inserir(Teste Teste)
        {
            Testes.Add(Teste);
        }

        public void Editar(Teste Teste)
        {
            Testes.Update(Teste);
        }

        public void Excluir(Teste Teste)
        {
            Testes.Remove(Teste);
        }

        public List<Teste> SelecionarTodos()
        {
            return Testes.ToList();
        }

        public Teste SelecionarPorId(Guid id)
        {
            return Testes.Find(id);
        }

    }
}
