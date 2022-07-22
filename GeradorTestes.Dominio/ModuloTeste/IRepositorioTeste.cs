using System.Collections.Generic;

namespace GeradorTestes.Dominio.ModuloTeste
{
    public interface IRepositorioTeste : IRepositorio<Teste>
    {
        List<Teste> SelecionarTodos(bool incluirDisciplinaEhMateria);
    }
}
