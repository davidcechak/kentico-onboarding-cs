using ItemList.Contracts.Api;

namespace ItemList.Contracts.DependencyInjection
{
    public interface IBootstrapper
    {
        void RegisterTypes(IDependencyInjectionContainer container);
    }
}