using System.IO;
using System.Text.Json;

namespace eAgenda.Infra.Arquivos
{
    internal class SerializadorDadosEmJson : ISerializador
    {
        private const string arquivo = @"C:\temp\dados.json";

        public GeradorTesteJsonContext CarregarDadosDoArquivo()
        {
            if (File.Exists(arquivo) == false)
                return new GeradorTesteJsonContext();

            string json = File.ReadAllText(arquivo);

            return JsonSerializer.Deserialize<GeradorTesteJsonContext>(json);
        }

        public void GravarDadosEmArquivo(GeradorTesteJsonContext dados)
        {
            var config = new JsonSerializerOptions { WriteIndented = true };

            string json = JsonSerializer.Serialize(dados, config);

            File.WriteAllText(arquivo, json);
        }
    }
}
