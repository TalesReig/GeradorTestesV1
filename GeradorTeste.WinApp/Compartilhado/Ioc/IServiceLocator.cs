namespace GeradorTeste.WinApp.Compartilhado.Ioc
{
    public interface IServiceLocator
    {
        T Get<T>() where T : ControladorBase;
    }
}
