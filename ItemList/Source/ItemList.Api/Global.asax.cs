using System.Web.Http;
using ItemList.DependencyInjection;

namespace ItemList.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(RegisterContainer.Register);
        }
    }
}
