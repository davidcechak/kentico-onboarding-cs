using ItemList.Contracts.Api;

namespace ItemList.IoCBootstraper
{
    public static class Bootstrap
    {
        public static void RegisterTypes(IIoCContainer container)
        {
            var databaseBootstrapper = new DatabaseLayer.Bootstraper();
            var serviceBootstrapper = new ServiceLayer.Bootstraper();

            databaseBootstrapper.RegisterTypes(container);
            serviceBootstrapper.RegisterTypes(container);
        }
    }
}