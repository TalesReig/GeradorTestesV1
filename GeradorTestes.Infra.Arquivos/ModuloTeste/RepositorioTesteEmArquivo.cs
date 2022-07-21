using FluentValidation;
using GeradorTestes.Dominio.ModuloTeste;
using System.Collections.Generic;

namespace eAgenda.Infra.Arquivos.ModuloTeste
{
    public class RepositorioTesteEmArquivo : RepositorioEmArquivoBase<Teste>, IRepositorioTeste
    {
        public RepositorioTesteEmArquivo(GeradorTesteJsonContext context) : base(context)
        {
        }      

        public override List<Teste> ObterRegistros()
        {
            return dataContext.Testes;
        }

    }
}
