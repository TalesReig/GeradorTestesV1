using eAgenda.Infra.Arquivos;
using eAgenda.Infra.Arquivos.ModuloTeste;
using GeradorTeste.WinApp.ModuloDisciplina;
using GeradorTeste.WinApp.ModuloMateria;
using GeradorTeste.WinApp.ModuloQuestao;
using GeradorTeste.WinApp.ModuloTeste;
using GeradorTestes.Aplicacao.ModuloDisciplina;
using GeradorTestes.Aplicacao.ModuloMateria;
using GeradorTestes.Aplicacao.ModuloQuestao;
using GeradorTestes.Aplicacao.ModuloTeste;
using GeradorTestes.Infra.Orm;
using GeradorTestes.Infra.Orm.ModuloDisciplina;
using GeradorTestes.Infra.Orm.ModuloMateria;
using GeradorTestes.Infra.Orm.ModuloQuestao;
using GeradorTestes.Infra.Orm.ModutoTeste;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace GeradorTeste.WinApp.Compartilhado.Ioc
{
    public class ServiceLocatorManual : IServiceLocator
    {        
        private Dictionary<string, ControladorBase> controladores;
        
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
            var serializador = new SerializadorDadosEmJsonDotnet();

            var contextoDados = new GeradorTesteJsonContext(serializador);                        
          
            var configuracao = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("ConfiguracaoAplicacao.json")
                 .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            var contextoDadosOrm = new GeradorTesteDbContext(connectionString);

            //var repositorioDisciplina = new RepositorioDisciplinaEmArquivo(contextoDados);
            var repositorioDisciplina = new RepositorioDisciplinaOrm(contextoDadosOrm);            
            var servicoDisciplina = new ServicoDisciplina(repositorioDisciplina, contextoDadosOrm);
            controladores.Add("ControladorDisciplina", new ControladorDisciplina(servicoDisciplina));

            //var repositorioMateria = new RepositorioMateriaEmArquivo(contextoDados);
            var repositorioMateria = new RepositorioMateriaOrm(contextoDadosOrm);
            var servicoMateria = new ServicoMateria(repositorioMateria, contextoDadosOrm);
            controladores.Add("ControladorMateria", new ControladorMateria(servicoMateria, servicoDisciplina));

            //var repositorioQuestao = new RepositorioQuestaoEmArquivo(contextoDados);
            var repositorioQuestao = new RepositorioQuestaoOrm(contextoDadosOrm);
            var servicoQuestao = new ServicoQuestao(repositorioQuestao, contextoDadosOrm);
            controladores.Add("ControladorQuestao", new ControladorQuestao(servicoQuestao, servicoDisciplina));

            //var repositorioTeste = new RepositorioTesteEmArquivo(contextoDados);
            var repositorioTeste = new RepositorioTesteOrm(contextoDadosOrm);
            var servicoTeste = new ServicoTeste(repositorioTeste, contextoDadosOrm);
            controladores.Add("ControladorTeste", new ControladorTeste(servicoTeste, servicoDisciplina));
        }
     
    }
}
