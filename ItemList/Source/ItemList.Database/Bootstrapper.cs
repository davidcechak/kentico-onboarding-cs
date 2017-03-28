using ItemList.Contracts.Api;
using ItemList.Contracts.Bootstrap;
using ItemList.Contracts.Database;
using ItemList.Database.Repositories;

namespace ItemList.Database
{
    public class Bootstrapper : IBootstrapper
    {
        public void RegisterTypes(IIoCContainer container)
        {
            container.RegisterRequestScoped<IItemsRepository, ItemsRepository>();
        }
    }
}