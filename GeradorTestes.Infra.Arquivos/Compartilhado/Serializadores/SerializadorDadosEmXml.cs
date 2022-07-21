using System.IO;
using System.Xml.Serialization;

namespace eAgenda.Infra.Arquivos
{
    public class SerializadorDadosEmXml : ISerializador
    {
        private const string arquivo = @"C:\temp\dados.xml";

        public GeradorTesteJsonContext CarregarDadosDoArquivo()
        {
            if (File.Exists(arquivo) == false)
                return new GeradorTesteJsonContext();

            XmlSerializer serializador = new XmlSerializer(typeof(GeradorTesteJsonContext));

            byte[] bytes = File.ReadAllBytes(arquivo);

            MemoryStream ms = new MemoryStream(bytes);

            return (GeradorTesteJsonContext)serializador.Deserialize(ms);
        }


        public void GravarDadosEmArquivo(GeradorTesteJsonContext dados)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(GeradorTesteJsonContext));

            MemoryStream ms = new MemoryStream();

            serializador.Serialize(ms, dados);

            byte[] bytes = ms.ToArray();

            File.WriteAllBytes(arquivo, bytes);
        }
    }
}
