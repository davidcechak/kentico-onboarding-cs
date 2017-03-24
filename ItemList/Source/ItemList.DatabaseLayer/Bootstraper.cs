using ItemList.Contracts.Api;
using ItemList.Contracts.DatabaseLayer;
using ItemList.DatabaseLayer.Repositories;

namespace ItemList.DatabaseLayer
{
    public static class Bootstraper
    {
        public static void RegisterTypes(IIoCContainer container)
        {
            container.RegisterRequestScope<IItemsRepository, ItemsRepository>();
        }
    }
}