using System.Collections.Generic;
using eAgenda.Infra.Arquivos;
using eAgenda.Infra.Arquivos.ModuloDisciplina;
using eAgenda.Infra.Arquivos.ModuloMateria;
using eAgenda.Infra.Arquivos.ModuloQuestao;
using eAgenda.Infra.Arquivos.ModuloTeste;
using GeradorTeste.WinApp.ModuloDisciplina;
using GeradorTeste.WinApp.ModuloMateria;
using GeradorTeste.WinApp.ModuloQuestao;
using GeradorTeste.WinApp.ModuloTeste;
using GeradorTestes.Aplicacao.ModuloDisciplina;
using GeradorTestes.Aplicacao.ModuloMateria;
using GeradorTestes.Aplicacao.ModuloQuestao;
using GeradorTestes.Aplicacao.ModuloTeste;
using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using GeradorTestes.Infra.Orm;
using GeradorTestes.Infra.Orm.ModuloDisciplina;

namespace GeradorTeste.WinApp.Compartilhado.Ioc
{
    public class ServiceLocatorManual : IServiceLocator
    {
        enum TipoPersistencia
        {
            Arquivo, Orm
        }

        private TipoPersistencia tipoPersistencia = TipoPersistencia.Orm;

        private Dictionary<string, ControladorBase> controladores;

        private IContextoDados contextoDados;

        private IRepositorioDisciplina repositorioDisciplina;
        private IRepositorioMateria repositorioMateria;
        private IRepositorioQuestao repositorioQuestao;
        private IRepositorioTeste repositorioTeste;

        public ServiceLocatorManual()
        {
            controladores = new Dictionary<string, ControladorBase>();

            ConfigurarControladores();
        }

        public T Get<T>() where T : ControladorBase
        {
            var tipo = typeof(T);

            var nomeControlador = tipo.Name;

            return (T)controladores[nomeControlador];
        }

        private void ConfigurarControladores()
        {            
            ConfiguraRepositorios();
            
            var servicoDisciplina = new ServicoDisciplina(repositorioDisciplina, contextoDados);
            controladores.Add("ControladorDisciplina", new ControladorDisciplina(servicoDisciplina));
            
            var servicoMateria = new ServicoMateria(repositorioMateria);
            controladores.Add("ControladorMateria", new ControladorMateria(servicoMateria, servicoDisciplina));
            
            var servicoQuestao = new ServicoQuestao(repositorioQuestao);
            controladores.Add("ControladorQuestao", new ControladorQuestao(servicoQuestao, servicoDisciplina));
           
            var servicoTeste = new ServicoTeste(repositorioTeste);
            controladores.Add("ControladorTeste", new ControladorTeste(servicoTeste, servicoQuestao, servicoDisciplina, servicoMateria));
        }

        private void ConfiguraRepositorios()
        {
            if (tipoPersistencia == TipoPersistencia.Arquivo)
            {
                var serializador = new SerializadorDadosEmJsonDotnet();

                contextoDados = new GeradorTesteJsonContext(serializador);

                repositorioDisciplina = new RepositorioDisciplinaEmArquivo(contextoDados);

                repositorioMateria = new RepositorioMateriaEmArquivo(contextoDados);

                repositorioQuestao = new RepositorioQuestaoEmArquivo(contextoDados);

                repositorioTeste = new RepositorioTesteEmArquivo(contextoDados);
            }
            else if (tipoPersistencia == TipoPersistencia.Orm)
            {
                contextoDados = new GeradorTesteDbContext();

                repositorioDisciplina = new RepositorioDisciplinaOrm(contextoDados);
            }
        }
    }
}
