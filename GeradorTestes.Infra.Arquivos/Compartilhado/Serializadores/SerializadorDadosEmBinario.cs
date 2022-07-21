using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace eAgenda.Infra.Arquivos
{
    public class SerializadorDadosEmBinario : ISerializador
    {
        private const string arquivo = @"C:\temp\dados.bin";

        public GeradorTesteJsonContext CarregarDadosDoArquivo()
        {
            if (File.Exists(arquivo) == false)
                return new GeradorTesteJsonContext();

            BinaryFormatter serializador = new BinaryFormatter();

            byte[] bytesTarefas = File.ReadAllBytes(arquivo);

            MemoryStream ms = new MemoryStream(bytesTarefas);

            return (GeradorTesteJsonContext)serializador.Deserialize(ms);
        }

        public void GravarDadosEmArquivo(GeradorTesteJsonContext dados)
        {
            BinaryFormatter serializador = new BinaryFormatter();

            MemoryStream ms = new MemoryStream();

            serializador.Serialize(ms, dados);

            byte[] bytes = ms.ToArray();

            File.WriteAllBytes(arquivo, bytes);
        }
    }
}
