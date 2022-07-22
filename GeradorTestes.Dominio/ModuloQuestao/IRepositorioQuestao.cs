using System;

namespace GeradorTestes.Dominio.ModuloQuestao
{
    public interface IRepositorioQuestao : IRepositorio<Questao>
    {
        Questao SelecionarPorId(Guid id, bool incluirMateria, bool incluirAlternativas = false);
    }
}
