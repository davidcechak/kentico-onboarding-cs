using System;
using ItemList.Contracts.ServiceLayer;

namespace ItemList.ServiceLayer.Utils
{
    internal class IdentifierService : IIdentifierService
    {
        public Guid GetIdentifier() => Guid.NewGuid();
    }
}