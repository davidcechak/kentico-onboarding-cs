namespace ItemList.Contracts.DependencyInjection
{
    public interface IBootstrapper
    {
        void RegisterTypes(IDependencyInjectionContainer container);
    }
}