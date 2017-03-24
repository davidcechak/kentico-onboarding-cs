using ItemList.Contracts.Api;
using Microsoft.Practices.Unity;

namespace ItemList.Api.DependecyInjection
{
    public class UnityAdapter : IIoCContainer
    {
        private readonly UnityContainer _container;

        public UnityAdapter(UnityContainer container)
        {
            _container = container;
        }
        public void RegisterRequestScope<TInterface, TImplementation>() where TImplementation : TInterface, new()
        {
            _container.RegisterType<TInterface, TImplementation>(new HierarchicalLifetimeManager());
        }
    }
}