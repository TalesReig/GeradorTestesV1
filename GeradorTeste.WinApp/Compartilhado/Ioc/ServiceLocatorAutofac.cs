using Autofac;

namespace GeradorTeste.WinApp.Compartilhado.Ioc
{
    public class ServiceLocatorAutofac
    {
        private readonly IContainer container;

        public ServiceLocatorAutofac()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterType<RepositorioClienteEmBancoDados>().As<IRepositorioCliente>();
            //builder.RegisterType<ServicoCliente>().AsSelf();
            //builder.RegisterType<ControladorCliente>().AsSelf();

            //builder.RegisterType<RepositorioGrupoVeiculosEmBancoDados>().As<IRepositorioGrupoVeiculos>();
            //builder.RegisterType<ServicoGrupoVeiculo>().AsSelf();
            //builder.RegisterType<ControladorGrupoVeiculos>().AsSelf();

            //builder.RegisterType<RepositorioFuncionarioEmBancoDados>().As<IRepositorioFuncionario>();
            //builder.RegisterType<ServicoFuncionario>().AsSelf();
            //builder.RegisterType<ControladorFuncionario>().AsSelf();

            //builder.RegisterType<RepositorioCondutorEmBancoDados>().As<IRepositorioCondutor>();
            //builder.RegisterType<ServicoCondutor>().AsSelf();
            //builder.RegisterType<ControladorCondutor>().AsSelf();

            //builder.RegisterType<RepositorioTaxaEmBancoDados>().As<IRepositorioTaxa>();
            //builder.RegisterType<ServicoTaxa>().AsSelf();
            //builder.RegisterType<ControladorTaxa>().AsSelf();

            container = builder.Build();
        }

        public T Get<T>() where T : ControladorBase
        {
            return container.Resolve<T>();
        }
    }
}
