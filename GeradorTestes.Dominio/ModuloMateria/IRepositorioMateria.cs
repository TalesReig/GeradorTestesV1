using System.Collections.Generic;

namespace GeradorTestes.Dominio.ModuloMateria
{
    public interface IRepositorioMateria : IRepositorio<Materia>
    {
        Materia SelecionarMateriaPorNome(string nome);

        List<Materia> SelecionarTodos(bool incluirDisciplina = false);
    }
}
