namespace eAgenda.Infra.Arquivos
{
    public interface ISerializador
    {
        GeradorTesteJsonContext CarregarDadosDoArquivo();

        void GravarDadosEmArquivo(GeradorTesteJsonContext dados);
    }
}
