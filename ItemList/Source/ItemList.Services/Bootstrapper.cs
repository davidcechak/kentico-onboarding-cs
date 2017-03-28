using ItemList.Contracts.Api;
using ItemList.Contracts.Bootstrap;
using ItemList.Contracts.Services;
using ItemList.Services.Wrappers;

namespace ItemList.Services
{
    public class Bootstrapper : IBootstrapper
    {
        public void RegisterTypes(IIoCContainer container)
        {
            container.RegisterRequestScoped<IIdentifierService, IdentifierService>();
        }
    }
}