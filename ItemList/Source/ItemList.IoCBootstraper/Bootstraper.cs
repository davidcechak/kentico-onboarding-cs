using ItemList.Contracts.Api;

namespace ItemList.IoCBootstraper
{
    public static class Bootstraper
    {
        public static void RegisterTypes(IIoCContainer container)
        {
            DatabaseLayer.Bootstraper.RegisterTypes(container);
            ServiceLayer.Bootstraper.RegisterTypes(container);
        }
    }
}