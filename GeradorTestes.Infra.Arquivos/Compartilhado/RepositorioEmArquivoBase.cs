using GeradorTestes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Arquivos
{
    public abstract class RepositorioEmArquivoBase<T> where T : EntidadeBase<T>
    {
        protected GeradorTesteJsonContext dataContext;

        public RepositorioEmArquivoBase(GeradorTesteJsonContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public abstract List<T> ObterRegistros();
        
        public virtual void Inserir(T novoRegistro)
        {
            var registros = ObterRegistros();

            registros.Add(novoRegistro);
        }

        public virtual void Editar(T registro)
        {
            var registros = ObterRegistros();

            foreach (var item in registros)
            {
                if (item.Id == registro.Id)
                {
                    item.Atualizar(registro);
                    break;
                }
            }
        }

        public virtual void Excluir(T registro)
        {
            var registros = ObterRegistros();

            registros.Remove(registro);
        }

        public virtual List<T> SelecionarTodos()
        {
            return ObterRegistros().ToList();
        }

        public virtual T SelecionarPorId(Guid numero)
        {
            return ObterRegistros()
                .FirstOrDefault(x => x.Id == numero);
        }
    }
}
