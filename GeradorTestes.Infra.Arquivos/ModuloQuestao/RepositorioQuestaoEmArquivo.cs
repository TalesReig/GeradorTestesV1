using FluentValidation;
using GeradorTestes.Dominio.ModuloQuestao;
using System.Collections.Generic;

namespace eAgenda.Infra.Arquivos.ModuloQuestao
{
    public class RepositorioQuestaoEmArquivo : RepositorioEmArquivoBase<Questao>, IRepositorioQuestao
    {
        public RepositorioQuestaoEmArquivo(GeradorTesteJsonContext dataContext) : base(dataContext)
        {
        }

        public override List<Questao> ObterRegistros()
        {
            return dataContext.Questoes;
        }

    }
}
