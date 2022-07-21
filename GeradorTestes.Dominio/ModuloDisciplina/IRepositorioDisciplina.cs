namespace GeradorTestes.Dominio.ModuloDisciplina
{
    public interface IRepositorioDisciplina : IRepositorio<Disciplina>
    {
        Disciplina SelecionarDisciplinaPorNome(string nome);
    }
}
