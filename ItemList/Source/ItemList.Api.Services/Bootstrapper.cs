using System.Net.Http;
using System.Web;
using ItemList.Api.Services.Helpers;
using ItemList.Contracts.Api;
using ItemList.Contracts.Bootstrap;

namespace ItemList.Api.Services
{
    public class Bootstrapper : IBootstrapper
    {
        public void RegisterTypes(IIoCContainer container)
        {
            container.RegisterRequestScoped<IItemUrlHelper, ItemUrlHelper>();
            container.RegisterRequestScoped(() => (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"]);
        }
    }
}