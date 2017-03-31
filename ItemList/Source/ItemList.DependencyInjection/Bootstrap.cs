using ItemList.Contracts.Api;

namespace ItemList.DependencyInjection
{
    internal static class Bootstrap
    {
        internal static void RegisterTypes(IIoCContainer container)
        {
            var databaseBootstrapper = new Database.Bootstrapper();
            var serviceBootstrapper = new Services.Bootstrapper();
            var apiBootstrapper = new Api.Services.Bootstrapper();

            apiBootstrapper.RegisterTypes(container);
            databaseBootstrapper.RegisterTypes(container);
            serviceBootstrapper.RegisterTypes(container);
        }
    }
}