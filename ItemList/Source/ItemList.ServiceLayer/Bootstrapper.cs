﻿using ItemList.Contracts.Api;
using ItemList.Contracts.Bootstrap;
using ItemList.Contracts.ServiceLayer;
using ItemList.ServiceLayer.Utils;

namespace ItemList.ServiceLayer
{
    public class Bootstrapper : IBootstrapper
    {
        public void RegisterTypes(IIoCContainer container)
        {
            container.RegisterRequestScoped<IIdentifierService, IdentifierService>();
        }
    }
}