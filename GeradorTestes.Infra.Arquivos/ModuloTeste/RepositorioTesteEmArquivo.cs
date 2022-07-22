using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloTeste;
using System.Collections.Generic;

namespace eAgenda.Infra.Arquivos.ModuloTeste
{
    public class RepositorioTesteEmArquivo : RepositorioEmArquivoBase<Teste>, IRepositorioTeste
    {
        protected GeradorTesteJsonContext contextoDados;

        public RepositorioTesteEmArquivo(IContextoPersistencia contexto)
        {
            contextoDados = contexto as GeradorTesteJsonContext;
        }

        public override List<Teste> ObterRegistros()
        {
            return contextoDados.Testes;
        }

        public List<Teste> SelecionarTodos(bool incluirDisciplinaEhMateria)
        {
            return ObterRegistros();
        }
    }
}
