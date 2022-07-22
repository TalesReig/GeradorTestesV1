using GeradorTestes.Dominio.ModuloMateria;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.Infra.Orm.ModuloMateria
{
    public class RepositorioMateriaOrm : IRepositorioMateria
    {
        private DbSet<Materia> materias;
        private readonly GeradorTesteDbContext dbContext;

        public RepositorioMateriaOrm(GeradorTesteDbContext dbContext)
        {
            materias = dbContext.Set<Materia>();
            this.dbContext = dbContext;
        }

        public void Inserir(Materia novoRegistro)
        {
            materias.Add(novoRegistro);
        }

        public void Editar(Materia registro)
        {
            materias.Update(registro);
        }

        public void Excluir(Materia registro)
        {
            materias.Remove(registro);
        }

        public Materia SelecionarMateriaPorNome(string nome)
        {
            return materias.FirstOrDefault(x => x.Nome == nome);
        }

        public Materia SelecionarPorId(Guid id)
        {
            //cache
            //return materias.Find(id);

            //executa a query no banco
            return materias.SingleOrDefault(x => x.Id == id);
        }

        public List<Materia> SelecionarTodos(bool incluirDisciplina = false)
        {
            if (incluirDisciplina)
                return materias.Include(x => x.Disciplina).ToList();

            return materias.ToList();
        }

        public List<Materia> SelecionarTodos()
        {
            return materias.ToList();
        }
    }
}
