using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;

namespace eAgenda.Infra.Arquivos.ModuloQuestao
{
    public class RepositorioQuestaoEmArquivo : RepositorioEmArquivoBase<Questao>, IRepositorioQuestao
    {
        protected GeradorTesteJsonContext contextoDados;

        public RepositorioQuestaoEmArquivo(IContextoPersistencia contexto)
        {
            contextoDados = contexto as GeradorTesteJsonContext;
        }

        public override List<Questao> ObterRegistros()
        {
            return contextoDados.Questoes;
        }

        public Questao SelecionarPorId(Guid id, bool incluirMateria, bool incluirAlternativas = false)
        {
            return SelecionarPorId(id);
        }
    }
}
