using System;
using ItemList.Contracts.ServiceLayer;

namespace ItemList.ServiceLayer.Wrappers
{
    internal class IdentifierService : IIdentifierService
    {
        public Guid GetIdentifier() => Guid.NewGuid();
    }
}