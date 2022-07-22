using GeradorTestes.Dominio.ModuloQuestao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.Infra.Orm.ModuloQuestao
{
    public class RepositorioQuestaoOrm : IRepositorioQuestao
    {
        private DbSet<Questao> questoes;
        private readonly GeradorTesteDbContext dbContext;

        public RepositorioQuestaoOrm(GeradorTesteDbContext dbContext)
        {
            questoes = dbContext.Set<Questao>();
            this.dbContext = dbContext;
        }

        public void Inserir(Questao novoRegistro)
        {
            questoes.Add(novoRegistro);
        }

        public void Editar(Questao registro)
        {
            questoes.Update(registro);
        }

        public void Excluir(Questao registro)
        {
            questoes.Remove(registro);
        }

        public Questao SelecionarPorId(Guid id)
        {
            return questoes.SingleOrDefault(x => x.Id == id);
        }

        public Questao SelecionarPorId(Guid id, bool incluirMateria=false, bool incluirAlternativas = false)
        {
            //cache
            //return questaos.Find(id);

            //executa a query no banco

            //if (incluirMateria && incluirAlternativas)
            //    return questoes                    
            //        .Include(x => x.Materia)
            //        .Include(x => x.Alternativas)
            //        .SingleOrDefault(x => x.Id == id);


            var questao = questoes.SingleOrDefault(x => x.Id == id);

            if (incluirAlternativas)
                dbContext.Entry(questao)
                    .Collection(x => x.Alternativas)
                    .Load();

            if (incluirMateria)
                dbContext.Entry(questao)
                    .Reference(x => x.Materia)
                    .Load();

            return questao;
        }        

        public List<Questao> SelecionarTodos()
        {
            return questoes
                 .Include(x => x.Materia)
                 .ThenInclude(x => x.Disciplina)                    
                .ToList();
        }

       
    }
}
