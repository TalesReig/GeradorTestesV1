using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloQuestao;
using System.Collections.Generic;

namespace eAgenda.Infra.Arquivos.ModuloQuestao
{
    public class RepositorioQuestaoEmArquivo : RepositorioEmArquivoBase<Questao>, IRepositorioQuestao
    {
        protected GeradorTesteJsonContext contextoDados;

        public RepositorioQuestaoEmArquivo(IContextoDados contexto)
        {
            contextoDados = contexto as GeradorTesteJsonContext;
        }

        public override List<Questao> ObterRegistros()
        {
            return contextoDados.Questoes;
        }

    }
}
