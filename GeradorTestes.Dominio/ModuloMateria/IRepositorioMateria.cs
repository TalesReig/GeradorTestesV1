namespace GeradorTestes.Dominio.ModuloMateria
{
    public interface IRepositorioMateria : IRepositorio<Materia>
    {
        Materia SelecionarMateriaPorNome(string nome);
    }
}
