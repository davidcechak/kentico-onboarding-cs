using ItemList.Contracts.Api;
using ItemList.Contracts.DependencyInjection;
using ItemList.Contracts.Database;
using ItemList.Database.Repositories;

namespace ItemList.Database
{
    public class Bootstrapper : IBootstrapper
    {
        public void RegisterTypes(IDependencyInjectionContainer container)
        {
            container.RegisterRequestScoped<IItemsRepository, ItemsRepository>();
        }
    }
}