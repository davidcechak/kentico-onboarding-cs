using ItemList.Contracts.Api;
using ItemList.Contracts.Bootstrap;
using ItemList.Contracts.DatabaseLayer;
using ItemList.DatabaseLayer.Repositories;

namespace ItemList.DatabaseLayer
{
    public class Bootstrapper : IBootstrapper
    {
        public void RegisterTypes(IIoCContainer container)
        {
            container.RegisterRequestScoped<IItemsRepository, ItemsRepository>();
        }
    }
}