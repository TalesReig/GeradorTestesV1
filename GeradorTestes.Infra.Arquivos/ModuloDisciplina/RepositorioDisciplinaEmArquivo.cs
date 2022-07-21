using FluentValidation;
using GeradorTestes.Dominio.ModuloDisciplina;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Arquivos.ModuloDisciplina
{
    public class RepositorioDisciplinaEmArquivo : RepositorioEmArquivoBase<Disciplina>, IRepositorioDisciplina
    {
        public RepositorioDisciplinaEmArquivo(GeradorTesteJsonContext dataContext) : base(dataContext)
        {
        }      

        public override List<Disciplina> ObterRegistros()
        {
            return dataContext.Disciplinas;
        }

        public Disciplina SelecionarDisciplinaPorNome(string nome)
        {
            return dataContext.Disciplinas.Where(x => x.Nome == nome).FirstOrDefault();
        }
    }
}
