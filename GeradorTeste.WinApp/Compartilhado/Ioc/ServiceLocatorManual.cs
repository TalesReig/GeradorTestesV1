using eAgenda.Infra.Arquivos;
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
using GeradorTestes.Infra.Orm.ModuloMateria;
using GeradorTestes.Infra.Orm.ModuloQuestao;
using GeradorTestes.Infra.Orm.ModuloTeste;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace GeradorTeste.WinApp.Compartilhado.Ioc
{
    public class ServiceLocatorManual : IServiceLocator
    {
        private TipoPersistencia tipoPersistencia = TipoPersistencia.Orm;

        private Dictionary<string, ControladorBase> controladores;

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
            var contextoDados = ObterContextoDados();

            ConfigurarRepositorios(contextoDados);

            var servicoDisciplina = new ServicoDisciplina(repositorioDisciplina, contextoDados);
            controladores.Add("ControladorDisciplina", new ControladorDisciplina(servicoDisciplina));

            var servicoMateria = new ServicoMateria(repositorioMateria, contextoDados);
            controladores.Add("ControladorMateria", new ControladorMateria(servicoMateria, servicoDisciplina));

            var servicoQuestao = new ServicoQuestao(repositorioQuestao, contextoDados);
            controladores.Add("ControladorQuestao", new ControladorQuestao(servicoQuestao, servicoDisciplina));

            var servicoTeste = new ServicoTeste(repositorioTeste, contextoDados);
            controladores.Add("ControladorTeste", new ControladorTeste(servicoTeste, servicoDisciplina));
        }

        private void ConfigurarRepositorios(IContextoDados contextoDados)
        {
            if (tipoPersistencia == TipoPersistencia.Orm)
            {
                repositorioDisciplina = new RepositorioDisciplinaOrm(contextoDados);
                repositorioMateria = new RepositorioMateriaOrm(contextoDados);
                repositorioQuestao = new RepositorioQuestaoOrm(contextoDados);
                repositorioTeste = new RepositorioTesteOrm(contextoDados);
            }
            else
            {
                repositorioMateria = new RepositorioMateriaEmArquivo(contextoDados);
                repositorioQuestao = new RepositorioQuestaoEmArquivo(contextoDados);
                repositorioTeste = new RepositorioTesteEmArquivo(contextoDados);

            }
        }

        private IContextoDados ObterContextoDados()
        {
            if (tipoPersistencia == TipoPersistencia.Orm)
            {
                var configuracao = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("ConfiguracaoAplicacao.json")
                    .Build();

                var connectionString = configuracao.GetConnectionString("SqlServer");

                return new GeradorTesteDbContext(connectionString);
            }
            else
            {
                var serializador = new SerializadorDadosEmJsonDotnet();

                return new GeradorTesteJsonContext(serializador);
            }
        }

        enum TipoPersistencia
        {
            Arquivo, Orm
        }
    }
}
