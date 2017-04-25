using ItemList.Contracts.Database;
using ItemList.Contracts.DependencyInjection;
using ItemList.Database.Repositories;

namespace ItemList.Database
{
    public class Bootstrapper : IBootstrapper
    {
        public void RegisterTypes(IDependencyInjectionContainer container)
        {
            container.RegisterSingleton<IItemsRepository, ItemsRepository>();
        }
    }
}