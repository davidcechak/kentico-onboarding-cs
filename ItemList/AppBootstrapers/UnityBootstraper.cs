using System.Web.Http;
using ItemList.Contracts.Api;
using ItemList.Contracts.DatabaseLayer;
using ItemList.DatabaseLayer.Repositories;
using Microsoft.Practices.Unity;

namespace ItemList.IoCBootstrapers
{
    public static class UnityBootstraper
    {
        public static void RegisterTypes(IIoCContainer container)
        {
            container.RegisterType<IItemsRepository, ItemsRepository>(new HierarchicalLifetimeManager());
        }
    }
}
