using System.Collections.Generic;

namespace GeradorTestes.Dominio.ModuloDisciplina
{
    public interface IRepositorioDisciplina : IRepositorio<Disciplina>
    {
        Disciplina SelecionarDisciplinaPorNome(string nome);

        List<Disciplina> SelecionarTodos(bool incluirMateriasEhQuestoes);
    }
}
