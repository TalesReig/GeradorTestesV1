using FluentValidation;
using GeradorTestes.Dominio.ModuloMateria;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Arquivos.ModuloMateria
{
    public class RepositorioMateriaEmArquivo : RepositorioEmArquivoBase<Materia>, IRepositorioMateria
    {
        public RepositorioMateriaEmArquivo(GeradorTesteJsonContext dataContext) : base(dataContext)
        {
        }
     
        public override List<Materia> ObterRegistros()
        {
            return dataContext.Materias;
        }

        public Materia SelecionarMateriaPorNome(string nome)
        {
            return dataContext.Materias.Where(x => x.Nome == nome).FirstOrDefault();
        }
    }
}
