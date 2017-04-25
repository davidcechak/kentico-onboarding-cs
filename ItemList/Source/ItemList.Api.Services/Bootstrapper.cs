using System.Net.Http;
using System.Web;
using ItemList.Api.Services.Helpers;
using ItemList.Contracts.Api;
using ItemList.Contracts.DependencyInjection;

namespace ItemList.Api.Services
{
    public class Bootstrapper : IBootstrapper
    {
        public void RegisterTypes(IDependencyInjectionContainer container) => container
            .RegisterRequestScoped<IItemUrlHelper, ItemUrlHelper>()
            .RegisterRequestScoped(() => (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"])
            .RegisterRequestScoped<IDatabaseConfiguration, DatabaseConfiguration>();
    }
}