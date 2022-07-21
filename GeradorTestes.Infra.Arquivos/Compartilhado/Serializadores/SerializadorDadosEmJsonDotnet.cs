using Newtonsoft.Json;
using System.IO;

namespace eAgenda.Infra.Arquivos
{
    public class SerializadorDadosEmJsonDotnet : ISerializador
    {
        private const string arquivo = @"C:\temp\dados.json";

        public GeradorTesteJsonContext CarregarDadosDoArquivo()
        {
            if (File.Exists(arquivo) == false)
                return new GeradorTesteJsonContext();

            string arquivoJson = File.ReadAllText(arquivo);

            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            return JsonConvert.DeserializeObject<GeradorTesteJsonContext>(arquivoJson, settings);
        }

        public void GravarDadosEmArquivo(GeradorTesteJsonContext dados)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            string arquivoJson = JsonConvert.SerializeObject(dados, settings);

            File.WriteAllText(arquivo, arquivoJson);
        }
    }
}
