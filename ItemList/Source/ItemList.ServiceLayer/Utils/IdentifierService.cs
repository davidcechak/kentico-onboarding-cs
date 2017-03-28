using System;
using ItemList.Contracts.ServiceLayer;

namespace ItemList.ServiceLayer.Utils
{
    public class IdentifierService : IIdentifierService
    {
        public Guid GetIdentifier() => Guid.NewGuid();
    }
}