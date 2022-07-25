using GeradorTestes.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.Infra.Orm.ModutoTeste
{
    public class RepositorioTesteOrm : IRepositorioTeste
    {
        private DbSet<Teste> testes;
        private readonly GeradorTesteDbContext dbContext;

        public RepositorioTesteOrm(GeradorTesteDbContext dbContext)
        {
            testes = dbContext.Set<Teste>();
            this.dbContext = dbContext;
        }

        public void Inserir(Teste novoRegistro)
        {
            testes.Add(novoRegistro);
        }

        public void Editar(Teste registro)
        {
            testes.Update(registro);
        }

        public void Excluir(Teste registro)
        {
            testes.Remove(registro);
        }       

        public Teste SelecionarPorId(Guid id)
        {
            return testes.SingleOrDefault(x => x.Id == id);
        }

        public List<Teste> SelecionarTodos(bool incluirDisciplinaEhMateria = false)
        {
            if (incluirDisciplinaEhMateria)
                return testes
                    .Include(x => x.Disciplina)
                    .Include(x => x.Materia)
                    .ToList();

            return testes.ToList();
        }

        public List<Teste> SelecionarTodos()
        {
            return testes.ToList();
        }       
    }
}
