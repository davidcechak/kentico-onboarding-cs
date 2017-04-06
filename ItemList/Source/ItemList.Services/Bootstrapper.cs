﻿using ItemList.Contracts.Api;
using ItemList.Contracts.DependencyInjection;
using ItemList.Contracts.Services;
using ItemList.Services.Items;
using ItemList.Services.Wrappers;

namespace ItemList.Services
{
    public class Bootstrapper : IBootstrapper
    {
        public void RegisterTypes(IDependencyInjectionContainer container)
        {
            container.RegisterRequestScoped<IIdentifierService, IdentifierService>();
            container.RegisterRequestScoped<IItemStoringService, ItemStoringService>();
        }
    }
}