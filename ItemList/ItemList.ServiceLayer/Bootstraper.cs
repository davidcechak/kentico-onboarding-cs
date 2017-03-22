using ItemList.Contracts.Api;
using ItemList.Contracts.ServiceLayer;
using ItemList.ServiceLayer.Utils;

namespace ItemList.ServiceLayer
{
    public static class Bootstraper
    {
        public static void RegisterTypes(IIoCContainer container)
        {
            container.RegisterRequestScope<IGuidGenerator, GuidGenerator>();
        }
    }
}