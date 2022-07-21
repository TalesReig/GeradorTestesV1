using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloMateria;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Infra.Orm.ModuloMateria
{
    public class RepositorioMateriaOrm : IRepositorioMateria
    {
        protected readonly GeradorTesteDbContext db;
        protected readonly DbSet<Materia> Materias;

        public RepositorioMateriaOrm(IContextoDados dbContext)
        {
            db = dbContext as GeradorTesteDbContext;
            Materias = db.Set<Materia>();
        }

        public void Inserir(Materia Materia)
        {
            Materias.Add(Materia);
        }

        public void Editar(Materia Materia)
        {
            Materias.Update(Materia);
        }

        public void Excluir(Materia Materia)
        {
            Materias.Remove(Materia);
        }

        public List<Materia> SelecionarTodos()
        {
            return Materias.ToList();
        }

        public Materia SelecionarPorId(Guid id)
        {
            return Materias.Find(id);
        }

        public Materia SelecionarMateriaPorNome(string nome)
        {
            return Materias.FirstOrDefault(x => x.Nome == nome);
        }
    }
}
