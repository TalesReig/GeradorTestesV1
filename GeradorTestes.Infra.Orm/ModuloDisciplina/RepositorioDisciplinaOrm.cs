using GeradorTestes.Dominio.ModuloDisciplina;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.Infra.Orm.ModuloDisciplina
{
    public class RepositorioDisciplinaOrm : IRepositorioDisciplina
    {
        private DbSet<Disciplina> disciplinas;
        private readonly GeradorTesteDbContext dbContext;

        public RepositorioDisciplinaOrm(GeradorTesteDbContext dbContext)
        {
            disciplinas = dbContext.Set<Disciplina>();
            this.dbContext = dbContext;
        }

        public void Inserir(Disciplina novoRegistro)
        {
            disciplinas.Add(novoRegistro);
        }

        public void Editar(Disciplina registro)
        {
            disciplinas.Update(registro);
        }

        public void Excluir(Disciplina registro)
        {
            disciplinas.Remove(registro);
        }       

        public Disciplina SelecionarDisciplinaPorNome(string nome)
        {
            return disciplinas.FirstOrDefault(x => x.Nome == nome);
        }

        public Disciplina SelecionarPorId(Guid id)
        {
            //cache
            //return disciplinas.Find(id);

            //executa a query no banco
            return disciplinas.SingleOrDefault(x => x.Id == id);
        }

        public List<Disciplina> SelecionarTodos(bool incluirMateriasEhQuestoes)
        {
            return disciplinas.ToList();
        }

        public List<Disciplina> SelecionarTodos()
        {
            return disciplinas.ToList();
        }
    }
}
