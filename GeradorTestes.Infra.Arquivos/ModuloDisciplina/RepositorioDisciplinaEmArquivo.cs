using FluentValidation;
using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloDisciplina;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Arquivos.ModuloDisciplina
{
    public class RepositorioDisciplinaEmArquivo : 
        RepositorioEmArquivoBase<Disciplina>, IRepositorioDisciplina
    {
        protected GeradorTesteJsonContext contextoDados;

        public RepositorioDisciplinaEmArquivo(IContextoPersistencia contexto)
        {
            contextoDados = contexto as GeradorTesteJsonContext;
        }

        public override List<Disciplina> ObterRegistros()
        {
            return contextoDados.Disciplinas;
        }

        public Disciplina SelecionarDisciplinaPorNome(string nome)
        {
            return contextoDados.Disciplinas.Where(x => x.Nome == nome).FirstOrDefault();
        }

        public List<Disciplina> SelecionarTodos(bool incluirMateriasEhQuestoes)
        {
            return ObterRegistros();
        }
    }
}
