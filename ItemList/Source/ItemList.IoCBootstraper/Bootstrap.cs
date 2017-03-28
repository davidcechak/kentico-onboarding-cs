using System.Web.Http.Controllers;
using ItemList.Contracts.Api;

namespace ItemList.IoCBootstraper
{
    internal static class Bootstrap
    {
        internal static void RegisterTypes(IIoCContainer container)
        {
            var databaseBootstrapper = new DatabaseLayer.Bootstraper();
            var serviceBootstrapper = new ServiceLayer.Bootstraper();
            var apiBootstrapper = new Api.Services.Bootstrapper();

            apiBootstrapper.RegisterTypes(container);
            databaseBootstrapper.RegisterTypes(container);
            serviceBootstrapper.RegisterTypes(container);
        }
    }
}